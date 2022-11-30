namespace CustomsFor179Client
{
    public class InquiryTaskWorker : BackgroundService
    {
        private readonly ILogger<InquiryTaskWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        public InquiryTaskWorker(ILogger<InquiryTaskWorker> logger,
             IServiceScopeFactory serviceScopeFactory,
             IConfiguration configuration)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // 使用范围依赖注入以便数据库释放
                using var scope = _serviceScopeFactory.CreateScope();
                // 查询订单
                var inquiryTaskService = scope.ServiceProvider.GetRequiredService<IInquiryTaskService>();
                var tasks = await inquiryTaskService.GetUnhandleTasks(10);
                _logger.LogInformation($"检查到{tasks.Count()}条海关查询任务。");
                if (tasks.Any())
                {
                    foreach (var task in tasks)
                    {
                        ThreadPool.QueueUserWorkItem(async task =>
                        {
                            using var scope2 = _serviceScopeFactory.CreateScope();
                            var inquiryTaskService2 = scope2.ServiceProvider.GetRequiredService<IInquiryTaskService>();
                            try
                            {
                                _logger.LogInformation($"任务【OrderNo:{task.OrderNo}/SessionId:{task.SessionId}】开始处理。");
                                // 查询订单数据
                                var realTimeDataService = scope2.ServiceProvider.GetRequiredService<IRealTimeDataService>();
                                var realTimeData = await realTimeDataService.Get(task.OrderNo);
                                if (realTimeData == null)
                                    throw new ArgumentNullException("查询实时数据为空。");

                                realTimeData.ServiceTime = DateTime.Now.ToTimestamp();
                                realTimeData.SessionId = task.SessionId;
                                realTimeData.CertNo = _configuration["AppSettings:CertNo"];
                                _logger.LogInformation($"获取的订单数据：{JsonConvert.SerializeObject(realTimeData)}");

                                // 签名 
                                var signatureService = scope2.ServiceProvider.GetRequiredService<ISignatureService>();
                                var signValue = await signatureService.Sign(realTimeData);
                                if (string.IsNullOrEmpty(signValue))
                                    throw new InvalidOperationException($"无效签名。");
                                realTimeData.SignValue = signValue;

                                // 发送通知海关
                                var realTimeDataUpSender = scope2.ServiceProvider.GetRequiredService<RealTimeDataUpSender>();
                                await realTimeDataUpSender.SendAsync(realTimeData);

                                // 更新任务状态
                                task.SetSuccess();
                                await inquiryTaskService2.Update(task);
                                _logger.LogInformation($"任务【OrderNo:{task.OrderNo}/SessionId:{task.SessionId}】完成处理。");

                            }
                            catch (Exception ex)
                            {
                                _logger.LogInformation($"任务【OrderNo:{task.OrderNo}/SessionId:{task.SessionId}】处理发生异常，异常消息：{ex.Message}。");
                                task.SetFail(ex.Message);
                                await inquiryTaskService2.Update(task);
                            }
                        }, task, true); 
                    }
                }
                // 一分钟
                await Task.Delay(1000 * 60, stoppingToken);
            }
        }
    }
}
namespace CustomsFor179Web.Controllers
{
    /// <summary>
    /// 海关通知
    /// </summary>
    public class NotifyController : Controller
    {
        private readonly ILogger<NotifyController> _logger;
        private readonly IInquiryTaskService _inquiryTaskService;
        public NotifyController(ILogger<NotifyController> logger,
            IInquiryTaskService inquiryTaskService)
        {
            _logger = logger;
            _inquiryTaskService = inquiryTaskService;
        }
        /// <summary>
        /// 接收通知接口
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">当参数格式错误时，会出现数据转换异常</exception>
        public async Task<dynamic> Receive()
        {
            string openReq = HttpUtility.UrlDecode(Request.Form["openReq"], Encoding.UTF8);
            try
            {
                PlatDataOpen? request = JsonConvert.DeserializeObject<PlatDataOpen>(openReq);
                _logger.LogInformation($"接收到海关数据：{openReq}");
                if (request == null)
                    throw new ArgumentException($"未读取到合法参数。");
                await _inquiryTaskService.Create(new InquiryTask(request.SessionId, request.OrderNo)); 
                return ApiResult.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return ApiResult.Error(ex.Message);
            }
        }
    }
}

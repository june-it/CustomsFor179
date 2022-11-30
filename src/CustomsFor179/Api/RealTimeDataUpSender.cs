namespace CustomsFor179.Api
{
    public class RealTimeDataUpSender
    {
        private readonly ILogger _logger;
        public RealTimeDataUpSender(ILogger<RealTimeDataUpSender> logger,
            IOptions<CustomsOptions> options)
        {
            _logger = logger;
            Options = options?.Value;
        }
        protected CustomsOptions? Options { get; }
        public async Task<ApiResult?> SendAsync(RealTimeDataUp request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            using var client = new HttpClient();
            string requestUrl = $"{Options?.ApiServerUrl?.TrimEnd('/')}/ceb2grab/grab/realTimeDataUpload";
            var postData = JsonConvert.SerializeObject(request);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string result = await RetryExecute(async () =>
            {
                var parameters = new Dictionary<string, string>();
                parameters.Add("payExInfoStr", postData);
                var content = new FormUrlEncodedContent(parameters);
                HttpResponseMessage response = await client.PostAsync(requestUrl, content);
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    throw new HttpRequestException($"请求服务器异常，{requestUrl}。");
                var stream = await response.Content.ReadAsStreamAsync();
                using StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                return await reader.ReadToEndAsync();
            });
            _logger.LogInformation($"【海关接口】请求地址：{requestUrl}，POST：{postData}，结果：{result}，耗时：{stopwatch.ElapsedMilliseconds}ms。");
            stopwatch.Stop();
            return JsonConvert.DeserializeObject<ApiResult>(result);
        }
        private async Task<string> RetryExecute(Func<Task<string>> doExecute)
        {
            // 异常重试
            Polly.Retry.AsyncRetryPolicy poily = Policy.Handle<HttpRequestException>()
                          .WaitAndRetryAsync(3,
                          retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                          (exception, timeSpan) => { });
            return await poily.ExecuteAsync(doExecute);
        }
    }
}

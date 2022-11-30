namespace CustomsFor179.Api.Models
{
    /// <summary>
    /// api响应结果
    /// </summary>
    public class ApiResult
    {
        /// <summary>
        /// 状态代号。10000为正常调用值。
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }
        /// <summary>
        /// 异常信息，正常时为空值
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }
        /// <summary>
        /// 系统响应时间
        /// </summary>
        [JsonProperty("serviceTime")]
        public long ServiceTime { get; set; }
        /// <summary>
        /// 返回成功的结果
        /// </summary>
        /// <returns></returns>
        public static ApiResult Success()
        {
            return new ApiResult
            {
                Code = "10000",
                Message = "",
                ServiceTime = DateTime.Now.ToTimestamp()
            };
        }
        /// <summary>
        /// 返回失败的结果
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static ApiResult Error(string message)
        {
            return new ApiResult
            {
                Code = "20000",
                Message = message,
                ServiceTime = DateTime.Now.ToTimestamp()
            };
        }
    }
}

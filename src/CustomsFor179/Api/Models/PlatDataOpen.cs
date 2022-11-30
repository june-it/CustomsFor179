namespace CustomsFor179.Api.Models
{
    /// <summary>
    /// 接收海关发起的支付相关实时数据获取请求参数
    /// </summary>
    public class PlatDataOpen
    {
        /// <summary>
        /// 申报订单的订单编号
        /// </summary>
        public string? OrderNo { get; set; }
        /// <summary>
        /// 海关发起请求时，平台接收的会话ID。
        /// </summary>
        public string? SessionId { get; set; }
        /// <summary>
        /// 调用时的系统时间
        /// </summary>
        public long? ServiceTime { get; set; }
    }
}

namespace CustomsFor179.Api.Models
{
    /// <summary>
    /// 企业返回海关所需获取的支付相关实时数据
    /// </summary>
    public class RealTimeDataUp
    {
        /// <summary>
        /// 海关发起请求时，平台接收的会话ID
        /// </summary>
        [JsonProperty("sessionID")]
        public string? SessionId { get; set; }
        /// <summary>
        /// 支付交易数据
        /// </summary>
        [JsonProperty("payExchangeInfoHead")]
        public PayExchangeInfoHeadModel? PayExchangeInfoHead { get; set; }
        /// <summary>
        /// 支付交易数据
        /// </summary>
        [JsonProperty("payExchangeInfoLists")]
        public List<PayExchangeInfoModel>? PayExchangeInfoLists { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("serviceTime")]
        public long ServiceTime { get; set; }
        /// <summary>
        /// 证书编号
        /// </summary>
        [JsonProperty("certNo")]
        public string? CertNo { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        [JsonProperty("signValue")]
        public string? SignValue { get; set; }
    }
    public class PayExchangeInfoHeadModel
    {
        /// <summary>
        /// 系统唯一序号
        /// </summary>
        [JsonProperty("guid")]
        public string? Guid { get; set; }
        /// <summary>
        /// 跨境电商平台企业向支付企业发送的原始信息
        /// </summary>
        [JsonProperty("initalRequest")]
        public string? InitalRequest { get; set; }
        /// <summary>
        /// 支付企业向跨境电商平台企业反馈的原始信息
        /// </summary>
        [JsonProperty("initalResponse")]
        public string? InitalResponse { get; set; }
        /// <summary>
        /// 电商平台的海关注册登记编号
        /// </summary>
        [JsonProperty("ebpCode")]
        public string? EbpCode { get; set; }
        /// <summary>
        /// 支付企业的海关注册登记编号
        /// </summary>
        [JsonProperty("payCode")]
        public string? PayCode { get; set; }
        /// <summary>
        /// 交易唯一编号（可在央行认可的机构验证
        /// </summary>
        [JsonProperty("payTransactionId")]
        public string? PayTransactionId { get; set; }
        /// <summary>
        /// 实际交易金额
        /// </summary>
        [JsonProperty("totalAmount")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 实际交易币制（海关编码）
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }
        /// <summary>
        /// 验核机构 1-银联 2-网联 3-其他
        /// </summary>
        [JsonProperty("verDept")]
        public string? VerDept { get; set; }
        /// <summary>
        /// 支付类型 用户支付的类型。1-APP 2-PC 3-扫码 4-其他
        /// </summary>
        [JsonProperty("payType")]
        public string? PayType { get; set; }
        /// <summary>
        /// 交易成功时间
        /// </summary>
        [JsonProperty("tradingTime")]
        public string? TradingTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("note")]
        public string? Note { get; set; }
    }
    public class PayExchangeInfoModel
    {
        /// <summary>
        /// 交易平台向海关申报订单的的订单编号
        /// </summary>
        [JsonProperty("orderNo")]
        public string? OrderNo { get; set; }
        /// <summary>
        /// 商品名称及商品展示链接地址列表
        /// </summary>
        [JsonProperty("goodsInfo")]
        public List<GoodsModel>? GoodsInfo { get; set; }
        /// <summary>
        /// 交易商品的卖方商户账号。电商平台自营商户
        /// 应填写自营商户的收款账户；非自营电商应填
        /// 写非自营商户的收款账户。
        /// </summary>
        [JsonProperty("recpAccount")]
        public string? RecpAccount { get; set; }
        /// <summary>
        /// 收款企业代码
        /// 应填写收款企业代码（境内企业为统一社会信用代码；境外企业可不填写）
        /// </summary>
        [JsonProperty("recpCode")]
        public string? RecpCode { get; set; }
        /// <summary>
        /// 收款企业名称
        /// </summary>
        [JsonProperty("recpName")]
        public string? RecpName { get; set; }

    }
    public class GoodsModel
    {
        /// <summary>
        /// 商品名称
        /// 商品名称应据实填报
        /// </summary>
        [JsonProperty("gname")]
        public string? Name { get; set; }
        /// <summary>
        /// 商品展示链接地址
        /// 商品展示链接地址应据实填报。
        /// </summary>
        [JsonProperty("itemLink")]
        public string? ItemLink { get; set; }
    }
}

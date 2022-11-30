namespace CustomsFor179.Services
{
    /// <summary>
    /// 海关电子客户端客户签名服务
    /// </summary>
    public interface ISignatureService
    {
        Task<string?> Sign(RealTimeDataUp request);
    }
}

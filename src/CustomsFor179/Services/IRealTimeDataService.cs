namespace CustomsFor179.Services
{
    /// <summary>
    /// 实时数据查询接口
    /// </summary>
    public interface IRealTimeDataService
    {
        /// <summary>
        /// 获取实时订单数据
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        Task<RealTimeDataUp?> Get(string orderNo);
    }
}

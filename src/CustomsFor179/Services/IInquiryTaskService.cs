namespace CustomsFor179.Services
{
    /// <summary>
    /// 定义查询任务服务接口
    /// </summary>
    public interface IInquiryTaskService
    {
        /// <summary>
        /// 创建查询任务
        /// </summary>
        /// <param name="inquiryTask"></param>
        /// <returns></returns>
        Task Create(InquiryTask inquiryTask);
        /// <summary>
        /// 更新查询任务
        /// </summary>
        /// <param name="inquiryTask"></param>
        /// <returns></returns>
        Task Update(InquiryTask inquiryTask); 
        /// <summary>
        /// 获取查询任务详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<InquiryTask> Get(int id);
        /// <summary>
        /// 通过订单号获取任务详情
        /// </summary>
        /// <param name="orderNo">订单号</param>
        /// <returns></returns>
        Task<InquiryTask> Get(string orderNo);

        /// <summary>
        /// 查询未处理的任务，包括未处理或处理失败且小于3次的任务
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task<List<InquiryTask>> GetUnhandleTasks(int count);
    }
}

namespace CustomsFor179.Services
{
    public class RealTimeDataService : IRealTimeDataService
    {
        public async Task<RealTimeDataUp?> Get(string orderNo)
        {
            return await Task.FromResult<RealTimeDataUp>(null);
        }
    }
}

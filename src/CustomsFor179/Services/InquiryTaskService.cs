namespace CustomsFor179.Services
{
    public class InquiryTaskService : IInquiryTaskService
    {
        private readonly CustomsDbContext _context;
        public InquiryTaskService(CustomsDbContext context)
        {
            _context = context;
        }
        public async Task Create(InquiryTask inquiryTask)
        {
            _context.Add(inquiryTask);
            await _context.SaveChangesAsync();
        }
        public async Task Update(InquiryTask inquiryTask)
        {
            _context.Update(inquiryTask);
            await _context.SaveChangesAsync();
        }
        public async Task<InquiryTask?> Get(int id)
        {
            return await _context.InquiryTasks.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<InquiryTask?> Get(string orderNo)
        {
            return await _context.InquiryTasks.FirstOrDefaultAsync(x => x.OrderNo == orderNo);
        }
        public async Task<List<InquiryTask>> GetUnhandleTasks(int count)
        {
            return await _context.InquiryTasks
                .Where(x => (x.TaskStatus == Data.TaskStatus.Unhandle) && x.AccessFailedCount < 3)
                .ToListAsync();
        }


    }
}

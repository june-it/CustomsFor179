namespace CustomsFor179.Data
{
    public class CustomsDbContext : DbContext
    {
        public CustomsDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomsDbContext).Assembly);
        }

        public DbSet<InquiryTask> InquiryTasks { get; set; }
    }
}
namespace CustomsFor179.Data
{
    public class CustomsDbContextDesignTimeDesignFactory : IDesignTimeDbContextFactory<CustomsDbContext>
    {
        public CustomsDbContext CreateDbContext(string[] args)
        {
            ServiceCollection service = new ServiceCollection();
            DbContextOptionsBuilder<CustomsDbContext> optionsBuilder = new DbContextOptionsBuilder<CustomsDbContext>();
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=TestDb;UserName=postgres;Password=123456;");
            return new CustomsDbContext(optionsBuilder.Options, service.BuildServiceProvider());
        }
    }
}

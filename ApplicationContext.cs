using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext() { }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public static string? ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false && ConnectionString !=null)
                optionsBuilder.UseSqlite(ConnectionString);

        }

       
    }
    //���� ���� ����� ��������� ����� �� ���������� � ����� �� ������������, ���������� �� ���������� ��������������� Entity Framework ��� �������� ��������.
    public class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            // �������� ������������ �� ����� appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // �������� ������ ����������� �� ����� appsettings.json
            ApplicationContext.ConnectionString = config.GetConnectionString("SQlite");
            optionsBuilder.UseSqlite(ApplicationContext.ConnectionString);
            return new ApplicationContext(optionsBuilder.Options);
        }
    }

}
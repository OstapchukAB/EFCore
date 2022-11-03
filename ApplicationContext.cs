using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;

        public ApplicationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var builder = new ConfigurationBuilder();
           // builder.SetBasePath(Config.EXE_PATH);
            string pathJson = new Uri(Path.Combine(Config.EXE_PATH ?? "", "appsettings.json")).AbsolutePath;
            builder.AddJsonFile(pathJson);
            var config = builder.Build();
            string connectionString = config.GetConnectionString("Sqlite");
            //var connect = new Uri(Path.Combine(Config.EXE_PATH ?? "", "BD", "mytest.db")).AbsolutePath;
            //optionsBuilder.UseSqlite($"Data Source = {connect}");
            optionsBuilder.UseSqlite(connectionString);
        }
    }

}
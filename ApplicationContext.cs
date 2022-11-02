using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(){}
           

      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connect =new Uri(Path.Combine(Config.EXE_PATH??"","BD","mytest.db")).AbsolutePath; 
            optionsBuilder.UseSqlite($"Data Source={connect}");
        }
    }

}
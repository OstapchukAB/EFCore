using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public static string ConnectionString { get; private set; } = "";
        public ApplicationContext() 
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            ConnectionString = config.GetConnectionString("Sqlite");
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connect = new Uri(Path.Combine(Config.EXE_PATH ?? "", "BD", "mytest.db")).AbsolutePath;
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }

}
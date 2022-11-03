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
            // ��������� ���� � �������� ��������
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // �������� ������������ �� ����� appsettings.json
            builder.AddJsonFile("appsettings.json");
            // ������� ������������
            var config = builder.Build();
            // �������� ������ �����������
            ConnectionString = config.GetConnectionString("Sqlite");
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connect = new Uri(Path.Combine(Config.EXE_PATH ?? "", "BD", "mytest.db")).AbsolutePath;
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }

}
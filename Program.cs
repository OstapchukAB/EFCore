using EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class Programm
{

    public static void Main()
    {

        //Здесь устанавливаем настройки пути к БД для работы с БД
        var builder = new ConfigurationBuilder();
        // установка пути к текущему каталогу
        builder.SetBasePath(Directory.GetCurrentDirectory());
        // получаем конфигурацию из файла appsettings.json
        builder.AddJsonFile("appsettings.json");
        // создаем конфигурацию
        var config = builder.Build();
        // получаем строку подключения
        string connectionString = config.GetConnectionString("SQlite");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionsBuilder.UseSqlite(connectionString).Options;
        ///
        Console.WriteLine("---Test EF Core---");
        using (ApplicationContext db = new ApplicationContext(options))
        {
           // db.Database.EnsureCreated(); 

            GetData(db, "---до добавления---");

            User tom = new User { Name = "Tom", Age = 33 };
            User alice = new User { Name = "Alice", Age = 26 };
            AddData(db, new List<User> { tom, alice });

            GetData(db, "---после добавления---");

            var users = db.Users.OrderBy(x => x.Id).LastOrDefault();
            if (users != null)
            {
                Console.WriteLine($"редактируем последнюю запись:{users.Id}");
                User NewUser = new User { Name = "New", Age = 100 };
                EditDataForSelectId(db, NewUser, users.Id);
            }

            GetData(db, "---после редактирования---");

            users = db.Users.OrderBy(x => x.Id).LastOrDefault();
            if (users != null)
            {
                Console.WriteLine($"удаляем последнюю запись:{users.Id}");
                RemoveDataForListId(db, new List<int> { users.Id, 0 });
            }
            GetData(db, "----после удаления----");

        }

        Console.ReadKey();

    }

    public static void GetData(ApplicationContext db, string? message = null)
    {
        if (message != null)
            Console.WriteLine(message);

        var users = db.Users.ToList();
        foreach (User u in users)
            Console.WriteLine($"{u.Id} {u.Name} - {u.Age}\t{u.Dt} ");

    }

    public static void AddData(ApplicationContext db, List<User> ListUser, string? message = null)
    {
        if (message != null)
            Console.WriteLine(message);

        foreach (User u in ListUser)
            db.Users.Add(u);

        db.SaveChanges();

    }
    public static void EditDataForSelectId(ApplicationContext db, User NewUser, int idForFind, string? message = null)
    {
        if (message != null)
            Console.WriteLine(message);

        User? user = db.Users.FirstOrDefault(x => x.Id.Equals(idForFind));
        if (user != null)
        {
            user.Age = NewUser.Age;
            user.Name = NewUser.Name;
            db.SaveChanges();
        }
    }

    public static void RemoveDataForListId(ApplicationContext db, List<int> ListIdForDelete, string? message = null)
    {
        if (message != null)
            Console.WriteLine(message);

        List<User> ListUser = new List<User>();
        foreach (int id in ListIdForDelete)
        {
            User? user = db.Users.FirstOrDefault(x => x.Id.Equals(id));
            if (user != null)
                ListUser.Add(user);
        }
        foreach (User userForDelete in ListUser)
        {
            db.Users.Remove(userForDelete);
            db.SaveChanges();
        }
    }

   
}

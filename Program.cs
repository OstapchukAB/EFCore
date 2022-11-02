using EF;
public class Programm
{

        public static void Main()
        {
                Console.WriteLine("Test EF Core");
                using (ApplicationContext db = new ApplicationContext())
                {
                    User tom = new User { Name = "Tom", Age = 33 };
                    User alice = new User { Name = "Alice", Age = 26 };
                
                    // Добавление
                    db.Users.Add(tom);
                    db.Users.Add(alice);
                    db.SaveChanges();
                }
        }
}

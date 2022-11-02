using EF;
public class Programm
{

        public static void Main()
        {
                Console.WriteLine("Test EF Core");
                using (ApplicationContext db = new ApplicationContext())
                {
                

                GetData(db,"до редактирования");

                User tom = new User { Name = "Tom", Age = 33 };
                User alice = new User { Name = "Alice", Age = 26 };

                AddData(db,new List<User>{tom,alice});

                GetData(db,"после редактирования");


                }

                Console.ReadKey();  
                 
        } 

        public static void GetData (ApplicationContext db, string? message=null)
        {
                if (message !=null )
                        Console.WriteLine(message);
                
                var users = db.Users.ToList();
                  foreach (User u in users)
                        Console.WriteLine($"{u.Id}.{u.Name} - {u.Age}");

        }

         public static void AddData (ApplicationContext db, List<User> ListUser, string? message=null)
        {
                if (message !=null )
                        Console.WriteLine(message);
                
                foreach(User u in ListUser) 
                        db.Users.Add(u);
                        
                db.SaveChanges();

        }
}

using EF;
public class Programm
{

        public static void Main()
        {
                Console.WriteLine("---Test EF Core---");
                using (ApplicationContext db = new ApplicationContext())
                {
                

                GetData(db,"---до добавления---");

                User tom = new User { Name = "Tom", Age = 33 };
                User alice = new User { Name = "Alice", Age = 26 };
                AddData(db,new List<User>{tom,alice});
                
                GetData(db,"---после добавления---");

                User NewUser = new User { Name = "New", Age = 100 };
                EditDataForSelectId(db,NewUser,20);
                
                GetData(db,"---после редактирования---");

                RemoveDataForListId(db,new List<int>{21,23,25,0});
                 GetData(db,"----после удаления----");

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
        public static void EditDataForSelectId (ApplicationContext db, User NewUser, int idForFind,  string? message=null)
        {
                if (message !=null )
                        Console.WriteLine(message);
                
                User? user = db.Users.FirstOrDefault(x=>x.Id.Equals(idForFind));
                if (user !=null)
                {
                        user.Age=NewUser.Age;
                        user.Name=NewUser.Name;   
                        db.SaveChanges(); 
                }
        }

        public static void RemoveDataForListId (ApplicationContext db, List<int> ListIdForDelete,  string? message=null)
        {
                if (message !=null )
                        Console.WriteLine(message);
                        
                List<User> ListUser= new List<User>();
                foreach(int id in ListIdForDelete)
                { 
                        User? user = db.Users.FirstOrDefault(x=>x.Id.Equals(id));
                        if (user !=null)
                        ListUser.Add(user);  
                }
                foreach(User userForDelete in  ListUser)
                {
                        db.Users.Remove(userForDelete); 
                        db.SaveChanges(); 
                }
        }
}

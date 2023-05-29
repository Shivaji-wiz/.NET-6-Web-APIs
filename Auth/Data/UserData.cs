using Auth.Model;

namespace Auth.Data
{
    public class UserData
    {
        public List<User> Users = new List<User>()
         {
             new User() { Id = 1,Name= "Shivaji",Age = 21},
             new User() { Id = 2,Name= "Manas",Age = 21},
             new User() { Id = 3,Name= "Aman",Age = 21},
             new User() { Id = 4,Name= "Shailedra",Age = 21}
         };
    }
}

using System.Threading.Tasks;

namespace DatingApp.API.Models.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username); //to check if the username already exists in db
         
    }
}
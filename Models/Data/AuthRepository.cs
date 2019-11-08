using System;
using System.Threading.Tasks;

namespace DatingApp.API.Models.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            //throw new System.NotImplementedException();
            //password hash and password salt
            byte[] passwordHash, passwordSalt;
            //method to create the passwordHash
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            //out: pass a reference of the variable

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
            
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            

        }



        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
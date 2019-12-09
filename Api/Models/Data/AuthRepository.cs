using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Models.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            //username to identify the user in our database
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHash,user.PasswordSalt))
                return null; //if the password doesn't match. Boolean function. 

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                //passwordSalt = hmac.Key;
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                //this is going to compute the hash from our password
                //computedHash must be the same as passwordHash above
                for(int i = 0; i < computedHash.Length; i++){
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            } 
            return true; //because we know the passwords match!
        }

        public async Task<User> Register(User user, string password) //returns a task of type user
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



        public async Task<bool> UserExists(string username)
        {
            //compare the username against any user in the database
            if(await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}
namespace DatingApp.API.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username{ get; set; }

        public byte[] PasswordHash { get; set; } //store the pw as PasswordHash

        public byte[] PasswordSalt { get; set;  } //to be able to recreate the hash


    }
}
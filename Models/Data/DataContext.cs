using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;
namespace DatingApp.API.Models.Data
{
    //inherit from DbContext. Use its methods or modify them (Override)
    public class DataContext : DbContext
    {
        //must have an instance of DbContext options
        //create a constructor: ctor
        //Override the class' name with our name (DataContext)
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<Value> Values { get; set; }
        
    }
}
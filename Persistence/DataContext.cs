using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Persistence
{
    //Supposedly the Identity namespace package will handle lots of different things.
    // We migrated Identity to database --> dotnet ef migrations add IdentityAdded -p Persistence -s API
    public class DataContext : IdentityDbContext<AppUser>
    {
        //Constructor--Takes name of class and has options
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        //Method--performs a task and does not take any parameters
        public DbSet<Activity> Activities { get; set; }
    }
}
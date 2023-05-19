using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        //Constructor--Takes name of class and has options
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        //Method--performs a task and does not take any parameters
        public DbSet<Activity> Activities { get; set; }
    }
}
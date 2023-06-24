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
        public DataContext(DbContextOptions options)
            : base(options) { }

        //Method--performs a task and does not take any parameters
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityAttendee> ActivitiesAttendees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivityAttendee>(
                x => x.HasKey(aa => new { aa.AppUserId, aa.ActivityId })
            );

            builder.Entity<ActivityAttendee>()
            .HasOne(u => u.AppUser)
            .WithMany(a => a.Activities)
            .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<ActivityAttendee>()
            .HasOne(u => u.Activity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(aa => aa.ActivityId);
        }
    }
}

//This is how we migrate
// dotnet ef migrations add ActivityAttendee -p Persistence -s API

/* 
-p specifies the name of the project that contains the Entity Framework Core (EF Core) model. In this case, the project is named Persistence.
-s specifies the name of the schema that the migration will be applied to. In this case, the schema is named API.
 */
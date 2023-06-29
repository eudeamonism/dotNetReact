using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class AppUser : IdentityUser
    {
        //Additional properties
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public ICollection<ActivityAttendee> Activities { get; set; }
        
        //One-to-many Relationship with Photo 
        public ICollection<Photo> Photos { get; set; }
    }
}

//To make the connnection, you need to use ICollection and specify the model
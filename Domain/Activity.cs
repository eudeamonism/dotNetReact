namespace Domain
{
    public class Activity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string City { get; set; }
        public string Venue { get; set; }
        
        //Below, joins two tables.
        //WE do this by initiliazing. Earlier, when we ran Get Activities endpoint in PostMan, for attendeess we had null.
        //with this we have an empty list.
        public ICollection<ActivityAttendee> Attendees { get; set; } = new List<ActivityAttendee>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace iBalekaService.Domain.Models
{
    public class Event
    {
        public Event() { }
        public Event(string title,string desc,DateTime dateAndTime,string suburbCityState,Club club)
        {
            Title = title;
            Description = desc;
            DateAndTime = dateAndTime;
            Club = club;
            DateCreated = DateTime.Now.Date;
            Deleted = false;
        }
        [Key]
        public int EventID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Location { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
        //foreign key
        public int ClubID { get; set; }
        //navigation property
        public virtual Club Club { get; set; }
        public virtual ICollection<EventRegistration> Participants { get; set; }
        public virtual ICollection<Event_Route> EventRoutes { get; set; }
        public virtual ICollection<Run> Runs { get; set; }

    }
}

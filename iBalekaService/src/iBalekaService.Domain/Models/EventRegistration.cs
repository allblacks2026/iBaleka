using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace iBalekaService.Domain.Models
{
    public class EventRegistration
    {
        public EventRegistration() { }
        public EventRegistration(Event_Route route,Athlete athlete)
        {
            DateRegistered = DateTime.Now;
            SelectedRoute = route.RouteID;
            Event = route.Event;
            Athlete = athlete;
            Arrived = false;
            Deleted = false;
        }
        [Key]
        public int RegistrationID { get; set; }
        public DateTime DateRegistered { get; set; }
        public int SelectedRoute { get; set; }
        public bool Arrived { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int EventID { get; set; }
        public int AthleteID { get; set; }
        //navigational properties
        public Event Event { get; set; }
        public Athlete Athlete { get; set; }
    }
}

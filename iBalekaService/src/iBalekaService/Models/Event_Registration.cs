using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Event_Registration
    {
        public Event_Registration(Event_Route route,Athlete athlete)
        {
            DateRegistered = DateTime.Now;
            SelectedRoute = route.RouteID;
            EventID = route.EventID;
            AthleteID = athlete.AthleteID;
            Arrived = false;
            Deleted = false;
        }
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

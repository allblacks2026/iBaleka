using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace iBalekaService.Domain.Models
{
    public class Event_Route
    {
        public Event_Route() { }
        public Event_Route(string desc,Event evnt,Route route)
        {
            Description = desc;
            Event = evnt;
            Route = route;
            DateAdded = DateTime.Now;
            Deleted = false;
        }
        [Key]
        public int EventRouteID { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int EventID { get; set; }
        public int RouteID { get; set; }
        
        //navigational properties
        public Event Event { get; set; }
        public Route Route { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace iBalekaService.Domain.Models
{
    public class Route
    {
        public Route(int distance,string mapImage)
        {
            Distance = distance;
            DateRecorded = DateTime.Now;
            MapImage = mapImage;
            Deleted = false;
        }
        [Key]
        public int RouteID { get; set; }
        public int Distance { get; set; }
        public DateTime DateRecorded { get; set; }
        public DateTime DateModified { get; set; }
        public string MapImage { get; set; }
        public bool Deleted { get; set; }
        //navigational properties
        public virtual ICollection<Checkpoint> Checkpoints { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Run> Runs { get; set; }
        public virtual ICollection<Event_Route> EventRoutes { get; set; }
    }
}

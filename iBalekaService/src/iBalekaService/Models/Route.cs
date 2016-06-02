using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
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

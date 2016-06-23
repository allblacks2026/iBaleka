using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaService.Domain.Models
{
    public class Checkpoint
    {
        public Checkpoint(string lat,string lng)
        {
            Latitude = lat;
            Longitude = lng;
            Deleted = false;
        }
        [Key]
        public int CheckpointID { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int? RouteID { get; set; }
        //navigational property
        public virtual Route Route { get; set; }
    }
}

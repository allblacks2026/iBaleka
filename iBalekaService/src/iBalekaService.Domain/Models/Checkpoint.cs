﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace iBalekaService.Domain.Models
{
    public class Checkpoint
    {
        public Checkpoint() { }
        public Checkpoint(double lat,double lng,Route route)
        {
            Latitude = lat;
            Longitude = lng;
            Route = route;
            Deleted = false;
        }
        [Key]
        public int CheckpointID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int? RouteID { get; set; }
        //navigational property
        public virtual Route Route { get; set; }
    }
}

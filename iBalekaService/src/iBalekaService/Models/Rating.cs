using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Rating
    {
        public Rating(int value,string comment,Route route)
        {
            Value = value;
            Comment = comment;
            DateAdded = DateTime.Now;
            RouteID = route.RouteID;
            Deleted = false;
        }
        public int RatingID { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int RouteID { get; set; }
        //navigational property
        public virtual Route Route{ get; set; }
}
}

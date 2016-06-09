using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Rating
    {
        public Rating(int value, string comment, Run run)
        {
            Value = value;
            Comment = comment;
            DateAdded = DateTime.Now;
            RunID = run.RunID;
            Deleted = false;
        }
        [Key]
        public int RatingID { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public DateTime DateAdded { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int RunID { get; set; }
        //navigational property
        [ForeignKey("RunID")]
        public virtual Run Run { get; set; }

}
}

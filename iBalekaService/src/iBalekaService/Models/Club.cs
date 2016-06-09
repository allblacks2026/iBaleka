using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Club
    {
        public Club(string name,string cityCountry,string desc)
        {
            Name = name;
            Location = cityCountry;
            Description = desc;
            DateCreated = DateTime.Now.Date;
            Deleted = false;
        }
        [Key]
        public int ClubID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int UserID { get; set; }
        //navigational properties
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual ICollection<Club_Athlete> Members { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}

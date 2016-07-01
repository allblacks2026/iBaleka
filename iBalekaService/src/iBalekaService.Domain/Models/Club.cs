using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace iBalekaService.Domain.Models
{
    public class Club
    {
        public Club(string name,string cityCountry,string desc,int userID)
        {
            Name = name;
            Location = cityCountry;
            Description = desc;
            UserID = userID;
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Athlete
    {
        public Athlete(string first,string surname,double weight,double height,char gender)
        {
            Firstname = first;
            Surname = surname;
            Weight = weight;
            Height = height;
            Gender = gender;
            DateJoined = DateTime.Now.Date;
            Deleted = false;
        }
        public Athlete(string first, string surname,char gender)
        {
            Firstname = first;
            Surname = surname;
            Gender = gender;
            DateJoined = DateTime.Now.Date;
            Deleted = false;
        }

        [Key]
        public int AthleteID { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public char? Gender { get; set; }
        public string LicenseNo { get; set; }
        public DateTime DateJoined { get; set; }
        public bool Deleted { get; set; }
        //navigational properties
        public virtual ICollection<EventRegistration> EventsRegistered { get; set; }
        public virtual ICollection<Club_Athlete> Clubs { get; set; }
        public virtual ICollection<Run> Runs { get; set; }

    }
}

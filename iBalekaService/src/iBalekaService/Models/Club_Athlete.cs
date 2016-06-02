using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Club_Athlete
    {
        public Club_Athlete(Club club,Athlete athlete)
        {
            ClubID = club.ClubID;
            AthleteID = athlete.AthleteID;
            DateJoined = DateTime.Now.Date;
            IsaMember = true;
        }
        public int MemberID { get; set; }
        public DateTime DateJoined { get; set; }
        public DateTime? DateLeft { get; set; }
        public bool IsaMember { get; set; }
        //foreign key
        public int ClubID { get; set; }
        public int AthleteID { get; set; }
        //navigational properties
        public virtual Athlete Athlete { get; set; }
        public virtual Club Club { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Models
{
    public class Run
    {
        public Run(DateTime start,DateTime end,string caloriesBurnt,Event evnt,Athlete athlete)
        {
            StartTime = start;
            EndTime = end;
            CaloriesBurnt = caloriesBurnt;
            DateRecorded = DateTime.Now;
            EventID = evnt.EventID;
            AthleteID = athlete.AthleteID;
            Deleted = false;
        }
        public Run(DateTime start, DateTime end, string caloriesBurnt, Route route, Athlete athlete)
        {
            StartTime = start;
            EndTime = end;
            CaloriesBurnt = caloriesBurnt;
            DateRecorded = DateTime.Now;
            RouteID = route.RouteID;
            AthleteID = athlete.AthleteID;
            Deleted = false;
        }
        [Key]
        public int RunID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CaloriesBurnt { get; set; }
        public DateTime DateRecorded { get; set; }
        public bool Deleted { get; set; }
        //foreign keys
        public int? EventID { get; set; } 
        public int? RouteID { get; set; }
        public int AthleteID { get; set; }
        //navigational properties
        public virtual Event Event { get; set;}
        public virtual Route  Route { get; set; }
        public virtual Athlete Athlete {get;set;}
    }
}

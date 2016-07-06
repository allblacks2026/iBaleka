using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace iBalekaService.Domain.Models
{
    public class Run
    {
        public Run() { }
        public Run(DateTime start,DateTime end,double caloriesBurnt,Event evnt,Athlete athlete)
        {
            StartTime = start;
            EndTime = end;
            CaloriesBurnt = caloriesBurnt;
            DateRecorded = DateTime.Now;
            Event = evnt;
            Athlete = athlete;
            Deleted = false;
        }
        public Run(DateTime start, DateTime end, double caloriesBurnt, Route route, Athlete athlete)
        {
            StartTime = start;
            EndTime = end;
            CaloriesBurnt = caloriesBurnt;
            DateRecorded = DateTime.Now;
            Route = route;
            Athlete = athlete;
            Deleted = false;
        }
        [Key]
        public int RunID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double CaloriesBurnt { get; set; }
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
   
        public virtual Rating Rating { get; set; }
    }
}

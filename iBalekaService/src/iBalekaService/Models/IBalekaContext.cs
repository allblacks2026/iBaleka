using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;



namespace iBalekaService.Models
{
    public class IBalekaContext : DbContext
    {
        public IBalekaContext(DbContextOptions<IBalekaContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //run - route optional one-to-many
            modelBuilder.Entity<Run>().HasOne(r => r.Route)
                .WithMany(rt => rt.Runs)
                .HasForeignKey(r => r.RouteID);
            //run - event optional one-to-many
            modelBuilder.Entity<Run>().HasOne(r => r.Event)
                .WithMany(e => e.Runs)
                .HasForeignKey(r => r.EventID);
            //athlete - run one-to-many
            modelBuilder.Entity<Run>().HasOne(r => r.Athlete)
                .WithMany(a => a.Runs)
                .HasForeignKey(r => r.AthleteID);
            //route - rating one-to-one
            modelBuilder.Entity<Rating>().HasOne(r => r.Run);
                
            //event_route - event one-to-many
            modelBuilder.Entity<Event_Route>().HasOne(er => er.Event)
                .WithMany(e => e.EventRoutes)
                .HasForeignKey(er => er.EventID);
            //event_route - route one-to-many
            modelBuilder.Entity<Event_Route>().HasOne(er => er.Route)
                .WithMany(r => r.EventRoutes)
                .HasForeignKey(er => er.RouteID);
            //event_registration - athlete one-to-many
            modelBuilder.Entity<EventRegistration>().HasOne(er => er.Athlete)
                .WithMany(a => a.EventsRegistered)
                .HasForeignKey(er => er.AthleteID);
            //event_registration - event one-to-many
            modelBuilder.Entity<EventRegistration>().HasOne(er => er.Event)
                .WithMany(e => e.Participants)
                .HasForeignKey(er => er.EventID);
            //club_athlete - club one-to-one
            modelBuilder.Entity<Club_Athlete>().HasOne(ca => ca.Club)
                .WithMany(c => c.Members)
                .HasForeignKey(ca => ca.ClubID);
            //club_athlete - athlete(member) one to many
            modelBuilder.Entity<Club_Athlete>().HasOne(c => c.Athlete)
                .WithOne(j => j.Club);
            //club - event one-to-many
            modelBuilder.Entity<Event>().HasOne(e => e.Club)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.ClubID);
            //user - club one-to-many
            modelBuilder.Entity<Club>().HasOne(c => c.User)
                .WithMany(u => u.Clubs)
                .HasForeignKey(c => c.UserID);
            //route - checkpoint one-to-many 
            modelBuilder.Entity<Checkpoint>().HasOne(c => c.Route)
                .WithMany(r => r.Checkpoints)
                .HasForeignKey(p => p.RouteID);
            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Athlete> Athlete { get; set; }
        public virtual DbSet<Checkpoint> Checkpoint { get; set; }
        public virtual DbSet<Club> Club { get; set; }
        public virtual DbSet<Club_Athlete> ClubMember { get; set; }
        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<EventRegistration> EventRegistration { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Route> Route { get; set; }
        public virtual DbSet<Run> Run { get; set; }
        public virtual DbSet<User> User { get; set; }



    }
}

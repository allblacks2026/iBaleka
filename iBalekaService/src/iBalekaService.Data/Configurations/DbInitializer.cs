using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace iBalekaService.Data.Configurations
{
    public static class DbInitializer
    {
        static IBalekaContext db;
        // TODO: Move this code when seed data is implemented in EF 7
        public static void SeedData(this IApplicationBuilder app)
        {
            db = app.ApplicationServices.GetService<IBalekaContext>();

            // TODO: Add seed logic here
            GetAthletes();
            GetUsers();
            GetClubs();
            GetMembers();
            GetRoutes();
            GetCheckPoints();
            GetEvents();
            GetEventRoutes();
            GetEventRegistrations();
            GetRuns();
            GetRatings();
            db.Commit();
        }
        public static void GetAthletes()
        {
            List<Athlete> _athletes = new List<Athlete>();

            Athlete athleteOne = new Athlete("John", "Snow", 80, 135, 'M');
            Athlete athleteTwo = new Athlete("Lance", "Howaseb", 76, 155, 'M');
            Athlete athleteThree = new Athlete("Sara", "Bones", 44, 140, 'F');
            Athlete athleteFour = new Athlete("Tina", "Keister", 81, 120, 'F');
            Athlete athleteFive = new Athlete("Cheryl", "Peters", 77, 160, 'F');

            _athletes.Add(athleteOne);
            _athletes.Add(athleteTwo);
            _athletes.Add(athleteThree);
            _athletes.Add(athleteFour);
            _athletes.Add(athleteFive);
            foreach (Athlete a in _athletes)
            {
              if(!db.Athlete.Any())
                {
                    db.Athlete.Add(a);
                }                
                
            }
            db.Commit();
        }
        public static void GetUsers()
        {
            List<User> _users = new List<User>();

            User userOne = new User("Tom", "Founder", "South Africa", DateTime.Parse("1990/02/07"));
            User userTwo = new User("Micheal", "Sonata", "South Africa", DateTime.Parse("1899/10/12"));
            User userThree = new User("Arnold", "Whiternager", "Russia", DateTime.Parse("1880/06/03"));
            User userFour = new User("Samantha", "Rocks", "Namibia", DateTime.Parse("1970/11/05"));
            User userFive = new User("Peter", "Mawoyo", "Zimbabwe", DateTime.Parse("1995/06/20"));

            _users.Add(userOne);
            _users.Add(userTwo);
            _users.Add(userThree);
            _users.Add(userFour);
            _users.Add(userFive);
            foreach (User a in _users)
            {
                if (!db.User.Any())
                {
                    db.User.Add(a);
                }
            }
            db.Commit();
        }
        public static void GetClubs()
        {
            List<Club> _clubs = new List<Club>();

            Club clubOne = new Club("NMMURunners", "Port Elizabeth", "NMMU runners and enthusiasts", db.User.Single<User>(a=>a.Name == "Tom"));
            Club clubTwo = new Club("LetabaRollers", "Port Elizabeth", "Letaba Res Runners", db.User.Single<User>(a => a.Name == "Peter"));
            Club clubThree = new Club("SSVBabes", "Port Elizabeth", "SSV Ladies Club", db.User.Single<User>(a => a.Name == "Arnold"));
            Club clubFour = new Club("BraveWariors", "Namibia", "Namibian Soccer Team", db.User.Single<User>(a => a.Name == "Samantha"));
            Club clubFive = new Club("Leviathans", "Cape Town", "Random dudes", db.User.Single<User>(a => a.Name == "Micheal"));

            _clubs.Add(clubOne);
            _clubs.Add(clubTwo);
            _clubs.Add(clubThree);
            _clubs.Add(clubFour);
            _clubs.Add(clubFive);
            foreach (Club a in _clubs)
            {
                if (!db.Club.Any())
                {
                    db.Club.Add(a);
                }
            }
            db.Commit();
        }
        public static void GetMembers()
        {
            List<Club_Athlete> _clubMembers = new List<Club_Athlete>();

            Club_Athlete memberOne = new Club_Athlete(db.Club.Single<Club>(a=>a.Name== "NMMURunners"), db.Athlete.Single<Athlete>(a=>a.Firstname=="John"));
            Club_Athlete memberTwo = new Club_Athlete(db.Club.Single<Club>(a => a.Name == "LetabaRollers"), db.Athlete.Single<Athlete>(a => a.Firstname == "Lance"));
            Club_Athlete memberThree = new Club_Athlete(db.Club.Single<Club>(a => a.Name == "SSVBabes"), db.Athlete.Single<Athlete>(a => a.Firstname == "Tina"));
            Club_Athlete memberFour = new Club_Athlete(db.Club.Single<Club>(a => a.Name == "BraveWariors"), db.Athlete.Single<Athlete>(a => a.Firstname == "Sara"));
            Club_Athlete memberFive = new Club_Athlete(db.Club.Single<Club>(a => a.Name == "BraveWariors"), db.Athlete.Single<Athlete>(a => a.Firstname == "Cheryl"));

            _clubMembers.Add(memberOne);
            _clubMembers.Add(memberTwo);
            _clubMembers.Add(memberThree);
            _clubMembers.Add(memberFour);
            _clubMembers.Add(memberFive);
            foreach (Club_Athlete a in _clubMembers)
            {
                if (!db.ClubMember.Any())
                {
                    db.ClubMember.Add(a);
                }
            }
            db.Commit();
            
        }
        public static void GetRoutes()
        {
            List<Route> _routes = new List<Route>();

            Route routeOne = new Route(5.5, "routeOne.jpg");
            Route routeTwo = new Route(2.2, "routeTwo.jpg");
            Route routeThree = new Route(2.1, "routeThree.jpg");
            Route routeFour = new Route(5.9, "routeFour.jpg");
            Route routeFive = new Route(6.8, "routeFive.jpg");

            _routes.Add(routeOne);
            _routes.Add(routeTwo);
            _routes.Add(routeThree);
            _routes.Add(routeFour);
            _routes.Add(routeFive);
            foreach (Route a in _routes)
            {
                if (!db.Route.Any())
                {
                    db.Route.Add(a);
                }
            }
            db.Commit();
            
        }
        public static void GetCheckPoints()
        {
            List<Checkpoint> _checkPoints = new List<Checkpoint>();

            Checkpoint checkOne = new Checkpoint(1, 1, db.Route.Single(r=>r.MapImage== "routeOne.jpg"));
            Checkpoint checkTwo = new Checkpoint(2, 2, db.Route.Single(r => r.MapImage == "routeOne.jpg"));
            Checkpoint checkThree = new Checkpoint(3, 3, db.Route.Single(r => r.MapImage == "routeOne.jpg"));
            Checkpoint checkFour = new Checkpoint(1, 1, db.Route.Single(r => r.MapImage == "routeTwo.jpg"));
            Checkpoint checkFive = new Checkpoint(2, 2, db.Route.Single(r => r.MapImage == "routeTwo.jpg"));
            Checkpoint checkSix = new Checkpoint(3, 3, db.Route.Single(r => r.MapImage == "routeTwo.jpg"));
            Checkpoint checkSeven = new Checkpoint(1, 1, db.Route.Single(r => r.MapImage == "routeThree.jpg"));
            Checkpoint checkEight = new Checkpoint(2, 2, db.Route.Single(r => r.MapImage == "routeThree.jpg"));
            Checkpoint checkNine = new Checkpoint(3, 3, db.Route.Single(r => r.MapImage == "routeThree.jpg"));
            Checkpoint checkTen = new Checkpoint(1, 1, db.Route.Single(r => r.MapImage == "routeFour.jpg"));
            Checkpoint checkEleven = new Checkpoint(2, 2, db.Route.Single(r => r.MapImage == "routeFour.jpg"));
            Checkpoint checkTwelf = new Checkpoint(3, 3, db.Route.Single(r => r.MapImage == "routeFour.jpg"));
            Checkpoint checkThirteen = new Checkpoint(1, 1, db.Route.Single(r => r.MapImage == "routeOne.jpg"));
            Checkpoint checkFourteen = new Checkpoint(2, 2, db.Route.Single(r => r.MapImage == "routeOne.jpg"));
            Checkpoint checkFifteen = new Checkpoint(3, 3, db.Route.Single(r => r.MapImage == "routeFive.jpg"));
            Checkpoint checkSixteen = new Checkpoint(1, 1, db.Route.Single(r => r.MapImage == "routeFive.jpg"));
            Checkpoint checkSeventeen = new Checkpoint(2, 2, db.Route.Single(r => r.MapImage == "routeFive.jpg"));
            Checkpoint checkEighteen = new Checkpoint(3, 3, db.Route.Single(r => r.MapImage == "routeFive.jpg"));

            _checkPoints.Add(checkOne);
            _checkPoints.Add(checkTwo);
            _checkPoints.Add(checkThree);
            _checkPoints.Add(checkFour);
            _checkPoints.Add(checkFive);
            _checkPoints.Add(checkSix);
            _checkPoints.Add(checkSeven);
            _checkPoints.Add(checkEight);
            _checkPoints.Add(checkNine);
            _checkPoints.Add(checkTen);
            _checkPoints.Add(checkEleven);
            _checkPoints.Add(checkTwelf);
            _checkPoints.Add(checkThirteen);
            _checkPoints.Add(checkFourteen);
            _checkPoints.Add(checkFifteen);
            _checkPoints.Add(checkSixteen);
            _checkPoints.Add(checkSeventeen);
            _checkPoints.Add(checkEighteen);
            foreach (Checkpoint a in _checkPoints)
            {
                if (!db.Checkpoint.Any())
                {
                    db.Checkpoint.Add(a);
                }
            }
            db.Commit();
            
        }
        public static void GetEvents()
        {
            List<Event> _events = new List<Event>();

            Event eventOne = new Event("Womans Run", "Ladies only run", DateTime.Now, "Summerstrand#PortElizabeth#EC", db.Club.Single<Club>(a => a.Name == "NMMURunners"));
            Event eventTwo = new Event("Mens Run", "Mens only run", DateTime.Now, "Corsten#PortElizabeth#EC", db.Club.Single(a => a.Name == "LetabaRollers"));
            Event eventThree = new Event("Gangstars Run", "A run where you actually run", DateTime.Now, "Westering#CapeTown#WC", db.Club.Single<Club>(a => a.Name == "SSVBabes"));
            Event eventFour = new Event("Leisure Walk", "Boring Walk", DateTime.Now, "LosSantos#Joburg#Gauteng", db.Club.Single<Club>(a => a.Name == "BraveWariors"));
            Event eventFive = new Event("Lovers Run", "Couples Runs", DateTime.Now, "Gomery#PortElizabeth#EC", db.Club.Single<Club>(a => a.Name == "BraveWariors"));

            _events.Add(eventOne);
            _events.Add(eventTwo);
            _events.Add(eventThree);
            _events.Add(eventFour);
            _events.Add(eventFive);
            foreach (Event a in _events)
            {
                if (!db.Event.Any())
                {
                    db.Event.Add(a);
                }
            }
            db.Commit();
            
        }
        public static void GetEventRoutes()
        {
            List<Event_Route> _eventRoutes = new List<Event_Route>();

            Event_Route routeOne = new Event_Route("Moderate Run", db.Event.Single(e=>e.Title== "Womans Run"), db.Route.Single(r => r.MapImage == "routeOne.jpg"));
            Event_Route routeTwo = new Event_Route("Lazy Walk", db.Event.Single(e => e.Title == "Mens Run"), db.Route.Single(r => r.MapImage == "routeTwo.jpg"));
            Event_Route routeThree = new Event_Route("Challenging Run", db.Event.Single(e => e.Title == "Gangstars Run"), db.Route.Single(r => r.MapImage == "routeThree.jpg"));
            Event_Route routeFour = new Event_Route("For the die hard", db.Event.Single(e => e.Title == "Leisure Walk"), db.Route.Single(r => r.MapImage == "routeFour.jpg"));
            Event_Route routeFive = new Event_Route("Doom ahead", db.Event.Single(e => e.Title == "Lovers Run"),db.Route.Single(r => r.MapImage == "routeFive.jpg"));

            _eventRoutes.Add(routeOne);
            _eventRoutes.Add(routeTwo);
            _eventRoutes.Add(routeThree);
            _eventRoutes.Add(routeFour);
            _eventRoutes.Add(routeFive);
            foreach (Event_Route a in _eventRoutes)
            {
                if (!db.EventRoute.Any())
                {
                    db.EventRoute.Add(a);
                }
            }
            db.Commit();
         
        }
        public static void GetEventRegistrations()
        {
            List<EventRegistration> _registrations = new List<EventRegistration>();

            EventRegistration regOne = new EventRegistration(db.EventRoute.Single(a=>a.Description== "Moderate Run"), db.Athlete.Single(a => a.Firstname == "John"));
            EventRegistration regTwo = new EventRegistration(db.EventRoute.Single(a => a.Description == "Moderate Run"), db.Athlete.Single(a => a.Firstname == "Lance"));
            EventRegistration regThree = new EventRegistration(db.EventRoute.Single(a => a.Description == "Lazy Walk"), db.Athlete.Single(a => a.Firstname == "Tina"));
            EventRegistration regFour = new EventRegistration(db.EventRoute.Single(a => a.Description == "Lazy Walk"), db.Athlete.Single(a => a.Firstname == "Sara"));
            EventRegistration regFive = new EventRegistration(db.EventRoute.Single(a => a.Description == "Challenging Run"), db.Athlete.Single(a => a.Firstname == "Cheryl"));
            EventRegistration regSeven = new EventRegistration(db.EventRoute.Single(a => a.Description == "Challenging Run"), db.Athlete.Single(a => a.Firstname == "Lance"));
            EventRegistration regEight = new EventRegistration(db.EventRoute.Single(a => a.Description == "For the die hard"), db.Athlete.Single(a => a.Firstname == "Tina"));
            EventRegistration regNine = new EventRegistration(db.EventRoute.Single(a => a.Description == "For the die hard"), db.Athlete.Single(a => a.Firstname == "Sara"));
            EventRegistration regTen = new EventRegistration(db.EventRoute.Single(a => a.Description == "Doom ahead"), db.Athlete.Single(a => a.Firstname == "Cheryl"));

            _registrations.Add(regOne);
            _registrations.Add(regTwo);
            _registrations.Add(regThree);
            _registrations.Add(regFour);
            _registrations.Add(regFive);
            _registrations.Add(regSeven);
            _registrations.Add(regEight);
            _registrations.Add(regNine);
            _registrations.Add(regTen);
            foreach (EventRegistration a in _registrations)
            {
                if (!db.EventRegistration.Any())
                {
                    db.EventRegistration.Add(a);
                }
            }
            db.Commit();
        }
        public static void GetRuns()
        {
            List<Run> _runs = new List<Run>();

            Run runOne = new Run(DateTime.Now, DateTime.Now.AddMinutes(10), 150, db.Event.Single(e => e.Title == "Womans Run"), db.Athlete.Single(a => a.Firstname == "John"));
            Run runTwo = new Run(DateTime.Now, DateTime.Now.AddMinutes(15), 150, db.Event.Single(e => e.Title == "Mens Run"), db.Athlete.Single(a => a.Firstname == "Lance"));
            Run runThree = new Run(DateTime.Now, DateTime.Now.AddMinutes(19), 150, db.Event.Single(e => e.Title == "Leisure Walk"), db.Athlete.Single(a => a.Firstname == "Sara"));
            Run runFour = new Run(DateTime.Now, DateTime.Now.AddMinutes(5), 150, db.Event.Single(e => e.Title == "Lovers Run"), db.Athlete.Single(a => a.Firstname == "Tina"));
            Run runFive = new Run(DateTime.Now, DateTime.Now.AddMinutes(12), 150, db.Event.Single(e => e.Title == "Womans Run"), db.Athlete.Single(a => a.Firstname == "Cheryl"));
            Run runSix = new Run(DateTime.Now, DateTime.Now.AddMinutes(10), 150, db.Route.Single(r => r.MapImage == "routeOne.jpg"), db.Athlete.Single(a => a.Firstname == "John"));
            Run runSeven = new Run(DateTime.Now, DateTime.Now.AddMinutes(20), 150, db.Route.Single(r => r.MapImage == "routeTwo.jpg"), db.Athlete.Single(a => a.Firstname == "Lance"));
            Run runEight = new Run(DateTime.Now, DateTime.Now.AddMinutes(22), 150, db.Route.Single(r => r.MapImage == "routeThree.jpg"), db.Athlete.Single(a => a.Firstname == "Lance"));
            Run runNine = new Run(DateTime.Now, DateTime.Now.AddMinutes(17), 150, db.Route.Single(r => r.MapImage == "routeFour.jpg"), db.Athlete.Single(a => a.Firstname == "Tina"));
            Run runTen = new Run(DateTime.Now, DateTime.Now.AddMinutes(13), 150, db.Event.Single(e => e.Title == "Mens Run"), db.Athlete.Single(a => a.Firstname == "Cheryl"));
            Run runEleven = new Run(DateTime.Now, DateTime.Now.AddMinutes(30), 150, db.Event.Single(e => e.Title == "Gangstars Run"), db.Athlete.Single(a => a.Firstname == "Sara"));

            _runs.Add(runOne);
            _runs.Add(runTwo);
            _runs.Add(runThree);
            _runs.Add(runFour);
            _runs.Add(runFive);
            _runs.Add(runSix);
            _runs.Add(runSeven);
            _runs.Add(runEight);
            _runs.Add(runNine);
            _runs.Add(runTen);
            _runs.Add(runEleven);
            foreach (Run a in _runs)
            {
                if (!db.Run.Any())
                {
                    db.Run.Add(a);
                }
            }
            db.Commit();
          
        }
        public static void GetRatings()
        {
            List<Rating> _ratings = new List<Rating>();

            Rating ratOne = new Rating(5, "Good Run", db.Run.Single(a=>a.Athlete.Firstname== "John"));
            Rating ratTwo = new Rating(2, "Weak Run", db.Run.Single(a => a.Athlete.Firstname == "Sara"));
            Rating ratThree = new Rating(4, "Awesome Run", db.Run.Single(a => a.Athlete.Firstname == "Cheryl"));
            Rating ratFour = new Rating(3, "Sweet Run", db.Run.Single(a => a.Athlete.Firstname == "Lance"));
            Rating ratFive = new Rating(1, "I almost died", db.Run.Single(a => a.Athlete.Firstname == "Tina"));

            _ratings.Add(ratOne);
            _ratings.Add(ratTwo);
            _ratings.Add(ratThree);
            _ratings.Add(ratFour);
            _ratings.Add(ratFive);
            foreach (Rating a in _ratings)
            {
                if (!db.Rating.Any())
                {
                    db.Rating.Add(a);
                }
            }
            db.Commit();
            
        }




    }
}

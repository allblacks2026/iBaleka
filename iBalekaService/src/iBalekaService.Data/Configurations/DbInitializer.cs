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
        // TODO: Move this code when seed data is implemented in EF 7
        public static void SeedData(this IApplicationBuilder app)
        {
            var db = app.ApplicationServices.GetService<IBalekaContext>();

            // TODO: Add seed logic here
            GetAthletes().ForEach(b => db.Athlete.Add(b));
            GetUsers().ForEach(b => db.User.Add(b));
            GetClubs().ForEach(b => db.Club.Add(b));
            GetMembers().ForEach(b => db.ClubMember.Add(b));
            GetRoutes().ForEach(b => db.Route.Add(b));
            GetCheckPoints().ForEach(b => db.Checkpoint.Add(b));
            GetEvents().ForEach(b => db.Event.Add(b));
            GetEventRoutes().ForEach(b => db.EventRoute.Add(b));
            GetEventRegistrations().ForEach(b => db.EventRegistration.Add(b));
            GetRuns().ForEach(b => db.Run.Add(b));
            GetRatings().ForEach(b => db.Rating.Add(b));
            db.Commit();
        }
        public static List<Athlete> GetAthletes()
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
            return _athletes;
        }
        public static List<User> GetUsers()
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
            return _users;
        }
        public static List<Club> GetClubs()
        {
            List<Club> _clubs = new List<Club>();

            Club clubOne = new Club("NMMURunners", "Port Elizabeth", "NMMU runners and enthusiasts", 1);
            Club clubTwo = new Club("LetabaRollers", "Port Elizabeth", "Letaba Res Runners", 3);
            Club clubThree = new Club("SSVBabes", "Port Elizabeth", "SSV Ladies Club", 2);
            Club clubFour = new Club("BraveWariors", "Namibia", "Namibian Soccer Team", 4);
            Club clubFive = new Club("Leviathans", "Cape Town", "Random dudes", 5);

            _clubs.Add(clubOne);
            _clubs.Add(clubTwo);
            _clubs.Add(clubThree);
            _clubs.Add(clubFour);
            _clubs.Add(clubFive);
            return _clubs;
        }
        public static List<Club_Athlete> GetMembers()
        {
            List<Club_Athlete> _clubMembers = new List<Club_Athlete>();

            Club_Athlete memberOne = new Club_Athlete(GetClubs()[1],GetAthletes()[1]);
            Club_Athlete memberTwo = new Club_Athlete(GetClubs()[1], GetAthletes()[0]);
            Club_Athlete memberThree = new Club_Athlete(GetClubs()[1], GetAthletes()[2]);
            Club_Athlete memberFour = new Club_Athlete(GetClubs()[2], GetAthletes()[3]);
            Club_Athlete memberFive = new Club_Athlete(GetClubs()[2], GetAthletes()[4]);

            _clubMembers.Add(memberOne);
            _clubMembers.Add(memberTwo);
            _clubMembers.Add(memberThree);
            _clubMembers.Add(memberFour);
            _clubMembers.Add(memberFive);
            return _clubMembers;
        }
        public static List<Route> GetRoutes()
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
            return _routes;
        }
        public static List<Checkpoint> GetCheckPoints()
        {
            List<Checkpoint> _checkPoints = new List<Checkpoint>();

            Checkpoint checkOne = new Checkpoint(1, 1, 1);
            Checkpoint checkTwo = new Checkpoint(2, 2, 1);
            Checkpoint checkThree = new Checkpoint(3, 3, 1);
            Checkpoint checkFour = new Checkpoint(1, 1, 2);
            Checkpoint checkFive = new Checkpoint(2, 2, 2);
            Checkpoint checkSix = new Checkpoint(3, 3, 2);
            Checkpoint checkSeven = new Checkpoint(1, 1, 3);
            Checkpoint checkEight = new Checkpoint(2, 2, 3);
            Checkpoint checkNine = new Checkpoint(3, 3, 3);
            Checkpoint checkTen = new Checkpoint(1, 1, 4);
            Checkpoint checkEleven = new Checkpoint(2, 2, 4);
            Checkpoint checkTwelf = new Checkpoint(3, 3, 4);
            Checkpoint checkThirteen = new Checkpoint(1, 1, 5);
            Checkpoint checkFourteen = new Checkpoint(2, 2, 5);
            Checkpoint checkFifteen = new Checkpoint(3, 3, 5);
            Checkpoint checkSixteen = new Checkpoint(1, 1, 0);
            Checkpoint checkSeventeen = new Checkpoint(2, 2, 0);
            Checkpoint checkEighteen = new Checkpoint(3, 3, 0);

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
            return _checkPoints;
        }
        public static List<Event> GetEvents()
        {
            List<Event> _events = new List<Event>();

            Event eventOne = new Event("Womans Run", "Ladies only run", DateTime.Now, "Summerstrand#PortElizabeth#EC", 0);
            Event eventTwo = new Event("Mens Run", "Mens only run", DateTime.Now, "Corsten#PortElizabeth#EC",1);
            Event eventThree = new Event("Gangstars Run", "A run where you actually run", DateTime.Now, "Westering#CapeTown#WC", 2);
            Event eventFour = new Event("Leisure Walk", "Boring Walk", DateTime.Now, "LosSantos#Joburg#Gauteng",3);
            Event eventFive = new Event("Lovers Run", "Couples Runs", DateTime.Now, "Gomery#PortElizabeth#EC", 5);

            _events.Add(eventOne);
            _events.Add(eventTwo);
            _events.Add(eventThree);
            _events.Add(eventFour);
            _events.Add(eventFive);
            return _events;
        }
        public static List<Event_Route> GetEventRoutes()
        {
            List<Event_Route> _eventRoutes = new List<Event_Route>();

            Event_Route routeOne = new Event_Route("Moderate Run", GetEvents()[0], GetRoutes()[0]);
            Event_Route routeTwo = new Event_Route("Lazy Walk", GetEvents()[1], GetRoutes()[1]);
            Event_Route routeThree = new Event_Route("Challenging Run", GetEvents()[2], GetRoutes()[2]);
            Event_Route routeFour = new Event_Route("For the die hard", GetEvents()[3], GetRoutes()[3]);
            Event_Route routeFive = new Event_Route("Doom ahead", GetEvents()[4], GetRoutes()[4]);

            _eventRoutes.Add(routeOne);
            _eventRoutes.Add(routeTwo);
            _eventRoutes.Add(routeThree);
            _eventRoutes.Add(routeFour);
            _eventRoutes.Add(routeFive);
            return _eventRoutes;
        }
        public static List<EventRegistration> GetEventRegistrations()
        {
            List<EventRegistration> _registrations = new List<EventRegistration>();

            EventRegistration regOne = new EventRegistration(GetEventRoutes()[0], GetAthletes()[0]);
            EventRegistration regTwo = new EventRegistration(GetEventRoutes()[0], GetAthletes()[1]);
            EventRegistration regThree = new EventRegistration(GetEventRoutes()[0], GetAthletes()[2]);
            EventRegistration regFour = new EventRegistration(GetEventRoutes()[0], GetAthletes()[3]);
            EventRegistration regFive = new EventRegistration(GetEventRoutes()[0], GetAthletes()[4]);
            EventRegistration regSix = new EventRegistration(GetEventRoutes()[0], GetAthletes()[5]);
            EventRegistration regSeven = new EventRegistration(GetEventRoutes()[0], GetAthletes()[2]);
            EventRegistration regEight = new EventRegistration(GetEventRoutes()[0], GetAthletes()[3]);
            EventRegistration regNine = new EventRegistration(GetEventRoutes()[0], GetAthletes()[2]);
            EventRegistration regTen = new EventRegistration(GetEventRoutes()[0], GetAthletes()[4]);

            _registrations.Add(regOne);
            _registrations.Add(regTwo);
            _registrations.Add(regThree);
            _registrations.Add(regFour);
            _registrations.Add(regFive);
            _registrations.Add(regSix);
            _registrations.Add(regSeven);
            _registrations.Add(regEight);
            _registrations.Add(regNine);
            _registrations.Add(regTen);
            return _registrations;
        }
        public static List<Run> GetRuns()
        {
            List<Run> _runs = new List<Run>();

            Run runOne = new Run(DateTime.Now, DateTime.Now.AddMinutes(10), 150, GetEvents()[0], GetAthletes()[0]);
            Run runTwo = new Run(DateTime.Now, DateTime.Now.AddMinutes(15), 150, GetEvents()[0], GetAthletes()[1]);
            Run runThree = new Run(DateTime.Now, DateTime.Now.AddMinutes(19), 150, GetEvents()[1], GetAthletes()[2]);
            Run runFour = new Run(DateTime.Now, DateTime.Now.AddMinutes(5), 150, GetEvents()[1], GetAthletes()[3]);
            Run runFive = new Run(DateTime.Now, DateTime.Now.AddMinutes(12), 150, GetEvents()[2], GetAthletes()[4]);
            Run runSix = new Run(DateTime.Now, DateTime.Now.AddMinutes(10), 150, GetRoutes()[0], GetAthletes()[0]);
            Run runSeven = new Run(DateTime.Now, DateTime.Now.AddMinutes(20), 150, GetRoutes()[1], GetAthletes()[1]);
            Run runEight = new Run(DateTime.Now, DateTime.Now.AddMinutes(22), 150, GetRoutes()[2], GetAthletes()[2]);
            Run runNine = new Run(DateTime.Now, DateTime.Now.AddMinutes(17), 150, GetRoutes()[3], GetAthletes()[3]);
            Run runTen = new Run(DateTime.Now, DateTime.Now.AddMinutes(13), 150, GetEvents()[2], GetAthletes()[4]);
            Run runEleven = new Run(DateTime.Now, DateTime.Now.AddMinutes(30), 150, GetEvents()[2], GetAthletes()[5]);

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
            return _runs;
        }
        public static List<Rating> GetRatings()
        {
            List<Rating> _ratings = new List<Rating>();

            Rating ratOne = new Rating(5, "Good Run", GetRuns()[0]);
            Rating ratTwo = new Rating(2, "Weak Run", GetRuns()[1]);
            Rating ratThree = new Rating(4, "Awesome Run", GetRuns()[2]);
            Rating ratFour = new Rating(3, "Sweet Run", GetRuns()[3]);
            Rating ratFive = new Rating(1, "I almost died", GetRuns()[4]);

            _ratings.Add(ratOne);
            _ratings.Add(ratTwo);
            _ratings.Add(ratThree);
            _ratings.Add(ratFour);
            _ratings.Add(ratFive);
            return _ratings;
        }
        
       
        

    }
}

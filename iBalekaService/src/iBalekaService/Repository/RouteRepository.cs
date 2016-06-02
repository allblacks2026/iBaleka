using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iBalekaService.Repository
{
    public class RouteRepository:IRouteRepository<Route,Checkpoint>
    {
        protected IBalekaContext _context { get; set; }
        public IiBalekaRepository<Checkpoint> _checkRepo;
        public RouteRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Route> GetRoutes()
        {
            return _context.Route.Where(x => x.Deleted == false).ToList();
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return _context.Checkpoint.Where(x => x.RouteID == id).ToList();
        }
        public Route GetRoute(int id)
        {
            return _context.Route.Where(x => x.RouteID == id && x.Deleted == false).SingleOrDefault();
        }
        public void AddRoute(Route route, Checkpoint[] checkpoints)
        {
            foreach(Checkpoint chp in checkpoints)
            {
                chp.RouteID = route.RouteID;
            }
            route.Deleted = false;
            route.DateRecorded = DateTime.Now.Date;
            //create map image?
            _context.Checkpoint.AddRange(checkpoints);
            _context.Entry(route).State = EntityState.Added;
            _context.SaveChanges();
        }
        public void Update(Route route, Checkpoint[] checkpoints)
        {
            IEnumerable<Checkpoint> Checkpoints = GetCheckpoints(route.RouteID);
            DeleteCheckPoints(Checkpoints);
            foreach (Checkpoint chp in checkpoints)
            {
                chp.RouteID = route.RouteID;
            }            
            route.DateModified = DateTime.Now.Date;
            _context.Checkpoint.AddRange(checkpoints);
            _context.Entry(route).State = EntityState.Modified;
            _context.SaveChanges();
        }
        //flags entity
        public void Delete(int id)
        {
            IEnumerable<Checkpoint> Checkpoints = GetCheckpoints(id);
            if (Checkpoints!=null)
            {
                foreach (Checkpoint check in Checkpoints)
                {
                    check.Deleted = true;
                    _context.Entry(check).State = EntityState.Modified;
                } 
            }
            Route deletedRoute = _context.Route.SingleOrDefault(x => x.RouteID == id);
            if (deletedRoute != null)
            {
                
                deletedRoute.Deleted = true;
                _context.Entry(deletedRoute).State = EntityState.Modified;
                
            }
            _context.SaveChanges();
        }
        public void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints)
        {
            foreach(Checkpoint checkpoint in checkpoints)
            {
                _context.Entry(checkpoint).State = EntityState.Deleted;
            }
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

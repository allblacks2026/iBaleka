using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaService.Data.Repositories
{
    public interface IRouteRepository : IRepository<Route>
    {
        Route GetRouteByID(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints);
        void AddRoute(Route route, Checkpoint[] checkpoints);
        void UpdateRoute(Route route, Checkpoint[] checkpoints);
    }
    public class RouteRepository : RepositoryBase<Route>, IRouteRepository
    {
        public RouteRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public void AddRoute(Route route, Checkpoint[] Checkpoints)
        {
            foreach (Checkpoint chp in Checkpoints)
            {
                chp.RouteID = route.RouteID;
            }
            route.Deleted = false;
            route.DateRecorded = DateTime.Now.Date;
            //create map image?
            DbContext.Checkpoint.AddRange(Checkpoints);
            DbContext.Entry(route).State = EntityState.Added;
        }
        public void UpdateRoute(Route entity, Checkpoint[] Checkpoints)
        {
            IEnumerable<Checkpoint> checkpoints = GetCheckpoints(entity.RouteID);
            DeleteCheckPoints(checkpoints);
            foreach (Checkpoint chp in Checkpoints)
            {
                chp.RouteID = entity.RouteID;
                chp.Deleted = false;
            }
            entity.DateModified = DateTime.Now.Date;
            DbContext.Checkpoint.AddRange(Checkpoints);
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public Route GetRouteByID(int id)
        {
            return DbContext.Route.Where(m => m.RouteID == id && m.Deleted == false).SingleOrDefault();
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return DbContext.Checkpoint.Where(x => x.RouteID == id).ToList();
        }
        public override IEnumerable<Route> GetAll()
        {
            return DbContext.Route.Where(a => a.Deleted == false).ToList();
        }
        public void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints)
        {
            foreach (Checkpoint checkpoint in checkpoints)
            {
                DbContext.Entry(checkpoint).State = EntityState.Deleted;
            }
        }
        public override void Delete(Route entity)
        {
            IEnumerable<Checkpoint> Checkpoints = GetCheckpoints(entity.RouteID);
            if (Checkpoints != null)
            {
                foreach (Checkpoint check in Checkpoints)
                {
                    check.Deleted = true;
                    DbContext.Entry(check).State = EntityState.Modified;
                }
            }
            Route deletedRoute = DbContext.Route.SingleOrDefault(x => x.RouteID == entity.RouteID);
            if (deletedRoute != null)
            {

                deletedRoute.Deleted = true;
                DbContext.Entry(deletedRoute).State = EntityState.Modified;

            }
        }
    }
}


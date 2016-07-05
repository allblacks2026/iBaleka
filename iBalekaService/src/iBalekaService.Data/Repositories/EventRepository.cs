using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaService.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        void AddEventRoute(Event_Route route);
        Event GetEventByID(int id);
        IEnumerable<Event_Route> GetEventRoute(int id);
    }
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public void AddEventRoute(Event_Route route)
        {
            DbContext.EventRoute.Add(route);
        }
        public IEnumerable<Event_Route> GetEventRoute(int id)
        {
            return DbContext.EventRoute.Where(m => m.EventID == id && m.Deleted == false).ToList();
        }
        public Event GetEventByID(int id)
        {
            return DbContext.Event.Where(m => m.EventID == id && m.Deleted == false).SingleOrDefault();
        }
        public override IEnumerable<Event> GetAll()
        {
            return DbContext.Event.Where(a => a.Deleted == false).ToList();
        }
        public override void Delete(Event entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
    }
}

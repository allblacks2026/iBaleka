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
        Event GetEventByID(int id);
    }
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

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

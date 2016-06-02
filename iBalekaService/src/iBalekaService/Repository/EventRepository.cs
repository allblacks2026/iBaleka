using iBalekaService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace iBalekaService.Repository
{
    public class EventRepository:IiBalekaRepository<Event>
    {
        protected IBalekaContext _context { get; set; }

        public EventRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Event> GetAll()
        {
            return _context.Event.Where(x => x.Deleted == false).ToList<Event>();
        }
        public Event Get(int id)
        {
            return _context.Event.Where(x => x.EventID == id && x.Deleted == false).SingleOrDefault();
        }
        public void Add(Event evnt)
        {
            try
            {
                evnt.Deleted = false;
                _context.Entry(evnt).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
        }
        public void Update(Event evnt)
        {
            try
            {
                _context.Entry(evnt).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
        }
        public void Delete(int id)
        {
            Event deletedEvent = _context.Event.SingleOrDefault(x => x.EventID == id);
            if (deletedEvent != null)
            {
                deletedEvent.Deleted = true;
                _context.Entry(deletedEvent).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

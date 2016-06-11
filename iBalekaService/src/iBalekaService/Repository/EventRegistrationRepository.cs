using iBalekaService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Repository
{
    public class EventRegistrationRepository:IiBalekaRepository<EventRegistration>
    {
        protected IBalekaContext _context { get; set; }

        public EventRegistrationRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<EventRegistration> GetAll()
        {
            return _context.EventRegistration.Where(x => x.Deleted == false).ToList();
        }
        public EventRegistration Get(int id)
        {
            return _context.EventRegistration.Where(x => x.RegistrationID == id && x.Deleted == false).SingleOrDefault();
        }
        public void Add(EventRegistration user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
            
        }
        public void Update(EventRegistration user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
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
            EventRegistration deletedUser = _context.EventRegistration.SingleOrDefault(x => x.RegistrationID == id);
            if (deletedUser != null)
            {
                deletedUser.Deleted = true;
                _context.Entry(deletedUser).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

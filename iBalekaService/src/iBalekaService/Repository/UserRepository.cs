using iBalekaService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Repository
{
    public class UserRepository:IiBalekaRepository<User>
    {
        protected IBalekaContext _context { get; set; }

        public UserRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.User.Where(x => x.Deleted == false).ToList<User>();
        }
        public User Get(int id)
        {
            return _context.User.Where(x => x.UserID == id && x.Deleted == false).SingleOrDefault();
        }
        public void Add(User user)
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
        public void Update(User user)
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
            User deletedUser = _context.User.SingleOrDefault(x => x.UserID == id);
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

using iBalekaService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Repository
{
    public class ClubRepository:IiBalekaRepository<Club>
    {
        protected IBalekaContext _context { get; set; }

        public ClubRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Club> GetAll()
        {
            return _context.Club.Where(x => x.Deleted == false).ToList<Club>();
        }
        public Club Get(int id)
        {
            return _context.Club.Where(x => x.ClubID == id && x.Deleted == false).SingleOrDefault();
        }
        public void Add(Club club)
        {
            try
            {
                club.Deleted = false;
                _context.Entry(club).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
            
        }
        public void Update(Club club)
        {
            try
            {
                _context.Entry(club).State = EntityState.Modified;
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
            Club deletedClub = _context.Club.SingleOrDefault(x => x.ClubID == id);
            if (deletedClub != null)
            {
                deletedClub.Deleted = true;
                _context.Entry(deletedClub).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

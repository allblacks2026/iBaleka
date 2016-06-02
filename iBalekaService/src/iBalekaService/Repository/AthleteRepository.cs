using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using iBalekaService.Models;

namespace iBalekaService.Repository
{
    public class AthleteRepository : IiBalekaRepository<Athlete>
    {
        protected IBalekaContext _context { get; set; }

        public AthleteRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Athlete> GetAll()
        {
            return _context.Athlete.Where(x => x.Deleted == false).ToList();
        }
        public Athlete Get(int id)
        {
            return _context.Athlete.Where(x => x.AthleteID == id && x.Deleted == false).SingleOrDefault();
        }
        public void Add(Athlete athlete)
        {
            try
            {
                athlete.Deleted = false;
                _context.Entry(athlete).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
        
        }
        public void Update(Athlete athlete)
        {
            try
            {
                _context.Entry(athlete).State = EntityState.Modified;
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
            Athlete deletedAthlete = _context.Athlete.SingleOrDefault(x => x.AthleteID == id);
            if (deletedAthlete != null)
            {
                deletedAthlete.Deleted = true;
                _context.Entry(deletedAthlete).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
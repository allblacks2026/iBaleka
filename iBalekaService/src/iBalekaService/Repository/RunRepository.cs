using iBalekaService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Repository
{
    public class RunRepository:IRunRepository<Run>
    {
        protected IBalekaContext _context { get; set; }

        public RunRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Run> GetAll(int id)
        {
            return _context.Run.Where(x => x.AthleteID == id && x.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetEventRuns(int id)
        {
            return _context.Run.Where(x => x.AthleteID==id && x.EventID != null && x.RouteID == null && x.Deleted == false);
        }
        public IEnumerable<Run> GetPersonalRuns(int id)
        {
            return _context.Run.Where(x => x.AthleteID == id && x.EventID == null && x.RouteID != null && x.Deleted == false);
        }
        public Run Get(int id)
        {
            return _context.Run.Where(x => x.RunID == id).SingleOrDefault();
        }
        public void Add(Run run)
        {
            try
            {
                run.Deleted = false;
                _context.Entry(run).State = EntityState.Added;
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
            Run deletedRun = _context.Run.SingleOrDefault(x => x.RunID == id);
            if (deletedRun != null)
            {
                deletedRun.Deleted = true;
                _context.Entry(deletedRun).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

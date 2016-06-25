using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iBalekaService.Data.Repositories
{
    public interface IRunRepository : IRepository<Run>
    {
        Run GetRunByID(int id);
        IEnumerable<Run> GetEventRuns(int id);
        IEnumerable<Run> GetPersonalRuns(int id);
        IEnumerable<Run> GetAllRuns(int id);
    }
    public class RunRepository : RepositoryBase<Run>, IRunRepository
    {
        public RunRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public Run GetRunByID(int id)
        {
            return DbContext.Run.Where(a => a.RunID == id && a.Deleted == false).SingleOrDefault();
        }
        public IEnumerable<Run> GetEventRuns(int id)
        {
            return DbContext.Run.Where(a => a.RunID == id && a.EventID != null && a.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetPersonalRuns(int id)
        {
            return DbContext.Run.Where(a => a.RunID == id && a.EventID == null && a.Deleted == false).ToList();
        }
        public IEnumerable<Run> GetAllRuns(int id)
        {
            return DbContext.Run.Where(a => a.RunID == id && a.Deleted == false).ToList();
        }
        public override void Delete(Run entity)
        {
            Run deletedRun = DbContext.Run.SingleOrDefault(x => x.RunID == entity.RunID);
            if (deletedRun != null)
            {
                deletedRun.Deleted = true;
                DbContext.Entry(deletedRun).State = EntityState.Modified;
            }
        }
        public override void Add(Run run)
        {
            run.Deleted = false;
            DbContext.Entry(run).State = EntityState.Added;
        }

    }
}

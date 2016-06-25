using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace iBalekaService.Data.Repositories
{
    public interface IAthleteRepository:IRepository<Athlete>
    {
        Athlete GetAthleteByID(int id);
    }
    public class AthleteRepository:RepositoryBase<Athlete>,IAthleteRepository
    {
        public AthleteRepository(IDbFactory dbFactory)
            : base(dbFactory) { }
        public Athlete GetAthleteByID(int id)
        {
            return DbContext.Athlete.Where(a => a.AthleteID == id && a.Deleted == false).SingleOrDefault();
        }
        public override IEnumerable<Athlete> GetAll()
        {
            return DbContext.Athlete.Where(a => a.Deleted == false).ToList();
        }
        public override void Delete(Athlete entity)
        {
            entity.Deleted = true;
            Update(entity);
        }

    }
}

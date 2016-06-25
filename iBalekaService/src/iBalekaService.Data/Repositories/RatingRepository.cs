using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaService.Data.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        Rating GetRatingByID(int id);
        Rating GetByRun(int id);
    }
    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Rating GetRatingByID(int id)
        {
            return DbContext.Rating.Where(m => m.RatingID == id && m.Deleted == false).SingleOrDefault();
        }
        public override IEnumerable<Rating> GetAll()
        {
            return DbContext.Rating.Where(a => a.Deleted == false).ToList();
        }
        public Rating GetByRun(int id)
        {
            return DbContext.Rating.Where(m => m.RunID == id).SingleOrDefault();
        }
        public override void Delete(Rating entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
    }
}

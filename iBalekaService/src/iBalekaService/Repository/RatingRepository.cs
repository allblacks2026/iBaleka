using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBalekaService.Models;
using Microsoft.EntityFrameworkCore;

namespace iBalekaService.Repository
{
    public class RatingRepository:IiBalekaRepository<Rating>
    {
        protected IBalekaContext _context { get; set; }

        public RatingRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Rating> GetAll()
        {
            return _context.Rating.Where(x => x.Deleted == false).ToList<Rating>();
        }
        public Rating Get(int id)
        {
            return _context.Rating.Where(x => x.RatingID == id && x.Deleted == false).SingleOrDefault();
        }
        public void Add(Rating rating)
        {
            try
            {
                rating.Deleted = false;
                _context.Entry(rating).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
        }
        public void Update(Rating rating)
        {
            try
            {
                _context.Entry(rating).State = EntityState.Modified;
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
            Rating deletedRating = _context.Rating.SingleOrDefault(x => x.RatingID == id);
            if (deletedRating != null)
            {
                deletedRating.Deleted = true;
                _context.Entry(deletedRating).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

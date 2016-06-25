﻿using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaService.Data.Repositories
{
    public interface IClubRepository : IRepository<Club>
    {
        Club GetClubByID(int id);
    }
    public class ClubRepository : RepositoryBase<Club>, IClubRepository
    {
        public ClubRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Club GetClubByID(int id)
        {
            return DbContext.Club.Where(m => m.ClubID == id && m.Deleted == false).SingleOrDefault();
        }
        public override IEnumerable<Club> GetAll()
        {
            return DbContext.Club.Where(a => a.Deleted == false).ToList();
        }
        public override void Delete(Club entity)
        {
            entity.Deleted = true;
            Update(entity);
        }
    }
}

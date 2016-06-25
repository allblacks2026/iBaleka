using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;

namespace iBalekaService.Data.Repositories
{
    public interface IClubMemberRepository:IRepository<Club_Athlete>
    {
        Club_Athlete GetMemberByID(int id);
    }
    public class ClubMemberRepository:RepositoryBase<Club_Athlete>,IClubMemberRepository
    {
        public ClubMemberRepository(IDbFactory dbFactory)
            : base(dbFactory) { }

        public Club_Athlete GetMemberByID(int id)
        {
            return DbContext.ClubMember.Where(m => m.MemberID == id && m.IsaMember == true).SingleOrDefault();
        }
        public override IEnumerable<Club_Athlete> GetAll()
        {
            return DbContext.ClubMember.Where(a => a.IsaMember == true).ToList();
        }
        public override void Delete(Club_Athlete entity)
        {
            entity.IsaMember = false;
            Update(entity);
        }
    }
}

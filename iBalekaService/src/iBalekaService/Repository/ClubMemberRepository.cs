using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace iBalekaService.Repository
{
    public class ClubMemberRepository:IiBalekaRepository<Club_Athlete>
    {
        protected IBalekaContext _context { get; set; }

        public ClubMemberRepository(IBalekaContext _context)
        {
            this._context = _context;
        }
        public IEnumerable<Club_Athlete> GetAll()
        {
            return _context.ClubMember.Where(x => x.IsaMember == true).ToList();
        }
        public Club_Athlete Get(int id)
        {
            return _context.ClubMember.Where(x => x.MemberID == id && x.IsaMember == true).SingleOrDefault();
        }
        public void Add(Club_Athlete member)
        {
            try
            {
                member.IsaMember = true;
                _context.Entry(member).State = EntityState.Added;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                //research entity framework exceptions
                throw;
            }
            
        }
        public void Update(Club_Athlete member)
        {
            try
            {
                _context.Entry(member).State = EntityState.Modified;
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
            Club_Athlete deletedMember = _context.ClubMember.SingleOrDefault(x => x.MemberID == id);
            if (deletedMember != null)
            {
                deletedMember.IsaMember = false;
                _context.Entry(deletedMember).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

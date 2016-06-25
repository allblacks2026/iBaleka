using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using iBalekaService.Data.Repositories;

namespace iBalekaService.Services
{
    public interface IClubMemberService
    {
        Club_Athlete GetMemberByID(int id);
        IEnumerable<Club_Athlete> GetAll();
        void AddMember(Club_Athlete member);
        void UpdateMember(Club_Athlete member);
        void Delete(Club_Athlete member);
        void SaveMember();

    }
    public class ClubMemberService:IClubMemberService
    {
        private readonly IClubMemberRepository _routeRepo;
        private readonly IUnitOfWork unitOfWork;

        public ClubMemberService(IClubMemberRepository _repo,IUnitOfWork _unitOfWork)
        {
            _routeRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        
        public Club_Athlete GetMemberByID(int id)
        {
            return _routeRepo.GetMemberByID(id);
        }
        public IEnumerable<Club_Athlete> GetAll()
        {
            return _routeRepo.GetAll();
        }
        public void AddMember(Club_Athlete member)
        {
            _routeRepo.Add(member);
        }
        public void UpdateMember(Club_Athlete member)
        {
            _routeRepo.Update(member);
        }
        public void Delete(Club_Athlete member)
        {
            _routeRepo.Delete(member);
        }
        public void SaveMember()
        {
            unitOfWork.Commit();
        }
    }
}

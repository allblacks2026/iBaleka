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
    public interface IEventService
    {
        void AddEventRoute(Event_Route route);
        Event GetEventByID(int id);
        IEnumerable<Event_Route> GetEventRoute(int id);
        IEnumerable<Event> GetAll();
        void AddEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void Delete(Event evnt);
        void SaveEvent();
    }
    public class EventService:IEventService
    {
        private readonly IEventRepository _eventRepo;
        private readonly IUnitOfWork unitOfWork;

        public EventService(IEventRepository _repo,IUnitOfWork _unitOfWork)
        {
            _eventRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        public Event GetEventByID(int id)
        {
            return _eventRepo.GetEventByID(id);
        }
        public IEnumerable<Event> GetAll()
        {
            return _eventRepo.GetAll();
        }
        public IEnumerable<Event_Route> GetEventRoute(int id)
        {
            return _eventRepo.GetEventRoute(id);
        }
        public void AddEventRoute(Event_Route route)
        {
            _eventRepo.AddEventRoute(route);
        }
        public void AddEvent(Event evnt)
        {
            _eventRepo.Add(evnt);
        }
        public void UpdateEvent(Event evnt)
        {
            _eventRepo.Update(evnt);
        }
        public void Delete(Event evnt)
        {
            _eventRepo.Delete(evnt);
        }
        public void SaveEvent()
        {
            unitOfWork.Commit();
        }
    }
}

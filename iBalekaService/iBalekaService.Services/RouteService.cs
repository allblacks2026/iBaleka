﻿using System;
using System.Collections.Generic;
using System.Linq;
using iBalekaService.Data.Infastructure;
using iBalekaService.Domain.Models;
using System.Threading.Tasks;
using iBalekaService.Data.Repositories;

namespace iBalekaService.Services
{
    public interface IRouteService
    {
        Route GetRouteByID(int id);
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        IEnumerable<Route> GetRoutes();
        void AddRoute(Route route, Checkpoint[] checkpoints);
        void UpdateRoute(Route route, Checkpoint[] checkpoints);
        void DeleteRoute(Route route);
        void SaveRoute();
    }

    public class RouteService:IRouteService
    {
        private readonly IRouteRepository _routeRepo;
        private readonly IUnitOfWork unitOfWork;

        public RouteService(IRouteRepository _repo, IUnitOfWork _unitOfWork)
        {
            _routeRepo = _repo;
            unitOfWork = _unitOfWork;
        }
        public IEnumerable<Checkpoint> GetCheckpoints(int id)
        {
            return _routeRepo.GetCheckpoints(id);
        }
        public IEnumerable<Route> GetRoutes()
        {
            return _routeRepo.GetAll();
        }
        public Route GetRouteByID(int id)
        {
            return _routeRepo.GetRouteByID(id);
        }
        public void AddRoute(Route route,Checkpoint[] Checkpoints)
        {
            _routeRepo.AddRoute(route, Checkpoints);
        }
        public void UpdateRoute(Route route, Checkpoint[] Checkpoints)
        {
            _routeRepo.UpdateRoute(route, Checkpoints);
        }
        public void DeleteRoute(Route route)
        {
            _routeRepo.Delete(route);
        }
        public void SaveRoute()
        {
            unitOfWork.Commit();
        }
    }
}


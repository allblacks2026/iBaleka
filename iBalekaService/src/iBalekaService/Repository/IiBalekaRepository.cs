using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iBalekaService.Repository
{
    public interface IiBalekaRepository<T>:IDisposable where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Add(T obj);
        void Update(T obj);
        //flags entity
        void Delete(int id);
    }
    public interface IRouteRepository<Route,Checkpoint> : IDisposable where Checkpoint:class
    {
        IEnumerable<Route> GetRoutes();
        IEnumerable<Checkpoint> GetCheckpoints(int id);
        Route GetRoute(int id);
        void AddRoute(Route route,Checkpoint[] checkpoints);
        void Update(Route route, Checkpoint[] checkpoints);
        //flags entity
        void Delete(int id);
        void DeleteCheckPoints(IEnumerable<Checkpoint> checkpoints);
    }
    public interface IRunRepository<Run>:IDisposable where Run:class
    {
        IEnumerable<Run> GetAll(int id);
        IEnumerable<Run> GetEventRuns(int id);
        IEnumerable<Run> GetPersonalRuns(int id);
        Run Get(int id);
        void Add(Run run);
        void Delete(int id);
    }



}

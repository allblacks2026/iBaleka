using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaService.Models;
using iBalekaService.Repository;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class RouteController : Controller
    {
        private IRouteRepository<Route, Checkpoint> _routeRepo;
        public RouteController(IRouteRepository<Route,Checkpoint> _repo)
        {
            _routeRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetRoutes()
        {
            return new JsonResult(_routeRepo.GetRoutes());
        }
        public IActionResult GetCheckPoints(int id)
        {
            Route route = _routeRepo.GetRoute(id);
            if (route == null)
            {
                return NotFound();
            }
            return new JsonResult(_routeRepo.GetCheckpoints(id));
        } 

        // GET api/values/5
        [HttpGet("{id}",Name ="GetRoute")]
        public IActionResult GetRoute(int id)
        {
            Route route = _routeRepo.GetRoute(id);
            if(route==null)
            {
                return NotFound();
            }
            return new JsonResult(route);
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddRoute([FromBody]Route route, [FromBody]Checkpoint[] checkpoints)
        {
            if(ModelState.IsValid)
            {
                _routeRepo.AddRoute(route, checkpoints);
                return CreatedAtRoute("GetRoute", new { Controller = "Route", id = route.RouteID }, route);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT 
        [HttpPut]
        public IActionResult Update([FromBody]Route route, [FromBody]Checkpoint[] checkpoints)
        {
            if (ModelState.IsValid)
            {
                Route existingRoute = _routeRepo.GetRoute(route.RouteID);
                if (existingRoute == null)
                {
                    return NotFound();
                }
                _routeRepo.Update(route, checkpoints);
                return new NoContentResult(); 
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _routeRepo.Delete(id);
        }
    }
}

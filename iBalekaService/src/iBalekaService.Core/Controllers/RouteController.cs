using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaService.Domain.Models;
using iBalekaService.Services;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaService.Core.Controllers
{
    [Route("api/[controller]")]
    public class RouteController : Controller
    {
        private IRouteService _routeRepo;
        
        public RouteController(IRouteService _repo)
        {
            _routeRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetRoutes()
        {
            return new JsonResult(_routeRepo.GetRoutes());
        }
        [HttpGet("{id}")]
        public IActionResult GetCheckPoints(int id)
        {
            Route route = _routeRepo.GetRouteByID(id);
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
            Route route = _routeRepo.GetRouteByID(id);
            if(route==null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(route));
        }

        // POST api/values
        [HttpPost]
        public IActionResult AddRoute([FromBody]Route route, [FromBody]Checkpoint[] checkpoints)
        {
            if(ModelState.IsValid)
            {
                _routeRepo.AddRoute(route, checkpoints);
                _routeRepo.SaveRoute();
                return CreatedAtRoute("GetRoute", new { Controller = "Route", id = route.RouteID }, route);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT 
        [HttpPut]
        public IActionResult Update([FromBody]Route route, [FromBody]Checkpoint[] checkpoints)
        {
            if (ModelState.IsValid)
            {               
                _routeRepo.UpdateRoute(route, checkpoints);
                try
                {
                    _routeRepo.SaveRoute();
                    return new NoContentResult();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.RouteID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody]Route route)
        {
            _routeRepo.DeleteRoute(route);
            try
            {
                _routeRepo.SaveRoute();
                return Ok(new JsonResult(route));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RouteExists(route.RouteID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }
        private bool RouteExists(int id)
        {
            return _routeRepo.GetRouteByID(id) != null;
        }

    }
}

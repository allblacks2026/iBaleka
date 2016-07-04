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
    public class EventController : Controller
    {
        private IEventService _eventRepo;
        public EventController(IEventService _repo)
        {
            _eventRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_eventRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}",Name ="GetEvent")]
        public IActionResult GetEvent(int id)
        {
            Event evnt = _eventRepo.GetEventByID(id);
            if(evnt==null)
            {
                return NotFound();
            }

            return Ok(new JsonResult(evnt));
        }

        public IActionResult GetEventRoute(int id)
        {
            if (EventExists(id))
            {
                IEnumerable<Event_Route> evntRoutes = _eventRepo.GetEventRoute(id);
                if(evntRoutes==null)
                {
                    return NotFound();
                }
                return new JsonResult(_eventRepo.GetEventRoute(id));
            }
            else
                return NotFound();
        }
        // POST api/values
 

        // PUT api/values/5
       
        [HttpPost]
        public IActionResult AddEventRoute([FromBody]Event_Route evnt_route)
        {
            if (ModelState.IsValid)
            {
                _eventRepo.AddEventRoute(evnt_route);
                _eventRepo.SaveEvent();
                return CreatedAtRoute("GetEventRoute", new { Controller = "Event", id = evnt_route.EventID}, evnt_route);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        // PUT api/values/5
        [HttpPut]
        public IActionResult UpdateEvent([FromBody]Event evnt)

        {
            if(ModelState.IsValid)
            {
                
                _eventRepo.UpdateEvent(evnt);
                try
                {
                    _eventRepo.SaveEvent();
                    return new NoContentResult();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!EventExists(evnt.EventID))
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
        [HttpDelete]
        public IActionResult Delete([FromBody]Event evnt)
        {
            _eventRepo.Delete(evnt);
            try
            {
                _eventRepo.SaveEvent();
                return Ok(new JsonResult(evnt));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(evnt.EventID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }
        private bool EventExists(int id)
        {
            return _eventRepo.GetEventByID(id) != null;
        }
    }
}

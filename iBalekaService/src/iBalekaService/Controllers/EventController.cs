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
    public class EventController : Controller
    {
        private IiBalekaRepository<Event> _eventRepo;
        public EventController(IiBalekaRepository<Event> _repo)
        {
            _eventRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_eventRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}",Name ="GetEvent")]
        public IActionResult Get(int id)
        {
            Event evnt = _eventRepo.Get(id);
            if(evnt==null)
            {
                return NotFound();
            }

            return new JsonResult(evnt);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Event evnt)
        {
            if(ModelState.IsValid)
            {
                _eventRepo.Add(evnt);
                return CreatedAtRoute("GetEvent", new { Controller = "Event", id = evnt.EventID }, evnt);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Event evnt)
        {
            if(ModelState.IsValid)
            {
                Event existingEvent = _eventRepo.Get(evnt.EventID);
                if(existingEvent==null)
                {
                    return NotFound();
                }
                _eventRepo.Update(evnt);
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
            _eventRepo.Delete(id);
        }
    }
}

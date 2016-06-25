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
    public class EventRegistrationController : Controller
    {
        private IEventRegService _eventRegRepo;
        public EventRegistrationController(IEventRegService _repo)
        {
            _eventRegRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_eventRegRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}",Name ="GetEventRegistration")]
        public IActionResult GetEventRegistration(int id)
        {
            EventRegistration existingReg = _eventRegRepo.GetEventRegByID(id);
            if(existingReg==null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(existingReg));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]EventRegistration evntReg)
        {
            if(ModelState.IsValid)
            {
                _eventRegRepo.AddEventRegistration(evntReg);
                _eventRegRepo.SaveEventRegistration();
                return CreatedAtRoute("GetEventRegistration", new { Controller = "EventRegistration", id = evntReg.RegistrationID }, evntReg);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]EventRegistration evntReg)
        {
            if(ModelState.IsValid)
            {
                _eventRegRepo.UpdateEventRegistration(evntReg);
                try
                {
                    _eventRegRepo.SaveEventRegistration();
                    return new NoContentResult();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!RegExist(evntReg.RegistrationID))
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
        public IActionResult Delete([FromBody]EventRegistration evntReg)
        {
            _eventRegRepo.Delete(evntReg);
            try
            {
                _eventRegRepo.SaveEventRegistration();
                return Ok(new JsonResult(evntReg));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegExist(evntReg.RegistrationID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }
        private bool RegExist(int id)
        {
            return _eventRegRepo.GetEventRegByID(id)!=null;
        }
    }
}

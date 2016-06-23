﻿using System;
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
    public class EventRegistrationController : Controller
    {
        private IiBalekaRepository<EventRegistration> _eventRegRepo;
        public EventRegistrationController(IiBalekaRepository<EventRegistration> _repo)
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
            EventRegistration existingReg = _eventRegRepo.Get(id);
            if(existingReg==null)
            {
                return NotFound();
            }
            return new JsonResult(existingReg);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]EventRegistration evntReg)
        {
            if(ModelState.IsValid)
            {
                _eventRegRepo.Add(evntReg);
                return CreatedAtRoute("GetEventRegistration", new { Controller = "EventRegistration", id = evntReg.RegistrationID }, evntReg);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Update([FromBody]EventRegistration evntReg)
        {
            if(ModelState.IsValid)
            {
                EventRegistration eventReg = _eventRegRepo.Get(evntReg.RegistrationID);
                if(eventReg==null)
                {
                    return NotFound();
                }
                _eventRegRepo.Update(evntReg);
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
            _eventRegRepo.Delete(id);
        }
    }
}

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
    public class AthleteController : Controller
    {
        private IiBalekaRepository<Athlete> _athleteRepo;
        public AthleteController(IiBalekaRepository<Athlete> _repo)
        {
            _athleteRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_athleteRepo.GetAll());
            
        }

        // GET api/values/5
        [HttpGet("{id}",Name="GetAthlete")]
        public IActionResult Get(int id)
        {
            Athlete athlete = _athleteRepo.Get(id);
            if(athlete == null)
            {
                return NotFound();
            }
            return new JsonResult(athlete);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Athlete athlete)
        {
            if(ModelState.IsValid)
            {
                _athleteRepo.Add(athlete);
                return CreatedAtRoute("GetAthlete", new { Controller = "Athlete", id = athlete.AthleteID, athlete });
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Athlete athlete)
        {
            if(ModelState.IsValid)
            {
                Athlete existingAthlete = _athleteRepo.Get(athlete.AthleteID);
                if(existingAthlete==null)
                {
                    return NotFound();
                }
                _athleteRepo.Update(athlete);
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
            _athleteRepo.Delete(id);
        }
        
    }
}

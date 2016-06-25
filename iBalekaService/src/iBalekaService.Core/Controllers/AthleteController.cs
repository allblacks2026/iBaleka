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
    public class AthleteController : Controller
    {
        private IAthleteService _athleteRepo;
        public AthleteController(IAthleteService _repo)
        {
            _athleteRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_athleteRepo.GetAthletes());
            
        }

        // GET api/values/5
        [HttpGet("{id}",Name="GetAthlete")]
        public IActionResult GetAthlete(int id)
        {
            Athlete athlete = _athleteRepo.GetAthlete(id);
            if(athlete == null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(athlete));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Athlete athlete)
        {
            if(ModelState.IsValid)
            {
                _athleteRepo.AddAthlete(athlete);
                _athleteRepo.SaveAthlete();
                return CreatedAtRoute("GetAthlete", new { Controller = "Athlete", id = athlete.AthleteID, athlete });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Athlete athlete)
        {
            if(ModelState.IsValid)
            {
                _athleteRepo.UpdateAthlete(athlete);
                try
                {

                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!AthleteExists(athlete.AthleteID))
                    {
                        return NotFound();
                    }
                    else
                        throw;
                }
                return new NoContentResult();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public IActionResult Delete([FromBody]Athlete athlete)
        {
            _athleteRepo.DeleteAthlete(athlete);
            try
            {
                _athleteRepo.SaveAthlete();
                return Ok(new JsonResult(athlete));
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!AthleteExists(athlete.AthleteID))
                {
                    return NotFound();
                }
                else
                    throw;
            }
        }
        private bool AthleteExists(int id)
        {
            return _athleteRepo.GetAthlete(id) != null;
        }
    }
}

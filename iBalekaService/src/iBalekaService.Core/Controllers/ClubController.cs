using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaService.Domain.Models;
using iBalekaService.Services;
using Microsoft.EntityFrameworkCore;

namespace iBalekaService.Core.Controllers
{
    [Route("api/[controller]")]
    public class ClubController : Controller
    {
        private IClubService _clubRepo;
        public ClubController(IClubService _repo)
        {
            _clubRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_clubRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}",Name = "GetClub")]
        public IActionResult GetClub(int id)
        {
            Club club = _clubRepo.GetClubByID(id);
            if(club==null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(club));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Club club)
        {
            if(ModelState.IsValid)
            {
                _clubRepo.AddClub(club);
                _clubRepo.SaveClub();
                return CreatedAtRoute("GetClub", new { Controller = "Club", id = club.ClubID }, club);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Club club)
        {
            if(ModelState.IsValid)
            {
                _clubRepo.UpdateClub(club);
                try
                {
                    _clubRepo.SaveClub();
                    return new NoContentResult();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!ClubExists(club.ClubID))
                    {
                        return NotFound();
                    }
                    else
                        throw;

                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromBody]Club club)
        {
            _clubRepo.Delete(club);
            try
            {
                _clubRepo.SaveClub();
                return Ok(new JsonResult(club));
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!ClubExists(club.ClubID))
                {
                    return NotFound();
                }
                else
                    throw;
            }
        }
        private bool ClubExists(int id)
        {
            return _clubRepo.GetClubByID(id) != null;
        }
    }
}

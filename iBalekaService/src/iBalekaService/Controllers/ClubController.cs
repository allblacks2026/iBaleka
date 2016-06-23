using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaService.Models;
using iBalekaService.Repository;

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class ClubController : Controller
    {
        private IiBalekaRepository<Club> _clubRepo;
        public ClubController(IiBalekaRepository<Club> _repo)
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
            Club club = _clubRepo.Get(id);
            if(club==null)
            {
                return NotFound();
            }
            return new JsonResult(club);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Club club)
        {
            if(ModelState.IsValid)
            {
                _clubRepo.Add(club);
                return CreatedAtRoute("GetClub", new { Controller = "Club", id = club.ClubID }, club);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Club club)
        {
            if(ModelState.IsValid)
            {
                Club existingClub = _clubRepo.Get(club.ClubID);
                if(existingClub==null)
                {
                    return NotFound();
                }
                _clubRepo.Update(club);
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
            _clubRepo.Delete(id);
        }
    }
}

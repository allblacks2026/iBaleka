using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBalekaService.Models;
using iBalekaService.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class ClubMemberController : Controller
    {
        private IiBalekaRepository<Club_Athlete> _clubMemberRepo;
        public ClubMemberController(IiBalekaRepository<Club_Athlete> _repo)
        {
            _clubMemberRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_clubMemberRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetMember")]
        public IActionResult GetMember(int id)
        {
            Club_Athlete member = _clubMemberRepo.Get(id);
            if (member == null)
            {
                return NotFound();
            }
            return new JsonResult(member);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Club_Athlete member)
        {
            if (ModelState.IsValid)
            {
                _clubMemberRepo.Add(member);
                return CreatedAtRoute("GetMember", new { Controller = "ClubMember", id = member.MemberID }, member);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Club_Athlete member)
        {
            if (ModelState.IsValid)
            {
                Club_Athlete existingMember = _clubMemberRepo.Get(member.MemberID);
                if (existingMember == null)
                {
                    return NotFound();
                }
                _clubMemberRepo.Update(member);
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
            _clubMemberRepo.Delete(id);
        }
    }
}

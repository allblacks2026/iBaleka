using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBalekaService.Domain.Models;
using iBalekaService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class ClubMemberController : Controller
    {
        private IClubMemberService _clubMemberRepo;
        public ClubMemberController(IClubMemberService _repo)
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
            Club_Athlete member = _clubMemberRepo.GetMemberByID(id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(member));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Club_Athlete member)
        {
            if (ModelState.IsValid)
            {
                _clubMemberRepo.AddMember(member);
                _clubMemberRepo.SaveMember();
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
                _clubMemberRepo.UpdateMember(member);
                try
                {
                    _clubMemberRepo.SaveMember();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberID) != null)
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
                return BadRequest();
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public IActionResult Delete([FromBody]Club_Athlete member)
        {
            _clubMemberRepo.Delete(member);
            try
            {
                _clubMemberRepo.SaveMember();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(member.MemberID))
                {
                    return NotFound();
                }
                else
                    throw;
            }
            return Ok(new JsonResult(member));
        }
        private bool MemberExists(int id)
        {
            return _clubMemberRepo.GetMemberByID(id) != null;
        }
    }
}

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
    public class UserController : Controller
    {
        private IiBalekaRepository<User> _userRepo;
        public UserController(IiBalekaRepository<User> _repo)
        {
            _userRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_userRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}",Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            User user = _userRepo.Get(id);
            if(user==null)
            {
                return NotFound();
            }
            return new JsonResult(user);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]User user)
        {
            if(ModelState.IsValid)
            {
                _userRepo.Add(user);
                return CreatedAtRoute("GetUser", new { Controller = "User", id = user.UserID }, user);
            }
            else
            {
                return BadRequest();
            }

            
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                User existingUser = _userRepo.Get(user.UserID);
                if(existingUser==null)
                {
                    return NotFound();
                }
                _userRepo.Update(user);
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
            _userRepo.Delete(id);
        }
    }
}

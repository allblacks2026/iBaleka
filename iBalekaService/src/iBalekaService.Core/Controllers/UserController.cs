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
    public class UserController : Controller
    {
        private IUserService _userRepo;
        public UserController(IUserService _repo)
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
        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetUser(int id)
        {
            User user = _userRepo.GetUserByID(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(user));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.AddUser(user);
                _userRepo.SaveUser();
                return CreatedAtRoute("GetUser", new { Controller = "User", id = user.UserID }, user);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]User user)
        {
            if (ModelState.IsValid)
            {
                _userRepo.UpdateUser(user);
                try
                {
                    _userRepo.SaveUser();
                    return new NoContentResult();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
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
        public IActionResult Delete([FromBody]User user)
        {
            _userRepo.Delete(user);
            try
            {
                _userRepo.SaveUser();
                return Ok(new JsonResult(user));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.UserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }

        
        private bool UserExists(int id)
        {
            return _userRepo.GetUserByID(id) != null;
        }
    }
}

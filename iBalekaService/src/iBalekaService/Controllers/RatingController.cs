using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBalekaService.Models;
using iBalekaService.Repository;
using Microsoft.AspNetCore.Mvc;

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class RatingController:Controller
    {
        private IiBalekaRepository<Rating> _ratingRepo;
        public RatingController(IiBalekaRepository<Rating> _repo)
        {
            _ratingRepo = _repo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult GetAll()
        {
            return new JsonResult(_ratingRepo.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetRating")]
        public IActionResult GetUser(int id)
        {
            Rating rating = _ratingRepo.Get(id);
            if (rating == null)
            {
                return NotFound();
            }
            return new JsonResult(rating);
        }
        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Rating rating)
        {
            if (ModelState.IsValid)
            {
                Rating existingRating = _ratingRepo.Get(rating.RatingID);
                if (existingRating == null)
                {
                    return NotFound();
                }
                _ratingRepo.Update(rating);
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
            _ratingRepo.Delete(id);
        }
    }
}

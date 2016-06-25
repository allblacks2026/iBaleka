using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iBalekaService.Domain.Models;
using iBalekaService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class RatingController:Controller
    {
        private IRatingService _ratingRepo;
        public RatingController(IRatingService _repo)
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
        public IActionResult GetRating(int id)
        {
            Rating rating = _ratingRepo.GetRatingByID(id);
            if (rating == null)
            {
                return NotFound();
            }
            return Ok(new JsonResult(rating));
        }
        // PUT api/values/5
        [HttpPut]
        public IActionResult Update([FromBody]Rating rating)
        {
            if (ModelState.IsValid)
            {
                _ratingRepo.UpdateRating(rating);
                try
                {
                    _ratingRepo.SaveRating();
                    return new NoContentResult();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingID))
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
        [HttpDelete]
        public IActionResult Delete([FromBody]Rating rating)
        {
            if (ModelState.IsValid)
            {
                _ratingRepo.DeleteRating(rating);
                try
                {
                    _ratingRepo.SaveRating();
                    return Ok(new JsonResult(rating));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.RatingID))
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
        private bool RatingExists(int id)
        {
            return _ratingRepo.GetRatingByID(id) != null;
        }
    }
}

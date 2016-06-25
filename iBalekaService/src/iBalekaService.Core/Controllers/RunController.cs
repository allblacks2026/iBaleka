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
    public class RunController : Controller
    {
        private IRunService _runRepo;
        private IRatingService _ratingRepo;
        public RunController(IRunService runRepo,IRatingService ratingRepo)
        {
            _runRepo = runRepo;
            _ratingRepo = ratingRepo;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            return new JsonResult(_runRepo.GetAllRuns(id));
        }
        // GET api/values/5
        [HttpGet("{id}",Name ="GetRun")]
        public IActionResult GetRun(int id)
        {
            Run run = _runRepo.GetRunByID(id);
            if(run==null)
            {
                return NotFound();
            }

            return Ok(new JsonResult(run));
        }
        [HttpGet("{id}")]
        public IActionResult GetEventRuns(int id)
        {
            return new JsonResult(_runRepo.GetEventRuns(id));
        }
        [HttpGet("{id}")]
        public IActionResult GetPersonalRuns(int id)
        {
            return new JsonResult(_runRepo.GetPersonalRuns(id));
        }
        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Run run,[FromBody]Rating rating)
        {
            if(ModelState.IsValid)
            {
                _runRepo.AddRun(run);
                _runRepo.SaveRun();
                _ratingRepo.AddRating(rating);
                _ratingRepo.SaveRating();
                return CreatedAtRoute("GetRun", new { Controller = "Run", run.RunID }, run);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // DELETE api/values/5
        [HttpDelete]
        public IActionResult Delete([FromBody]Run run,[FromBody]Rating rating)
        {
            _runRepo.Delete(run);
            _ratingRepo.DeleteRating(rating);
            try
            {
                _runRepo.SaveRun();
                _ratingRepo.SaveRating();
                return Ok(new JsonResult(run));
            }
            catch(DbUpdateConcurrencyException)
            {
                if(!RunExists(run.RunID) || !RatingExists(rating.RatingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            
        }
        private bool RunExists(int id)
        {
            return _runRepo.GetRunByID(id) != null;
        }
        private bool RatingExists(int id)
        {
            return _ratingRepo.GetRatingByID(id) != null;
        }
    }
}

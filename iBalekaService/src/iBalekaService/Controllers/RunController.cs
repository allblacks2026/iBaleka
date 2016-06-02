using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using iBalekaService.Models;
using iBalekaService.Repository;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace iBalekaService.Controllers
{
    [Route("api/[controller]")]
    public class RunController : Controller
    {
        private IRunRepository<Run> _runRepo;
        public RunController(IRunRepository<Run> _repo)
        {
            _runRepo = _repo;
        }
        // GET: api/values
        [HttpGet("{id}")]
        public IActionResult GetAll(int id)
        {
            return new JsonResult(_runRepo.GetAll(id));
        }
        // GET api/values/5
        [HttpGet("{id}",Name ="GetRun")]
        public IActionResult Get(int id)
        {
            Run run = _runRepo.Get(id);
            if(run==null)
            {
                return NotFound();
            }
            return new JsonResult(run);
        }
        [HttpGet]
        public IActionResult GetEventRuns(int id)
        {
            return new JsonResult(_runRepo.GetEventRuns(id));
        }
        [HttpGet]
        public IActionResult GetPersonalRuns(int id)
        {
            return new JsonResult(_runRepo.GetPersonalRuns(id));
        }
        // POST api/values
        [HttpPost]
        public IActionResult Add([FromBody]Run run)
        {
            if(ModelState.IsValid)
            {
                _runRepo.Add(run);
                return CreatedAtRoute("GetRun", new { Controller = "Run", run.RunID }, run);
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
            _runRepo.Delete(id);
        }
    }
}

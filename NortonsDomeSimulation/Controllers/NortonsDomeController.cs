using Microsoft.AspNetCore.Mvc;
using NortonsDomeSimulation.Models;
using NortonsDomeSimulation.Services;

namespace NortonsDomeSimulation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NortonsDomeController : ControllerBase
    {
        private readonly INortonDomeService _domeService;

        public NortonsDomeController(INortonDomeService domeService)
        {
            _domeService = domeService;
        }

        [HttpGet("simulate")]
        public ActionResult<IEnumerable<ParticleState>> Simulate([FromQuery] double duration, [FromQuery] double step)
        {
            var states = _domeService.SimulateTrajectory(duration, step);
            return Ok(states);
        }
    }
}
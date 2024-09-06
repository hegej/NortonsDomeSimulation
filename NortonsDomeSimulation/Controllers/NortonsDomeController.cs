using Microsoft.AspNetCore.Mvc;
using NortonsDomeSimulation.Models;
using NortonsDomeSimulation.Services;
using Serilog;

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
            if (duration <= 0 || step <= 0)
            {
                Log.Warning("Invalid simulation parameters: Duration={Duration}, Step={Step}", duration, step);
                return BadRequest("Duration and step size must be positive numbers.");
            }

            try
            {
                var states = _domeService.SimulateTrajectory(duration, step);
                if (!states.Any())
                {
                    Log.Information("Simulation ran but returned no data. Duration={Duration}, Step={Step}", duration, step);
                    return NoContent();
                }
                Log.Information("Simulation successfully completed. Duration={Duration}, Step={Step}", duration, step);
                return Ok(states);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while running the simulation. Duration={Duration}, Step={Step}", duration, step);
                return StatusCode(500, "Internal server error. Please try again.");
            }
        }
    }
}
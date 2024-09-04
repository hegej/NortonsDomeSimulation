using NortonsDomeSimulation.Models;

namespace NortonsDomeSimulation.Services
{
    public interface INortonDomeService
    {
        IEnumerable<ParticleState> SimulateTrajectory(double duration, double step);
    }
}

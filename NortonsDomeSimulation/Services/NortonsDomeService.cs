using NortonsDomeSimulation.Models;

namespace NortonsDomeSimulation.Services
{
    public class NortonsDomeService : INortonDomeService
    {
        public IEnumerable<ParticleState> SimulateTrajectory(double duration, double step)
        {
            for (double t = 0; t <= duration; t += step)
            {
                double x = CalculatePosition(t);
                double y = CalculateHight(x);
                yield return new ParticleState(t, x, y);
            }
        }

        private static double CalculatePosition(double t) => Math.Pow(t, 4) / 144;
        private static double CalculateHight(double x) => Math.Pow(x / 3, 0.75);
    }
}


namespace NortonsDomeSimulation.Models
{
    public record ParticleState(double Time, double Position, double Height)
    {
        public double Velocity => 4 * Math.Pow(Time, 3) / 144; // the derivative of position
    }
}

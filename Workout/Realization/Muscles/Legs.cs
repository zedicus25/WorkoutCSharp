using System.Collections.Generic;

namespace Workout
{
    public class Legs : Muscle
    {
        public Legs(List<Exercise> exercises) : base("Legs", exercises)
        {
        }
    }
}
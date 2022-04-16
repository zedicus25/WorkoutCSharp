using System;
using System.Collections.Generic;

namespace Workout
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkoutManager wm = new WorkoutManager();
            Chest chest = new Chest();
            Legs legs = new Legs();
            legs.AddExercise(new Squat(10,2));
            chest.AddExercise(new DumbbellLayout(10,3));
            chest.AddExercise(new BenchPress(15,2));
            wm.AddDay(new List<Muscle>()
            {
                chest
            });
            wm.AddDay(new DateTime(2022,4,18), new List<Muscle>()
            {
                legs
            });
            //wm.UpdateDay(DateTime.Now);
            wm.RemoveDay(DateTime.Now);
            Console.WriteLine(wm);
        }
    }
}

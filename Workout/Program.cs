using System;
using System.Collections.Generic;

namespace Workout
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkoutManager wm = new WorkoutManager();
           /* Chest chest = new Chest(new List<Exercise>()
            {
                new DumbbellLayout(10,3),
                new BenchPress(15,2)
            }); 
            Legs legs = new Legs(new List<Exercise>()
            {
                new Squat(10,2)
            });
            wm.AddDay(new List<Muscle>()
            {
                chest
            });
            wm.AddDay(new DateTime(2022,4,18), new List<Muscle>()
            {
                legs
            });
            wm.SaveToFile();
            //wm.UpdateDay(DateTime.Now);
            //wm.RemoveDay(DateTime.Now);
            //wm.AddDayExercise(DateTime.Now);*/
            wm.LoadFromFile();
            Console.WriteLine(wm);
        }
    }
}

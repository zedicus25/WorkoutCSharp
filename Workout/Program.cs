using System;
using System.Collections.Generic;

namespace Workout
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Trainer trainer = new Trainer();
            trainer.GetTodayWorkout();
            Console.WriteLine();
            trainer.GetWorkoutByDate(new DateTime(2022,4,17));
            Console.WriteLine();
            trainer.GetAllWorkout();
        }
    }
}

using System;

namespace Workout
{
    public class Trainer
    {
        private WorkoutManager _workoutManager;
        
        public Trainer()
        {
            _workoutManager = new WorkoutManager();
        }

        public void GetTodayWorkout()
        {
            _workoutManager.GetTodayWorkout();
        }

        public void GetWorkoutByDate(DateTime date)
        {
            _workoutManager.GetWorkout(date);
        }

        public void GetAllWorkout()
        {
            Console.WriteLine(_workoutManager);
        }
    }
}
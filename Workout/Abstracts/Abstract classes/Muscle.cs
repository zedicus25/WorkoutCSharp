using System;
using System.Collections.Generic;

namespace Workout
{
    public abstract class Muscle
    {
        public string Name { get; private set; }
        private List<Exercise> _exercises;

        protected Muscle(string name)
        {
            Name = name;
            _exercises = new List<Exercise>();
        }

        public void AddExercise(Exercise exercise)
        {
            if (IsHas(exercise.GetName()) == false)
            {
                Console.WriteLine("This exercise has today");
                return;
            }
            
            _exercises.Add(exercise);
        }

        public void DeleteExercise(Exercise exercise)
        {
            DeleteExercise(exercise.GetName());
        }
        public void DeleteExercise(string exerciseName)
        {
            if (IsHas(exerciseName) == false)
            {
                Console.WriteLine("Dont has in list");
                return;
            }

            Console.WriteLine(_exercises.Remove(GetExerciseByName(exerciseName)) ? "Successful" : "Error");
        }

        public void UpdateExercise(Exercise exercise)
        {
            UpdateExercise(exercise.GetName());
        }
        public void UpdateExercise(string exerciseName)
        {
            if (IsHas(exerciseName) == false)
            {
                Console.WriteLine("Dont has in list");
                return;
            }

            int ind = _exercises.IndexOf(GetExerciseByName(exerciseName));
            
            Console.WriteLine("If you dont need to change reps or sets, press enter");
            Console.WriteLine("Enter new sets");
            string str = Console.ReadLine();
            if (int.TryParse(str, out var sets) == false)
            {
                Console.WriteLine("Incorrect input");
                Console.WriteLine("Press any key, to exit");
            }
            
            Console.WriteLine("Enter new sets");
            str = Console.ReadLine();
            if (int.TryParse(str, out var reps) == false)
            {
                Console.WriteLine("Incorrect input");
                Console.WriteLine("Press any key, to exit");
            }
            
            _exercises[ind].SetSets(sets);
            _exercises[ind].SetReps(reps);
        }

        private bool IsHas(string name)
        {
            foreach (var item in _exercises)
            {
                if (item.GetName().Equals(name))
                    return true;
            }

            return false;
        }
        private Exercise GetExerciseByName(string name)
        {
            foreach (var item in _exercises)
            {
                if (item.GetName().Equals(name))
                    return item;
            }

            return null;
        }
    }
}
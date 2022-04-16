using System;
using System.Collections.Generic;
using System.Text;

namespace Workout
{
    public abstract class Muscle
    {
        public string Name { get; private set; }
        private List<Exercise> _exercises;

        protected Muscle(string name)
        {
            Name = name.ToLower();
            _exercises = new List<Exercise>();
        }

        public void AddExercise(Exercise exercise)
        {
            if (IsHas(exercise.GetName()))
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
            exerciseName = exerciseName.ToLower();
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name);
            foreach (var item in _exercises)
            {
                sb.AppendLine($"- {item}");
            }

            sb.AppendLine(new string('-', 20));
            return sb.ToString();
        }

        public Exercise GetExercise(int ind)
        {
            if (ind >= 0 && ind < _exercises.Count)
                return _exercises[ind];
            return null;
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
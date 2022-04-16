using System;
using System.Collections.Generic;

namespace Workout
{
    public class WorkoutManager
    {
        private Dictionary<DateTime, List<Muscle>> _program;

        public WorkoutManager()
        {
            _program = new Dictionary<DateTime, List<Muscle>>();
        }

        public void AddDay(DateTime date, List<Muscle> muscles)
        {
            if(_program.TryAdd(new DateTime(date.Ticks), new List<Muscle>(muscles)))
                Console.WriteLine("Successful!");
            else
                Console.WriteLine("Duplicates date or another error");
        }

        public void AddDay(List<Muscle> muscles)
        {
            AddDay(DateTime.Now, muscles);
        }

        public void RemoveDay(DateTime day)
        {
            
        }

        public void UpdateDay(DateTime day)
        {
            
        }

        public void SaveToFile()
        {
            
        }

        public void LoadFromFile()
        {
            
        }
        
    }
}
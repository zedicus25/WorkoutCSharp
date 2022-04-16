using System;
using System.Collections.Generic;
using System.Text;

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
            if(_program.TryAdd(CreateDate(date), new List<Muscle>(muscles)))
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
            day = CreateDate(day);
            if (_program.Remove(day))
                Console.WriteLine("Successful");
            else
                Console.WriteLine("This day is not on the list");
        }

        public void UpdateDay(DateTime day)
        {
            day = CreateDate(day);
            if (_program.ContainsKey(day) == false)
            {
                Console.WriteLine("This day is not on the list");
                return;
            }

            List<Muscle> mus = new List<Muscle>();
            _program.TryGetValue(day, out mus);
            mus.ForEach(Console.WriteLine);
            
            Console.WriteLine("Enter index muscle");
            int indMus = EnterIndex();
            Console.WriteLine("Enter index exercise");
            int ind = EnterIndex();
            if (indMus < 0 || indMus > mus.Count)
            {
                Console.WriteLine("Incorrect index");
                return;
            }
            mus[indMus-1].UpdateExercise(mus[indMus-1].GetExercise(ind-1));
            
        }

        public void SaveToFile()
        {
            
        }

        public void LoadFromFile()
        {
            
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _program.Keys)
            {
                foreach (var it in _program[item])
                {
                    sb.AppendLine(it.ToString());
                }
            }
            return sb.ToString();
        }

        private DateTime CreateDate(DateTime tmp)
        {
            return new DateTime(tmp.Year, tmp.Month, tmp.Day);
        }

        private int EnterIndex()
        {
            string str = Console.ReadLine();
            return int.TryParse(str, out int ind) ? ind : 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Workout
{
    public class WorkoutManager
    {
        private Dictionary<DateTime, List<Muscle>> _program;
        private List<Func<int, int, Exercise>> _create;
        public WorkoutManager()
        {
            _program = new Dictionary<DateTime, List<Muscle>>();
            _create = new List<Func<int, int, Exercise>>()
            {
                CreateArmyPress,
                CreateBenchPress,
                CreateDumbbellLayout,
                CreateDumbbellPull,
                CreateSquat,
                CreateTiltPull
            };
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

        public void UpdateDayExercise(DateTime day)
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
        public void AddDayExercise(DateTime day)
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
            if (indMus < 0 || indMus > mus.Count)
            {
                Console.WriteLine("Incorrect index");
                return;
            }

            for (int i = 0; i < _create.Count; i++)
            {
                Console.WriteLine($"{i+1} - {_create[i].Method.Name}");
            }
            Console.WriteLine("Enter index exercise");
            int ind = EnterIndex();
            Console.WriteLine("Enter sets");
            int sets = EnterIndex();
            Console.WriteLine("Enter reps");
            int reps = EnterIndex();
            mus[indMus-1].AddExercise(_create[ind-1]?.Invoke(reps,sets));
        }

        public void SaveToFile()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var list in _program)
            {
                sb.AppendLine(list.Key.ToShortDateString());
                foreach (var item in list.Value)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (File.Exists("dataBase.txt"))
            {
                File.AppendAllText("dataBase.txt",sb.ToString());
            }
            else
            {
                File.WriteAllText("dataBase.txt",sb.ToString());
            }
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

        private Exercise CreateArmyPress(int reps, int sets) => new ArmyPress(reps, sets);
        private Exercise CreateBenchPress(int reps, int sets) => new BenchPress(reps, sets);
        private Exercise CreateDumbbellLayout(int reps, int sets) => new DumbbellLayout(reps, sets);
        private Exercise CreateDumbbellPull(int reps, int sets) => new DumbbellPull(reps, sets);
        private Exercise CreateSquat(int reps, int sets) => new Squat(reps, sets);
        private Exercise CreateTiltPull(int reps, int sets) => new TiltPull(reps, sets);
    }
}
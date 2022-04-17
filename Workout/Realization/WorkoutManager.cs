using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Workout
{
    public class WorkoutManager
    {
        private Dictionary<DateTime, List<Muscle>> _program;
        private List<Func<int, int, Exercise>> _createExercise;
        private List<Func<List<Exercise>,Muscle>> _createMuscle;
        public WorkoutManager()
        {
            _program = new Dictionary<DateTime, List<Muscle>>();
            _createExercise = new List<Func<int, int, Exercise>>()
            {
                CreateArmyPress,
                CreateBenchPress,
                CreateDumbbellLayout,
                CreateDumbbellPull,
                CreateSquat,
                CreateTiltPull
            };
            _createMuscle = new List<Func<List<Exercise>, Muscle>>()
            {
                CreateBack,
                CreateBiceps,
                CreateChest,
                CreateLegs,
                CreateShoulders
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

            for (int i = 0; i < _createExercise.Count; i++)
            {
                Console.WriteLine($"{i+1} - {_createExercise[i].Method.Name}");
            }
            Console.WriteLine("Enter index exercise");
            int ind = EnterIndex();
            Console.WriteLine("Enter sets");
            int sets = EnterIndex();
            Console.WriteLine("Enter reps");
            int reps = EnterIndex();
            mus[indMus-1].AddExercise(_createExercise[ind-1]?.Invoke(reps,sets));
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
            if (File.Exists("dataBase.txt") == false)
            {
                Console.WriteLine("Cant find file");
                return;
            }

            string allFile = File.ReadAllText("dataBase.txt");
            List<string> days = new List<string>(allFile.Split(new string('-', 20)));
            days.Remove("\r\n");
            foreach (var t in days)
            {
                List<string> daysLine = new List<string>(t.Split('\n'));
                if(daysLine.Count == 0)
                    return;
                for (int j = 0; j < daysLine.Count; j++)
                {
                    daysLine[j] = daysLine[j].Trim();
                    daysLine[j] = daysLine[j].Replace("- ", "");
                    daysLine[j] = daysLine[j].Replace("sets: ", "");
                    daysLine[j] = daysLine[j].Replace("reps: ", "");
                    if (daysLine.Remove(""))
                        j--;
                }

                List<Exercise> exercises = new List<Exercise>();
                foreach (var str in daysLine)
                {
                    str.Trim();
                    int ind = GetIndexExercise(str);
                    if (ind != -1)
                    {
                        int sets = int.Parse(daysLine[daysLine.IndexOf(str)+1]);
                        int reps = int.Parse(daysLine[daysLine.IndexOf(str)+2]);
                        exercises.Add(_createExercise[ind]?.Invoke(reps,sets));
                    }
                    
                }
                List<Muscle> muscles = new List<Muscle>();
                foreach (var str in daysLine)
                {
                    int ind = GetIndexMuscle(str);
                    if(ind != -1)
                        muscles.Add(_createMuscle[ind]?.Invoke(exercises));
                }
                AddDay(DateTime.Parse(daysLine[0]), muscles);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _program.Keys)
            {
                sb.AppendLine(item.ToShortDateString());
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

        private int GetIndexMuscle(string name)
        {
            switch (name)
            {
                case "back":
                    return 0;
                case "biceps":
                    return 1;
                case  "chest":
                    return 2;
                case "legs":
                    return 3;
                case "shoulders":
                    return 4;
            }

            return -1;
        }

        private int GetIndexExercise(string name)
        {
            switch (name)
            {
                case "army press":
                    return 0;
                case "bench press":
                    return 1;
                case  "dumbbell layout":
                    return 2;
                case "dumbbell pull":
                    return 3;
                case "squat":
                    return 4;
                case "tilt pull":
                    return 5;
            }

            return -1;
        }

        private Exercise CreateArmyPress(int reps, int sets) => new ArmyPress(reps, sets);
        private Exercise CreateBenchPress(int reps, int sets) => new BenchPress(reps, sets);
        private Exercise CreateDumbbellLayout(int reps, int sets) => new DumbbellLayout(reps, sets);
        private Exercise CreateDumbbellPull(int reps, int sets) => new DumbbellPull(reps, sets);
        private Exercise CreateSquat(int reps, int sets) => new Squat(reps, sets);
        private Exercise CreateTiltPull(int reps, int sets) => new TiltPull(reps, sets);

        private Muscle CreateBack(List<Exercise> ex) => new Back(ex);
        private Muscle CreateBiceps(List<Exercise> ex) => new Biceps(ex);
        private Muscle CreateChest(List<Exercise> ex) => new Chest(ex);
        private Muscle CreateLegs(List<Exercise> ex) => new Legs(ex);
        private Muscle CreateShoulders(List<Exercise> ex) => new Shoulders(ex);
    }
}
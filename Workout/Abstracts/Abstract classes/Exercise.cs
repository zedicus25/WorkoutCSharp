using System.Text;

namespace Workout
{
    public abstract class Exercise
    {
        private string _name;
        private int _sets;
        private int _reps;

        protected Exercise(string name)
        {
            _name = name;
        }
        public string GetName() => _name;
        

        public int GetSets() => _sets;
        public int GetReps() => _reps;
        
        public void SetReps(int reps) => _reps = reps > 0 ? reps : _reps;
        
        public void SetSets(int sets) => _sets = sets > 0 ? sets : _sets;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_name);
            sb.AppendLine("Sets: " + _sets);
            sb.AppendLine("Reps: " + _reps);
            return sb.ToString();
        }
    }
}
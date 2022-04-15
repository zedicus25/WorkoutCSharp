namespace Workout
{
    public interface IExercise
    {
        string GetName();
        int GetSets();
        int GetReps();
        void SetReps(int reps);
        void SetSets(int sets);
    }
}
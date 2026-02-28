using ExaminationSystem.Classes;
using static System.Console;
namespace ExaminationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FliesOperators.CreateFolderInMain("Exams");
            FliesOperators.CreateFolderInMain("Answers");

        }
    }
}

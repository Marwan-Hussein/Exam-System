using System.IO;
namespace ExaminationSystem.Classes
{
    internal class FliesOperators
    {
        internal static string ProjectPath{ private set; get; }
        internal static string ExamsFolderPath{ private set; get; }
        internal static void CreateFolderInMain(string folderName)
        {
            string currPath = Directory.GetCurrentDirectory(); // path\\ExaminationSystem\bin\Debug\net5.0
            ProjectPath = GetNthParent(currPath,3);
            ExamsFolderPath = Path.Combine(ProjectPath, folderName);
            if (!Directory.Exists(ExamsFolderPath))
                Directory.CreateDirectory(ExamsFolderPath);
        }

        private static string GetNthParent(string path, int n=1)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            for (int i = 0; i < n; i++)
            {
                if (dir.Parent == null)
                    return null;
                dir = dir.Parent;
            }
            return dir.FullName;
        }
    }
}

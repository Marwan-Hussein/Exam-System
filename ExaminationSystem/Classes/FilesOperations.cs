using System.IO;
namespace ExaminationSystem.Classes
{
    internal class FilesOperations
    {
        #region properties
        internal static string ProjectPath{ private set; get; }
        internal static string ExamsFolderPath{ private set; get; }
        internal static string AnswersFolderPath{ private set; get; }
        #endregion

        // to create exams, answers folders if not exists
        internal static void CreateMainFolders()
        {
            string currPath = Directory.GetCurrentDirectory(); // path\\ExaminationSystem\bin\Debug\net5.0
            ProjectPath = GetNthParent(currPath,3);
            ExamsFolderPath = Path.Combine(ProjectPath, "Exams");
            AnswersFolderPath = Path.Combine(ProjectPath, "Answers");
            if (!Directory.Exists(ExamsFolderPath))
                Directory.CreateDirectory(ExamsFolderPath);
            if (!Directory.Exists(AnswersFolderPath))
                Directory.CreateDirectory(AnswersFolderPath);
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

        internal static string GetAnswersFolder() => AnswersFolderPath;
        internal static string GetExamsFolder() => ExamsFolderPath;
    }
}

using System;
using System.IO;
using static System.Console;
namespace ExaminationSystem.Classes
{
    internal class FilesOperations
    {
        #region properties
        internal static string ProjectPath{ private set; get; }
        internal static string ExamsFolderPath{ private set; get; }
        internal static string AnswersFolderPath{ private set; get; }
        internal static string StudentsFolderPath{ private set; get; }
        #endregion

        static FilesOperations() { CreateMainFolders(); }

        // to create exams, answers folders if not exists
        internal static void CreateMainFolders()
        {
            try {
                string currPath = Directory.GetCurrentDirectory(); 
                // path\\ExaminationSystem\bin\Debug\net5.0
                
                ProjectPath = GetNthParent(currPath, 3);
                ExamsFolderPath = Path.Combine(ProjectPath, "Exams");
                AnswersFolderPath = Path.Combine(ProjectPath, "Answers");
                StudentsFolderPath = Path.Combine(ProjectPath, "Students");

                CreateDirectoryIfNotExists(ExamsFolderPath);
                CreateDirectoryIfNotExists(AnswersFolderPath);
                CreateDirectoryIfNotExists(StudentsFolderPath);
            }
            catch(Exception ex)
            {
                WriteLine($"Error Creating Folders: {ex.Message}");

                ProjectPath = Directory.GetCurrentDirectory();
                ExamsFolderPath = Path.Combine(ProjectPath, "Exams");
                AnswersFolderPath = Path.Combine(ProjectPath, "Answers");
                StudentsFolderPath = Path.Combine(ProjectPath, "Students");

                CreateDirectoryIfNotExists(ExamsFolderPath);
                CreateDirectoryIfNotExists(AnswersFolderPath);
                CreateDirectoryIfNotExists(StudentsFolderPath);
            }

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

        private static void CreateDirectoryIfNotExists(string path)
        {
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        internal static string GetAnswersFolder() => AnswersFolderPath;
        internal static string GetExamsFolder() => ExamsFolderPath;
        internal static string GetStudentsFolder() => StudentsFolderPath;

        internal static string GetExamFilePath(string examName)
            => Path.Combine(ExamsFolderPath, $"{examName}_{DateTime.Now:yyMMdd_HHmmss}.txt");
        internal static string GetStudentAnswerPath(int studentId, int examId)
            => Path.Combine(AnswersFolderPath, $"Student{studentId}_Exam{examId}.txt");
    }
}

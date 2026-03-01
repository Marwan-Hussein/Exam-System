using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExaminationSystem.Classes
{
    internal class QuestionList : List<Question>
    {
        internal string LogFilePath { private set; get; }
        int _nextQuestionId = 1;
        QuestionList(string logFilePath)
        {
            LogFilePath = logFilePath;
            if (File.Exists(logFilePath))
            {
                var lastId = LoadFromFile().Max(q => q.QuestionId);
                _nextQuestionId = lastId+1;
            }
        }

        public new void Add(Question question)
        {
            question.QuestionId = _nextQuestionId++;
            base.Add(question);
            LogToFile(question);
        }

        public void AddRange(IEnumerable<Question> questions)
        {

        }

        public void LogToFile(Question q)
        {
            using (StreamWriter sw = new StreamWriter(LogFilePath, true))
            {
                // Question form: Id|Header|Marks|Body
                sw.WriteLine(q.ToString());
            }
        }

        public List<Question> LoadFromFile()
        {
            // Question form: Id|Header|Marks|Body
            List<Question> list = new List<Question>();
            if (File.Exists(LogFilePath))
            {
                using (StreamReader sr = new StreamReader(LogFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split('|');

                        int.TryParse(parts[0], out int id);
                        string header = parts[1];
                        double.TryParse(parts[2], out double marks);
                        string body = parts[3];
                        list.Add(new Question(id, header, body, marks));
                    }
                }
            }
            return list;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace ExaminationSystem.Classes
{
    internal class QuestionList : List<Question>
    {
        internal string LogFilePath { private set; get; }
        private static int? _nextQuestionId = 1;
        internal QuestionList(string logFilePath)
        {
            LogFilePath = logFilePath;
            InitializeFromFile();
        }

        private void InitializeFromFile()
        {
            if (File.Exists(LogFilePath))
            {
                var questions = LoadFromFile();
                if (questions.Any())
                {
                    var lastId = questions.Max(q => q.QuestionId);
                    _nextQuestionId = lastId + 1;

                    foreach(var q in questions)
                        base.Add(q);
                }
            }
        }
        public static int? GetQuestionId() => _nextQuestionId++;

        public new void Add(Question question)
        {
            if(!question.QuestionId.HasValue || question.QuestionId == 0)
                question.QuestionId = _nextQuestionId++;
            //question.QuestionId = _nextQuestionId++; // (only in case it doesn't have id yet)
            base.Add(question);
            LogToFile(question);
        }

        public void AddRange(IEnumerable<Question> questions)
        {
            foreach (var q in questions)
                Add(q);
        }

        public void LogToFile(Question q)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(LogFilePath, true))
                {
                    // Question form: Type|Id|Header|Marks|Body
                    sw.WriteLine(q.ToString());
                }
            }

            catch(Exception ex)
            {
                WriteLine($"Error logging to the file {LogFilePath}!");
            }
        }

        public List<Question> LoadFromFile()
        {
            // Question form: Type|Id|Header|Marks|Body
            List<Question> list = new List<Question>();
            if (!File.Exists(LogFilePath))
                return list;
            try
            {
                using (StreamReader sr = new StreamReader(LogFilePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split('|');
                        if(parts.Length < 5)
                            continue;

                        // base.ToString() parsing
                        string type = parts[0];
                        int.TryParse(parts[1], out int id);
                        string header = parts[2];
                        double.TryParse(parts[3], out double marks);
                        string body = parts[4];
                        // parts.Lenght must be min 5 here

                        Question question = null;

                        //list.Add(new Question(id, header, body, marks));
                        switch (type)
                        {
                            case nameof(TFQuestion): // format: base(parts in previous)|{CorrectAnswer}
                                if(parts.Length > 5 && bool.TryParse(parts[5], out bool ans))
                                    question = new TFQuestion(id, header, body, marks, ans);
                                break;

                            case nameof(ChooseOneQuestion): // format: base(parts in previous)|CorrectIndex'\n'Options
                                if(parts.Length > 5)
                                {
                                    var CorrectAndOptions = parts[5].Split('\n');
                                    int.TryParse(CorrectAndOptions[0], out int coreectIdx);
                                    string[] options = new string[CorrectAndOptions.Length-1];
                                    Array.Copy(CorrectAndOptions, 1, options, 0, CorrectAndOptions.Length - 1);

                                    question = new ChooseOneQuestion(id, header, body, marks, coreectIdx, options.ToList());
                                }
                                break;

                            case nameof(ChooseAllQuestion): // format: base(parts in previous)|CA1,ca2'\n'op1'\n'op2..
                                if(parts.Length > 5)
                                {
                                    var answers_options = parts[5].Split(", ");
                                    var lastAns_Options = answers_options[answers_options.Length - 1].Split('\n'); // last answer + options

                                    int AnswersSize = answers_options.Length - lastAns_Options.Length +1;
                                    int OptionsSize = lastAns_Options.Length - 1;

                                    int[] AnswersOnly = new int[AnswersSize];
                                    string[] OptionsOnly = new string[OptionsSize];

                                    Array.Copy(answers_options, 0, AnswersOnly, 0, AnswersSize);
                                    Array.Copy(lastAns_Options, 1, OptionsOnly, 0, OptionsSize);

                                    question = new ChooseAllQuestion(id, header, body, marks, AnswersOnly.ToList(), OptionsOnly.ToList());

                                }
                                break;

                            default:
                                continue; // not of the previous types
                        }

                        if(question != null)
                            list.Add(question);
                    
                    }
                }
            }

            catch (Exception ex)
            {
                WriteLine($"Error logging to the file {LogFilePath}!");
            }
            return list;
        }
    }
}

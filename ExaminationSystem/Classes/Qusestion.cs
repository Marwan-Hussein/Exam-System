using System.Collections.Generic;
using static System.Console;
namespace ExaminationSystem.Classes
{
    internal class Question
    {
        #region properties
        internal int? QuestionId {get; set;}
        internal string Header{get; set;}
        internal string Body {get; set;}
        internal double Marks {get; set;}
        internal AnswerList Answers { get; set; }
        #endregion

        #region Constructors
        internal Question() { }
        internal Question(int id, string header, string body, double marks)
        {
            QuestionId = id;
            Header = header;
            Body = body;
            Marks = marks;
        }
        internal Question(string header, string body, double marks) : this(QuestionList.GetQuestionId(), header, body, marks)
        {}
        #endregion

        public override string ToString()
        {
            // Question form: Id|Header|Marks|Body
            return $"{QuestionId}|{Header}|{Marks}|{Body}";
        }

        public virtual void Display() { }

    }

    internal class TFQuestion : Question
    {
        internal bool CorrectAnswer { get; set;}

        internal TFQuestion() { }
        internal TFQuestion(string header, string body, double marks, bool answer) : base(header, body, marks)
        {
            CorrectAnswer = answer;
        }
        public override void Display()
        {
            WriteLine($"Q{QuestionId}. {Header} | {Marks} Marks\n{Body}");
            WriteLine($"Answer: {CorrectAnswer}");
        }
    }

    internal class ChooseOneQuestion : Question
    {
        internal int CorrectAnswerIdx { get; set; }

        internal ChooseOneQuestion() { }
        internal ChooseOneQuestion(string header, string body, double marks, int answer) : base(header, body, marks)
        {
            CorrectAnswerIdx = answer;
        }
        public override void Display()
        {
            WriteLine($"Q{QuestionId}. {Header} | {Marks} Marks\n{Body}");
            WriteLine($"Answer: {CorrectAnswerIdx}");
        }
    }

    internal class ChooseAllQuestion : Question
    {
        internal List<int> CorrectAnswers { get; set; }

        internal ChooseAllQuestion() { }
        internal ChooseAllQuestion(string header, string body, double marks, List<int> answers) : base(header, body, marks)
        {
            CorrectAnswers = answers;
        }
        public override void Display()
        {
            WriteLine($"Q{QuestionId}. {Header} | {Marks} Marks\n{Body}");
            Write("Answers: ");
            foreach (var ans in CorrectAnswers)
                Write(ans);
            WriteLine();
        }
    }
}

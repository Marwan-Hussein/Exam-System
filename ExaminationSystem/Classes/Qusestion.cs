using System;
using System.Collections.Generic;
using static System.Console;
namespace ExaminationSystem.Classes
{
    internal abstract class Question : ICloneable, IComparable<Question>
    {
        #region properties
        internal int? QuestionId {get; set;}
        internal string Header{get; set;}
        internal string Body {get; set;}
        internal double Marks {get; set;}
        internal AnswerList Answers { get; set; }
        #endregion

        #region Constructors
        internal Question() { Answers = new AnswerList(); }
        internal Question(int? id, string header, string body, double marks) : this()
        {
            QuestionId = id;
            Header = header;
            Body = body;
            Marks = marks;
        }
        internal Question(string header, string body, double marks) 
            : this(QuestionList.GetQuestionId(), header, body, marks){}
        #endregion

        public override string ToString()
        {
            // Question form: Type|Id|Header|Marks|Body
            return $"{GetType().Name}|{QuestionId}|{Header}|{Marks}|{Body}";
        }

        public abstract void Display();

        #region iFaces implementation

        public object Clone() {return MemberwiseClone();}
        public int CompareTo(Question other)
        {
            if(other == null) return 1;
            return Nullable.Compare(QuestionId, other.QuestionId);
        }
        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != GetType())
                return false;
            Question q = (Question)obj;
            return QuestionId == q.QuestionId;
        }
        public override int GetHashCode() { return QuestionId.GetHashCode(); }
        #endregion

    }

    internal class TFQuestion : Question
    {
        internal bool CorrectAnswer { get; set;}

        #region ctors
        internal TFQuestion() { }
        internal TFQuestion(string header, string body, double marks, bool answer) 
            : base(header, body, marks)
        {
            CorrectAnswer = answer;
        }
        /*internal TFQuestion(int? id, string header, string body, double marks, bool answer) :
            base(id, header, body, marks)
        {
            CorrectAnswer = answer;
        }*/
        #endregion

        public override void Display()
        {
            WriteLine($"Q{QuestionId}. {Header} | {Marks} Marks\n{Body}");
            WriteLine($"a) True \tb) False");
        }

        public override string ToString()
        {
            return base.ToString() + $"|{CorrectAnswer}";
        }
    }

    internal class ChooseOneQuestion : Question
    {
        #region properties
        internal int CorrectAnswerIdx { get; set; }
        internal List<string> Options { get; set; }
        #endregion

        #region ctors
        internal ChooseOneQuestion() { Options = new List<string>(); }
        internal ChooseOneQuestion(string header, string body, double marks, int answer, List<string> options) 
            : base(header, body, marks)
        {
            CorrectAnswerIdx = answer;
            Options = options?? new List<string>();
        }
        /*internal ChooseOneQuestion(int? id,string header, string body, double marks, int answer, List<string> options)
            : base(id,header, body, marks)
        {
            CorrectAnswerIdx = answer;
            Options = options ?? new List<string>();
        }*/
        #endregion

        public override void Display()
        {
            WriteLine($"Q{QuestionId}. {Header} | {Marks} Marks\n{Body}");
            int i = 1;
            foreach (string option in Options)
                WriteLine($"{i++}) {option}");
            
            //WriteLine($"Answer: {CorrectAnswerIdx}");
        }

        public override string ToString()
        {
            string optionsString = string.Join("\n", Options);
            return base.ToString() + $"|{CorrectAnswerIdx}|{optionsString}";
        }
    }

    internal class ChooseAllQuestion : Question
    {
        internal List<int> CorrectAnswers { get; set; }
        internal List<string> Options { get; set; }

        #region ctors
        internal ChooseAllQuestion() 
        {
            CorrectAnswers = new List<int>();
            Options = new List<string>();
        }
        internal ChooseAllQuestion(string header, string body, double marks, List<int> answers, List<string> options) 
            : base(header, body, marks)
        {
            CorrectAnswers = answers;
            Options = options;
        }

        internal ChooseAllQuestion(int? id, string header, string body, double marks, List<int> answers, List<string> options) :
            base(id, header, body, marks)
        {
            CorrectAnswers = answers ?? new List<int>();
            Options = options ?? new List<string>();
        }
#endregion
        public override void Display()
        {
            WriteLine($"Q{QuestionId}. {Header} | {Marks} Marks\n{Body}");
            //Write("Answers: ");
            int i = 1;
            /*foreach (var ans in CorrectAnswers)
                Write(ans);*/
            WriteLine("(Select all that apply)");
            foreach (var option in Options)
                WriteLine($"{i++}) {option}");
            WriteLine();
        }

        public override string ToString()
        {
            string correctAnsstr = string.Join(" | ", CorrectAnswers);
            string optionsStr = string.Join("\n", Options);
            return base.ToString() + $"\t{correctAnsstr}\n{optionsStr}";
        }
    }
}

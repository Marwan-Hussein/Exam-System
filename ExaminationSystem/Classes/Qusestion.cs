using static System.Console;
namespace ExaminationSystem.Classes
{
    internal abstract class Question
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

        public abstract void Display();

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
            WriteLine("a) True\tb) False");
        }
    }
}

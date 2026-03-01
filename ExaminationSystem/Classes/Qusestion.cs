namespace ExaminationSystem.Classes
{
    internal class Question
    {
        #region properties
        internal int QuestionId {get; set;}
        internal string Header{get; set;}
        internal string Body {get; set;}
        internal double Marks {get; set;}
        internal AnswerList Answers { get; set; }
        #endregion

        #region Constructors
        internal Question() { }
        internal Question(string header, string body, double marks)
        {
            Header = header;
            Body = body;
            Marks = marks;
        }
        internal Question(int id, string header, string body, double marks) : this(header, body, marks)
        {
            QuestionId = id;
        }
        #endregion

        public override string ToString()
        {
            // Question form: Id|Header|Marks|Body
            return $"{QuestionId}|{Header}|{Marks}|{Body}";
        }

    }

    internal class TFQuestion : Question
    {

    }
}

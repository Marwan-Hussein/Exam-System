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
        #endregion
    }

    internal class TFQuestion : Question
    {

    }
}

using System.Collections.Generic;

namespace ExaminationSystem.Classes
{
    internal class Answer
    {
        internal int? QuestionId {get;}
        internal object UserAnswer { get; set; }

        internal Answer() { }
        internal Answer(int QId) { QuestionId = QId; }
        internal virtual bool IsCorrect(Question q)
        {
            return UserAnswer.Equals(q.Answers);
        }
    }

    internal class TFAnswer : Answer
    {
        internal bool SelectedOption { get; set; }
        internal TFAnswer() { }
        internal TFAnswer(int QId, bool selectedOption) : base(QId)
            { base.UserAnswer = selectedOption; }
        //internal override bool IsCorrect(Question q)
        //    { return SelectedOption.Equals(q.Answers); }
    }

    internal class ChooseOneAnswer : Answer
    {
        internal int SelectedOption {  get; set; }
        internal ChooseOneAnswer() { }
        internal ChooseOneAnswer(int QId, int selectedOption) : base(QId)
        {
            SelectedOption = selectedOption;
        }

        //internal override bool IsCorrect(Question q) { return SelectedOption.Equals(q.Answers);
    }

    internal class ChooseAllAnswer : Answer
    {
        internal List<int> Answers { get; set; }
        internal ChooseAllAnswer() { }
        internal ChooseAllAnswer(int QId, List<int> answers) : base(QId)
        { Answers = answers; }

        //internal override bool IsCorrect(Question q)
        //    {return Answers.Equals(q.Answers);}

    }


}

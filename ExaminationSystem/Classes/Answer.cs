using System;
using System.Collections.Generic;

namespace ExaminationSystem.Classes
{
    internal abstract class Answer : ICloneable
    {
        internal int? QuestionId { get; set; }
        internal object UserAnswer { get; set; }

        internal Answer() { }
        internal Answer(int QId) { QuestionId = QId; }
        internal abstract bool IsCorrect(Question q);

        public object Clone() { return MemberwiseClone(); }
        public override string ToString()
        {
            return $"Q{QuestionId}: {UserAnswer}";
        }
    }

    internal class TFAnswer : Answer
    {
        internal bool SelectedOption 
        {
            get => (bool)UserAnswer;
            set => UserAnswer = value;
        }
        internal TFAnswer() { }
        internal TFAnswer(int QId, bool selectedOption) : base(QId)
            { SelectedOption = selectedOption; }
        internal override bool IsCorrect(Question q)
        {
            if (q is TFQuestion tfq) 
                return SelectedOption == tfq.CorrectAnswer;
            return false;
        }
    }

    internal class ChooseOneAnswer : Answer
    {
        internal int SelectedOption 
        {
            get => (int)UserAnswer;
            set => UserAnswer= value;
        }
        internal ChooseOneAnswer() { }
        internal ChooseOneAnswer(int QId, int selectedOption) : base(QId)
        {
            SelectedOption = selectedOption;
        }

        internal override bool IsCorrect(Question q)
        {
            if(q is ChooseOneQuestion Ch1Q)
                return SelectedOption.Equals(Ch1Q.CorrectAnswerIdx);
            return false;
        }
    }

    internal class ChooseAllAnswer : Answer
    {
        internal List<int> SelectedOptions 
        {
            get => (List<int>)UserAnswer;
            set => UserAnswer = value;
        }
        internal ChooseAllAnswer() { SelectedOptions = new List<int>(); }
        internal ChooseAllAnswer(int QId, List<int> selectedOptions) : base(QId)
        { 
            SelectedOptions = selectedOptions??new List<int>(); 
        }

        internal override bool IsCorrect(Question q)
        {
            if (q is ChooseAllQuestion ChAllQ)
            {
                if (SelectedOptions.Count != ChAllQ.CorrectAnswers.Count)
                    return false;

                for(int i=0; i<SelectedOptions.Count; i++)
                    if (SelectedOptions[i] != ChAllQ.CorrectAnswers[i])
                        return false;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Q{QuestionId}: [{string.Join(", ", SelectedOptions)}]";
        }

    }


}

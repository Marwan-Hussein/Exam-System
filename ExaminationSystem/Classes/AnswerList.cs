using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Classes
{
    internal class AnswerList
    {
        internal List<Answer> Answers;

        internal AnswerList() { }
        internal void AddAnswer(Answer answer)
        {

        }

        internal Answer GetAnswer(int questionId)
        {
            return Answers[questionId];
        }

        internal int CalculateScore(QuestionList questions)
        {
            int score = 0;
            foreach (var (q , ans) in questions.Zip(Answers, (x,y) => (x,y)))
                if(ans.IsCorrect(q))
                    ++score;
            return score;
        }
    }
}

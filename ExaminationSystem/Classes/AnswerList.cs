using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Classes
{
    // answers for one exam
    internal class AnswerList
    {
        internal List<Answer> Answers {  get; set; }

        internal AnswerList() 
            { Answers = new List<Answer>(); }

        internal void AddAnswer(Answer answer)
        {
            Answers.Add(answer);
        }
        internal Answer GetAnswer(int questionId)
            => Answers.FirstOrDefault(q => q.QuestionId == questionId);

    }

    // answers for one student in one exam
    internal class AnswerSet
    {
        private List<Answer> Answers { get; set; }
        internal DateTime SubmissionTime {  get; set; }
        internal int StudentId { get; set; }

        internal AnswerSet(int studentId)
        {
            Answers = new List<Answer>();
            SubmissionTime = DateTime.Now;
            StudentId = studentId;
        }

        internal void AddAnswer(Answer answer) { Answers.Add(answer); }

        internal Answer GetAnswer(int )
        internal int CalculateScore(QuestionList questions)
        {
            int score = 0;
            foreach (var (q , ans) in questions.Zip(Answers, (x,y) => (x,y)))
                if(ans.IsCorrect(q))
                    ++score;
            return score;
        }
}

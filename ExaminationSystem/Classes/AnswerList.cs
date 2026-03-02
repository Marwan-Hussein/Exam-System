using System;
using System.Collections.Generic;
using System.Linq;

namespace ExaminationSystem.Classes
{
    internal struct QuestionInfo
    {
        public QuestionInfo(int id, double mark, bool isCorrect)
        {
            Id = id;
            Mark = mark;
            IsCorrect = isCorrect;
        }

        public int Id {  get; set; }
        public double Mark {  get; set; }
        public bool IsCorrect { get; set; }
    }
    // answers for one exam
    internal class AnswerList
    {
        internal List<Answer> Answers { get; set; }

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
        internal DateTime SubmissionTime { get; set; }
        internal int StudentId { get; set; }

        internal AnswerSet(int studentId)
        {
            Answers = new List<Answer>();
            SubmissionTime = DateTime.Now;
            StudentId = studentId;
        }

        internal void AddAnswer(Answer answer) { Answers.Add(answer); }

        internal Answer GetAnswer(int questionId)
            => Answers.FirstOrDefault(q => q.QuestionId == questionId);
        
        internal int CalculateScore(QuestionList questions)
        {
            double score = 0;

            foreach (Answer ans in Answers)
            {
                Question question = questions.FirstOrDefault(q => q.QuestionId == ans.QuestionId);
                if (question != null && ans.IsCorrect(question))
                    score += question.Marks;
            }
            return (int)Math.Round(score);
        }
        internal List<QuestionInfo> GetResults(QuestionList questions)
        {
            var result = new List<QuestionInfo>();
            foreach (Answer ans in Answers)
            {
                Question question = questions.FirstOrDefault(q => q.QuestionId == ans.QuestionId);
                if (question != null)
                    result.Add(new QuestionInfo(question.QuestionId.Value, question.Marks ,ans.IsCorrect(question)));
            }
            return result;
        }
    }
}
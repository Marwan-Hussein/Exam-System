using System;
using System.Collections.Generic;
using static System.Console;
namespace ExaminationSystem.Classes
{
    enum ExamMode
    {
        Starting,
        Queued,
        Finished
    }

    internal abstract class Exam : ICloneable, IComparable<Exam>
    {
        #region Properties
        internal int ExamId { get; set; }
        internal TimeSpan Duration { get; set; }
        //internal int NumberOfQuestions { get; set; }
        internal Dictionary<int, Answer> CorrectAnswers { get; set; } // <QuestionID, CorrectAnswer>
        internal Subject ExamSubject { get; set; }
        internal ExamMode Mode { get; set; }
        internal QuestionList Questions {  get; set; }
        internal List<Student> EnrolledStudents  {get; set;}
        internal List<AnswerSet> Submissions  { get; set; }

        internal event EventHandler<ExamEventArgs> ExamStatusChanged;
        #endregion

        #region Constructors
        internal Exam() 
        {
            CorrectAnswers = new Dictionary<int, Answer>();
            EnrolledStudents = new List<Student>();
            Submissions = new List<AnswerSet>();
        }
        internal Exam(TimeSpan duration, Subject subject) : this()
        {
            Duration = duration;
            ExamSubject = subject;
            Mode = ExamMode.Queued;
        }

        #endregion

        #region methods
        internal void ChangeMode(ExamMode newMode) 
        {
            ExamMode oldMode = Mode;
            Mode = newMode;
            OnExamStatusChanged(new ExamEventArgs(newMode, ExamSubject.SubjectName,
                $"Exam mode changed from {oldMode} to {newMode}"
                ));
        }
        internal void EnrollStudent(Student student)
        {
            if (!EnrolledStudents.Contains(student))
            {
                EnrolledStudents.Add(student);
                ExamStatusChanged += student.HandleExamNotification;
            }
        }

        internal void SubmitAnswers(AnswerSet answers)
        {
            Submissions.Add(answers);
        }
        #endregion


        #region Interfaces implementations and Overrides
        public object Clone()
        {
            Exam cloned = (Exam)MemberwiseClone();
            cloned.ExamSubject = new Subject(ExamSubject);
            cloned.Questions = new QuestionList(Questions.LogFilePath);
            cloned.EnrolledStudents = new List<Student>(EnrolledStudents);
            cloned.CorrectAnswers = new Dictionary<int, Answer>(CorrectAnswers);
            cloned.Submissions = new List<AnswerSet>(Submissions);
            return cloned;
        }

        public int CompareTo(Exam other)
        {
            if (other == null) return 1;
            return Duration.CompareTo(other.Duration);
        }

        public override string ToString()
            => $"Exam{ExamId}-{ExamSubject.SubjectName}\t Duration: {Duration}\t Mode: {Mode}";

        public override bool Equals(object obj)
        {
            if (obj is Exam exam)
                return ExamId.Equals(exam.ExamId);
            return false;
        }
        public override int GetHashCode()
            => ExamId.GetHashCode();
        
        #endregion
        #region For Children
        protected virtual void OnExamStatusChanged(ExamEventArgs e)
        {
            ExamStatusChanged ?.Invoke(this, e);
        }
        internal abstract void ShowExam();

        #endregion
    }

    internal class PracticeExam : Exam
    {
        internal PracticeExam() { }
        internal PracticeExam(TimeSpan duration, Subject subject) : base(duration, subject) { }
    
        internal override void ShowExam() {
            WriteLine('\n'+new string('=', 50));
            WriteLine($"Pratice Exam - {ExamSubject.SubjectName}");
            WriteLine(new string('=', 50));

            WriteLine($"Duration: {Duration}\t Number of Questions: {Questions.Count}");
            WriteLine($"Qustions With Correct Answers: ");
            foreach(Question q in Questions)
            {
                q.Display();
                WriteLine("\nCorrectAnswer: " + GetCorrectAnswer(q));
                WriteLine(new string('-', 25));
            }

        }

        private string GetCorrectAnswer(Question q)
        {
            switch(q.GetType().Name)
            {
                case nameof(TFQuestion):
                    TFQuestion tfq = q as TFQuestion;
                    return tfq.CorrectAnswer.ToString();

                case nameof(ChooseOneQuestion):
                    ChooseOneQuestion ch1q = q as ChooseOneQuestion;
                    return (ch1q.CorrectAnswerIdx + 1).ToString();
                
                case nameof(ChooseAllQuestion):
                    ChooseAllQuestion chAllQ = q as ChooseAllQuestion;
                    return string.Join(", ", chAllQ.CorrectAnswers);

                default:
                    return "N/A";
            }
        }
    }

    internal class FinalExam : Exam
    {
        internal FinalExam() { }
        internal FinalExam(TimeSpan duration, Subject subject) : base(duration, subject) { }

        internal override void ShowExam()
        {
            foreach (var item in Answers)
            {
                WriteLine(item);
            }
        }
    }
}

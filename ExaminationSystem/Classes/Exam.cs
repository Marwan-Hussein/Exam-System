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
        internal int NumberOfQuestions { get; set; }
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


        internal void ChangeMode(ExamMode newMode) 
        {
            ExamMode oldMode = Mode;
            Mode = newMode;
            OnExamStatusChanged(new ExamEventArgs(newMode, ExamSubject.SubjectName,
                $"Exam mode changed from {oldMode} to {newMode}"
                ));
            
        }

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
            foreach (var item in Answers)
            {
                WriteLine(item);
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

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
    internal abstract class Exam
    {
        #region Properties
        internal int ExamId { get; set; }
        internal TimeSpan Duration { get; set; }
        internal int NumberOfQuestions { get; set; }
        internal Dictionary<int, Answer> Answers { get; set; }
        internal Subject ExamSubject { get; set; }
        internal ExamMode Mode { get; set; }
        internal QuestionList Questions {  get; set; }
        //event EventHandler<ExamEventHandler>
        #endregion

        internal Exam() { }
        internal Exam(TimeSpan duration, Subject subject)
        {
            Duration = duration;
            ExamSubject = subject;
        }

        void ChangeMode(ExamMode newMode) 
        { Mode = newMode; }
        internal abstract void ShowExam();
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

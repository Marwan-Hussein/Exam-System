using System;

namespace ExaminationSystem.Classes
{
    internal class ExamEventArgs : EventArgs
    {
        internal ExamMode CurrentMode {  get; set; }
        internal string ExamName { get; set; }
        internal DateTime NotificationTime {  get; set; }
        internal string Message {  get; set; }

        internal ExamEventArgs(ExamMode mode, string name)
        {
            CurrentMode = mode;
            ExamName = name;
            NotificationTime = DateTime.Now;
            Message = $"Exam {name} is now {mode}";
        }

        internal ExamEventArgs(ExamMode mode, string name, string message)
            : this(mode, name)
        { Message = message; }
    }
}

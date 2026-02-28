using System;

namespace ExaminationSystem.Classes
{
    internal class ExamEventArgs
    {
        internal ExamMode CurrentMode {  get; set; }
        internal string ExamName { get; set; }
        internal DateTime NotificationTime {  get; set; }

        internal ExamEventArgs(ExamMode mode, string name)
        {
            CurrentMode = mode;
            ExamName = name;
            NotificationTime = DateTime.Now;
        }
    }
}

using static System.Console;
using System;
using System.Reflection.Metadata;

namespace ExaminationSystem.Classes
{
    internal class Student : ICloneable, IComparable<Student>
    {
        #region properties
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string Email { get; set; }

        #endregion

        #region Ctors
        internal Student() { }
        internal Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        #endregion
        internal void HandleExamNotification(object sender, ExamEventArgs ex)
        {
            WriteLine($"\nNotification for: {Name} ({Email})");
            WriteLine($"Exam: {ex.ExamName}");
            WriteLine($"Status: {ex.CurrentMode}");
            WriteLine($"Time: {ex.NotificationTime}");
            WriteLine("___________________________\n");
        }

        #region iFace implementations and overrides
        public object Clone() => new Student(Id,Name, Email);
        public int CompareTo(Student Std)
        {
            if (Std == null)
                return 1;
            return Id.CompareTo(Std.Id);
        }
        public override string ToString()
            => $"ID: {Id}\t Name: {Name}\t Email: {Email}";
        public override bool Equals(object obj)
        {
            if(obj is Student Std)
                return Id.Equals(Std.Id);
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion
    }
}

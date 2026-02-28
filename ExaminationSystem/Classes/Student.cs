using System.Reflection.Metadata;

namespace ExaminationSystem.Classes
{
    internal class Student
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string Email { get; set; }

        internal Student() { }
        internal Student(int id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }
        void HandleExamNotification(object sender, ExamEvent) 
    }
}

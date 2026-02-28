namespace ExaminationSystem.Classes
{
    internal class Subject
    {
        internal string SubjectName { get; set; }
        internal string ProfessorName { get; set; }

        internal Subject() { }
        internal Subject(string subName, string profName)
        {
            SubjectName = subName;
            ProfessorName = profName;
        }
    }
}

namespace ExaminationSystem.Classes
{
    internal class Subject
    {
        internal string SubjectName { get; set; }
        internal string ProfessorName { get; set; }

        #region ctors
        internal Subject() { }
        internal Subject(string subName, string profName)
        {
            SubjectName = subName;
            ProfessorName = profName;
        }
        internal Subject(Subject other)
        {
            SubjectName = other.SubjectName;
            ProfessorName = other.ProfessorName;
        }
        #endregion

        public override string ToString()
        {
            return $"Name: {SubjectName}\t Prof: {ProfessorName}";
        }

        public override bool Equals(object obj)
        {
            if(obj is Subject sub)
                return SubjectName == sub.SubjectName && ProfessorName == sub.ProfessorName;
            return false;
        }
    }
}

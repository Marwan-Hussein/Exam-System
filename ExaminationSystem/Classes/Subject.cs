using System;

namespace ExaminationSystem.Classes
{
    internal class Subject : ICloneable, IComparable<Subject>
    {
        #region properties
        internal int SubjectId { get; set; }
        internal string SubjectName { get; set; }
        internal string ProfessorName { get; set; }

        #endregion

        #region ctors
        internal Subject() { }
        internal Subject(int id, string subName, string profName)
        {
            SubjectId = id;
            SubjectName = subName;
            ProfessorName = profName;
        }
        internal Subject(Subject other)
        {
            SubjectId = other.SubjectId;
            SubjectName = other.SubjectName;
            ProfessorName = other.ProfessorName;
        }
        #endregion

        #region IFaces and overrides
        public override string ToString()
        {
            return $"ID:{SubjectId}\t Name: {SubjectName}\t Prof: {ProfessorName}";
        }

        public override bool Equals(object obj)
        {
            if(obj is Subject sub)
                return SubjectName == sub.SubjectName && ProfessorName == sub.ProfessorName;
            return false;
        }

        public override int GetHashCode()
        {
            return SubjectId.GetHashCode();
        }

        public object Clone() => new Subject(this);
        public int CompareTo(Subject other)
        {
            if(other == null)
                return 1;
            return SubjectId.CompareTo(other.SubjectId);
        }
        #endregion
    }
}

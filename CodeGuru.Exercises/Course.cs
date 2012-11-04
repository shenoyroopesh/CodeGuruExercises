using System.Collections.Generic;

namespace CodeGuru.Exercises
{
    /// <summary>
    /// A Course
    /// </summary>
    public class Course
    {
        public int Id { get; set; }

        /// <summary>
        /// Course Name for this course
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// Various levels in this course
        /// </summary>
        public IList<Level> Levels { get; set; }
    }
}

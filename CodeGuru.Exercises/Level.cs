using System;
using System.Collections.Generic;

namespace CodeGuru.Exercises
{
    /// <summary>
    /// A Level in this course
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Circular reference back to the course that this level belongs to
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// Uri for this level
        /// </summary>
        public Uri LevelVideoUri { get; set; }

        /// <summary>
        /// Number of the level
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<IChallenge> Challenges { get; set; }
    }
}

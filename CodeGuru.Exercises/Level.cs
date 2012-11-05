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
        /// Course Id to which this level belongs
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Uri for this level
        /// </summary>
        public Uri LevelVideoUri { get; set; }

        /// <summary>
        /// Number of the level
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// List of challenges in this level
        /// </summary>
        public IList<IChallenge> Challenges { get; set; }
    }
}

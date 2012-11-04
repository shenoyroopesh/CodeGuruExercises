using System;
using System.Collections.Generic;
using CodeGuru.Exercises;
using System.Linq;

namespace CodeGuru.Data
{
    public class Courses : List<Course>
    {
        /// <summary>
        /// Initializes this data class
        /// </summary>
        public Courses()
        {
            //Initialize Courses data
            //TODO: make this come from a database!
            Add(new Course
                {
                    Id = 1,
                    CourseName = "Testing With NUnit",
                    Levels = new List<Level>
                        {
                            new Level
                                {
                                    LevelVideoUri = new Uri("http://player.vimeo.com/video/52632375?badge=0"),
                                    Number = 1,
                                    Challenges = new List<IChallenge>
                                        {
                                            new NUnitExercises.Level1.Challenge1()
                                        }
                                }
                        },
                });
        }
    }
}

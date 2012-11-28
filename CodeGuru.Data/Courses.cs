using System;
using System.Collections.Generic;
using CodeGuru.Exercises;
using System.Linq;
using NUnitExercises.Level1;

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
                                    CourseId = 1,
                                    LevelVideoUri = new Uri("http://player.vimeo.com/video/52632375?badge=0"),
                                    Number = 1,
                                    Challenges = new List<IChallenge>
                                        {
                                            new Challenge1
                                                {
                                                    ChallengeNo = 1,
                                                    CourseId = 1,
                                                    LevelNo = 1
                                                },
                                            new Challenge2
                                                {
                                                    ChallengeNo = 2,
                                                    CourseId = 1,
                                                    LevelNo = 1
                                                },
                                            new Challenge3
                                                {
                                                    ChallengeNo = 3,
                                                    CourseId = 1,
                                                    LevelNo = 1
                                                }
                                        }
                                }
                        },
                });
        }
    }
}

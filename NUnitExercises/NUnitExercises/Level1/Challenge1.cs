using System.Collections.Generic;
using CodeGuru.Exercises;

namespace NUnitExercises.Level1
{
    public class Challenge1 : NUnitChallenge, IChallenge
    {
        #region constants
        public const string ProperClass = @"
        public class Person 
        {
            public string Name { get; set; }

            public Person(string name)
            {
                Name = name;
            }
        }
        ";

        public const string BadClass1 = @"
        public class Person 
        {
            public string Name { get; set; }

            public Person(string name)
            {
            }
        }
        ";

        public const string InitialTestValue = @"
        using NUnit.Framework;

            //add necessary attributes to the test class and the method

            public class PersonTest
            {
                
                public void CheckConstructor()
                {
                    //write test logic here
                }
            }
        ";
        #endregion

        public Challenge1()
        {
            Instructions = "Modify the below test to ensure that it sufficiently tests the given class";
            CodeFiles.Add(new CodeFile{Order = 1,Editable = true, InitialText = InitialTestValue});
            CodeFiles.Add(new CodeFile {Order = 2, Editable = false, InitialText = ProperClass});
        }

        /// <summary>
        /// Validates the Challenge1 user input
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string Validate(List<string> code)
        {
            //check good case
            code.Add(ProperClass);
            var result = RunTests(code);
            if (!result.IsSuccess)
                return result.Message ?? "Check if tests are properly defined";

            //check bad case(s)
            code.Remove(ProperClass);
            code.Add(BadClass1);
            result = RunTests(code);
            if (result.IsSuccess)
                return "Test doesn't test all class behavior properly";

            //reaches here, means code is fine!
            return string.Empty;
        }
    }
}
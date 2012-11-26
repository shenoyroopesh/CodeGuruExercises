using System;
using System.Collections.Generic;
using CodeGuru.Exercises;

namespace NUnitExercises.Level1
{
    public class Challenge2 : NUnitChallenge, IChallenge
    {
        #region constants
        public const string InitialClass = @"
public class Person 
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }

    public Person(string name)
    {
        var names = name.Split(' ');
        FirstName = names[0];
        SecondName = names[1];
    }
}
        ";


        public const string ProperClass = @"
public class Person 
{
    public string FirstName { get; set; }
    public string SecondName { get; set; }

    public Person(string name)
    {
        var names = name.Split(' ');
        FirstName = names[0];
        SecondName = names.Length > 1 ? names[1] : """";
    }
}
        ";

        public const string BadClass1 = @"
        public class Person 
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }

            public Person(string name)
            {
            }
        }
        ";
        public const string BadClass2 = @"
        public class Person 
        {
            public string FirstName { get; set; }
            public string SecondName { get; set; }

            public Person(string name)
            {
                var names = name.Split(' ');
                FirstName = names[0];
                SecondName = names.Length > 1 ? names[1] : ""Something"";
            }
        }
        ";

        public const string InitialTestValue = @"
using NUnit.Framework;

//add necessary attributes to the test class and the method
[TestFixture]
public class PersonTest
{
    [Test]
    public void CheckConstructor()
    {
        //write test logic here
        var me = new Person(""Roopesh"");
        Assert.AreEqual(""Roopesh"", me.Name);
    }
}
";
        #endregion

        public Challenge2()
        {
            Instructions = "Modify the below test to ensure that it sufficiently tests the modified class, with two name properties instead of one";
            CodeFiles.Add(new CodeFile
                {
                    Order = 1,
                    Editable = true,
                    InitialText = InitialTestValue,
                    FileName = "PersonTest.cs"
                });

            CodeFiles.Add(new CodeFile
                {
                    Order = 2,
                    Editable = false,
                    InitialText = InitialClass,
                    FileName = "Person.cs"
                });
        }
        
        /// <summary>
        /// Validates the Challenge1 user input
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string Validate(List<string> code)
        {
            try
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

                code.Remove(BadClass1);
                code.Add(BadClass2);

                result = RunTests(code);
                if (result.IsSuccess)
                    return "Test doesn't test all class behavior properly";

                code.Remove(BadClass2);
                code.Add(InitialClass);
                result = RunTests(code);
                if (result.IsSuccess)
                    return 
                        "Test doesn't test all class behavior properly - remember one of the tests should fail so that the bug in class is exposed";
                //reaches here, means code is fine!
                return string.Empty;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
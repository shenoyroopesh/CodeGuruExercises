using System;
using System.Collections.Generic;
using CodeGuru.Exercises;

namespace NUnitExercises.Level1
{
    public class Challenge3 : NUnitChallenge, IChallenge
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


        public const string InitialTestValue = @"
using NUnit.Framework;

[TestFixture]
public class PersonTest
{
    [Test]
    public void CheckConstructor()
    {
        var me = new Person(""Roopesh Shenoy"");
        Assert.AreEqual(""Roopesh"", me.FirstName);
        Assert.AreEqual(""Shenoy"", me.SecondName);
    }

    [Test]
    public void CheckConstructorWithoutSpace()
    {
        var me = new Person(""Roopesh"");
        Assert.AreEqual(""Roopesh"", me.FirstName);
        Assert.AreEqual("""", me.SecondName);
    }
}
";
        #endregion

        public Challenge3()
        {
            Instructions = "Modify the below class to ensure that all the defined tests pass";
            CodeFiles.Add(new CodeFile
                {
                    Order = 1,
                    Editable = true,
                    InitialText = InitialClass,
                    FileName = "Person.cs"
                });

            CodeFiles.Add(new CodeFile
                {
                    Order = 2,
                    Editable = false,
                    InitialText = InitialTestValue,
                    FileName = "PersonTest.cs"
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
                code.Add(InitialTestValue);
                var result = RunTests(code);
                if (!result.IsSuccess)
                    return result.Message ?? "Check if the bug is fixed properly";

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
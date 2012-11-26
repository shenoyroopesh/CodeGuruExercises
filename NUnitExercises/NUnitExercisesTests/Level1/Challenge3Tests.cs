using System;
using CodeGuru.Exercises;
using NUnit.Framework;
using System.Linq;
using NUnitExercises.Level1;

namespace NUnitExercisesTests.Level1
{
    public class Challenge3Tests
    {
        private IChallenge _sut;

        private const string ProperInput = @"
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

        private const string ImproperInput = @"
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
        [SetUp]
        public void SetUp()
        {
            _sut = new Challenge3();
        }

        [Test]
        public void CheckProperInput()
        {
            var result = _sut.Validate(new[] {ProperInput}.ToList());
            Console.WriteLine("result: {0}", result);           
            Assert.AreEqual(string.Empty, result);
        }

        [TestCase("")]
        [TestCase(ImproperInput)]
        [TestCase(Challenge3.InitialClass)]
        public void CheckInvalidInput(string input)
        {
            var result = _sut.Validate(new[] {input}.ToList());
            Console.WriteLine(input);
            Console.WriteLine("result: {0}", result);
            Assert.IsNotNullOrEmpty(result);
        }
    }
}

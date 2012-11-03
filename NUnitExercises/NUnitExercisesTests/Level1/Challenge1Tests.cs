using System;
using CodeGuru.Exercises;
using NUnit.Framework;
using System.Linq;
using NUnitExercises.Level1;

namespace NUnitExercisesTests.Level1
{
    public class Challenge1Tests
    {
        private IChallenge _sut;

        private const string ProperInput = @"
            using NUnit.Framework;

            [TestFixture]
            public class PersonTest
            {
                [Test]
                public void CheckConstructor()
                {
                    var me = new Person(""Roopesh Shenoy"");
                    Assert.AreEqual(""Roopesh Shenoy"", me.Name);
                }
            }
";

        private const string ImproperInput1 = @"
            using NUnit.Framework;

            [TestFixture]
            public class PersonTest
            {
                [Test]
                public void CheckConstructor()
                {
                }
            }
";

        private const string ImproperInput2 = @"
            using NUnit.Framework;

            [TestFixture]
            public class PersonTest
            {
            }
";

        private const string ImproperInput3 = @"
            public class PersonTest
            {
            }
";

        [SetUp]
        public void SetUp()
        {
            _sut = new Challenge1();
        }

        [Test]
        public void CheckProperInput()
        {
            var result = _sut.Validate(new[] {ProperInput}.ToList());
            Console.WriteLine("result: {0}", result);           
            Assert.AreEqual(string.Empty, result);
        }

        [TestCase("")]
        [TestCase(ImproperInput1)]
        [TestCase(ImproperInput2)]
        [TestCase(ImproperInput3)]
        [TestCase(Challenge1.InitialTestValue)]
        public void CheckInvalidInput(string input)
        {
            var result = _sut.Validate(new[] {input}.ToList());
            Console.WriteLine("result: {0}", result);
            Assert.IsNotNullOrEmpty(result);
        }
    }
}

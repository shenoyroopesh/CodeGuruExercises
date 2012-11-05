using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CodeGuru.Exercises;
using Microsoft.CSharp;
using NUnit.Core;

namespace NUnitExercises
{
    public abstract class NUnitChallenge
    {
        public string Instructions { get; set; }

        public List<CodeFile> CodeFiles { get; set; }

        public int CourseId { get; set; }

        public int LevelNo { get; set; }

        public int ChallengeNo { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        protected NUnitChallenge()
        {
            //inject dependencies for NUnit to run successfully
            CoreExtensions.Host.InstallBuiltins();
            CodeFiles = new List<CodeFile>();
        }

        /// <summary>
        /// Validates whether the code files are valid inputs for this challenge
        /// </summary>
        /// <param name="codefiles"></param>
        /// <returns></returns>
        public TestResult RunTests(List<string> codefiles)
        {
            var assembly = BuildAssembly(codefiles.ToArray());

            //to avoid NullReferenceException - don't know why this is needed!
            TestExecutionContext.CurrentContext.TestPackage = new TestPackage(assembly.GetName().FullName);

            var suite = GetTestSuiteFromAssembly(assembly);
            return suite.Run(new NullListener(), TestFilter.Empty);
            
        }

        /// <summary>
        /// Converts a given assembly containing tests to a runnable TestSuite
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        protected static TestSuite GetTestSuiteFromAssembly(Assembly assembly)
        {
            var treeBuilder = new NamespaceTreeBuilder(new TestAssembly(assembly, assembly.GetName().FullName));
            treeBuilder.Add(GetFixtures(assembly));
            return treeBuilder.RootSuite;
        }

        /// <summary>
        /// Builds an assembly
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected static Assembly BuildAssembly(string[] code)
        {
            var compilerparams = new CompilerParameters(new[] {"nunit.framework.dll"})
                {
                    GenerateExecutable = false,
                    GenerateInMemory = true
                };

            var results = new CSharpCodeProvider()
                .CompileAssemblyFromSource(compilerparams, code);

            if (!results.Errors.HasErrors) return results.CompiledAssembly;

            throw new Exception(GetErrors(results));
        }

        /// <summary>
        /// Gets Errors from the Compiler results
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        protected static String GetErrors(CompilerResults results)
        {
            var errors = new StringBuilder("Compiler Errors :\r\n");
            foreach (CompilerError error in results.Errors)
            {
                errors.AppendFormat("Line {0},{1}\t: {2}\n", error.Line, error.Column, error.ErrorText);
            }
            return errors.ToString();
        }

        /// <summary>
        /// Creates a tree of fixtures and containing TestCases from the given assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        protected static IList GetFixtures(Assembly assembly)
        {
            return assembly.GetTypes()
                           .Where(TestFixtureBuilder.CanBuildFrom)
                           .Select(TestFixtureBuilder.BuildFrom).ToList();
        }
    }
}

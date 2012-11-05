using System.Collections.Generic;

namespace CodeGuru.Exercises
{
    public interface IChallenge
    {
        /// <summary>
        /// Course id to which this challenge belongs
        /// </summary>
        int CourseId { get; set; }

        /// <summary>
        /// Level no to which this challenge belongs
        /// </summary>
        int LevelNo { get; set; }

        /// <summary>
        /// Provides instructions to this challenge
        /// </summary>
        string Instructions { get; set; }

        /// <summary>
        /// Code files that are required to be shown to the user for this challenge
        /// </summary>
        List<CodeFile> CodeFiles { get; set; }

        /// <summary>
        /// Challenges are ordered by this
        /// </summary>
        int ChallengeNo { get; set; }

        /// <summary>
        /// Validates the user input code. Only editable code files are sent back for verification
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string Validate(List<string> userCode);
    }
}

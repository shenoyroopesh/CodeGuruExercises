namespace CodeGuru.Exercises
{
    public class CodeFile
    {
        /// <summary>
        /// Initial Text
        /// </summary>
        public string InitialText { get; set; }

        /// <summary>
        /// Whether the file is user editable or just for viewing
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// In case of multiple code files, this will decide what order the Code files are arranged in.
        /// </summary>
        public int Order { get; set; }
    }
}
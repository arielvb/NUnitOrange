namespace NUnitOrange
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    class Program
    {
        /// <summary>
        /// NUnitOrange usage
        /// </summary>
        private static string orangeUsage = "[INFO] Usage 1:  NUnitOrange \"path-to-folder\"\n[INFO] Usage 2:  NUnitOrange \"input.xml\" \"output.html\"";

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">Accepts 2 types of input arguments
        ///     Type 1:  nunitorange "path-to-folder"
        ///         args.length = 1 && args[0] is a directory
        ///     Type 2: nunitorange "input.xml" "output.html"
        ///         args.length = 2 && args[0] is xml-input && args[1] is html-output
        /// </param>
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("[ERROR] Invalid number of arguments specified.\n" + orangeUsage);
                return;
            }

            foreach (string arg in args)
            {
                if (arg.Trim() == "" || arg == "\\\\")
                {
                    Console.WriteLine("[ERROR] Invalid argument(s) specified.\n" + orangeUsage);
                    return;
                }
            }

            if (args.Length == 2)
            {
                new TestSuiteParser().SetFiles(args[0], args[1]).BuildReport();
                return;
            }

            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("{ERROR] The path of directory you have specified does not exist.\n" + orangeUsage);
                return;
            }

            new FolderParser().SetFolder(args[0]).BuildReport();
        }
    }
}

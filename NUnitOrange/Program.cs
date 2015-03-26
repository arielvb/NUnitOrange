namespace NUnitOrange
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 2)
            {
                Console.WriteLine("[ERROR] Invalid number of arguments specified.");
                Console.WriteLine("[INFO] Usage 1:  NUnitOrange \"path-to-folder\"");
                Console.WriteLine("[INFO] Usage 2:  NUnitOrange \"input.xml\" \"output.html\"");
                return;
            }

            foreach (string arg in args)
            {
                if (arg.Trim() == "" || arg == "\\\\")
                {
                    Console.WriteLine("[ERROR] Invalid argument(s) specified.");
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
                Console.WriteLine("{ERROR] The path of directory you have specified does not exist.");
                return;
            }

            new FolderParser().SetFolder(args[0]).BuildReport();
        }
    }
}

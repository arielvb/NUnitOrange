namespace NUnitOrange
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Text.RegularExpressions;

    internal class FolderParser
    {
        private string dir = "";

        public FolderParser SetFolder(string TargetDirectory)
        {
            this.dir = TargetDirectory;
            
            return this;
        }

        public void BuildReport()
        {
            List<string> allFiles = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly)
                            .Where(s => s.ToLower().EndsWith("xml"))
                            .ToList();

            if (allFiles.Count == 0)
            {
                Console.WriteLine("[INFO] No XML files were found in the given location. Exiting..");
                return;
            }

            TestSuiteParser fileParser = new TestSuiteParser();
            Dictionary<string, string> data;
            string html = HTML.FolderLevelPage.Base;

            foreach (string file in allFiles)
            {
                data = fileParser.SetFiles(file, Path.GetDirectoryName(file) + "\\" + Path.GetFileNameWithoutExtension(file) + ".html").BuildReport();

                if (data != null)
                {
                    html = html.Replace(OrangeHelper.MarkupFlag("insertResult"), HTML.FolderLevelPage.Row)
                                .Replace(OrangeHelper.MarkupFlag("fullFilename"), Path.GetFileNameWithoutExtension(file) + ".html")
                                .Replace(OrangeHelper.MarkupFlag("filename"), Path.GetFileNameWithoutExtension(file))
                                .Replace(OrangeHelper.MarkupFlag("assembly"), Path.GetFileName(data["AssemblyName"]))
                                .Replace(OrangeHelper.MarkupFlag("runresult"), data["Result"].ToLower())
                                .Replace(OrangeHelper.MarkupFlag("totalTests"), data["Total"])
                                .Replace(OrangeHelper.MarkupFlag("totalPassed"), data["Passed"])
                                .Replace(OrangeHelper.MarkupFlag("totalFailed"), data["Failed"])
                                .Replace(OrangeHelper.MarkupFlag("allOtherTests"), data["Other"])
                                .Replace(OrangeHelper.MarkupFlag("passedPercentage"), (Convert.ToInt32(data["Passed"]) * 100 / Convert.ToInt32(data["Total"])).ToString())
                                .Replace(OrangeHelper.MarkupFlag("failedPercentage"), (Convert.ToInt32(data["Failed"]) * 100 / Convert.ToInt32(data["Total"])).ToString())
                                .Replace(OrangeHelper.MarkupFlag("othersPercentage"), (Convert.ToInt32(data["Other"]) * 100 / Convert.ToInt32(data["Total"])).ToString());
                }

                Console.WriteLine("");
            }

            Console.WriteLine("\nNUnitOrange master file created!");

            File.WriteAllText(dir + "\\Index.html", html);
        }
    }
}

namespace NUnitOrange
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Creates executive level summary from all NUnit XML run files
    /// </summary>
    internal class FolderParser
    {
        /// <summary>
        /// Path to the directory where all XML files are kept
        /// </summary>
        private string inDir = "";

        /// <summary>
        /// Path to the directory where all HTML files will be stored
        /// </summary>
        private string outDir = "";


        /// <summary>
        /// Set the Target folder where all XML files are kept
        /// </summary>
        /// <param name="TargetDirectory"></param>
        /// <returns>FolderParser</returns>
        public FolderParser SetFolder(string TargetDirectory)
        {
            inDir = TargetDirectory;
            outDir = TargetDirectory;

            return this;
        }

        /// <summary>
        /// Set the Target folder where all XML files are kept
        /// </summary>
        /// <param name="TargetDirectory"></param>
        /// <returns>FolderParser</returns>
        public FolderParser SetFolder(string TargetDirectory, string ResultsDirectory)
        {
            inDir = TargetDirectory;
            outDir = ResultsDirectory;

            return this;
        }

        /// <summary>
        /// Builds the folder-level / executive-summary report from all input XML files
        /// </summary>
        public void BuildReport()
        {
            List<string> allFiles = Directory.GetFiles(inDir, "*.*", SearchOption.TopDirectoryOnly)
                            .Where(s => s.ToLower().EndsWith("xml"))
                            .ToList();

            // if no XML files, end process
            if (allFiles.Count == 0)
            {
                Console.WriteLine("[INFO] No XML files were found in the given location. Exiting..");
                return;
            }

            TestSuiteParser fileParser = new TestSuiteParser();

            // data passed from the TestSuite level parser
            Dictionary<string, string> data;

            // folder-level HTML source
            string html = HTML.FolderLevelPage.Base;

            // build report for each input file
            foreach (string file in allFiles)
            {
                data = fileParser.SetFiles(file, Path.Combine(outDir, Path.GetFileNameWithoutExtension(file) + ".html")).AddTopBar(true).BuildReport();

                if (data != null)
                {
                    if (data["Total"] == "0")
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
                                    .Replace(OrangeHelper.MarkupFlag("passedPercentage"), "0")
                                    .Replace(OrangeHelper.MarkupFlag("failedPercentage"), "0")
                                    .Replace(OrangeHelper.MarkupFlag("othersPercentage"), "0");
                    }
                    else
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
                }
            }
            string outputfile = Path.Combine(outDir, "Index.html");
            // write the entire source with all fixture/test-suite level data row-wise
            File.WriteAllText(outputfile, html);

            Console.WriteLine("\nNUnitOrange executive summary created: " + outputfile);
        }
    }
}

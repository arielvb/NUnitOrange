namespace NUnitOrange
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Text.RegularExpressions;
    
    internal class TestSuiteParser
    {
        private XmlDocument doc; 
        private string NUnitResultFile = "";
        private string OrangeFile = "";
        private Dictionary<string, string> data;

        public TestSuiteParser SetFiles(string NUnitResultFile, string OrangeFile)
        {
            if (!File.Exists(NUnitResultFile) || Path.GetExtension(NUnitResultFile).ToLower() == "xml")
            {
                throw new Exception("[ERROR] Input file does not exist or is invalid: " + NUnitResultFile);
            }

            this.NUnitResultFile = NUnitResultFile;
            this.OrangeFile = OrangeFile;

            if (doc == null) doc = new XmlDocument();

            try
            {
                doc.Load(NUnitResultFile);
            }
            catch (Exception)
            {
                Console.WriteLine("\n[ERROR] Skipping " + NUnitResultFile + ". It is not a valid NUnit TestResult XML file.");
            }

            return this;
        }

        public Dictionary<string, string> BuildReport()
        {
            if (doc.DocumentElement == null)
            {
                return null;
            }

            if (data == null)
            {
                data = new Dictionary<string, string>();
            }

            data.Clear();

            Console.WriteLine("\n[INFO] Processing file '" + NUnitResultFile + "'..");

            int totalTests = doc.GetElementsByTagName("test-case").Count;

            if (totalTests >= 1)
            {
                Console.WriteLine("[INFO] Processing root and test-suite elements...");

                string html = HTML.TestSuiteLevelPage.Base;
                int passed = doc.SelectNodes(".//test-case[@result='Success' or @result='Passed']").Count;
                int failed = doc.SelectNodes(".//test-case[@result='Failed' or @result='Failure']").Count;
                int inconclusive = doc.SelectNodes(".//test-case[@result='Inconclusive' or @result='NotRunnable']").Count;
                int skipped = doc.SelectNodes(".//test-case[@result='Skipped' or @result='Ignored']").Count;
                int errors = doc.SelectNodes(".//test-case[@result='Error']").Count;
                string runResult = doc.SelectNodes("//test-suite")[0].Attributes["result"].InnerText;
                string name = doc.SelectNodes("//test-suite")[0].Attributes["name"].InnerText;
                string timeTaken = "";

                data.Add("Total", totalTests.ToString()); 
                data.Add("Passed", passed.ToString());
                data.Add("Failed", failed.ToString());
                data.Add("Other", (inconclusive + skipped + errors).ToString());
                data.Add("Result", runResult);
                data.Add("AssemblyName", name);

                Console.WriteLine("[INFO] Number of tests: " + totalTests);

                try { timeTaken = doc.SelectNodes("//test-suite")[0].Attributes["duration"].InnerText; }
                catch { try { timeTaken = doc.SelectNodes("//test-suite")[0].Attributes["time"].InnerText; } catch { } }

                html = html.Replace(OrangeHelper.MarkupFlag("totalTests"), totalTests.ToString())
                                .Replace(OrangeHelper.MarkupFlag("passed"), passed.ToString())
                                .Replace(OrangeHelper.MarkupFlag("failed"), failed.ToString())
                                .Replace(OrangeHelper.MarkupFlag("inconclusive"), inconclusive.ToString())
                                .Replace(OrangeHelper.MarkupFlag("skipped"), skipped.ToString())
                                .Replace(OrangeHelper.MarkupFlag("errors"), errors.ToString())
                                .Replace(OrangeHelper.MarkupFlag("inXml"), Path.GetFullPath(NUnitResultFile))
                                .Replace(OrangeHelper.MarkupFlag("duration"), timeTaken)
                                .Replace(OrangeHelper.MarkupFlag("result"), runResult)
                                .Replace(OrangeHelper.MarkupFlag("name"), name);

                try
                {
                    XmlNode env = doc.GetElementsByTagName("environment")[0];

                    html = html.Replace(OrangeHelper.MarkupFlag("userDomain"), env.Attributes["user-domain"].InnerText)
                                .Replace(OrangeHelper.MarkupFlag("user"), env.Attributes["user"].InnerText)
                                .Replace(OrangeHelper.MarkupFlag("machineName"), env.Attributes["machine-name"].InnerText)
                                .Replace(OrangeHelper.MarkupFlag("platform"), env.Attributes["platform"].InnerText)
                                .Replace(OrangeHelper.MarkupFlag("osVersion"), env.Attributes["os-version"].InnerText)
                                .Replace(OrangeHelper.MarkupFlag("clrVersion"), env.Attributes["clr-version"].InnerText)
                                .Replace(OrangeHelper.MarkupFlag("nunitVersion"), env.Attributes["nunit-version"].InnerText);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[ERROR] There was an error processing the _ENVIRONMENT_ node: " + ex.Message);
                }

                BuildFixtureBlocks(html);
            }
            else
            {
                Console.WriteLine("[INFO] There are no tests present in your input XML. No report created.");

                return null;
            }

            return data;
        }

        private void BuildFixtureBlocks(string html)
        {
            Console.WriteLine("[INFO] Building fixture blocks...");

            string pre = null;
            ArrayList fixtureStatus = new ArrayList();
            XmlNodeList testSuite = doc.SelectNodes("//test-suite[@type='TestFixture']");
            int testCount = 0;

            foreach (XmlNode suite in testSuite)
            {
                html = html.Replace(OrangeHelper.MarkupFlag("fixtureresult"), OrangeHelper.GetFixtureStatus(fixtureStatus))
                                    .Replace(OrangeHelper.MarkupFlag("inserttest"), "")
                                    .Replace(OrangeHelper.MarkupFlag("insertfixture"), HTML.TestSuiteLevelPage.Fixture)
                                    .Replace(OrangeHelper.MarkupFlag("fixturename"), suite.Attributes["name"].InnerText);

                fixtureStatus.Clear();

                foreach (XmlNode testcase in suite.SelectNodes(".//test-case"))
                {
                    fixtureStatus.Add(testcase.Attributes["result"].InnerText);
                    pre = "";

                    if (testcase.SelectNodes(".//message").Count == 1)
                    {
                        pre = testcase.SelectNodes(".//message").Count == 1 ? "<pre>" + testcase.SelectNodes(".//message")[0].InnerText : "";
                        pre += testcase.SelectNodes(".//stack-trace").Count == 1 ? " -> " + testcase.SelectNodes(".//stack-trace")[0].InnerText.Replace("\r", "").Replace("\n", "") : "";
                        pre += "</pre>";

                        if (pre == "<pre></pre>")
                            pre = "";
                    }

                    html = html.Replace(OrangeHelper.MarkupFlag("inserttest"), HTML.TestSuiteLevelPage.Test)
                            .Replace(OrangeHelper.MarkupFlag("testname"), testcase.Attributes["name"].InnerText.Replace("<", "[").Replace(">", "]"))
                            .Replace(OrangeHelper.MarkupFlag("teststatus"), testcase.Attributes["result"].InnerText.ToLower())
                            .Replace(OrangeHelper.MarkupFlag("teststatusmsg"), pre);

                    Console.Write("\r{0} tests processed...", ++testCount);
                }
            }

            html = html.Replace(OrangeHelper.MarkupFlag("fixtureresult"), OrangeHelper.GetFixtureStatus(fixtureStatus));

            File.WriteAllText(OrangeFile, html);
        }

        public TestSuiteParser() {}
    }
}

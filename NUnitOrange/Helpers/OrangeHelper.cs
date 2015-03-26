namespace NUnitOrange
{
    using System;
    using System.Collections;
    using System.Xml;

    class OrangeHelper
    {
        // icons from font-awesome
        // http://fortawesome.github.io/Font-Awesome/
        public static string FontIco(string status)
        {
            switch (status.ToLower())
            {
                case "passed": return "fa-check";
                case "failed": return "fa-times";
                case "skipped": return "fa-angle-double-right";
                case "inconclusive": return "fa-question";
                default: return "info";
            }
        }

        // fixture level status codes
        public static string GetFixtureStatus(ArrayList list)
        {
            if (list.Contains("Failed") || list.Contains("Failure")) return "failed";
            if (list.Contains("Error")) return "error";
            if (list.Contains("Inconclusive") || list.Contains("NotRunnable")) return "inconclusive";
            if (list.Contains("Passed") || list.Contains("Success")) return "passed";
            if (list.Contains("Skipped") || list.Contains("Ignored") || list.Contains("NotRun") || list.Contains("Not-Run")) return "skipped";
            else
                return "unknown";
        }

        public static string MarkupFlag(string name)
        {
            return "<!--%" + name.ToUpper() + "%-->";
        }
    }
}

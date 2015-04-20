NUnitOrange creates masonry style HTML reports for NUnit. Its a simple Console application that creates easy to use, attractive dashboard. It shows the XML output from NUnit in a presentable manner with charts, graphs and tables. View <a href='http://relevantcodes.com/Tools/NUnitOrange/reports/Index.html'>folder-level sample</a> and <a href='http://relevantcodes.com/Tools/NUnitOrange/reports/TestResult.html'>test-suite level</a> samples from the following <a href='http://relevantcodes.com/Tools/NUnitOrange/TestResult.xml'>NUnit XML report</a>.


### Download Exe / NuGet

Download using <a href='http://relevantcodes.com/nunit-orange-nunit-html-report-generator/'>this</a> link.

### Usage: Building Folder-Level Summary
To build a summary for all NUnit TestResult files, simply open cmd.exe and point to the folder where the XML files are stored:

```
nunitorange "path-to-folder"
nunitorange "C:\InputXMLs"
nunitorange .  // for current folder

nunitorange "input-folder" "output-folder"
nunitorange "C:\InputXMLs" "C:\OutputXMLs"
```

### Usage: Building TestSuite-Level Summary

To build report from any NUnit TestResult XML file, point to the input file and also specify the name of the output file:

```
nunitorange "input.xml" "output.html"
nunitorange "C:\InputXMLs\TestResult.xml" "C:\InputXMLs\TestResult.html"
```


### Snapshots

##### Folder-level

Below is a snapshot of the folder-level summary, which you can view live <a href='http://relevantcodes.com/Tools/NUnitOrange/reports/Index.html'>here</a>:

![](http://relevantcodes.com/Tools/NUnitOrange/folder.png)

##### TestSuite-level

Below is a snapshot of the testsuite-level summary, which you can view live <a href='http://relevantcodes.com/Tools/NUnitOrange/reports/TestResult.html'>here</a>.

![](http://relevantcodes.com/Tools/NUnitOrange/testsuite.png)


### Software License Agreement (BSD License)
Copyright 2015 Anshoo Arora (Relevant Codes)

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.  
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL Anshoo Arora or RelevantCodes.com BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

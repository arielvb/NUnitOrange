namespace NUnitOrange.HTML
{
    /// <summary>
    /// Creates a summary for the executive summary
    /// </summary>
    internal class FolderLevelPage
    {
        /// <summary>
        /// Main source written to the executive-summary level report
        /// </summary>
        internal static string Base
        {
            get
            {
                return @"<!DOCTYPE html>
                        <html xmlns='http://www.w3.org/1999/xhtml' xml:lang='en'> 
                        <!--
	                        NUnit Orange Library [Folder Summary] v2.0 | http://relevantcodes.com/nunit-orange-nunit-html-report-generator/ | https://github.com/relevantcodes/
                            Owners:  Anshoo Arora
                            Contributor:  Shaiju Janardhanan
                        --> 
	                        <head>
		                        <title>NUnit Orange Executive Report</title>
		                        <link href='http://fonts.googleapis.com/css?family=Source+Sans+Pro:400,600|Montserrat' rel='stylesheet' type='text/css' />
		                        <link href='http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css' rel='stylesheet' />
		                        <link href='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/css/bootstrap.min.css' rel='stylesheet'>
		                        <style type='text/css'>
			                        html {overflow-y: scroll;}
			                        body {font-family: 'Source Sans Pro', Arial;font-size: 15px; line-height: 1.3; margin: 0;}
			                        table {border-collapse: collapse;width: 100%;}
			                        /*---:[ containers ]:---*/
			                        #orange-container {margin: 0;padding: 0;width: 100%;}
			                        .header, .dashboard, .content {margin: 0 auto;width: 1053px;}
			                        /*---:[ header ]:---*/
			                        .header { font-size: 14px; margin-top: -1px; padding-bottom: 15px;}
			                        #title { margin-top: 30px; }
                                    .title-orange { color: #ef5f3c; font-family: Montserrat; }
			                        .header .name {color: #bbb;float: right;font-size: 15px; margin-top: -18px;}
			                        /*---:[ dashboard ]:---*/
			                        #dashboard {background: #f9f9f9;border-bottom: 1px solid #ddd;margin: 20px 0 40px;}
			                        .dashboard {padding: 20px 0 0;text-align: center;}
			                        .dashboard > div {display: inline-block;}
			                        /*---:[ content ]:---*/
			                        .orange-table tr {border-bottom: 1px solid #ccc;}
			                        .totals-row {background-color: #f9f9f9;font-weight: 600;}
			                        .orange-table th {background-color: #444;color: #fff;font-size: 13px;padding: 6px 14px;text-align: left;text-transform: uppercase;}
			                        .orange-table td {padding: 17px 12px;word-break: break-all;word-wrap: break-word;}
			                        .orange-table td:last-child {min-width: 250px;}
			                        .totals-row td {font-size: 14px;padding: 8px 12px;}
			                        .progress {margin-bottom: 0;}
			                        .label {font-size: 13px;font-weight:600;padding: 2px 7px;text-transform:capitalize;}
			                        .failed > .label, .failure > .label {background-color: #d9534f;}
			                        .passed > .label, .success > .label {background-color: #5cb85c;}
		                        </style>
	                        </head>
	                        <body>
		                        <div id='orange-container'>
			                        <div class='orange-container'>
				                        <div id='header'>
					                        <div class='header'>
						                        <div id='title'>
							                        <i class='fa fa-desktop' style='margin-right:10px;'></i><span>NUnit<span class='title-orange'>Orange.</span></span>
						                        </div>
						                        <div class='name'>
							                        Folder Summary
						                        </div>
					                        </div>
				                        </div>
				                        <div id='dashboard'>
					                        <div class='dashboard'>
						                        <div id='file-summary'></div>
						                        <div id='test-summary'></div>
					                        </div>
				                        </div>
				                        <div id='content'>
					                        <div class='content'>
						                        <div class='summary'>
							                        <table class='orange-table'>
								                        <tr>
									                        <th>File</th>
									                        <th>Assembly</th>
									                        <th>Result</th>
									                        <th>Total</th>
									                        <th>Passed</th>
									                        <th>Failed</th>
									                        <th>Other</th>
									                        <th>Quick Status</th>
								                        </tr>
								                        <!--%INSERTRESULT%-->
								                        <tr class='totals-row'>
									                        <td>TOTAL</td>
									                        <td></td>
									                        <td></td>
									                        <td class='totals-all'></td>
									                        <td class='totals-passed' ></td>
									                        <td class='totals-failed'></td>
									                        <td class='totals-others'></td>
									                        <td></td>
								                        </tr>
							                        </table>
						                        </div>
					                        </div>
				                        </div>
			                        </div>
		                        </div>
	                        </body>
	                        <script type='text/javascript' src='http://code.jquery.com/jquery-1.10.1.min.js'></script>
	                        <script type='text/javascript' src='https://www.google.com/jsapi'></script>
	                        <script src='https://maxcdn.bootstrapcdn.com/bootstrap/3.3.4/js/bootstrap.min.js'></script>
	                        <script type='text/javascript'>
		                        google.load('visualization', '1', {packages:['corechart']});
		                        $(document).ready(function() {
			                        function testCountByStatus(status) {
				                        var sum = 0;
				                        $(status).each(function() {
					                        sum += Number($(this).text());
				                        });
				                        return sum;
			                        }
			                        $('.totals-all').text(testCountByStatus('td.total-count'));
			                        $('.totals-passed').text(testCountByStatus('td.pass-count'));
			                        $('.totals-failed').text(testCountByStatus('td.fail-count'));
			                        $('.totals-others').text(testCountByStatus('td.others-count'));
			                        google.setOnLoadCallback(fileSummary);
			                        google.setOnLoadCallback(testSummary);
			                        function fileSummary() {
				                        var data = google.visualization.arrayToDataTable([
				                          ['File Status', 'Count'],
				                          ['Pass', $('td.file-status.pass').length + $('td.file-status.passed').length + $('td.file-status.success').length],
				                          ['Fail', $('td.file-status.fail').length + $('td.file-status.failure').length + $('td.file-status.failed').length]
				                        ]);
				                        var options = {
				                          backgroundColor: { fill:'transparent' },
				                          chartArea: {'width': '65%', 'height': '65%'},
				                          colors: ['green', 'tomato', 'orange', 'red', '#999'],
				                          fontSize: '11',
				                          height: 275,
				                          pieHole: 0.6,
				                          pieSliceText: 'value', 
				                          title: 'OVERALL SUMMARY', 
				                          width: 450
				                        };
				                        var chart = new google.visualization.PieChart(document.getElementById('file-summary'));
				                        chart.draw(data, options);
			                          }
			                          function testSummary() {
				                        var data = google.visualization.arrayToDataTable([
				                          ['File Status', 'Count'],
				                          ['Pass', testCountByStatus('td.pass-count')],
				                          ['Fail', testCountByStatus('td.fail-count')],
				                          ['Other',  testCountByStatus('td.others-count')]
				                        ]);
				                        var options = {
				                          backgroundColor: { fill:'transparent' },
				                          chartArea: {'width': '65%', 'height': '65%'},
				                          colors: ['green', 'tomato', 'orange', 'red', '#999'],
				                          fontSize: '11',
				                          height: 275,
				                          pieHole: 0.6,
				                          pieSliceText: 'value', 
				                          title: 'TEST SUMMARY', 
				                          width: 450
				                        };
				                        var chart = new google.visualization.PieChart(document.getElementById('test-summary'));
				                        chart.draw(data, options);
			                          }
		                        })
	                        </script>
                        </html>";
            }
        }

        /// <summary>
        /// Each row represents an input file from NUnit
        /// </summary>
        public static string Row
        {
            get
            {
                return @"<tr>
	                        <td><a href='<!--%FULLFILENAME%-->'><!--%FILENAME%--></a></td>
	                        <td><!--%ASSEMBLY%--></td>
	                        <td class='file-status <!--%RUNRESULT%-->'><span class='label'><!--%RUNRESULT%--></span></td>
	                        <td class='total-count'><!--%TOTALTESTS%--></td>
	                        <td class='pass-count'><!--%TOTALPASSED%--></td>
	                        <td class='fail-count'><!--%TOTALFAILED%--></td>
	                        <td class='others-count'><!--%ALLOTHERTESTS%--></td>
	                        <td>
		                        <div class='progress'>
		                            <div class='progress-bar progress-bar-success' style='width: <!--%PASSEDPERCENTAGE%-->%'><span class='sr-only'><!--%PASSEDPERCENTAGE%-->%</span><!--%TOTALPASSED%--></div>
                                    <div class='progress-bar progress-bar-danger' style='width: <!--%FAILEDPERCENTAGE%-->%'><span class='sr-only'><!--%FAILEDPERCENTAGE%-->%</span><!--%TOTALFAILED%--></div>
                                    <div class='progress-bar progress-bar-warning' style='width: <!--%OTHERSPERCENTAGE%-->%'><span class='sr-only'><!--%OTHERSPERCENTAGE%-->%</span><!--%ALLOTHERTESTS%--></div>
		                        </div>
	                        </td>
                        </tr>
                        <!--%INSERTRESULT%-->";
            }
        }
    }
}

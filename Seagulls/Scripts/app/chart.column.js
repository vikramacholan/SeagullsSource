$(document).ready(function () {
    // Load the Visualization API and the corechart package.
    google.charts.load('current', { 'packages': ['corechart'] });
    
    $("#PlayerId").change(function () {
            // Load the Visualization API and the corechart package.
            google.charts.load('current', { 'packages': ['corechart'] });

            // Set a callback to run when the Google Visualization API is loaded.
            google.charts.setOnLoadCallback(drawChart);
    });  
});

function drawChart() {
    
    //Make Ajax call to get player details and render chart
    var playerId = $("#PlayerId").val();
    if (playerId == "")
        return;
        
    var currentUrl = url + "?playerId=" + playerId;

    $.getJSON(currentUrl, function (chartData) {
      
        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Opponent');
        data.addColumn('number', 'Runs');

        for (var i = 0; i < chartData.Batting.length; i++) {
            data.addRow([chartData.Batting[i].Opponent, chartData.Batting[i].RunsScored]);
        }

        var options = {
            'title': 'Batting', 
            'width': 800,
            'height': 300
        };

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.ColumnChart(document.getElementById('bat_chart_div'));
        chart.draw(data, options);


        var data = new google.visualization.DataTable();
        data.addColumn('string', 'Opponent');
        data.addColumn('number', 'Wickets');

        for (var i = 0; i < chartData.Bowling.length; i++) {
            data.addRow([chartData.Bowling[i].Opponent, chartData.Bowling[i].Wickets]);
        }

        var options = {
            'title': 'Bowling',
            'width': 800,
            'height': 300
        };

        // Instantiate and draw our chart, passing in some options.
        var chart = new google.visualization.ColumnChart(document.getElementById('bowl_chart_div'));
        chart.draw(data, options);
    })
}
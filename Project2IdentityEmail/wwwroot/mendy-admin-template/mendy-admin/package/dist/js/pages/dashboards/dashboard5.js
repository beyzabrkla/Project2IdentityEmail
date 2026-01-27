$(function () {
    'use strict';

    function randomData(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }

    // line chart1

    var chart = document.getElementById('crypto-chart').getContext('2d')

    var data = {
        labels: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12', '13', '14', '15', '16', '17', '18', '19', '20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31'],
        datasets: [{
            label: 'Bitcoin',
            backgroundColor: 'transparent',
            pointRadius: 2,
            lineTension: 0,
            borderWidth: 2,
            borderColor: '#30ccff',
            data: [randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20)]
        }, {
            label: 'Ethereum',
            backgroundColor: 'transparent',
            pointRadius: 2,
            lineTension: 0,
            borderWidth: 2,
            borderColor: '#c581fc',
            data: [randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20)]
        }, {
            label: 'Ripple',
            backgroundColor: 'transparent',
            pointRadius: 2,
            lineTension: 0,
            borderWidth: 2,
            borderColor: '#ffaf30',
            data: [randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20), randomData(0, 20)]
        }]
    };

    var options = {
        maintainAspectRatio: false,
        animation: {
            easing: 'easeInOutQuad',
            duration: 520
        },
        scales: {
            xAxes: [{

            }],
            yAxes: [{

                ticks: {
                    min: 0,
                    max: 20,
                    stepSize: 5,
                    suggestedMax: 25,
                    callback: function (label, index, labels) {
                        return label + '%';
                    },
                    beginAtZero: true
                }
            }]
        },
        elements: {
            line: {
                tension: 0.4
            }
        },
        legend: {
            display: false
        }
    };

    var chartInstance = new Chart(chart, {
        type: 'line',
        data: data,
        options: options
    });

});
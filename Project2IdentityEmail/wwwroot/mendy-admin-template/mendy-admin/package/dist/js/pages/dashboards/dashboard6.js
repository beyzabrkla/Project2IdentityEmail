$(function () {
    'use strict';

    function randomData(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }

    var bar_ctx = document.getElementById('bar-chart').getContext('2d');

    var primary_gradient = bar_ctx.createLinearGradient(0, 0, 0, 200);
    primary_gradient.addColorStop(0.5, '#f158d0');
    primary_gradient.addColorStop(1, '#c581fc');

    var info_gradient = bar_ctx.createLinearGradient(0, 0, 0, 200);
    info_gradient.addColorStop(0.5, '#30ccff');
    info_gradient.addColorStop(1, '#5a9cff');

    var bar_chart = new Chart(bar_ctx, {
        type: 'bar',
        data: {
            labels: [
                'Jan',
                'Feb',
                'Mar',
                'Apr',
                'May',
                'Jun',
                'Jul',
                'Aug',
                'Sep',
                'Oct',
                'Nov',
                'Dec'
            ],
            datasets: [{
                label: 'Product A',
                data: [1.1, 1.4, 1.1, 0.9, 2.1, 1, 1.8, 1.2, 2, 1.9, 1.1, 0.9],
                backgroundColor: primary_gradient,
                hoverBackgroundColor: primary_gradient,
                hoverBorderWidth: 2,
                hoverBorderColor: '#c581fc'
            }, {
                label: 'Product B',
                data: [1.9, 1.3, 0.8, 1.4, 1.1, 1.9, 0.6, 0.9, 1.5, 0.9, 0.6, 1.7],
                backgroundColor: info_gradient,
                hoverBackgroundColor: info_gradient,
                hoverBorderWidth: 2,
                hoverBorderColor: '#5a9cff'
            }]
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false
            },
            scales: {
                xAxes: [{
                    gridLines: {
                        display: false
                    },
                    barPercentage: 0.9,
                    categoryPercentage: 0.6
                }],
                yAxes: [{
                    gridLines: {
                        drawBorder: false
                    },
                    ticks: {
                        min: 0,
                        max: 2,
                        stepSize: 1,
                        suggestedMax: 2.5,
                        callback: function (label, index, labels) {
                            return label + 'k';
                        },
                        beginAtZero: true
                    }
                }]
            }
        }
    });

    // line chart1

    var chart = document.getElementById('weather').getContext('2d'),
        gradient = chart.createLinearGradient(0, 0, 0, 130);

    //gradient.addColorStop(0, '#30ccff');
    gradient.addColorStop(0.7, 'rgba(255, 255, 255, 0.3)');

    var data = {
        labels: ['1 AM', '5 AM', '10 AM', '3 PM', '8 PM'],
        datasets: [{
            label: 'Temp(C)',
            backgroundColor: gradient,
            pointRadius: 2,
            borderWidth: 2,
            borderColor: '#fff',
            data: [22, 20, 26, 25, 19]
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
                display: false
            }],
            yAxes: [{
                display: false,
                ticks: {
                    min: 0,
                    max: 30,
                    stepSize: 10,
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

    // campaign chart

    var bar_campaign = document.getElementById('campaign').getContext('2d');

    var primary_gradient = bar_campaign.createLinearGradient(0, 0, 0, 60);
    primary_gradient.addColorStop(0.5, '#f158d0');
    primary_gradient.addColorStop(1, '#c581fc');

    var bar_chart = new Chart(bar_campaign, {
        type: 'bar',
        data: {
            labels: [
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                '10',
                '11',
                '12',
                '13',
                '14',
                '15',
                '16',
                '17',
                '18',
                '19',
                '20'
            ],
            datasets: [{
                label: 'Cost',
                data: [1.1, 1.4, 1.1, 0.9, 2.1, 1, 0.3, 1.2, 2, 1.5, 1.1, 0.9, 1.1, 1.4, 1.1, 0.4, 1, 1.3, 0.6, 1.7],
                backgroundColor: primary_gradient,
                hoverBackgroundColor: primary_gradient,
                hoverBorderWidth: 2,
                hoverBorderColor: '#c581fc'
            }]
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false
            },
            tooltips: {
                enabled: false
            },
            scales: {
                xAxes: [{
                    display: false,
                    barPercentage: 0.7
                }],
                yAxes: [{
                    display: false
                }]
            }
        }
    });

    // earnings chart

    var bar_earnings = document.getElementById('earnings').getContext('2d');

    var primary_gradient = bar_earnings.createLinearGradient(0, 0, 0, 80);
    primary_gradient.addColorStop(0.5, '#f158d0');
    primary_gradient.addColorStop(1, '#c581fc');

    var bar_chart = new Chart(bar_earnings, {
        type: 'bar',
        data: {
            labels: [
                '1',
                '2',
                '3',
                '4',
                '5',
                '6',
                '7',
                '8',
                '9',
                '10',
                '11',
                '12'
            ],
            datasets: [{
                label: 'Cost',
                data: [1.1, 1.4, 1.1, 0.9, 2.1, 1, 0.3, 1.2, 2, 1.5, 1.1, 0.9],
                backgroundColor: primary_gradient,
                hoverBackgroundColor: primary_gradient,
                hoverBorderWidth: 2,
                hoverBorderColor: '#c581fc'
            }]
        },
        options: {
            maintainAspectRatio: false,
            legend: {
                display: false
            },
            tooltips: {
                enabled: false
            },
            scales: {
                xAxes: [{
                    display: false,
                    barPercentage: 0.2
                }],
                yAxes: [{
                    display: false
                }]
            }
        }
    });

    // ==============================================================
    // sales ratio
    // ==============================================================
    var chart = new Chartist.Line(
        '.pro-sales', {
            labels: ['16/08', '17/08', '18/08', '19/08', '20/08'],
            series: [
                [randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)],
                [randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)]
            ]
        }, {
            low: 0,
            high: 48,
            showArea: true,
            fullWidth: true,
            plugins: [Chartist.plugins.tooltip()],
            axisY: {
                onlyInteger: true,
                scaleMinSpace: 40,
                offset: 20,
                labelInterpolationFnc: function (value) {
                    return value / 10 + 'k';
                }
            }
        }
    );

    // Offset x1 a tiny amount so that the straight stroke gets a bounding box
    // Straight lines don't get a bounding box
    // Last remark on -> http://www.w3.org/TR/SVG11/coords.html#ObjectBoundingBox
    chart.on('draw', function (ctx) {
        if (ctx.type === 'area') {
            ctx.element.attr({
                x1: ctx.x1 + 0.001
            });
        }
    });

    // Create the gradient definition on created event (always after chart re-render)
    chart.on('created', function (ctx) {
        var defs = ctx.svg.elem('defs');
        defs
            .elem('linearGradient', {
                id: 'gradient',
                x1: 0,
                y1: 1,
                x2: 0,
                y2: 0
            })
            .elem('stop', {
                offset: 0,
                'stop-color': 'rgba(255, 255, 255, 1)'
            })
            .parent()
            .elem('stop', {
                offset: 1,
                'stop-color': 'rgba(64, 196, 255, 1)'
            });
    });

    var chart = [chart];
});
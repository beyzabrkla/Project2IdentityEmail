$(function () {
    'use strict';

    function randomData(min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }

    // ==============================================================
    // sales ratio
    // ==============================================================
    var chart = new Chartist.Line(
        '.pro-sales-2', {
            labels: ['16/04', '17/04', '18/04', '19/04', '20/04', '21/04', '22/04', '23/04'],
            series: [
                [randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)],
                [randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)]
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

    var chartinit = [chart];

    // ==============================================================
    // Badnwidth usage
    // ==============================================================
    new Chartist.Line(
        '.usage-2', {
            labels: ['0', '4', '8', '12', '16', '20', '24', '30'],
            series: [
                [randomData(0, 10), randomData(0, 10), randomData(0, 10), randomData(0, 10), randomData(0, 10), randomData(0, 10), randomData(0, 10), randomData(0, 10)]
            ]
        }, {
            high: 10,
            low: 0,
            showArea: true,
            fullWidth: true,
            plugins: [Chartist.plugins.tooltip()], // As this is axis specific we need to tell Chartist to use whole numbers only on the concerned axis
            axisY: {
                onlyInteger: true,
                offset: 20,
                showLabel: false,
                showGrid: false,
                labelInterpolationFnc: function (value) {
                    return value / 1 + 'k';
                }
            },
            axisX: {
                showLabel: false,
                divisor: 1,
                showGrid: false,
                offset: 0
            }
        }
    );
    // ==============================================================
    // Download count
    // ==============================================================
    var sparklineLogin = function () {
        $('.spark-count').sparkline(
            [4, 5, 0, 10, 9, 12, 4, 9, 4, 5, 3, 10, 9, 12, 10, 9, 12, 4, 9], {
                type: 'bar',
                width: '100%',
                height: '70',
                barWidth: '2',
                resize: true,
                barSpacing: '6',
                barColor: '#c581fc'
            }
        );
    };

    sparklineLogin();

    // ============================================================== 
    // world map
    // ==============================================================
    $('#usa').vectorMap({
        map: 'us_aea_en',
        backgroundColor: 'transparent',
        zoomOnScroll: false,
        regionStyle: {
            initial: {
                fill: '#c581fc'
            }
        },
        markerStyle: {
            initial: {
                r: 5,
                'fill': '#fff',
                'fill-opacity': 1,
                'stroke': '#fff',
                'stroke-width': 1,
                'stroke-opacity': 1
            },
        },
        enableZoom: true,
        hoverColor: '#fff',
        markers: [{
            latLng: [31.96, -99.90],
            name: 'Texas',
            style: {
                fill: '#30ccff'
            }
        }, {
            latLng: [43.07, -107.29],
            name: 'Wyoming',
            style: {
                fill: '#f158d0'
            }
        }, {
            latLng: [40.63, -89.39],
            name: 'Illinois',
            style: {
                fill: '#ff7430'
            }
        }],
    });
});
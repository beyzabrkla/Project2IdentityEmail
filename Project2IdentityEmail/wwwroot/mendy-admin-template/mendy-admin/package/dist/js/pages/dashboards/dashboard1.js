$(function () {
	'use strict';

	function randomData(min, max) {
		return Math.floor(Math.random() * (max - min + 1) + min);
	}

	// ==============================================================
	// sales ratio
	// ==============================================================
	var chart = new Chartist.Line(
		'.sales', {
			labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul'],
			series: [
				[randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)],
				[randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)],
				[randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)],
				[randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48), randomData(0, 48)]
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
					return value * 10;
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

	// ==============================================================
	// campaign
	// ==============================================================

	var chart = c3.generate({
		bindto: '#campaign',
		data: {
			columns: [
				['Vermont', 25],
				['Arizona', 20],
				['New York', 10],
				['Washington', 15],
				['Illinois', 15],
				['Nevada', 15]
			],

			type: 'donut',
			tooltip: {
				show: true
			}
		},
		donut: {
			label: {
				show: false
			},
			title: 5400,
			width: 45
		},

		legend: {
			hide: true
		},
		color: {
			pattern: [
				'#ff7430',
				'#f158d0',
				'#c581fc',
				'#30ccff',
				'#5cd050',
				'#ffaf30'
			]
		}
	});
});
$(function () {
	'use strict';

	var bar_ctx = document.getElementById('bar-chart').getContext('2d');

	var purple_orange_gradient = bar_ctx.createLinearGradient(0, 0, 0, 200);
	purple_orange_gradient.addColorStop(0.5, '#f158d0');
	purple_orange_gradient.addColorStop(1, '#c581fc');

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
				label: 'Performance Grade',
				data: [1.1, 1.4, 1.1, 0.9, 2.1, 1, 1.8, 1.2, 2, 1.9, 1.1, 0.9],
				backgroundColor: purple_orange_gradient,
				hoverBackgroundColor: purple_orange_gradient,
				hoverBorderWidth: 2,
				hoverBorderColor: '#c581fc'
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
					barPercentage: 1.0,
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

	var chart = document.getElementById('chart1').getContext('2d'),
		gradient = chart.createLinearGradient(0, 0, 0, 130);

	gradient.addColorStop(0, '#30ccff');
	gradient.addColorStop(0.7, 'rgba(255, 255, 255, 0.5)');

	var data = {
		labels: ['January', 'February', 'March', 'April', 'May', 'June'],
		datasets: [{
			label: 'Custom Label Name',
			backgroundColor: gradient,
			pointRadius: 0,
			borderWidth: 2,
			borderColor: '#30ccff',
			data: [0.7, 1, 1.2, 0.8, 1.9, 1.7]
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
					max: 2,
					stepSize: 1,
					suggestedMax: 2.5,
					callback: function (label, index, labels) {
						return label + 'k';
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

	// line chart2

	var chart2 = document.getElementById('chart2').getContext('2d'),
		gradient2 = chart2.createLinearGradient(0, 0, 0, 130);

	gradient2.addColorStop(0, '#c581fc');
	gradient2.addColorStop(0.7, 'rgba(255, 255, 255, 0.5)');

	var data2 = {
		labels: ['January', 'February', 'March', 'April', 'May', 'June'],
		datasets: [{
			label: 'Custom Label Name',
			backgroundColor: gradient2,
			pointRadius: 0,
			borderWidth: 2,
			borderColor: '#c581fc',
			data: [0.8, 1.1, 1.7, 1.2, 0.9, 0.7]
		}]
	};

	var options2 = {
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
					max: 2,
					stepSize: 1,
					suggestedMax: 2.5,
					callback: function (label, index, labels) {
						return label + 'k';
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

	var chartInstance2 = new Chart(chart2, {
		type: 'line',
		data: data2,
		options: options2
	});

	// line chart3

	var chart3 = document.getElementById('chart3').getContext('2d'),
		gradient3 = chart3.createLinearGradient(0, 0, 0, 130);

	gradient3.addColorStop(0, '#f158d0');
	gradient3.addColorStop(0.7, 'rgba(255, 255, 255, 0.5)');

	var data3 = {
		labels: ['January', 'February', 'March', 'April', 'May', 'June'],
		datasets: [{
			label: 'Custom Label Name',
			backgroundColor: gradient3,
			pointRadius: 0,
			borderWidth: 2,
			borderColor: '#f158d0',
			data: [1.2, 0.8, 1.5, 1.1, 0.9, 1.8]
		}]
	};

	var options3 = {
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
					max: 2,
					stepSize: 1,
					suggestedMax: 2.5,
					callback: function (label, index, labels) {
						return label + 'k';
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

	var chartInstance3 = new Chart(chart3, {
		type: 'line',
		data: data3,
		options: options3
	});

	// line chart4

	var chart4 = document.getElementById('chart4').getContext('2d'),
		gradient4 = chart4.createLinearGradient(0, 0, 0, 130);

	gradient4.addColorStop(0, '#5cd050');
	gradient4.addColorStop(0.7, 'rgba(255, 255, 255, 0.5)');

	var data4 = {
		labels: ['January', 'February', 'March', 'April', 'May', 'June'],
		datasets: [{
			label: 'Custom Label Name',
			backgroundColor: gradient4,
			pointRadius: 0,
			borderWidth: 2,
			borderColor: '#5cd050',
			data: [0.2, 1, 0.8, 1.6, 0.9, 1.5]
		}]
	};

	var options4 = {
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
					max: 2,
					stepSize: 1,
					suggestedMax: 2.5,
					callback: function (label, index, labels) {
						return label + 'k';
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

	var chartInstance4 = new Chart(chart4, {
		type: 'line',
		data: data4,
		options: options4
	});

	// power chart

	var chart = c3.generate({
		bindto: '#power-chart',
		data: {
			columns: [
				['Satisfied', 78],
				['Unsatisfied', 22]
			],
			type: 'donut',
			tooltip: {
				show: true
			}
		},
		donut: {
			title: 'Customers',
			label: {
				show: false
			},
			width: 20
		},

		legend: {
			hide: true
		},
		color: {
			pattern: ['#f158d0', '#f3f3f5']
		}
	});
});
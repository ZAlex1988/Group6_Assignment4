
async function callApi(uri, data) {
	//console.log(`Calling uri ${uri} with data ${JSON.stringify(data)}`);
	const result = await $.ajax({
		url: uri,
		type: "POST",
		data: JSON.stringify(data),
		contentType: "application/json; charset=utf-8",		
		dataType: "json",
		error: function () {
			console.log(`Error calling API: ${uri}`);
		}
	});
	return result;
}

$(document).on('click', '#search_click', function () {	
	var parkSearchInfo = {};
	parkSearchInfo.description = $("#park_name").val().trim();
	parkSearchInfo.state = $("#park_state").val();
	parkSearchInfo.parkCode = $("#park_code").val();
	if (parkSearchInfo.description != "" || parkSearchInfo.state != "" || parkSearchInfo.parkCode != "") {
		callApi('/Search/FindParks', parkSearchInfo).then(json => {
			//console.log("Result fetched: " + JSON.stringify(json));
			displayResults(json);
		});
	}

	
});

$(document).on('click','#check_avail_button',function(){
	var parkCode = $(this).attr("value");
	if (parkCode !== undefined && parkCode != '') {
		callApi('/Search/FindCampgrounds', parkCode).then(json => {
			//console.log("Result fetched: " + JSON.stringify(json));
			displayCampgrounds(json, parkCode)
		});
	}	
	
});

function displayCampgrounds(results, parkCode) {
	var pName = `#park_${parkCode}`;
	//console.log(results);
	var campgroundTable = getCampsTable(results);
	if (campgroundTable !== undefined && campgroundTable != '') {
		var paragraph = $(pName);
		paragraph.empty();
		paragraph.append(campgroundTable);
	}
}

function displayResults(res) {
		if (res.length > 0) {			
			$(".results").css("display", "block");
			$(".output").empty();
			$(".output").append(`<p><b>Results found:</b>${res.length}</p>`);
			res.forEach(park => {				
				var activitiesStr = getActivities(park);
				var feesTbl = getFeesTbl(park);
				
				var formulateLi = `<div class="top-level"><h3><a href="${park.url}" id="${park.parkCode}">${park.fullName}</a></h3><p><b>Description:</b>${park.description}</p>`;
				if (activitiesStr.length != 0) { 
					formulateLi = formulateLi + `<p><b>Activities Available: </b>${activitiesStr}</p>`
				}
				
				if (feesTbl.length != 0) { 
					formulateLi = formulateLi + `<p><b>Entrance Fees: </b>${feesTbl}</p>`
				}
				
				
				formulateLi = formulateLi + `<p><b>Campgrounds: </b><button id="check_avail_button" class = "camp_button" value=${park.parkCode}>Check availability</button></p><p id="${'park_' + park.parkCode}"></p></div>`;
				$(".output").append(formulateLi);				
			});			
		} else {
			$(".results").css("display", "block");
			$(".output").empty();
			$(".output").append('<p class="no_results">No results matched search criteria. Please change your selection criteria and try again<p>');
		}
}


function getActivities(park) {
	var activitiesStr = '';
	if (park.activities !== undefined) {
		activitiesStr = park.activities.join(", ");
	}
	return activitiesStr;
}

function getFeesTbl(park) {
	var feesTbl = '';
	if (park.entranceFees !== undefined && park.entranceFees.length != 0) {
		if (park.fees.length == 1 & park.fees[0].cost == "0.00") {
			feesTbl = 'There is no entrance fee to the park';
		} else {
			park.fees.forEach(feeObj => {
				if (feesTbl.length == 0) {
					feesTbl = `<thead><tr><th>Cost</th><th>Description</th></tr></thead><tbody><tr><td>${feeObj.cost}</td><td>${feeObj.title}</td></tr>`;
				} else {
					feesTbl = feesTbl + `<tr><td>$${feeObj.cost}</td><td>${feeObj.title}</td></tr>`;
				}
			});
			feesTbl = '<table id="parksTable">' + feesTbl + '</tbody></table>';
		}
	}
	return feesTbl;
}

function getCampsTable(results) {
	var campTbl = '';
	if (results !== undefined && results.length > 0) {
		results.forEach(camp => {
			campTbl = campTbl + `<li><p id="${camp.campgroundId}"><b>${camp.campgroundName}</b></p><p>Total Available: ${camp.sites}</p><p>${camp.campgroundDescription}</p>`;
			if (camp.reservable == true) {
				campTbl = campTbl + `<p><form action="/Reservation/Reserve" method="GET"><input type="hidden" name="ids" value="${camp.parkCode + "___" + camp.campgroundId}" /><button type="submit" id="reserve_button" class = "camp_button">Reserve</button></form></p></li>`;
			} else {
				campTbl = campTbl + `<p class="no_results">This campground is not reservable</p>`;
			}
			campTbl = campTbl + '<br></li>';
			
		});
		campTbl = '<ol>' + campTbl + '</ol>';
	} else {
		campTbl = '<p class="no_results"> No Campgrounds Found!</p>';
	}
	return campTbl;
}

$(document).ready(function () {
	callApi('/Search/GetChartData').then(json => {
		console.log("Chart Result fetched: " + JSON.stringify(json));
		createChart(json);
	});

});

function createChart(json) {
	console.log("Start creating chart");
	var ctx = document.getElementById('chart').getContext('2d');

	var chart = new Chart(ctx, {
		type: 'doughnut',
		data: {
			labels: json.labels,
			fontColor: 'black',
			datasets: [
				{
					label: "Number Of Parks",
					backgroundColor: json.colors,
					data: json.data,
					fontColor: 'black'
				}
			]
		},
		options: {
			legend: {
				display: true,
				fontColor: 'black'
			},
			title: {
				display: true,
				fontSize: 16,
				text: 'State Share of National Parks - (Hover over the chart to see how many parks each state offers)',
				fontColor: 'black'
			},
		}
	});
}







       
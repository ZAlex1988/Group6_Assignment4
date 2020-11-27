
async function callApi(uri, data) {
	console.log(`Calling uri ${uri} with data ${JSON.stringify(data)}`);
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
			console.log("Result fetched: " + JSON.stringify(json));
			displayResults(json);
		});
	}

	
});


$(document).on('click','#check_avail_button',function(){
	var parkCode = $(this).attr("value");
	if (parkCode !== undefined && parkCode != '') {
		var query = `campgrounds?parkCode=${parkCode}`;
		execQuery(query, 'campSearch', parkCode);
	}	
	
});

$(document).on('click','#reserve_button',function(){
	var parkCampIds = $(this).attr("value");
	var ids = parkCampIds.split("___");
	var parkId = ids[0];
	var campId = ids[1];
	var parkName = $("#"+parkId).text();
	var campName = $("#"+campId).text();	
	var urlParams = `?parkId=${parkId}&campId=${campId}&parkName=${parkName}&campName=${campName}`;
	window.location.replace(`Reservations.html${urlParams}`);
	
	
	
});


function execQuery(query, type, parkCode) {
	let endpoint = 'https://developer.nps.gov/api/v1/';
	let apiKey = 'Xui3j5YQKjKLv7a5gqDYxeWdkMft7k9xdtduFSgt';
	$.ajaxSetup({
		headers: { 'X-Api-Key': apiKey }
	});

	$.ajax({		
        url: endpoint + query,		
        contentType: "application/json",
        dataType: 'json',
		success: function(result) { 
			if ('parkSearch' == type) {
				displayResults(result) 
			} else if ('campSearch' == type) {
				displayCampgrounds(result, parkCode);
			}
		},
		error: function() {
			console.log("error!");
		}
    })  
}

function displayCampgrounds(results, parkCode) {
	var pName = `#park_${parkCode}`;
	console.log(results);
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
	if (results !== undefined && results.total != 0) {
		results.data.forEach(camp => {
			campTbl = campTbl + `<li><p id="${camp.id}"><b>${camp.name}</b></p><p>Total Available: ${camp.campsites.totalSites}</p><p>${camp.description}</p>`;
			if (camp.reservationUrl != "") {
				 campTbl = campTbl + `<p><button id="reserve_button" class = "camp_button" value=${camp.parkCode + "___" + camp.id}>Reserve</button></p></li>`;
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







       
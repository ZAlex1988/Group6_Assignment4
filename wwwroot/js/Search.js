//Make Footer stay at the bottom
var states = ["AK", "AL", "AR", "AS", "AZ", "CA", "CO", "CT", "DC", "DE", "FL", "GA", "GU", "HI", "IA", "ID", "IL", "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND", "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VA", "VI", "VT", "WA", "WI", "WV", "WY"];
var parkCodes = [{ code: 'DENA', name: 'Denali National Park & Preserve'}, { code: 'GAAR', name: 'Gates Of The Arctic National Park & Preserve'}, { code: 'GLBA', name: 'Glacier Bay National Park & Preserve'}, { code: 'KATM', name: 'Katmai National Park & Preserve'}, { code: 'KEFJ', name: 'Kenai Fjords National Park'}, { code: 'KOVA', name: 'Kobuk Valley National Park'}, { code: 'LACL', name: 'Lake Clark National Park & Preserve'}, { code: 'WRST', name: 'Wrangell - St Elias National Park & Preserve'}, { code: 'HOSP', name: 'Hot Springs National Park'}, { code: 'GRCA', name: 'Grand Canyon National Park'}, { code: 'PEFO', name: 'Petrified Forest National Park'}, { code: 'SAGU', name: 'Saguaro National Park'}, { code: 'CHIS', name: 'Channel Islands National Park'}, { code: 'DEVA', name: 'Death Valley National Park'}, { code: 'JOTR', name: 'Joshua Tree National Park'}, { code: 'LAVO', name: 'Lassen Volcanic National Park'}, { code: 'PINN', name: 'Pinnacles National Park'}, { code: 'SEKI', name: 'Sequoia & Kings Canyon National Parks'}, { code: 'YOSE', name: 'Yosemite National Park'}, { code: 'BLCA', name: 'Black Canyon Of The Gunnison National Park'}, { code: 'GRSA', name: 'Great Sand Dunes National Park & Preserve'}, { code: 'MEVE', name: 'Mesa Verde National Park'}, { code: 'ROMO', name: 'Rocky Mountain National Park'}, { code: 'BISC', name: 'Biscayne National Park'}, { code: 'DRTO', name: 'Dry Tortugas National Park'}, { code: 'EVER', name: 'Everglades National Park'}, { code: 'HALE', name: 'Haleakala National Park'}, { code: 'HAVO', name: 'Hawaii Volcanoes National Park'}, { code: 'YELL', name: 'Yellowstone National Park'}, { code: 'INDU', name: 'Indiana Dunes National Park'}, { code: 'MACA', name: 'Mammoth Cave National Park'}, { code: 'ACAD', name: 'Acadia National Park'}, { code: 'ISRO', name: 'Isle Royale National Park'}, { code: 'VOYA', name: 'Voyageurs National Park'}, { code: 'JEFF', name: 'Gateway Arch National Park'}, { code: 'GLAC', name: 'Glacier National Park'}, { code: 'YELL', name: 'Yellowstone National Park'}, { code: 'GRSM', name: 'Great Smoky Mountains National Park'}, { code: 'THRO', name: 'Theodore Roosevelt National Park'}, { code: 'CAVE', name: 'Carlsbad Caverns National Park'}, { code: 'WHSA', name: 'White Sands National Park'}, { code: 'DEVA', name: 'Death Valley National Park'}, { code: 'GRBA', name: 'Great Basin National Park'}, { code: 'CUVA', name: 'Cuyahoga Valley National Park'}, { code: 'CRLA', name: 'Crater Lake National Park'}, { code: 'CONG', name: 'Congaree National Park'}, { code: 'BADL', name: 'Badlands National Park'}, { code: 'WICA', name: 'Wind Cave National Park'}, { code: 'GRSM', name: 'Great Smoky Mountains National Park'}, { code: 'BIBE', name: 'Big Bend National Park'}, { code: 'GUMO', name: 'Guadalupe Mountains National Park'}, { code: 'ARCH', name: 'Arches National Park'}, { code: 'BRCA', name: 'Bryce Canyon National Park'}, { code: 'CANY', name: 'Canyonlands National Park'}, { code: 'CARE', name: 'Capitol Reef National Park'}, { code: 'ZION', name: 'Zion National Park'}, { code: 'SHEN', name: 'Shenandoah National Park'}, { code: 'VIIS', name: 'Virgin Islands National Park'}, { code: 'MORA', name: 'Mount Rainier National Park'}, { code: 'NOCA', name: 'North Cascades National Park'}, { code: 'OLYM', name: 'Olympic National Park'}, { code: 'GRTE', name: 'Grand Teton National Park'}, { code: 'YELL', name: 'Yellowstone National Park'}];
$(document).ready(function () {
	//populate dropdowns
	var parkCodeOptions = $("#park_code");	
    parkCodes.forEach(parkCode => {
        parkCodeOptions.append($("<option />").val(parkCode.code).text(parkCode.name));
    });
	
	var stateOptions = $("#park_state");	
    states.forEach(state => {
        stateOptions.append($("<option />").val(state).text(state));
    });
	
	stateOptions.prop("selectedIndex", -1);
	parkCodeOptions.prop("selectedIndex", -1);

});



$(document).on('click','#search_click',function(){	
	var parkName = $("#park_name").val().trim();
	var parkState = $("#park_state").val();
	var parkCode = $("#park_code").val();	

	if (parkName != "" || parkState != null || parkCode != null) {
		var query = 'parks?';
		if (parkName != "") {
			query = query + `q="${parkName}"`;
		}
		if (parkState != null && parkState != "") {
			query = query + `&stateCode=${parkState}`;
		};
		if (parkCode != null  && parkCode != "") {
			query = query + `&parkCode=${parkCode}`;
		};
			
	
		if (query != 'parks?') {
			execQuery(query, 'parkSearch', '');	
		} 
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
		console.log(res);
		res = filterNationalParks(res);
		if (res.total > 0) {			
			$(".results").css("display", "block");
			$(".output").empty();
			$(".output").append(`<p><b>Results found:</b>${res.total}</p>`);
			res.parks.forEach(park => {				
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

function filterNationalParks(results) {
	var filteredResults = {};
	filteredResults.parks = [];
	var counter = 0;
	if (results !== undefined && results.total > 0) {
		results.data.forEach(park => {
			if (park.designation.includes("National Park")) {
				counter = counter + 1;
				filteredResults.parks.push(park);
			}
		});
	}
	filteredResults.total = counter;
	return filteredResults;
}

function getActivities(park) {
	var activitiesStr = '';
	if (park.activities !== undefined) {
		park.activities.forEach(activity => {
			if (activitiesStr.length != 0) {
				activitiesStr = activitiesStr + ', ' + activity.name;
			} else {
				activitiesStr = activity.name;
			}
		});
	}
	return activitiesStr;
}

function getFeesTbl(park) {
	var feesTbl = '';
	if (park.entranceFees !== undefined && park.entranceFees.length != 0) {
		if (park.entranceFees.length == 1 & park.entranceFees[0].cost == "0.00") {
			feesTbl = 'There is no entrance fee to the park';
		} else {
			park.entranceFees.forEach(feeObj => {
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







       
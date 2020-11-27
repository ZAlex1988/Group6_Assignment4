var searchForm = {};
var params = {};

$(document).on('click','#find_reservation',function(){
	console.log("Here");
	$(".no_results").hide();
	var reservationId = $("#reservationNum").val().trim();
	if (reservationId=="123") {
		searchForm.show();
		$("#search_click_button").hide();
		prefillStaticDataForm();
	} else if (reservationId=="") {
		searchForm.hide();
	} else {
		searchForm.hide();
		$(".no_results").show();
	}
	
});

function prefillStaticDataForm() {
	$("#park_name").text("Park Name: Arches National Park");
	$("#camp_name").text("Campground: Devils Garden Campground");
	$("input[name=Cust_First_Names]").val("John");
	$("input[name=Cust_Last_Name]").val("Smith");
	$("select[name=Rig_Type] option[value=Tent]").attr("selected", true).change();
	$("select[name=Num_Adults] option[value=2]").attr("selected", true).change();
	$("select[name=Site_Type] option[value=Tent]").attr("selected", true).change();
	$("input[name=Num_Sites]").val("1");	
	$("select[name=Resv_First_Date] option[value=6]").attr("selected", true).change();
	$("select[name=First_Date2] option[value=1]").attr("selected", true).change();
	$("select[name=First_Date3] option[value=2021]").attr("selected", true).change();
}

$(document).on('click','#update_reservation',function(){
	window.location.replace("Thankyou.html?action=update");
	
});

$(document).on('click','#delete_reservation',function(){
	window.location.replace("Thankyou.html?action=delete");
	
});
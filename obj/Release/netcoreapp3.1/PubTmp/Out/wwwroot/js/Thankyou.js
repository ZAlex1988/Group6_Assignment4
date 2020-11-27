$(document).ready(function () {
	var urlParamsStr = window.location.href.split('?')[1];
	
	
	if (urlParamsStr === undefined) {
		console.log("URL is not defined");
		$(".action_message").hide();
	} else if (urlParamsStr.includes("update")) {
		$(".action_message").show();
		$(".action_message").text("Reservation updated Successfully");
	} else if (urlParamsStr.includes("delete")) {
		$(".action_message").show();
		$(".action_message").text("Reservation deleted Successfully");
	}
 });
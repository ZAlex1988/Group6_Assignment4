var imgs = [];
var endpoint = 'https://developer.nps.gov/api/v1/';
var apiKey = 'Xui3j5YQKjKLv7a5gqDYxeWdkMft7k9xdtduFSgt';
var imageCount = 0;
var total = 0;
var time = window.setInterval(autoRotate, 6000);
	
console.log("Here");


$.ajaxSetup({
	headers: { 'X-Api-Key': apiKey }
});
	
$.ajax({
	url: endpoint + "parks?parkCode=arch,glac,ever",		
	contentType: "application/json",
	dataType: 'json',
	success: function(result){	
		//manually add default image
		var sliderData = {};		
		sliderData.park = "Everglades National Park";
		sliderData.descr = "Entrace Sign for the Everglades National Park";
		sliderData.url = "Images/Banner/1.jpg";
		imgs.push(sliderData);	
	
		result.data.forEach(rec => {			
			rec.images.forEach(img => {
				var sliderData = {};		
				sliderData.park = rec.fullName;
				sliderData.descr = img.title + "." + img.caption;
				sliderData.url = img.url;
				imgs.push(sliderData);
			})
			
		});  
				
	},
	async: false
}) ;
total = imgs.length;


	 

function rotateOnce(x) {
	var image = document.getElementById('image');
	var imgHeader = document.getElementById('park_name');
	var imgDescr = document.getElementById('img_descr');
	imageCount = imageCount + x;
	if(imageCount > total-1){
        imageCount = 0;
	}
	if(imageCount < 0){
        imageCount = total-1;
    }	
	
	image.src = imgs[imageCount].url;
	imgHeader.innerHTML = imgs[imageCount].park;
	imgDescr.innerHTML = imgs[imageCount].descr;
	
	clearInterval(time); 								
	time =  window.setInterval(autoRotate,6000);
}

function autoRotate() {       
	var image = document.getElementById('image');
	var imgHeader = document.getElementById('park_name');
	var imgDescr = document.getElementById('img_descr');
	
	imageCount = imageCount + 1;
	if(imageCount > total-1){
            imageCount = 0;
    }
	if(imageCount < 0){
        imageCount = total-1;
    }	
    if (image !== null) {
		image.src = imgs[imageCount].url;
		imgHeader.innerHTML = imgs[imageCount].park;
		imgDescr.innerHTML = imgs[imageCount].descr;		
    }
        
}
	













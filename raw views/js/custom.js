$(function() {
	// Sidemenu collapse button
	$('#-sidemenu-collapse-button').click(function() {	
		$('#-sidemenu ul span').toggle();
		$('#-sidemenu').toggleClass('-sidemenu-collapsed');
		$('#-content-wrapper').toggleClass('-content-sidemenu-collapsed');
		
		$('#-sidemenu-collapse-left').toggleClass('d-none');
		$('#-sidemenu-collapse-right').toggleClass('d-none');
	});
	
	// Quizes list on click
	$('.-quizes-card').click(function() {
		$('.-quizes-card-active').toggleClass('-quizes-card-active');
		$(this).toggleClass('-quizes-card-active');
	});
});
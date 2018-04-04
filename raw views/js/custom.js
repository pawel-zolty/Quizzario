$(function() {
	$('#-sidemenu-collapse-button').click(function() {	
		$('.-sidemenu-label').toggle();
		$('#-sidemenu').toggleClass('-sidemenu-collapsed');
		$('#-content-wrapper').toggleClass('-content-sidemenu-collapsed');
		
		$('#-sidemenu-collapse-left').toggleClass('d-none');
		$('#-sidemenu-collapse-right').toggleClass('d-none');
	});
});
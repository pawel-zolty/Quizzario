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

// Universal form validation
(function() {
	'use strict';
	window.addEventListener('load', function() {
	// Fetch all the forms we want to apply custom Bootstrap validation styles to
	var forms = document.getElementsByClassName('needs-validation');
	// Loop over them and prevent submission
	var validation = Array.prototype.filter.call(forms, function(form) {
		form.addEventListener('submit', function(event) {
			if (form.checkValidity() === false) {
				event.preventDefault();
				event.stopPropagation();
			}
			form.classList.add('was-validated');
		}, false);
	});
	}, false);
})();

// Register password form validation
$(function() {
	$('#-register-form-confirm-password').keyup(function() {
		if ($('#-register-form-confirm-password').val() != $('#-register-form-password').val()) {
			$('#-register-form-confirm-password')[0].setCustomValidity('Passwords must match.');
		}
		else {
			$('#-register-form-confirm-password')[0].setCustomValidity('');
		}
	});
});
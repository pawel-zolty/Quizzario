$(function () {
	// Sidemenu collapse button
	$('#-sidemenu-collapse-button').click(function () {
		$('#-sidemenu ul span').toggle();
		$('#-sidemenu').toggleClass('-sidemenu-collapsed');
		$('#-content-wrapper').toggleClass('-content-sidemenu-collapsed');

		$('#-sidemenu-collapse-left').toggleClass('d-none');
		$('#-sidemenu-collapse-right').toggleClass('d-none');
	});

	// Quizes list on click
	$('.-quizes-card').click(function () {
		if ($(this).hasClass('-quizes-card-active')) return;

		$('#-quizes-right-panel-content').fadeOut(50);

		$('.-quizes-card-active').toggleClass('-quizes-card-active');
		$(this).toggleClass('-quizes-card-active');

		$('#-quizes-right-panel-title').html(
			$(".-quizes-card-data", this).data('title')
		);
		$('#-quizes-right-panel-type').html(
			$(".-quizes-card-data", this).data('type')
		);
		$('#-quizes-right-panel-date-created').html(
			$(".-quizes-card-data", this).data('date-created')
		);
		$('#-quizes-right-panel-last-edited').html(
			$(".-quizes-card-data", this).data('last-edited')
		);
		$('#-quizes-right-panel-description').html(
			$(".-quizes-card-data", this).data('description')
		);

		$('#-quizes-right-panel-content').fadeIn(500);
	});

	$(document).ready(function () {
		$('.-quizes-card').first().click();
	});
});

// Universal form validation
(function () {
	'use strict';
	window.addEventListener('load', function () {
		// Fetch all the forms we want to apply custom Bootstrap validation styles to
		var forms = document.getElementsByClassName('needs-validation');
		// Loop over them and prevent submission
		var validation = Array.prototype.filter.call(forms, function (form) {
			form.addEventListener('submit', function (event) {
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
$(function () {
	$('#-register-form-confirm-password').keyup(function () {
		if ($('#-register-form-confirm-password').val() !== $('#-register-form-password').val()) {
			$('#-register-form-confirm-password')[0].setCustomValidity('Passwords must match.');
		}
		else {
			$('#-register-form-confirm-password')[0].setCustomValidity('');
		}
	});
});
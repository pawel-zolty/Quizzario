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

		var link = $('#-view-link').attr("href");
		link = link.replace(/(\/[0-9]*|)$/, "/" + $(".-quizes-card-data", this).data('id'));
		$('#-view-link').attr("href", link);


		$('#-quizes-right-panel-content').fadeIn(500);
	});

	// Favourite button click handler
	$('.-favourite-button').click(function () {
		var target;
		if ($(this).data('is-favourite') === "True") {
			target = $(this).data('target-remove');
		}
		else {
			target = $(this).data('target-add');
		}

		var button = $(this);
		$.post(target,
			{
				quizId: $(this).data('id')
			},
			function (data, status) {
				if (status === "success") {
					button.data('is-favourite', button.data('is-favourite') === "True" ? "False" : "True");
					button.toggleClass("btn-danger");
					button.toggleClass("btn-outline-danger");
				}
			}
		);
	});

	// Adds new answer button to create quiz question card click handler
	$('#-questions').on('click', '.-question-card-add-new-answer', function () {
		var parent = $(this).parent();
		var fieldset = parent.find("fieldset");
		fieldset.append('\
			<div class="form-check my-1 w-100">\
				<input class="form-check-input" type="radio" name="radio">\
				<input type="text" class="w-100" placeholder="Type answer here">\
			</div>\
		');
		fieldset.find("input").last().focus();
	});

	// Adds new question card to create quiz click handler
	$('.-question-card-add-new-question').click(function () {
		$('#-questions').append('\
			<div class="py-2 px-3 mb-2 -question-card">\
				<h6 class="d-inline">Question <span class="-question-card-index">1</span>. <span class="-question-card-title font-weight-bold">Question title</span></h6>\
				<button type="button" class="btn btn-sm btn-outline-primary py-0 float-right" data-toggle="button" autocomplete="off">Multiple answers</button>\
				<fieldset class="form-group pl-2 mb-1">\
				</fieldset>\
				<button type="button" class="-question-card-add-new-answer btn btn-sm btn-link px-3">Add new answer...</button>\
			</div>\
		');
	});

	$(document).ready(function () {
		// Clicking on the first card after page load
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
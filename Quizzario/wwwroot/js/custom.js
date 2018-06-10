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

        $('#-quizes-right-panel-content').hide();

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
        //$('#-quizes-right-panel-last-edited').html(
        //    $(".-quizes-card-data", this).data('last-edited')
        //);
        $('#-quizes-right-panel-description').html(
            $(".-quizes-card-data", this).data('description')
        );

        var viewLink = $('#-view-link').attr("href");
        if (viewLink !== undefined) {
            viewLink = viewLink.replace(/(\/[0-9a-z\-]*|)$/, "/" + $(".-quizes-card-data", this).data('id'));
            $('#-view-link').attr("href", viewLink);
        }

         var editLink = $('#-edit-link').attr("href");
        if (editLink !== undefined) {
            editLink = editLink.replace(/(\/[0-9a-z\-]*|)$/, "/" + $(".-quizes-card-data", this).data('id'));
            $('#-edit-link').attr("href", editLink);
        } 


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

    $('#-search').focus(function () {
        if (typeof ($(this).data("default-width")) === "undefined") {
            $(this).attr('data-default-width', $(this).css("width"));
        }
        var expand_width = $(this).data("expand-width");
        $(this).stop().animate({
            width: expand_width
        }, 300);
    }).blur(function () { /* lookup the original width */
        var w = $(this).data("default-width");
        $(this).stop().animate({
            width: w
        }, 300);
    });
    
    $(document).ready(function () {
        // Clicking on the first card after page load
        $('.-quizes-card').first().click();  
    });
    
});

function addQuiz() {
    var model = scrappModel();
    $.ajax({
        type: "POST",
        url: '/Quizes/Create',
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (res) {
        window.location.replace("/Quizes/MyQuizes");
    }).fail(function (res) {
    })
}


function addQuestion() {
    var model = scrappModel();
    $.ajax({
        type: "POST",
        url: '/Quizes/AddQuestion',
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (res) {
        $("#-questions").html(res.responseText);
    }).fail(function (res) {
        $("#-questions").html(res.responseText);
    })
};

function addAnswer(elem) {
    var questionIndex = $(elem).attr('question-index');
    var model = scrappModel();
    model.Questions[questionIndex].NewAnswerRequested = true;
    console.log(JSON.stringify(model));
    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: '/Quizes/AddAnswer',
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (res) {
        $("#quiz-question-" + questionIndex).html(res.responseText);
    }).fail(function (res) {
        $("#quiz-question-" + questionIndex).html(res.responseText);
    });
};
function editQuiz() {
    var model = scrappModel();
    $.ajax({
        type: "POST",
        url: '/Quizes/Edit',
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (res) {
        window.location.replace("/Quizes/MyQuizes");
    }).fail(function (res) {
    })
}
function scrappModel() {
    var model = {
        Title: $("#-quiz-title").val(),
        Description: $("#-quiz-description").val(),
        Id: $("#-quiz-id").text(),
        Path: $("#-quiz-path").text(),
        QuizAccessLevel: $("#QuizAccessLevel option:selected").text(),
        QuizType: $("#QuizType option:selected").text(),
        Questions: []
    };

    $('.-question-card').each(function () {
        var question = $(this).find('.-question-card-title').val();
        var answers = [];
        $(this).find('[name="AnswerForm"]').each(function () {
            var answer = $(this).find('#Answer').val();
            var isCorrect = $(this).find('#isCorrect').prop('checked');
            answers.push({ Answer: answer, isCorrect: isCorrect, NewAnswerRequested: false })
        });
        model.Questions.push({ Question: question, Answers: answers });
    })
    return model;
}

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
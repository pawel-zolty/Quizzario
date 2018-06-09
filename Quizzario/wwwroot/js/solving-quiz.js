$(function () {
    var solvingGetQuestionXHR;
    var solvingGetQuestionActive = false;



    $('.-go-to-question').click(function () {
        // Send data
        updateAnswer();

        // Make parent element height const
        var content = $("#-taking-quiz-right-panel-content");
        var parent = content.parent();
        var heightOfParent = parent.height();
        parent.height(heightOfParent);

        content.html("<h5>Loading...</h5>");

        // Number of question to get
        var number = $(this).attr('data-number');
        var quizId = $(this).attr('data-quizId');
        highlightButton(number);

        // Abort pending ajax requests
        if (solvingGetQuestionActive) solvingGetQuestionXHR.abort();       
        solvingGetQuestionActive = true;
        solvingGetQuestionXHR = $.post("/Quizes/SolvingGetQuestion", {
            number: number,
            quizId: quizId
        }).done(function (res) {
            content.hide();
            content.html(res);
        }).always(function () {
            content.fadeIn(500);
            parent.height("");

            updateNavigationButtons();
            solvingGetQuestionActive = false;
        });
    });

    $("#-taking-quiz-submit").click(function (e) {
        e.preventDefault();
        updateAnswer(function () {
            $('#-taking-quiz-form-submit').submit(); 
        });
    });

    updateNavigationButtons();
    highlightButton(1);
});

$(document).keydown(function (e) {
    switch (e.key) {
        case 'ArrowLeft':
            $('#-taking-quiz-previous-button').click();
            break;
        case 'ArrowRight':
            $('#-taking-quiz-next-button').click();
            break;
        default: return;
    }
});

// Scraps data and sends ajax update request
function updateAnswer(_callback) {
    var model = {
        QuestionNumber: $("#-question-number").val(),
        SelectedAnswersNumbers: [],
        QuizId: $("#-quiz-id").val(),
    };

    $("input[name=answer]:checked").each(function () {
        model.SelectedAnswersNumbers.push($(this).val());
    });


    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: '/Quizes/SolvingUpdateAnswer',
        dataType: "json",
        contentType: "application/json; charset=utf-8"
    }).done(function (res) {
        console.log(res);
    }).fail(function (res) {
        console.log(res);
    }).always(function () {
        if (_callback !== undefined) _callback();
    });
}

function updateNavigationButtons() {
    var currentQuestionNumber = parseInt($("#-question-number").val());
    var totalNumberOfQuestions = $(".-taking-quiz-button").last().data("number");
    var previousButton = $('#-taking-quiz-previous-button');
    var nextButton = $('#-taking-quiz-next-button');


    if (currentQuestionNumber > 1) {
        previousButton.data("number", currentQuestionNumber - 1);
        previousButton.removeAttr("disabled");
    }
    else {
        previousButton.attr("disabled", "true");
    }

    if (currentQuestionNumber < totalNumberOfQuestions) {
        nextButton.data("number", currentQuestionNumber + 1);
        nextButton.removeAttr("disabled");
    }
    else {
        nextButton.attr("disabled", "true");
    }
}

function highlightButton(number) {
    $(".-taking-quiz-button.btn-primary").removeClass("btn-primary").addClass("btn-outline-primary");
    $(".-taking-quiz-button[data-number=" + number + "]").removeClass("btn-outline-primary").addClass("btn-primary");
}
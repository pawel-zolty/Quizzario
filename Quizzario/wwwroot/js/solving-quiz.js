$(function () {
    var solvingGetQuestionXHR;
    var solvingGetQuestionActive = false;

    $('.-go-to-question').click(function () {
        updateAnswer();

        var content = $("#-taking-quiz-right-panel-content");
        var parent = content.parent();
        var heightOfParent = parent.height();
        parent.height(heightOfParent);

        content.html("<h5>Loading...</h5>");

        var number = $(this).data('number');

        if (solvingGetQuestionActive) solvingGetQuestionXHR.abort();       // Abort pending ajax requests
        solvingGetQuestionActive = true;
        solvingGetQuestionXHR = $.post("/Quizes/SolvingGetQuestion", {
            number: number
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

    updateNavigationButtons();
});

// Scraps data and sends ajax update request
function updateAnswer() {
    var model = {
        QuestionNumber: $("#-question-number").val(),
        SelectedAnswersNumbers: []
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
$(function () {
    var solvingGetQuestionXHR;
    var solvingGetQuestionActive = false;

    $('.-taking-quiz-button').click(function () {
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

            solvingGetQuestionActive = false;
        });
    });
});
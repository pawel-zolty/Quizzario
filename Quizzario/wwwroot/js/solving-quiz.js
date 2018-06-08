$(function () {
    $('.-taking-quiz-button').click(function () {
        var content = $("#-taking-quiz-right-panel-content");
        var parent = content.parent();
        var heightOfParent = parent.height();
        parent.height(heightOfParent);

        content.hide();

        var number = $(this).data('number');
        $.post("/Quizes/SolvingGetQuestion", {
            number: number
        }).done(function (res) {
            content.html(res);
        }).fail(function (res) {
            content.html("dupa");
        }).always(function () {
            content.fadeIn(500);
            parent.height("");
        });
    });
});
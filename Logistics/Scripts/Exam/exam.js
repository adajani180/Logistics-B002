
$(document).ready(function () {
    $("#sb-exams").toggleClass("active");
    
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Exams/Details";
});
$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Exams";
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmExam");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmExam");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var exam = {
        Id: $("#Id").val(),
        Name: $("#Name").val(),
        Description: $("#Description").val(),
        ExpirationCycle: $("#ExpirationCycle").val()
    };

    $.ajax({
        type: "post",
        url: "/Exams/Save",
        data: { exam: exam },
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
        }
    });
});
$(document).on("click", "#btn-delete", function (e) {
    e.preventDefault();
    var examId = $(this).data("id");
    if (confirm("Are you sure you want to delete this Exam?")) {

        $.ajax({
            type: "post",
            url: "/Exams/Delete",
            data: { id: examId },
            success: function (result) {
                displayMessage(result);
            },
            complete: function () {
                MVCGrid.reloadGrid("Exams");
            }
        });

    }
});


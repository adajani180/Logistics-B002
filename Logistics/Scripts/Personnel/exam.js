
$(document).ready(function () {
    $("#sb-personnel").toggleClass("active");
});

$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Personnel/Details/" + $("#PersonId").val();
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
        PersonId: $("#PersonId").val(),
        ExamId: $("#ExamId").val(),
        ExamDate: $("#ExamDate").val(),
        AssetId: $("#AssetId").val(),
        ResultLookupId: $("#ResultLookupId").val(),
        ResultDate: $("#ResultDate").val(),
        Notes: $("#Notes").val()
    };
    
    $.ajax({
        type: "post",
        url: "/Personnel/SaveExam",
        data: { exam: exam },
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
        }
    });
});

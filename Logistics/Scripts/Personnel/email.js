
$(document).ready(function () {
    $("#sb-personnel").toggleClass("active");
});

$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Personnel/Details/" + $("#PersonId").val();
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmEmail");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmEmail");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var email = {
        Id: $("#Id").val(),
        PersonId: $("#PersonId").val(),
        TypeLookupId: $("#TypeLookupId").val(),
        Email: $("#Email").val()
    };
    console.log(email);
    $.ajax({
        type: "post",
        url: "/Personnel/SaveEmail",
        data: { email: email },
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
        }
    });
});

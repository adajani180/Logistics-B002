
$(document).ready(function () {
    $("#sb-personnel").toggleClass("active");
});

$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Personnel/Details/" + $("#PersonId").val();
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmAddress");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmAddress");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var address = {
        Id: $("#Id").val(),
        PersonId: $("#PersonId").val(),
        TypeLookupId: $("#TypeLookupId").val(),
        Address1: $("#Address1").val(),
        Address2: $("#Address2").val(),
        Address3: $("#Address3").val(),
        City: $("#City").val(),
        //StateId: $("#StateId").val(),
        ZipCode: $("#ZipCode").val()
    };
    
    $.ajax({
        type: "post",
        url: "/Personnel/SaveAddress",
        data: { address: address },
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
        }
    });
});

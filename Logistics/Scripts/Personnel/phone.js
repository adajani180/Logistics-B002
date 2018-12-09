
$(document).ready(function () {
    $("#sb-personnel").toggleClass("active");
});

$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Personnel/Details/" + $("#PersonId").val();
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmPhone");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmPhone");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var phone = {
        Id: $("#Id").val(),
        PersonId: $("#PersonId").val(),
        TypeLookupId: $("#TypeLookupId").val(),
        Number: $("#Number").val(),
        Ext: $("#Ext").val()
    };
    
    $.ajax({
        type: "post",
        url: "/Personnel/SavePhone",
        data: { phone: phone },
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
        }
    });
});

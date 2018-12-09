
$(document).ready(function () {
    $("#sb-access").toggleClass("active");
    $("#sb-users").toggleClass("active");
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Access/SystemUserForm";
});
$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Access";
});
$(document).on("click", "#btn-save", function (e) {

    var $form = $("#frmSystemUser");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmSystemUser");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var usr = {
        Id: $("#Id").val(),
        UserName: $("#UserName").val(),
        StatusLookupId: $("#StatusLookupId").val()
    };

    $.ajax({
        type: "post",
        url: "/Access/SystemUserForm",
        data: usr,
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
            //$("#btn-close").trigger("click");
            //setTimeout(function () {
            //    $("#btn-close").trigger("click")
            //}, 2000);//setTimeout(, 3000);
        }
    });
});
$(document).on("click", "#btn-delete", function (e) {
    e.preventDefault();

    if (confirm("Are you sure you want to delete this System User?")) {

        $.ajax({
            type: "post",
            url: "/Access/DeleteSystemUser",
            data: { id: $(this).data("id") },
            success: function (result) {
                //$("panelSystemUsers").toggleClass("ld-loading");
                displayMessage(result);
            },
            complete: function () {
                MVCGrid.reloadGrid("SystemUsers");
                //$("panelSystemUsers").toggleClass("ld-loading");
                //setTimeout(function () {
                //    $("#btn-close").trigger("click")
                //}, 2000);//setTimeout(, 3000);
            }
        });

    }
});
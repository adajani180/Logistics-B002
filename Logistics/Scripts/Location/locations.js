$(document).ready(function () {
    $("#sb-locations").toggleClass("active");

    //var personId = $("#PersonId").val();
    //if (personId != undefined && personId != "") {
    //    refreshGrid(personId);
    //}
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Locations/Details";
});
$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Locations";
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmLocation");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmLocation");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var location = {
        Id: $("#LocationId").val(),
        Name: $("#Name").val()
    };

    $.ajax({
        type: "post",
        url: "/Locations/Save",
        data: location,
        success: function (result) {
            //var msg = result.Message;
            //var newId = result.NewId;
            displayMessage(result);
            $("#btn-close").trigger("click");

            //if (newId != person.Id) {
            //    $("#Id").val(newId);
            //    refreshGrid(newId);
            //} else {
            //    $("#btn-close").trigger("click");
            //}
        },
        complete: function () {
        }
    });
});
$(document).on("click", "#btn-delete", function (e) {
    e.preventDefault();

    if (confirm("Are you sure you want to delete this Location?")) {

        $.ajax({
            type: "post",
            url: "/Locations/Delete",
            data: { id: $(this).data("id") },
            success: function (result) {
                displayMessage(result);
            },
            complete: function () {
                MVCGrid.reloadGrid("Personnel");
            }
        });

    }
});
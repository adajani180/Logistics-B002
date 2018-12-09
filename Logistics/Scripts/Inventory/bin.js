
$(document).ready(function () {
    $("#sb-inventory").toggleClass("active");
    $("#sb-bins").toggleClass("active");
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Inventory/Bins/Details";
});
$(document).on("click", "#btn-close", function (e) {
    window.location = "/Inventory/Bins";
});
$(document).on("click", "#btn-save", function (e) {

    var $form = $("#frmBin");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmBin");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var bin = {
        Id: $("#Id").val(),
        Name: $("#Name").val(),
        Description: $("#Description").val(),
        WarehouseId: $("#WarehouseId").val()
    };

    $.ajax({
        type: "post",
        url: "/Inventory/Bins/Save",
        data: bin,
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
    var binId = $(this).data("id");
    bootbox.confirm({
        closeButton: false,
        message: "<h5> Are you sure you want to delete this Bin? </h5>",
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "post",
                    url: "/Inventory/Bins/Delete",
                    data: { id: binId },
                    success: function (result) {
                        displayMessage(result);
                    },
                    complete: function () {
                        MVCGrid.reloadGrid("Bins");
                    }
                });
            }
        }
    });
});
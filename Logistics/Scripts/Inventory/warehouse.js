
$(document).ready(function () {
    $("#sb-inventory").toggleClass("active");
    $("#sb-warehouse").toggleClass("active");
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Inventory/Warehouse/Details";
});
$(document).on("click", "#btn-close", function (e) {
    window.location = "/Inventory/Warehouse";
});
$(document).on("click", "#btn-save", function (e) {

    var $form = $("#frmWarehouse");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmWarehouse");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var warehouse = {
        Id: $("#Id").val(),
        Name: $("#Name").val(),
        Description: $("#Description").val(),
        Location: $("#Location").val()
    };

    $.ajax({
        type: "post",
        url: "/Inventory/Warehouse/Save",
        data: warehouse,
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
    var warehouseId = $(this).data("id");
    bootbox.confirm({
        closeButton: false,
        message: "<h5> Are you sure you want to delete this Warehouse? </h5>",
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "post",
                    url: "/Inventory/Warehouse/Delete",
                    data: { id: warehouseId },
                    success: function (result) {
                        displayMessage(result);
                    },
                    complete: function () {
                        MVCGrid.reloadGrid("Warehouses");
                    }
                });
            }
        }
    });
});
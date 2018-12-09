
$(document).ready(function () {
    $("#sb-inventory").toggleClass("active");
    $("#sb-assets").toggleClass("active");
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Inventory/Assets/Details";
});
$(document).on("click", "#btn-close", function (e) {
    window.location = "/Inventory/Assets";
});
$(document).on("click", "#btn-save", function (e) {

    var $form = $("#frmAsset");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmAsset");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var asset = {
        Id: $("#Id").val(),
        Name: $("#Name").val(),
        Description: $("#Description").val(),
        WarehouseId: $("#WarehouseId").val(),
        BinId: $("#BinId").val(),
        TypeLookupId: $("#TypeLookupId").val(),
        Manufacturer: $("#Manufacturer").val(),
        SerialNumber: $("#SerialNumber").val(),
        Quantity: $("#Quantity").val(),
        Price: $("#Price").val(),
        DateAcquired: $("#DateAcquired").val(),
        Threshold: $("#Threshold").val(),
        Notes: $("#Notes").val()
    };

    $.ajax({
        type: "post",
        url: "/Inventory/Assets/Save",
        data: asset,
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
    var assetId = $(this).data("id");
    bootbox.confirm({
        closeButton: false,
        message: "<h5> Are you sure you want to delete this Bin? </h5>",
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "post",
                    url: "/Inventory/Assets/Delete",
                    data: { id: assetId },
                    success: function (result) {
                        displayMessage(result);
                    },
                    complete: function () {
                        MVCGrid.reloadGrid("Assets");
                    }
                });
            }
        }
    });
});
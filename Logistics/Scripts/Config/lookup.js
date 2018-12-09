
$(document).ready(function () {
    $("#sb-config").toggleClass("active");
    $("#sb-lookup").toggleClass("active");
    //$("#sb-config").find("a[aria-expanded=false]")
    //    .toggleClass("collapsed")
    //    .attr("aria-expanded", "true");
    //$("#config").toggleClass("in")
    //    .attr("aria-expanded", "true");
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Config/Lookup/Details";
});
$(document).on("click", "#btn-close", function (e) {
    window.location = "/Config/Lookup";
});
$(document).on("click", "#btn-save", function (e) {

    var $form = $("#frmLookup");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmLookup");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var lookup = {
        Id: $("#Id").val(),
        Name: $("#Name").val(),
        Description: $("#Description").val(),
        Code: $("#Code").val(),
        Type: $("#Type").val()
    };

    $.ajax({
        type: "post",
        url: "/Config/Lookup/Save",
        data: lookup,
        success: function (result) {
            displayMessage(result);
            $("#btn-close").trigger("click");
        },
        complete: function () {
            //setTimeout(function () {
            //    $("#btn-close").trigger("click")
            //}, 2000);//setTimeout(, 3000);
        }
    });
});
$(document).on("click", "#btn-delete", function (e) {
    e.preventDefault();
    var lookupId = $(this).data("id");
    bootbox.confirm({
        closeButton: false,
        message: "<h5> Are you sure you want to delete this Lookup? </h5>",
        callback: function (result) {
            if (result) {
                $.ajax({
                    type: "post",
                    url: "/Config/Lookup/Delete",
                    data: { id: lookupId },
                    success: function (result) {
                        displayMessage(result.Message);
                        bindList($("#Type"), result.LookupTypes);
                    },
                    complete: function () {
                        MVCGrid.reloadGrid("ConfigLookups");
                    }
                });
            }
        }
    });
});

function bindList($select, values) {
    var listItems = "";
    $.each(values, function (key, value) {
        listItems += "<option>" + value + "</option>";
    });
    $select.empty()
        .append("<option></option>")
        .append(listItems);
}
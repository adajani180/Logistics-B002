
$(document).ready(function () {
    $("#sb-appointments").toggleClass("active");

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        } else {
            return decodeURI(results[1]) || 0;
        }
    }

    //var myDate;
    var param = $.urlParam("d")
    if (param != null) {
        param = param.replace(/%2F/g, "/");
        $("#AppointmentDate").val(param);
        MVCGrid.setAdditionalQueryOptions("Appointments", { d: param });
    }
});

$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    //var appDate = moment($("#Appointment_AppointmentDate").val()).format("MM/DD/YYYY");
    window.location = "/Appointments";
});
$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    $("#appointmentModal").modal("show");
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmDetails");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmDetails");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");
    
    var appointment = {
        Id: $("#Id").val(),
        AppointmentDate: $("#AppointmentDate").val(),
        AppointmentTime: moment($("#AppointmentTime").val(), ["hmm", "hhmm", "Hmm", "HHmm"]).format("HH:mm"),
        Location: $("#Location").val(),
        Notes: $("#Notes").val()
    };

    $.ajax({
        type: "post",
        url: "/Appointments/Save",
        data: { appointment: appointment },
        success: function (result) {
            displayMessage(result);
            MVCGrid.reloadGrid("Appointments");
            $("#appointmentModal").modal("hide");
        },
        complete: function () {
            $("#btn-save").text("Save")
                .prop("disabled", false);
        }
    });
});
$(document).on("click", "#btn-edit", function (e) {
    e.preventDefault();
    var id = $(this).data("id");
    $.ajax({
        type: "post",
        url: "/Appointments/Get",
        data: { id: id },
        success: function (result) {
            if (result == null) return;

            var appTime = result.AppointmentTime.Hours + result.AppointmentTime.Minutes;
            
            $("#Id").val(result.Id);
            $("#AppointmentTime").val(moment(appTime, ["hmm", "hhmm", "Hmm", "HHmm"]).format("HH:mm"));
            $("#Location").val(result.Location);
            $("#Notes").val(result.Notes);
        },
        complete: function () {
            $("#appointmentModal").modal("show");
        }
    });    
});
$(document).on("click", "#btn-delete", function (e) {
    e.preventDefault();
    var id = $(this).data("id");
    bootbox.confirm("Are you sure you want to delete this Appointment?", function (result) {
        if (result) {
            $.ajax({
                type: "post",
                url: "/Appointments/Delete",
                data: { id: id },
                success: function (result) {
                    displayMessage(result);
                    MVCGrid.reloadGrid("Appointments");
                },
                complete: function () {
                }
            });
        }
    });
});
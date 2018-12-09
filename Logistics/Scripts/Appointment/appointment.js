
var $appPicker = $("#appointments-picker");

$(document).ready(function () {
    $("#sb-appointments").toggleClass("active");
    
    $appPicker.datepicker({
        format: "mm/dd/yyyy",
        weekStart: 1,
        multidate: false,
        daysOfWeekDisabled: "0,6",
        todayHighlight: true
    });

    $appPicker.on("changeDate", function (e) {
        var appDate = $appPicker.datepicker("getDate");
        getAppointmentSchedules(appDate);
    });

    $.urlParam = function (name) {
        var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return null;
        } else {
            return decodeURI(results[1]) || 0;
        }
    }

    var myDate = moment().format("MM/DD/YYYY");
    var param = $.urlParam("appointmentdate")
    if (param != null)
        myDate = param.replace(/%2F/g, "/");
    
    $appPicker.datepicker("setDate", myDate);
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Appointments/Details?val=" + $(this).data("date");
});
$(document).on("click", "#btn-editAppointmentDetail", function (e) {
    e.preventDefault();
    window.location = "/Appointments/Details?val=" + $(this).data("id");
});
$(document).on("click", "#btn-deleteAppointmentDetail", function (e) {
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
                },
                complete: function () {
                    var appDate = $appPicker.datepicker("getDate");
                    getAppointmentSchedules(appDate);
                }
            });
        }
    });
});

function getAppointmentSchedules(appointmentDate) {
    var myDate = moment(appointmentDate);
    console.log("selected date " + myDate.format("MM/DD/YYYY"));

    $("#appointment-date").html(myDate.format("dddd, MMMM Do, YYYY"));
    $("#btn-add").data("date", myDate.format("MM/DD/YYYY"));
    MVCGrid.setFilters("AppointmentSchedules", { AppointmentDate: myDate.format("MM/DD/YYYY") });
}
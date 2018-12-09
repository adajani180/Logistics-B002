
var $cal = $("#calendar");

$(document).ready(function () {
    $("#sb-appointments").toggleClass("active");
    
    initCalendar();

    $(".fc-prev-button").removeClass("fc-state-default").addClass("btn-accent");
    $(".fc-next-button").removeClass("fc-state-default").addClass("btn-accent");
});

$(document).on("click", "#btn-scheduleAppointment", function (e) {
    e.preventDefault();
    var sched = {
        Id: $("#schedId").val(),
        AppointmentId: $("#appId").val(),
        PersonId: $(this).data("id"),
        ExamId: $("#Exam").val()
    };
    updateAppointmentSchedule("/Appointments/Schedule", sched);
});
$(document).on("click", "#btn-confirm", function (e) {
    e.preventDefault();
    var data = { id: $("#schedId").val() };
    updateAppointmentSchedule("/Appointments/Confirm", data);
});
$(document).on("click", "#btn-cancel", function (e) {
    e.preventDefault();
    var data = { id: $("#schedId").val() };
    updateAppointmentSchedule("/Appointments/Cancel", data);
});

function initCalendar() {

    $("#calendar").fullCalendar({
        header: {
            left: "title",
            center: "",
            right: "prev,next"
        },
        height: 575,
        //timeFormat: "HHmm",
        //defaultDate: selectedDate,
        fixedWeekCount: false,
        selectable: false,
        selectHelper: true,
        editable: false,
        eventLimit: 4, // allow "more" link when too many events
        events: function (start, end, timezone, callback) {
            renderSchedules(start.format("MM/DD/YYYY"), end.format("MM/DD/YYYY"), callback);
        },
        eventClick: function (calEvent, jsEvent, view) {
            showAppointmentSchedule(calEvent);  
        },
        dayClick: function (date, jsEvent, view) {
            window.location = "/Appointments/Details?d=" + date.format("MM/DD/YYYY");
        }//,
        //dayRender: function (date, cell) {
        //    // like that
        //    var dateString = moment(date).format('YYYY-MM-DD');
        //    $('#calendar').find('.fc-day-number[data-date="' + dateString + '"]').css('background-color', '#FAA732');
        //}
    });

}

function renderSchedules(start, end, callback) {
    $.ajax({
        type: "post",
        url: "/Appointments/GetAll",
        data: { start: start, end: end },
        success: function (result) {
            var events = [];
            var totalAppointments = 0;
            var totalConfirmed = 0;
            var totalScheduled = 0;
            $.map(result, function (calEvent, i) {
                var color = "#f6a821";
                if (calEvent.StatusDescription == "Confirmed") {
                    color = "#1bbf89";
                    totalConfirmed += 1;
                } else if (calEvent.StatusDescription == "Scheduled") {
                    color = "#56c0e0";
                    totalScheduled += 1;
                } else {
                    totalAppointments += 1;
                }                    

                events.push({
                    id: calEvent.AppointmentId,
                    schedId: calEvent.AppointmentScheduleId,
                    start: calEvent.AppointmentDateTime,
                    title: calEvent.PersonFullName,
                    status: calEvent.StatusDescription,
                    examId: calEvent.ExamId,
                    allDay: false,
                    color: color,
                    location: calEvent.Location,
                    notes: calEvent.Notes
                });
            });

            callback(events);

            $("#totalAppointments").html(totalAppointments);
            $("#totalScheduled").html(totalScheduled);
            $("#totalConfirmed").html(totalConfirmed);
        },
        complete: function () {
        }
    });
}

function showAppointmentSchedule(calEvent) {
    $("#appId").val(calEvent.id);
    $("#schedId").val(calEvent.schedId);

    var eventDate = calEvent.start.format("dddd, MMMM Do, YYYY");
    var eventTime = calEvent.start.format("h:mm A");
    //var eventSchdule = eventDate + " at " + eventTime;
    var title = calEvent.title;
    var hidePersonnelRow = (title == "" || title == null);

    $("#schedDate").html(eventDate);
    $("#schedTime").html(eventTime);
    $("#schedLocation").html(calEvent.location);
    $("#schedNotes").html(calEvent.notes);
    $("#schedPerson").html(title);
    $("#Exam").val(calEvent.examId);

    $("#divPersonnonel").toggle(hidePersonnelRow);
    $("#trPerson").toggle(!hidePersonnelRow);

    var isConfirmed = (calEvent.status == "Confirmed");
    var isScheduled = (calEvent.status == "Scheduled");
    
    $("#btn-confirm").toggle(isScheduled);
    $("#btn-cancel").toggle(isScheduled || isConfirmed);

    $("#scheduleModal").modal("show");
}

function updateAppointmentSchedule(url, data) {
    $.ajax({
        type: "post",
        url: url,
        data: data,
        success: function (result) {
            displayMessage(result);
            $("#scheduleModal").modal("hide");
        },
        complete: function () {
            $cal.fullCalendar("refetchEvents");
        }
    });
}


function getAppointmentSchedules(appointmentDate) {
    var myDate = moment(appointmentDate);
    console.log("selected date " + myDate.format("MM/DD/YYYY"));

    $("#appointment-date").html(myDate.format("dddd, MMMM Do, YYYY"));
    $("#btn-add").data("date", myDate.format("MM/DD/YYYY"));
    MVCGrid.setFilters("AppointmentSchedules", { AppointmentDate: myDate.format("MM/DD/YYYY") });
}
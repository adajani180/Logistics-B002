
$(document).ready(function () {
    $("#sb-personnel").toggleClass("active");

    var personId = $("#PersonId").val();
    if (personId != undefined && personId != "") {
        refreshGrid(personId);
    }
});

$(document).on("click", "#btn-add", function (e) {
    e.preventDefault();
    window.location = "/Personnel/Details";
});
$(document).on("click", "#btn-close", function (e) {
    e.preventDefault();
    window.location = "/Personnel";
});
$(document).on("click", "#btn-save", function (e) {
    var $form = $("#frmPersonnel");
    $form.validator();
    $form.validator("validate");

    var isValid = isFormValid("#frmPersonnel");
    if (!isValid) {
        e.preventDefault();
        return false;
    }

    $(this).text("Saving...");
    $(this).attr("disabled", "disabled");

    var person = {
        Id: $("#PersonId").val(),
        FirstName: $("#FirstName").val(),
        MiddleName: $("#MiddleName").val(),
        LastName: $("#LastName").val()
    };

    $.ajax({
        type: "post",
        url: "/Personnel/Save",
        data: person,
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

    if (confirm("Are you sure you want to delete this Person?")) {

        $.ajax({
            type: "post",
            url: "/Personnel/Delete",
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

$(document).on("click", "#btn-addEmail", function (e) {
    e.preventDefault();
    window.location = "/Personnel/EmailDetails?personId=" + $("#PersonId").val();
});
$(document).on("click", "#btn-deleteEmail", function (e) {
    e.preventDefault();
    var emailId = $(this).data("id");
    if (confirm("Are you sure you want to delete this Email?")) {

        $.ajax({
            type: "post",
            url: "/Personnel/DeleteEmail",
            data: { id: emailId },
            success: function (result) {
                displayMessage(result);
            },
            complete: function () {
                MVCGrid.setFilters("PersonnelEmails", { PersonId: $("#PersonId").val() });
            }
        });

    }
});

$(document).on("click", "#btn-addAddress", function (e) {
    e.preventDefault();
    window.location = "/Personnel/AddressDetails?personId=" + $("#PersonId").val();
});
$(document).on("click", "#btn-deleteAddress", function (e) {
    e.preventDefault();
    var addressId = $(this).data("id");
    if (confirm("Are you sure you want to delete this Address?")) {

        $.ajax({
            type: "post",
            url: "/Personnel/DeleteAddress",
            data: { id: addressId },
            success: function (result) {
                displayMessage(result);
            },
            complete: function () {
                MVCGrid.setFilters("PersonnelAddresses", { PersonId: $("#PersonId").val() });
            }
        });

    }
});

$(document).on("click", "#btn-addPhone", function (e) {
    e.preventDefault();
    window.location = "/Personnel/PhoneDetails?personId=" + $("#PersonId").val();
});
$(document).on("click", "#btn-deletePhone", function (e) {
    e.preventDefault();
    var phoneId = $(this).data("id");
    if (confirm("Are you sure you want to delete this Phone?")) {

        $.ajax({
            type: "post",
            url: "/Personnel/DeletePhone",
            data: { id: phoneId },
            success: function (result) {
                displayMessage(result);
            },
            complete: function () {
                MVCGrid.setFilters("PersonnelPhones", { PersonId: $("#PersonId").val() });
            }
        });

    }
});

function refreshGrid(personId) {
    MVCGrid.setFilters("PersonnelEmails", { PersonId: personId });
    MVCGrid.setFilters("PersonnelAddresses", { PersonId: personId });
    MVCGrid.setFilters("PersonnelPhones", { PersonId: personId });
    MVCGrid.setFilters("PersonnelExams", { PersonId: personId });
}
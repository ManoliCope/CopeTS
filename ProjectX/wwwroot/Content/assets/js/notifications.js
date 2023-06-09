////var projectname = checkurlserver();
var projectname = '';
$(document).ready(function () {
    if ($("#newsTicker").length > 0) {
        $("#btnAddNewNote").click(function () {
            var newTitle = $("#newTitle").val();
            var newText = $("#newText").val();
            var newExpDate = $("#newExpDate").val();
            var newExpTime = $("#newExpTime").val();
            var isImportant = $('#isImportant').is(':checked');
            var isActive = $('#isActive').is(':checked');
            if (validateForm("#add-new-note", 'required')) {
                return;
            }
            var addNew =
            {
                "Title": newTitle,
                "Text": newText,
                "ExpiryDate": newExpDate,
                "ExpiryTime": newExpTime,
                "isImportant": isImportant,
                "isActive": isActive
            }
            $.ajax({
                type: 'Post',
                url: projectname + "/Notifications/CreateNewNote",
                data: {
                    req: addNew
                },
                success: function (result) {
                    if (result.statusCode.code == 1) {
                        var check = '';
                        if (isActive)
                            check = 'checked';
                        var expired = 'No expiry date';
                        if (newExpDate != null && newExpDate != '') {
                            if (newExpTime == null || newExpTime == '')
                                newExpTime = '23:59'
                            const [year, month, day] = newExpDate.split('-');
                            expired = 'expired in: ' + month + '-' + day + '-' + year + ' ' + newExpTime;
                        }
                        else if (newExpTime != null && newExpTime != '') {
                            var dateToday = $('#newExpDate').attr('min');
                            const [year, month, day] = dateToday.split('-');
                            expired = 'expired in: ' + month + '-' + day + '-' + year + ' ' + newExpTime;
                        }
                        var textAll = ` <div class="pt-4 px-3 col-md-4">
                                <div class="toast fade show" role="alert" aria-live="assertive"
                                     aria-atomic="true" data-toggle="toast">
                                    <div class="toast-header">
                                        <input type="checkbox" id="chkBoxNote_`+ result.id + `" height="18" class="mr-1" ` + check + ` onclick="activationPopup(` + result.id + `)"/>
                                        <strong class="mr-auto">`+ newTitle + `</strong>
                                        <small>`+ expired + `</small>
                                         <button type="button" class="ml-2 mb-1 notifClose"  onclick="sendIdForpopup(`+ result.id + `)">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                    </div>
                                    <div class="toast-body">
                                       `+ newText + `
                                        </div>
                                </div>
                            </div>`;
                        if (isActive) {
                            $('#NoteContainer').prepend(textAll)
                        }
                        else {
                            $('#NoteContainer').append(textAll)
                        }
                    }
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    getChyron();
                },
                failure: function (data, success, failure) {
                    showresponsemodal("Error", "Bad Request")
                },
                error: function (data) {
                    showresponsemodal("Error", "Bad Request")
                }
            });
        });
        $('#notedelete').click(function () {
            var id = $(this).attr('notid');
            $('#confirm-delete').find('#notedelete').removeAttr('notid');
            deleteNote(id);

        });
        $('#note-agree').click(function () {
            var id = $(this).attr('notid');
            var status = $(this).attr('notstat');
            $('#confirm-active').find('#note-agree').removeAttr('notid');
            $('#confirm-active').find('#note-agree').removeAttr('notstat');
            updateNoteStatus(id, status);

        });
        $('#note-close').click(function () {
            var id = $(this).attr('notid');
            var chkbx = '#chkBoxNote_' + id;
            var status = $(chkbx).is(':checked');
            if (status) {
                $(chkbx).prop("checked", false)
            }
            else {
                $(chkbx).prop("checked", true)
            }
        });
        getChyron();
    }
});
function resetAllFields() {
    $("#newTitle").val('');
    $("#newText").val('');
    $("#newExpDate").val('');
    $("#newExpTime").val('');
    $('#isImportant').find('input:checkbox').prop("checked", false);
}
function deleteNote(id) {
    $.ajax({
        type: 'Post',
        url: projectname + "/Notifications/DeleteNote",
        data: {
            Id: id
        },
        success: function (result) {
            removeNoteFromView(id);
            showresponsemodal(1, "activated");
            getChyron();
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
function sendIdForpopup(id) {
    $('#confirm-delete').find('#notedelete').removeAttr('notid').attr('notid', id)
    $('#confirm-delete').modal('toggle');
}
function addNewNotePopup() {
    $('#add-new-note').modal('toggle');
}
function updateNoteStatus(id, status) {
    $.ajax({
        type: 'Post',
        url: projectname + "/Notifications/UpdateNote",
        data: {
            Id: id
        },
        success: function (result) {
            showresponsemodal(result.statusCode.code, result.statusCode.message);
            getChyron();
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
function activationPopup(id) {

    $('#confirm-active').find('#note-agree').removeAttr('notid').attr('notid', id);
    $('#confirm-active').find('#note-close').removeAttr('notid').attr('notid', id);
    $('#confirm-active').modal('toggle');
}
function removeNoteFromView(id) {
    var divId = '#noteDiv_' + id;
    $(divId).remove();
}
function getChyron() {
    $.ajax({
        type: 'Post',
        url: projectname + "/Notifications/GetNotificationsChyron",
        success: function (result) {
            if (result) {
                $("#newsTicker").removeAttr("hidden");
                $("#chyronText").remove();
                $("#newsTicker").append('<p id="chyronText"></p>');
                $("#newsTicker p").append("<span class='story'>" + result + "</span>");
                var countchar = result.length
                //alert(countchar);
                if (countchar <= 300)
                    $("#newsTicker p").addClass("timer1")
                else if (countchar > 300 && countchar <= 400)
                    $("#newsTicker p").addClass("timer2")
                else if (countchar > 400 && countchar <= 600)
                    $("#newsTicker p").addClass("timer3")
                else if (countchar > 600 && countchar <= 800)
                    $("#newsTicker p").addClass("timer4")
                else if (countchar > 800 && countchar <= 1000)
                    $("#newsTicker p").addClass("timer5")
                else if (countchar > 1000)
                    $("#newsTicker p").addClass("timer6")
            }
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
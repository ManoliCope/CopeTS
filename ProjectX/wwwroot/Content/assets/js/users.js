var projectname = checkurlserver();
$(document).ready(function () {
    var usersid = sessionStorage.getItem('userid');

    sessionStorage.removeItem('userid');

    if (usersid != null && usersid > 0)
        fillUser(usersid);
    else
    getAllUsers();

});
function drawtable(data) {
    console.log(data)

    var table = $('#userstable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "u_Full_Name" },
            { "title": "Insured Number", "className": "text-center filter", "orderable": true, "data": "u_Insured_Number" },
            { "title": "Phone Number", "className": "text-center filter", "orderable": true, "data": "u_Telephone" },
            { "title": "Email", "className": "text-center filter", "orderable": true, "data": "u_Email" },
            {
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Edit" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="gotouser(this)"><i class="fas fa-book"/></a>`;
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }, {
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Delete" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="deleteuser(this)"><i class="fas fa-trash"/></a>`;
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "users")
}
function Search() {
    if (validateForm("#searchform")) {
        return;
    }
    showloader("load")

    var filter = {
        userName: $("#prname").val().trim(),
        idUserType: $("#sprtype").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Users/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.users);
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            //alert("Error:" + failure);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            //alert("fail");
        }
    });
}
function resetpassword() {
    var oldPass = $("#old-password").val();
    var newPass = $("#new-password").val();
    var conPass = $("#confirm-password").val();
    var res = {};
    res = {
        "oldPass": "sss",
        "newPass": "sss",
        "conPass": "sss"    }
    //res["oldPass"] = oldPass;
    //res["newPass"] = newPass;
    //res["conPass"] = conPass;
    console.log(res);
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/ResetPassword",
        data: { res: res },
        //data: res ,
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.users);
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            //alert("Error:" + failure);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            //alert("fail");
        }
    });

}
function saveUser() {
    // Array to store all the fields
    var user = {};
    var url = '';
    // Loop through each input, select, and textarea element inside the form
    $('#addBrokerForm input, #addBrokerForm select, #addBrokerForm textarea').each(function () {
        var field = $(this).attr('field-name'); // Get the field's ID
        var value = $(this).val(); // Get the field's value
        if ($(this).attr('type') == 'checkbox')
            value = $(this).prop("checked");
        if (value == null) {
            var id = '#' + $(this).attr('id');
            value = $(id).val();
        }

        var obj = {};
        user[field] = value;

    });
    var userid = $('#addBrokerForm').attr('userid');
    user.id = userid;
    if (userid > 0)
        url = "/Users/EditUser";
    else url="/Users/createNewUser";
    console.log(user);
    $.ajax({
        type: 'POST',
        url: projectname + url,
        data: { req: user },
        success: function (result) {
            removeloader();
            console.log(result)

            if (result.statusCode.code != 1)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            else {

                showresponsemodal(1, result.statusCode.message)
                window.location.href = '/users/';

                //drawtable(result);
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            //alert("Error:" + failure);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            //alert("fail");
        }
    });
}
$("#searchprofile").click(function () {
    
    
    getAllUsers();
    
    
});
$("#kt_reset").click(function () {
    var divname = $(this).closest(".card-body").attr("id")
    resetAllValues(divname);
    var dropdown = $('.select2-hidden-accessible');
    dropdown.val(null).trigger('change');
});
function getAllUsers() {
    var filter = {};
    var name = $('#prname').val();
    filter.Last_Name = $('#prname').val();
    console.log(filter)
    $.ajax({
        type: 'GET',
        url: projectname + "/Users/GetUsersList",
        data: { name: name },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.users);
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            //alert("Error:" + failure);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            //alert("fail");
        }
    });
}
function edit() {
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")
    // Array to store all the fields
    var user = {};

    // Loop through each input, select, and textarea element inside the form
    $('#addBrokerForm input, #addBrokerForm select, #addBrokerForm textarea').each(function () {
        var field = $(this).attr('field-name'); // Get the field's ID
        var value = $(this).val(); // Get the field's value
        if ($(this).attr('type') == 'checkbox')
            value = $(this).prop("checked");
        if (value == null) {
            var id = '#' + $(this).attr('id');
            value = $(id).val();
        }

        var obj = {};
        user[field] = value;

    });


    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Users/EditUsers",
        data: { req: user },
        success: function (result) {
            removeloader();
            showresponsemodal(result.statusCode.code, result.statusCode.message)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
function deleteuser(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).attr("userid")
    alert(thisid);

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Users/DeleteUsers",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                
                    removebtnloader(me);
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                getAllUsers();
                }
                
            
            else
                showresponsemodal(result.statusCode.code, result.statusCode.message)


        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
function gotouser(me) {
    console.log(me)
    var userId = $(me).attr('userid');
    sessionStorage.setItem('userid', userId);
    window.location.href = '/users/createuser';
}
function fillUser(usersid) {
    var users = {};

    $.ajax({
        type: 'POST',
        url: projectname + "/Users/GetUserById",
        data: { userId: usersid },
        success: function (result) {
            removeloader();

            users = result;
            $('#addBrokerForm').attr('userid', users.id.toString());
            $('#addBrokerForm input, #addBrokerForm select, #addBrokerForm textarea').each(function () {
                var field = $(this).attr('field-name'); // Get the field's ID
                var value = users[field];
                var id = $(this).attr('id');// Get the field's value
                if ($('#' + id).is(":checkbox")) {
                    $('#' + id).prop("checked", value);
                } else {
                    $('#' + id).val(value);
                }
            });

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            //alert("Error:" + failure);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            //alert("fail");
        }
    });
    }
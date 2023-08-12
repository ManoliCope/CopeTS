var projectname = checkurlserver();
var userRights = [];
$(document).ready(function () {
   // var usersid = sessionStorage.getItem('userid');
    var usid = $('#addUserForm').attr('userid');
  
    ////sessionStorage.removeItem('userid');
    if (usid != null && usid > 0) {
        getUserRights(usid);
        fillUser(usid);
    }
    else 
    getAllUsers();

});
function drawtable(data) {
    console.log(data)
    var generate = $('#userstable').attr('generate');
    var table = $('#userstable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            {
            'data': 'u_Id',
            className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    if (full.u_Have_Parents) {
                return `<a  href="#" title="View Users" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="showChildren(` + full.u_Id.toString() + `)"><i class="fas fa-eye"/></a>`;

                    } else {
                        return '';
                    }
                //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
            }
        },
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "u_Full_Name" },
            { "title": "Insured Number", "className": "text-center filter", "orderable": true, "data": "u_Insured_Number" },
            { "title": "Phone Number", "className": "text-center filter", "orderable": true, "data": "u_Telephone" },
            { "title": "Email", "className": "text-center filter", "orderable": true, "data": "u_Email" },
            {
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    if (generate=='1') {

                    return `<a  href="#" title="Add User" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="createChildUser(`+ full.u_Id.toString() +`)"><i class="fas fa-plus"/></a>`;
                    } else {
                        return '';
                    }
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            },{
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
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")

    // Array to store all the fields
    var user = {};
    var url = '';
    // Loop through each input, select, and textarea element inside the form
    $('#addUserForm input, #addUserForm select, #addUserForm textarea').each(function () {
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
    var userid = $('#addUserForm').attr('userid');
    user.id = userid;
    if (userid > 0)
        url = "/Users/EditUser";
    else {
        var parentid = sessionStorage.getItem('parentid');
        sessionStorage.removeItem('parentid');
        if (parentid != null)
            user.Parent_Id = parentid;
            url = "/Users/createNewUser";

    }
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
                //window.location.href = '/users/';

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

    var parentid = sessionStorage.getItem('parid');
    sessionStorage.removeItem('parid');

    filter.Last_Name = $('#prname').val();
    console.log(filter)
    $.ajax({
        type: 'GET',
        url: projectname + "/Users/GetUsersList",
        data: { name: name, parentid: parentid },
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
    $('#addUserForm input, #addUserForm select, #addUserForm textarea').each(function () {
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
    //sessionStorage.setItem('userid', userId);
    window.location.href = '/users/createuser?userid=' + userId;
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
            $('#addUserForm').attr('userid', users.id.toString());
            $('#addUserForm input, #addUserForm select, #addUserForm textarea').each(function () {
                var field = $(this).attr('field-name'); // Get the field's ID
                var value = users[field];
                var id = $(this).attr('id');// Get the field's value
                if ($('#' + id).is(":checkbox")) {
                    $('#' + id).prop("checked", value);
                }
                else {
                    $('#' + id).val(value);
                }
            });
            console.log(users);
            if (users.creation_Date != null)
                $('#creationDate').text(formatDate(users.creation_Date));

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
function getUserRights(userid) {

    $.ajax({
        type: 'POST',
        url: projectname + "/Users/GetUserRights",
        data: { userid: userid },
        success: function (result) {
            
            userRights = result;

        }
    });
}
function formatDate(data) {
    
       
            var date = new Date(data);
            var day = date.getDate().toString().padStart(2, '0');
            var month = (date.getMonth() + 1).toString().padStart(2, '0');
            var year = date.getFullYear();
            return day + '/' + month + '/' + year;
        
    
    
}
function getUserPass(userid) {

    $.ajax({
        type: 'GET',
        data: { userid: userid },
        url: projectname + "/Users/getUserPass",
        
        success: function (result) {

            $('#inputPassword').val(result);

        }
    });
}

$('#changePassword').click(function(){
    var pass = $('#inputPassword').val();
    window.location.href = "/Users/ResetPass?pass=" + pass;
});
function createChildUser(parentid) {
    sessionStorage.setItem('parentid', parentid);
    window.location.href = '/users/createuser';
}
function showChildren(parentid) {
    sessionStorage.setItem('parid', parentid);
    //window.location.href = '/users/createuser';
    getAllUsers();
}
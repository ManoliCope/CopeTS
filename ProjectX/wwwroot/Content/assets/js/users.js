var projectname = checkurlserver();
var userRights = [];



$(document).ready(function () {
    // var usersid = sessionStorage.getItem('userid');
    // var usid = $('#addUserForm').attr('userid');

    ////sessionStorage.removeItem('userid');
    //if (usid != null && usid >= 0) {                    ///////1557/////// for creation its 0 so >=
    //getUserRights(usid);                          ///////1557/////// do u need it?
    //fillUser(usid);
    //}
    //else

    if ($('#userstable').length) {
        Search();
    }
    resetPassScreen();

    $("#searchprofile").click(function () {
        Search();
    });
    $("#confirmdeleteuser").click(function () {
        deleteuser(this)
    });
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
            { "title": "UserName", "className": "text-center filter", "orderable": true, "data": "u_User_Name" },
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "u_Full_Name" },
            { "title": "Email", "className": "text-center filter", "orderable": true, "data": "u_Email" },
            {
                "title": "Created On", "className": "text-center filter", "orderable": true, "data": "u_Creation_Date", "render": function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        var date = new Date(data);
                        var day = date.getDate().toString().padStart(2, '0');
                        var month = (date.getMonth() + 1).toString().padStart(2, '0');
                        var year = date.getFullYear();

                        var formattedDate = `${day}/${month}/${year}`;
                        return formattedDate;
                    }
                    return data; // Return the raw data for other types (sorting, etc.)
                },
            },
            {
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    if (generate == '1') {

                        return `<a  href="#" title="Add User" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="createChildUser(` + full.u_Id.toString() + `)"><i class="fas fa-plus"/></a>`;
                    } else {
                        return '';
                    }
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }, {
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
                    if (generate == '1') {

                        return `<a  href="#" title="Assign Product" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="gotoAssignProduct(` + full.u_Id.toString() + `)"><i class="fas fa-briefcase"/></a>`;
                    } else {
                        return '';
                    }
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            },
            {
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    if (generate == '1') {

                        return `<div id="thisloader" class="xsmloader hide"></div>
                                <a  href="#" title="Credentials" userid="` + full.u_Id.toString() + `"  class="text-black-50" onclick="getuserscredentials(this,` + full.u_Id.toString() + `)"><i class="fas fa-key"/></a>`;
                    } else {
                        return '';
                    }
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }
            //, {
            //    'data': 'u_Id',
            //    className: "dt-center editor-edit",
            //    "render": function (data, type, full, meta) {
            //        return `<a  href="#" title="Delete" userid="` + full.u_Id.toString() + `"  class="red-star" onclick="showresponsemodalbyid('confirm-delete-user',${full.u_Id},${meta.row})"><i class="fas fa-trash"/></a>`;
            //    }
            //}
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "users")
}
function drawusercredentialstable(data) {

    var table = $('#userscrendentials').DataTable({
        "data": data,
        "paging": false,
        "searching": false,
        "info": false,
        "destroy": true,
        "columns": [

            { "title": "UserName", "className": "text-center filter", "orderable": true, "data": "u_User_Name" },
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "u_Full_Name" },
            { "title": "Password", "className": "text-center filter", "orderable": true, "data": "u_Password" },
        ],
        orderCellsTop: true,
        fixedHeader: true
    });
}
function getuserscredentials(me, userid) {
    togglebtnloader(me)
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/credentialbyuser",
        data: { iduser: userid },
        success: function (result) {
            console.log(result)
            drawusercredentialstable(result)
            showresponsemodalbyid('view-credentials');
            removebtnloader(me)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me)
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me)
        }
    });

}
function resetpassword() {
    var userid = $("#old-password").attr("userid");
    var oldPass = $("#old-password").val();
    var newPass = $("#new-password").val();
    var conPass = $("#confirm-password").val();
    var pass = {};

    pass["oldPass"] = oldPass;
    pass["newPass"] = newPass;
    pass["conPass"] = conPass;
    pass["userId"] = userid;
    console.log(pass);
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/ResetPassword",
        data: pass,
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            else {
                showresponsemodal(1, "Password Changed")
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
            user.Super_Agent_Id = parentid;
        url = "/Users/createNewUser";
    }
    console.log(user);
    $.ajax({
        type: 'POST',
        url: projectname + url,
        data: { req: user },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            else {
                showresponsemodal(1, result.statusCode.message)
                if (!userid > 0)
                    $("#responsemodal button").click(function () {
                        window.location.href = '/users/details?userid=' + result.id;
                    });
                //drawtable(result);
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")


        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request - Username Exists")

        }
    });
}
$("#kt_reset").click(function () {
    var divname = $(this).closest(".card-body").attr("id")
    resetAllValues(divname);
    var dropdown = $('.select2-hidden-accessible');
    dropdown.val(null).trigger('change');
});
function Search() {
    var filter = {};
    var name = $('#prname').val();

    var parentid = sessionStorage.getItem('parid');
    sessionStorage.removeItem('parid');

    filter.Last_Name = $('#prname').val();
    console.log(filter)
    showloader("load")

    $.ajax({
        type: 'GET',
        url: projectname + "/Users/Search",
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


        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")

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
    console.log(me)
    togglebtnloader(me)
    //var thisid = $(me).attr("userid")
    var thisid = $(me).closest("#confirm-delete-user").attr("actid")
    //var thisid = $("#userstable").closest("row").attr("userid")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Users/DeleteUsers",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {

                removebtnloader(me);
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                Search();      ///////1557///////    why do u need to get all users after deleting?
            }


            else
                showresponsemodal(result.statusCode.code, result.statusCode.message)

            removebtnloader(me)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        }
    });
}
function gotouser(me) {
    //console.log(me)
    var userId = $(me).attr('userid');
    //sessionStorage.setItem('userid', userId);
    window.location.href = '/users/details?userid=' + userId;
}
function formatDate(data) {


    var date = new Date(data);
    var day = date.getDate().toString().padStart(2, '0');
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var year = date.getFullYear();
    return day + '/' + month + '/' + year;



}
function getUserPass(me, userid) {
    togglebtnloader(me)

    $.ajax({
        type: 'GET',
        data: { userid: userid },
        url: projectname + "/Users/getUserPass",
        success: function (result) {

            $('#inputPassword').val(result);
            showresponsemodalbyid('openPasswordView');
            removebtnloader(me)
        }, failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        }
    });
}
$('#changePassword').click(function () {

    var pass = $('#inputPassword').val();
    var userid = $('#inputPassword').attr("userid");
    var passuser = {
        password: pass,
        id: userid
    };
    sessionStorage.setItem('pass', JSON.stringify(passuser));
    window.location.href = "/Users/Reset";
});
function createChildUser(parentid) {
    sessionStorage.setItem('parentid', parentid);
    window.location.href = '/users/create?userid=' + parentid;
}
function showChildren(parentid) {
    sessionStorage.setItem('parid', parentid);
    //window.location.href = '/users/createuser';
    Search();
}
function resetPassScreen() {
    var pass = JSON.parse(sessionStorage.getItem('pass'));
    if (pass) {
        sessionStorage.removeItem('pass');
        if (pass != null) {
            $("#old-password").val(pass['password']);
            $("#old-password").attr('userid', pass['id']);
        }
    }
}
function gotoAssignProduct(userid) {
    window.location.href = '/users/assignproduct?userid=' + userid;
}
$('#saveUploadedLogo').click(function () {

    var thisformData = new FormData();
    var $fileInput = $('#uploadFile');

    var selectedfiles = Getuploadedlogo($fileInput)
    if (selectedfiles) {
        thisformData = selectedfiles;
        thisformData.append("UsersId", $("#addUserForm").attr('userid'));
    }
    else
        return false;


    showloader("load");
    $.ajax({
        url: projectname + "/Users/saveUploadedLogo",
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            else {
                $(".logoname").text(result.uploadedFile)
                showresponsemodal(1, result.statusCode.message)
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")


        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")

        }
    });

});

function Getuploadedlogo(me) {
    var files = me.get(0).files;
    //var files = $(me)[0].files;
    var formData = new FormData();

    if (files.length > 0) {
        var allowedExtensions = ['png', 'jpg', 'jpeg', 'pnb'];
        var valid = true;
        for (var i = 0; i != files.length; i++) {
            var path = files[i].name.split('.');
            var extension = path[path.length - 1]
            if ($.inArray(extension.toLowerCase(), allowedExtensions) < 0)
                if ($.inArray(extension, allowedExtensions) < 0)
                    valid = false;

            formData.append("files", files[i]);
        }
        return formData;
    } else {
        return formData;
    }
}
$('#logo').click(function (me) {
    togglebtnloader(me)
    showresponsemodalbyid('openLogoUpload');
    removebtnloader(me)
});

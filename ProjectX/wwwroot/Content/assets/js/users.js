var projectname = checkurlserver();

$(document).ready(function () {
    $("#search").click(function () {
        var data = [
            {
                "name": "John Doe",
                "allProfileTypes": "ProfileA",
                "phoneNumber": "+1 555-123-4567",
                "idProfile": 123456789
            },
            {
                "name": "Jane Smith",
                "allProfileTypes": "ProfileB",
                "phoneNumber": "+1 555-987-6543",
                "idProfile": 987654321
            },
            {
                "name": "Alex Johnson",
                "allProfileTypes": "ProfileC",
                "phoneNumber": "+1 555-555-5555",
                "idProfile": 555555555
            },
            {
                "name": "Emily Davis",
                "allProfileTypes": "ProfileA",
                "phoneNumber": "+1 555-111-2222",
                "idProfile": 111222333
            },
            {
                "name": "Michael Brown",
                "allProfileTypes": "ProfileB",
                "phoneNumber": "+1 555-444-7777",
                "idProfile": 444777888
            }
        ]
        drawtable(data)
        //Search();
    });
    drawtable();

});

function drawtable(data) {
    //console.log(data)

    var table = $('#userstable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "name" },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "allProfileTypes" },
            { "title": "Phone Number", "className": "text-center filter", "orderable": true, "data": "phoneNumber" },
            {
                'data': 'idUsers',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Edit" userid="` + full.idUsers + `"  class="text-black-50" onclick="gotouser(this)"><i class="fas fa-book"/></a>`;
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

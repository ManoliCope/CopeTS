﻿var projectname = checkurlserver();

$(document).ready(function () {
    $("#searchprofile").click(function () {
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

    var table = $('#profiletable').DataTable({
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
                'data': 'idProfile',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Edit" profid="` + full.idProfile + `"  class="text-black-50" onclick="gotoprofile(this)"><i class="fas fa-book"/></a>`;
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "profile")
}


function Search() {
    if (validateForm("#searchform")) {
        return;
    }
    showloader("load")

    var filter = {
        profileName: $("#prname").val().trim(),
        idProfileType: $("#sprtype").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Profile/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.profiles);
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

function createNewUser() {
    // Array to store all the fields
    var user = {};

    // Loop through each input, select, and textarea element inside the form
    $('#addBrokerForm input, #addBrokerForm select, #addBrokerForm textarea').each(function () {
        var field = $(this).attr('field-name'); // Get the field's ID
        var value = $(this).val(); // Get the field's value
        if ($(this).attr('type') == 'checkbox')
            value = $(this).prop("checked");
        if (value == null) {
            var id = '#'+$(this).attr('id');
            value = $(id).val();
        }

        var obj = {};
        user[field] = value;
       
    });

    console.log(user);
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/createNewUser",
        data: { user: user },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.profiles);
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
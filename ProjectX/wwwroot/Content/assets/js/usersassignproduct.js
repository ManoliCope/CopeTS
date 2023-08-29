﻿var projectname = checkurlserver();
var userRights = [];



$(document).ready(function () {                     
   
    drawwtable([]);
    Search();
});

function drawwtable(data) {
    console.log(data)
    var table = $('#assprodtable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
           
            { "title": "Product", "className": "text-center filter", "orderable": true, "data": "pR_Name" },
            { "title": "Issuing Fees", "className": "text-center filter", "orderable": true, "data": "uP_IssuingFees" },
            { "title": "Creation Date", "className": "text-center filter", "orderable": true, "data": "uP_CreationDate" },
            {
                "title": "Files",
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="ViewFiles" userid="` + full.u_Id.toString() + `"  class="red-star" ><i class="fas fa-eye"/></a>`;
                }
            },
            {
                
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="ViewFiles" onclick="deleteUsersProduct(` + full.u_Id.toString() + `)"  class="red-star" ><i class="fas fa-trash"/></a>`;
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "usersProduct")
}

function openAssignView() {
    
    showresponsemodalbyid('openAssignProdView');
}

$('#saveUsersProd').click(function () {
    //if (validateForm(".container-fluid")) {
    //    return;
    //}
    var req = {};
    req['Action'] = 'Create';
    req['ProductId'] = $('#productId').val();
    req['IssuingFees'] = $('#issuingFees').val();
    req['UsersId'] = $('#assprodtable').attr('userid');

    showloader("load");
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/assignUsersProduct",
        data: { req: req },
        success: function (result) {
            removeloader();
            console.log(result)

            if (result.statusCode.code != 1)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            else {
                Search();
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

function Search() {
    var userid = $('#assprodtable').attr('userid');

    $.ajax({
        type: 'GET',
        url: projectname + "/Users/getUsersProduct",
        data: { userid : userid },
        success: function (result) {
            removeloader();
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawwtable(result.usersproduct);
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
function deleteUsersProduct(userid) {
    showloader("load");
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/deleteUsersProduct",
        data: { userid: userid },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            else {
                Search();
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
}
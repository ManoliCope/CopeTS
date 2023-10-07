﻿var projectname = checkurlserver();
var userRights = [];



$(document).ready(function () {                     
   
    drawwtable([]);
    Search();
});

function drawwtable(data, directory) {
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
                    return `<a onclick="testhere()"  title="ViewFiles" userid="` + full.u_Id.toString() + `"  class="red-star" ><i class="fas fa-eye"/></a>`;
                }
            },
            {
                
                'data': 'u_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="ViewFiles" onclick="deleteUsersProduct(` + full.uP_Id.toString() + `)"  class="red-star" ><i class="fas fa-trash"/></a>`;
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "usersProduct")
}
function testhere() {
    // URL you want to open in a new tab
    var urlToOpen = "file:///C:/Users/it4/OneDrive%20-%20Securite%20Assurance/Desktop/usafile/25/WhatsApp%20Image%202023-09-11%20at%209.32.56%20AM.pdf";

    // Open the URL in a new tab
    //var newTab = window.open(urlToOpen, '_blank');
}

function openAssignView() {
    
    showresponsemodalbyid('openAssignProdView');
}

$('#saveUsersProd').click(function () {

    var thisformData = new FormData();
    var $fileInput = $('#uploadFile');

    var selectedfiles = Getuploadedpdf($fileInput)
    if (selectedfiles) {
        thisformData = selectedfiles;
        thisformData.append("Action","Create");
        thisformData.append("ProductId", $("#productId").val());
        thisformData.append("IssuingFees", $("#issuingFees").val());
        thisformData.append("UsersId", $("#assprodtable").attr('userid'));
    }
    else
        return false;


    showloader("load");
    $.ajax({
        url: projectname + "/Users/assignUsersProduct",
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
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


function Getuploadedpdf(me) {
    var files = me.get(0).files;
    //var files = $(me)[0].files;
    var formData = new FormData();

    if (files.length > 0) {
        var allowedExtensions = ['pdf'];
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
                drawwtable(result.usersproduct,result.directory);
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
function deleteUsersProduct(upid) {
    showloader("load");
    $.ajax({
        type: 'POST',
        url: projectname + "/Users/deleteUsersProduct",
        data: { upid: upid },
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
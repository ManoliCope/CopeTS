var projectname = checkurlserver();

$(document).ready(function () {
    drawtable();

    if ($("#search").length > 0)
        Search()

    $("#search").click(function () {
        Search();
    });
    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname);
        resetdatatable("#plantable");
    });
    $("#create").click(function () {
        addnew();
    });
    $("#edit").click(function () {
        edit();
    });
    $("#btndelete").click(function () {
        showresponsemodalbyid('confirm-email-approval', $("#divinfo").attr("mid"))
    });
    $("#confirmdeletebtn").click(function () {
        deleteplan(this);
    });
});

function drawtable(data) {
    console.log(data)
    var table = $('#manualproductiontable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            /*{ "title": "ID", "className": "text-center filter", "orderable": true, "data": "pB_Id" },*/
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "pB_Title" },
            {
                "title": "Creation Date", "className": "text-center filter", "orderable": true, "data": "pB_CreationDate",
                "render": function (data, type, row) {
                    var date = new Date(data);
                    var formattedDate = formatDate_DdMmYyyy(date);
                    return formattedDate;

                    return data;
                }
            },
            {
                'data': 'pB_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  title="Show Policies" productiontitle="` + full.pB_Title + `" productionbatchid="` + full.pB_Id + `"  class="text-black-50" onclick="getAPoliciesByBatchId(this)" ><i class="fas fa-eye "></i></a>`;

                }
            }

            //{ "title": "Description", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            //{
            //    'data': 'pB_Id',
            //    className: "dt-center editor-edit",
            //    "render": function (data, type, full) {
            //        return `<a title="Edit" planid="` + full.pB_Id + `"  class="text-black-50" onclick="gotoplan(this)"><i class="fas fa-edit pr-1"></i></a>`;
            //    }
            //},
            //{
            //    'data': 'pB_Id',
            //    className: "dt-center editor-edit",
            //    "render": function (data, type, full, meta) {
            //        return `<a  title="Delete" productionbatchid="` + full.pB_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.pB_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


            //    }
            //}
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
        "title": $("#title").val(),
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/ProductionBatch/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.productionbatch);
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

function addnew() {
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")
    var planReq = {
        "title": $("#title").val(),
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Plan/CreatePlan",
        data: { req: planReq },
        success: function (result) {
            console.log(result)
            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("plan", "Edit", result.id);
            });

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deletebatch(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Plan/DeletePlan",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#plantable').length > 0) {
                    deletedatatablerowbyid(thisid, "pL_Id", "plantable")
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Plan")

            }
            else
                showresponsemodal(result.statusCode.code, result.statusCode.message)

            removebtnloader(me);
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

function clearimportpopup() {
    $('#import-production-batch').find("#divinfo").find('input,select').css("border-color", "#e2e7f1").val('');
    //$("#filtericon").css("color", "inherit");
    $("#divproductiontable").attr("hidden", "hidden")
    $("#import-production-batch").find(".modal-footer").attr("hidden", "hidden")
    $("#import-production-batch .modal-dialog").removeClass("modal-lg")
    showresponsemodalbyid('import-production-batch');
}
$("#productionbatchimport").click(function () {
    importproduction();
});
function importproduction() {

    if (validateForm("#divinfo")) {
        return;
    }

    if ($('#productionbatchtable tbody tr').length == 0) {
        $(".importresponse").text("No Batch Uploaded!")
        return
    }
    else
        $(".importresponse").text("")


    var table = $('#productionbatchtable').DataTable();
    var importedbatch = []
    importedbatch = table.data().toArray();
    console.log(importedbatch)

    //var title = $("#title").val();
    var stringifiedreq = JSON.stringify(importedbatch);
    var batchtitle = $("#batch-title").val();
    //var filter = {
    //    importedbatch: stringifiedreq,
    //    title: $("#title").val()
    //}

    $.ajax({
        type: 'POST',
        url: projectname + "/ProductionBatch/importproduction",
        data: { importedbatch: stringifiedreq, title: batchtitle },
        success: function (result) {
            console.log(result)
            if (result.statusCode.code == 1) {
                //drawproductionbatchtable(result.productionbatches)
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                Search()
            }
            else {
                //showresponsemodal(result.statusCode.code, result.statusCode.message)
                // SearchContract();
                drawproductionbatchtable(result.productionbatches)
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

var importbutton = $("#importupload");
importbutton.click(function () {

    if (validateForm("#import-production-batch")) {
        return;
    }
    var importupload = $(this).parent().find(".file-upload");
    importupload.click();

    importupload.change(function () {
        togglebtnloader($("#importupload"));
        //var profileid = $("#prname").attr("name")

        importproductionfiles(this)

        importupload.unbind();
    });
});

function importproductionfiles(me) {
    var thisformData = new FormData();

    var selectedfiles = GetProductions(me)
    if (selectedfiles)
        thisformData = selectedfiles;
    else
        return false;

    $.ajax({
        url: projectname + '/ProductionBatch/exceltotable',
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (result) {
            $("#import-production-batch .modal-dialog").addClass("modal-lg")
            $("#divproductiontable").removeAttr("hidden")
            drawproductionbatchtable(result)
            $(me).val('');
            $("#import-production-batch").find(".modal-footer").removeAttr("hidden");

            removebtnloader($("#importupload"));
            $(me).closest(".modal").find(".importresponse").html('').css("color", "Green")

            //$("#import-adherent").find(".modal-footer").show();

        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            $('#post').html(msg);
        }
    });

}
function GetProductions(me) {


    var files = $(me)[0].files;
    var formData = new FormData();

    if (files.length > 0) {
        var allowedExtensions = ['xlsx', 'xls'];
        var valid = true;
        for (var i = 0; i != files.length; i++) {
            var path = files[i].name.split('.');
            var extension = path[path.length - 1]
            if ($.inArray(extension.toLowerCase(), allowedExtensions) < 0)
                if ($.inArray(extension, allowedExtensions) < 0)
                    valid = false;

            formData.append("files", files[i]);
        }

        if (!valid) {
            //addFileAlert('Not allowed file extension', 'danger')
            //alert('Not allowed file extension')
            removebtnloader($(".btnFileUpload"));
            $(me).closest(".modal").find(".importresponse").html('Not allowed file extension').css("color", "red")

            removebtnloader($("#importupload"));
            //$("#import-adherent").find(".modal-footer").hide();

            return;
        }


        return formData;
    } else {
        return formData;
    }
}
function importadherents() {

    if (validateForm("#divinfo")) {
        return;
    }

    if ($('#adherenttable tbody tr').length == 0) {
        $(".importresponse").text("No Adherents Uploaded!")
        return
    }
    else
        $(".importresponse").text("")


    var table = $('#adherenttable').DataTable();
    var importedadherents = []
    importedadherents = table.data().toArray();
    var filter = {
        adherents: importedadherents,
        idProduct: $("#productid").val(),
        IdProfile: $("#profileid").val(),
        fromdate: $("#datefrom").val(),
        todate: $("#dateto").val(),
    }

    var stringifiedreq = JSON.stringify(filter)
    $.ajax({
        type: 'POST',
        url: projectname + "/Profile/importadherents",
        data: { reqadherent: stringifiedreq },
        success: function (result) {
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                SearchContract();
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
function drawproductionbatchtable(result) {
    console.log(result);
    var table = $('#productionbatchtable').DataTable({
        "data": result,
        "paging": true,
        "ordering": true, // Enable ordering
        "order": [[17, 'asc']], // Order based on the 'status' column (index 15) in ascending order

        "filter": false,
        "searching": true,
        "destroy": true,
        "lengthMenu": [[5, -1], [5, "All"]],
        "columns": [
            { "title": "ReferenceNumber", "className": "text-center filter", "orderable": true, "data": "referenceNumber" },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "type" },
            { "title": "Plan", "className": "text-center filter", "orderable": true, "data": "plan" },
            { "title": "Zone", "className": "text-center filter", "orderable": true, "data": "zone" },
            { "title": "Days", "className": "text-center filter", "orderable": true, "data": "days" },
            { "title": "StartDate", "className": "text-center filter", "orderable": true, "data": "startDate" },
            { "title": "FirstName", "className": "text-center filter", "orderable": true, "data": "firstName" },
            { "title": "MiddleName", "className": "text-center filter", "orderable": true, "data": "middleName" },
            { "title": "LastName", "className": "text-center filter", "orderable": true, "data": "lastName" },
            { "title": "DateOfBirth", "className": "text-center filter", "orderable": true, "data": "dateOfBirth" },
            { "title": "Age", "className": "text-center filter", "orderable": true, "data": "age" },
            { "title": "Gender", "className": "text-center filter", "orderable": true, "data": "gender" },
            { "title": "PassportNumber", "className": "text-center filter", "orderable": true, "data": "passportNumber" },
            { "title": "Nationality", "className": "text-center filter", "orderable": true, "data": "nationality" },
            { "title": "PremiumInUSD", "className": "text-center filter", "orderable": true, "data": "premiumInUSD" },
            { "title": "NetInUSD", "className": "text-center filter", "orderable": true, "data": "netInUSD" }
            , { "title": "Reason", "className": "text-center filter", "orderable": true, "data": "reason" }
            , { "visible": false, "data": "status" }

        ],
        orderCellsTop: true,
        fixedHeader: true,
        "rowCallback": function (row, data) {
            if (data.status == 0) {
                $(row).addClass("validaterow");
            }

        }
    });

}
function getAPoliciesByBatchId(me) {
    var batchid = $(me).attr('productionbatchid');
    var productiontitle = $(me).attr('productiontitle');
    $.ajax({
        url: projectname + '/Report/GenerateManualPolicies',
        data: { batchid: batchid },
        type: "POST",
        xhrFields: {
            responseType: 'blob'
        },
        success: function (response) {
            var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var blobUrl = URL.createObjectURL(blob);

            var link = document.createElement('a');
            link.href = blobUrl;
            link.download = productiontitle + ' Policies.xlsx';
            link.click();

            URL.revokeObjectURL(blobUrl);
        },
        error: function (error) {
            console.error("Error generating report:", error);
        }
    });
}
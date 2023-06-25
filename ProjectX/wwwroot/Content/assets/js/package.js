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
        resetdatatable("#packagetable");
    });
    $("#create").click(function () {
        addnew();
    });
    $("#edit").click(function () {
        edit();
    });
    $("#btndelete").click(function () {
        showresponsemodalbyid('confirm-email-approval', $("#title").attr("mid"))
    });
    $("#confirmdeletebtn").click(function () {
        deletepkg(this);
    });
});

function drawtable(data) {
    console.log(data)

    var table = $('#packagetable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "P_Id", "className": "column-id text-center filter", "orderable": true, "data": "p_Id" },
            { "title": "P_Name", "className": "column-name text-center filter", "orderable": true, "data": "p_Name" },
            { "title": "PR_Id", "className": "column-cob text-center filter", "orderable": true, "data": "pR_Id" },
            { "title": "P_ZoneID", "className": "column-cob-id text-center filter", "orderable": true, "data": "p_ZoneID" },
            { "title": "P_Remove_deductable", "className": "column-remove-deductible text-center filter", "orderable": true, "data": "p_Remove_deductable" },
            { "title": "P_Adult_No", "className": "column-adult-no text-center filter", "orderable": true, "data": "p_Adult_No" },
            { "title": "P_Children_No", "className": "column-children-no text-center filter", "orderable": true, "data": "p_Children_No" },
            {
                "title": "p_PA_Included",
                "className": "column-pa-included text-center filter",
                "orderable": true,
                "data": "p_PA_Included",
                "render": function (data) {
                    return data ? "Yes" : "No";
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
        "P_Id": $("#P_Id").val(),
        "P_Name": $("#P_Name").val(),
        "PR_Id": $("#PR_Id").val(),
        "P_ZoneID": $("#P_ZoneID").val(),
        "P_Remove_deductable": $("#P_Remove_deductable").val(),
        "P_Adult_No": $("#P_Adult_No").val(),
        "P_Children_No": $("#P_Children_No").val(),
        "P_PA_Included": $("#P_PA_Included").prop('checked')
    };


    $.ajax({
        type: 'POST',
        url: projectname + "/package/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.package);
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

    var pkgReq = {
        "P_Id": $("#P_Id").val(),
        "P_Name": $("#P_Name").val(),
        "PR_Id": $("#PR_Id").val(),
        "P_ZoneID": $("#P_ZoneID").val(),
        "P_Remove_deductable": $("#P_Remove_deductable").val(),
        "P_Adult_No": $("#P_Adult_No").val(),
        "P_Children_No": $("#P_Children_No").val(),
        "P_PA_Included": $("#P_PA_Included").prop('checked')
    };



    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/package/Createpackage",
        data: { req: pkgReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("package", "Edit", 35);
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

function edit() {
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")
    var pkgReq = {
        "id": $("#divinfo").attr("mid"),
        "name": $("#name").val(),
        "cob": $("#cob").val(),
        "cobId": $("#cobId").val(),
        "plan": $("#plan").val(),
        "planId": $("#planId").val(),
        "zone": $("#zone").val(),
        "zoneId": $("#zoneId").val(),
        "remove_deductable": $("#remove_deductable").val(),
        "adult_no": $("#adult_no").val(),
        "children_no": $("#children_no").val(),
        "pa_included": $("#pa_included").prop('checked')
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/package/Editpackage",
        data: { req: pkgReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(1, result.statusCode.message, "package")
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deletepkg(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/package/Deletepackage",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#packagetable').length > 0) {
                    var myTable = $('#packagetable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "package")

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
function gotopkg(me) {
    showloader("load")
    window.location.href = "/package/edit/" + $(me).attr("pkgid");
    removeloader();
    return
}

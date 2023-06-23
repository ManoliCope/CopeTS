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
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "pR_Id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            //{ "title": "Description", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            { "title": "Family", "className": "text-center filter", "orderable": true, "data": "pR_Is_Family" },
            {
                "title": "Activation Date", "className": "text-center filter", "orderable": true, "data": "pR_Activation_Date",
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        var date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
                }
            },
            { "title": "Active", "className": "text-center filter", "orderable": true, "data": "pR_Is_Active" },
            //{ "title": "Is_Deductible", "className": "text-center filter", "orderable": true, "data": "pR_Is_Deductible" },
            //{ "title": "Sports Activities", "className": "text-center filter", "orderable": true, "data": "pR_Sports_Activities" },
            { "title": "Additional Benefits", "className": "text-center filter", "orderable": true, "data": "pR_Additional_Benefits" },
            {
                'data': 'PR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Edit" pkgid="` + full.pR_Id + `"  class="text-black-50" onclick="gotopkg(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'PR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="Delete" pkgid="` + full.pR_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.pR_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


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
        "id": $("#id").val(),
        "name": $("#name").val(),
        "cobId": $("#cobId").val(),
        "zoneId": $("#zoneId").val(),
        "remove_deductable": $("#remove_deductable").val(),
        "adult_no": $("#adult_no").val(),
        "children_no": $("#children_no").val(),
        "pa_included": $("#pa_included").prop('checked')
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
                drawtable(result.packages);
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
        "id": $("#id").val(),
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
        "id": $("#id").val(),
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

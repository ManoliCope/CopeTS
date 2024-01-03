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
    var table = $('#plantable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "pL_Id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "pL_Title" },
            //{ "title": "Description", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            {
                'data': 'pL_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a title="Edit" planid="` + full.pL_Id + `"  class="text-black-50" onclick="gotoplan(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'pL_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  title="Delete" planid="` + full.pL_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.pL_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


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
        "title": $("#title").val(),
    }


    $.ajax({
        type: 'POST',
        url: projectname + "/Plan/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.plan);
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

function edit() {
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")
    var planReq = {
        "id": $("#divinfo").attr("mid"),
        "title": $("#title").val(),
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Plan/EditPlan",
        data: { req: planReq },
        success: function (result) {
            removeloader();
            showresponsemodal(1, result.statusCode.message)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deleteplan(me) {
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
function gotoplan(me) {
    showloader("load")
    window.location.href = "/plan/edit/" + $(me).attr("planid");
    removeloader();
    return
}


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
        resetdatatable("#benefittable");
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
        deleteben(this);
    });
});

function drawtable(data) {
    console.log(data)

    var table = $('#benefittable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "b_Id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "b_Title" },
            { "title": "Limit", "className": "text-center filter", "orderable": true, "data": "b_Limit" },
            {
                "data": 'id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a href="#" title="Edit" benid="` + full.b_Id + `" class="text-black-50" onclick="gotoben(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                "data": 'id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a href="#" title="Delete" benid="` + full.b_Id + `" class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.b_Id},${meta.row})"><i class="fas fa-times red"></i></a>`;
                }
            }
        ],
        "orderCellsTop": true,
        "fixedHeader": true
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
        "title": $("#title").val(),
        "limit": isNaN(parseFloat($("#limit").val())) ? $("#limit").val() : parseFloat($("#limit").val())
    };

    $.ajax({
        type: 'POST',
        url: projectname + "/benefit/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            console.log(result)
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.benefit);
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


    var benReq = {
        "id": $("#id").val(),
        "title": $("#title").val(),
        "limit": parseFloat($("#limit").val())
    };



    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/benefit/Createbenefit",
        data: { req: benReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("benefit", "Edit", result.id);
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

    var benReq = {
        "id": $("#divinfo").attr("mid"),
        "title": $("#title").val(),
        "limit": parseFloat($("#limit").val())
    };


    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/benefit/Editbenefit",
        data: { req: benReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(1, result.statusCode.message, "benefit")
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deleteben(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/benefit/Deletebenefit",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#benefittable').length > 0) {
                    var myTable = $('#benefittable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "benefit")

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
function gotoben(me) {
    showloader("load")
    window.location.href = "/benefit/edit/" + $(me).attr("benid");
    removeloader();
    return
}

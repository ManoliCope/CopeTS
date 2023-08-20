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
        //resetdatatable("#bentitletable");
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
        deletebentitle(this);
    });
});

function drawtable(data) {
    console.log(data)
    var table = $('#bentitletable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "bT_Id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "bT_Title" },
            //{ "title": "Description", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            {
                'data': 'bT_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a title="Edit" bentitleid="` + full.bT_Id + `"  class="text-black-50" onclick="gotobentitle(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'bT_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  title="Delete" bentitleid="` + full.bT_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.bT_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "bentitletable")
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
        url: projectname + "/BenefitTitle/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.benefit_title);
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
    var benTitleReq = {
        "title": $("#title").val(),
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/BenefitTitle/CreateBenTitle",
        data: { req: benTitleReq },
        success: function (result) {
            console.log(result)
            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("benefittitle", "Edit", result.id);
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
    var benTitleReq = {
        "id": $("#divinfo").attr("mid"),
        "title": $("#title").val(),
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/BenefitTitle/EditBenTitle",
        data: { req: benTitleReq },
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

function deletebentitle(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/BenefitTitle/DeleteBenTitle",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#bentitletable').length > 0) {
                    deletedatatablerowbyid(thisid, "bT_Id", "bentitletable")
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Benefit Title")

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
function gotobentitle(me) {
    showloader("load")
    window.location.href = "/benefittitle/edit/" + $(me).attr("bentitleid");
    removeloader();
    return
}


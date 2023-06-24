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
        resetdatatable("#zonetable");
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
        deletezne(this);
    });
});

function drawtable(data) {
    console.log(data)

    var table = $('#zonetable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "title" },
            {
                "title": "Destination ID",
                "className": "text-center filter",
                "orderable": true,
                "data": "destinationId",
                "render": function (data) {
                    return data.join(", ");
                }
            },
            {
                "title": "Destination",
                "className": "text-center filter",
                "orderable": true,
                "data": "destination",
                "render": function (data) {
                    return data.join(", ");
                }
            },
            {
                "data": 'id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a href="#" title="Edit" benid="` + full.id + `" class="text-black-50" onclick="gotoben(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                "data": 'id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a href="#" title="Delete" benid="` + full.id + `" class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.id},${meta.row})"><i class="fas fa-times red"></i></a>`;
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
        "destinationId": [],
        "destination": []
    };

    // Retrieve multiple values for destinationId and destination
    $("#destinationId option:selected").each(function () {
        filter.destinationId.push($(this).val());
    });

    $("#destination option:selected").each(function () {
        filter.destination.push($(this).val());
    });




    $.ajax({
        type: 'POST',
        url: projectname + "/zone/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.zones);
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
   
    var zneReq = {
        "id": $("#id").val(),
        "title": $("#title").val(),
        "destinationId": [],
        "destination": []
    };

    // Populate the destinationId array
    $(".destinationId").each(function () {
        zneReq.destinationId.push($(this).val());
    });

    // Populate the destination array
    $(".destination").each(function () {
        zneReq.destination.push($(this).val());
    });

    // Convert the date to ISO format
    var tariff_starting_date = new Date($("#tariff_starting_date").val()).toISOString();
    zneReq.tariff_starting_date = tariff_starting_date;

    // Convert the numeric fields to appropriate types
    zneReq.id = parseInt(zneReq.id);
    zneReq.destinationId = zneReq.destinationId.map(function (value) {
        return parseInt(value);
    });



    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/zone/Createzone",
        data: { req: zneReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("zone", "Edit", 35);
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

    var zneReq = {
        "id": $("#divinfo").attr("mid"),
        "title": $("#title").val(),
        "destinationId": [],
        "destination": []
    };

    // Populate the destinationId array
    $(".destinationId").each(function () {
        zneReq.destinationId.push($(this).val());
    });

    // Populate the destination array
    $(".destination").each(function () {
        zneReq.destination.push($(this).val());
    });

    // Convert the date to ISO format
    var tariff_starting_date = new Date($("#tariff_starting_date").val()).toISOString();
    zneReq.tariff_starting_date = tariff_starting_date;

    // Convert the numeric fields to appropriate types
    zneReq.id = parseInt(zneReq.id);
    zneReq.destinationId = zneReq.destinationId.map(function (value) {
        return parseInt(value);
    });

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/zone/Editzone",
        data: { req: zneReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(1, result.statusCode.message, "zone")
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deletezne(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/zone/Deletezone",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#zonetable').length > 0) {
                    var myTable = $('#zonetable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "zone")

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
function gotozne(me) {
    showloader("load")
    window.location.href = "/zone/edit/" + $(me).attr("zneid");
    removeloader();
    return
}

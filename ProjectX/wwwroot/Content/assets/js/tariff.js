﻿var projectname = checkurlserver();

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
        resetdatatable("#tarifftable");
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
        deletetariff(this);
    });
});

function drawtable(data) {
    console.log(data)

    var table = $('#tarifftable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "id" },
            { "title": "Package ID", "className": "text-center filter", "orderable": true, "data": "idPackage" },
            { "title": "Package", "className": "text-center filter", "orderable": true, "data": "package" },
            { "title": "Start Age", "className": "text-center filter", "orderable": true, "data": "start_age" },
            { "title": "End Age", "className": "text-center filter", "orderable": true, "data": "end_age" },
            { "title": "Number of Days", "className": "text-center filter", "orderable": true, "data": "number_of_days" },
            { "title": "Price Amount", "className": "text-center filter", "orderable": true, "data": "price_amount" },
            { "title": "Net Premium Amount", "className": "text-center filter", "orderable": true, "data": "net_premium_amount" },
            { "title": "PA Amount", "className": "text-center filter", "orderable": true, "data": "pa_amount" },
            {
                "title": "Tariff Starting Date",
                "className": "text-center filter",
                "orderable": true,
                "data": "tariff_starting_date",
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        var date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
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
        "id": $("#id").val(),
        "idPackage": $("#idPackage").val(),
        "package": $("#package").val(),
        "start_age": $("#start_age").val(),
        "end_age": $("#end_age").val(),
        "number_of_days": $("#number_of_days").val(),
        "price_amount": $("#price_amount").val(),
        "net_premium_amount": $("#net_premium_amount").val(),
        "pa_amount": $("#pa_amount").val(),
        //"tariff_starting_date": new Date($("#tariff_starting_date").val()).toISOString()
    };


    $.ajax({
        type: 'POST',
        url: projectname + "/Tariff/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            console.log(result)
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.tariffs);
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

    var tariffReq = {
        "idPackage": $("#idPackage").val(),
        "package": $("#package").val(),
        "start_age": $("#start_age").val(),
        "end_age": $("#end_age").val(),
        "number_of_days": $("#number_of_days").val(),
        "price_amount": $("#price_amount").val(),
        "net_premium_amount": $("#net_premium_amount").val(),
        "pa_amount": $("#pa_amount").val(),
        "tariff_starting_date": new Date($("#tariff_starting_date").val()).toISOString()
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Tariff/CreateTariff",
        data: { req: tariffReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("tariff", "Edit", 35);
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
    var tariffReq = {
        "id": $("#divinfo").attr("mid"),
        "idPackage": $("#idPackage").val(),
        "package": $("#package").val(),
        "start_age": $("#start_age").val(),
        "end_age": $("#end_age").val(),
        "number_of_days": $("#number_of_days").val(),
        "price_amount": $("#price_amount").val(),
        "net_premium_amount": $("#net_premium_amount").val(),
        "pa_amount": $("#pa_amount").val(),
        "tariff_starting_date": new Date($("#tariff_starting_date").val()).toISOString()
    };


    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Tariff/EditTariff",
        data: { req: tariffReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(1, result.statusCode.message, "Tariff")
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deletetariff(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Tariff/DeleteTariff",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#tarifftable').length > 0) {
                    var myTable = $('#tarifftable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Tariff")

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
function gototariff(me) {
    showloader("load")
    window.location.href = "/tariff/edit/" + $(me).attr("tariffid");
    removeloader();
    return
}

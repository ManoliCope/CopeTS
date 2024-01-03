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
        //resetdatatable("#currtable");
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
        deletecurrencyrate(this);
    });
});

function drawtable(data) {
    console.log(data)
    var table = $('#currtable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "Currency", "className": "text-center filter", "orderable": true, "data": "cR_Currency" },
            { "title": "Rate", "className": "text-center filter", "orderable": true, "data": "cR_Rate" },
            { "title": "Creation Date", "className": "text-center filter", "orderable": true, "data": "cR_Creation_Date" },
            {
                'data': 'cR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a title="Edit" currid="` + full.cR_Id + `"  class="text-black-50" onclick="gotocurrencyrate(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'CR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  title="Delete" currid="` + full.cR_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.cR_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "currencyrate")
}

function Search() {
    if (validateForm("#searchform")) {
        return;
    }
    showloader("load")


    var filter = {
        "Currency": $("#currency").val(),
    }


    $.ajax({
        type: 'POST',
        url: projectname + "/currencyrate/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                $(result.currencyRate).each(function (index, currency) {
                    currency.cR_Creation_Date = formatDate(currency.cR_Creation_Date);

                });
                drawtable(result.currencyRate);
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
    var currReq = {
        "Currency_Id": $("#currencyId").val(),
        "Rate": $("#rateId").val()
    }
    console.log(currReq)

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/currencyrate/CreateCurrencyRate",
        data: { req: currReq },
        success: function (result) {
            console.log(result)
            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("currencyrate", "Edit", result.id);
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
    var currReq = {
        "Id": $("#divinfo").attr("mid"),
        "Currency_Id": $("#currencyId").val(),
        "Rate": $("#rateId").val()
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/currencyrate/EditCurrencyRate",
        data: { req: currReq },
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

function deletecurrencyrate(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/currencyrate/DeleteCurrencyRate",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#currtable').length > 0) {
                    deletedatatablerowbyid(thisid, "CR_Id", "currtable")
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    Search()
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Currency Rate")

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
            showresponsemodal("Error", "Bad Request - Currency Linked")
            removebtnloader(me);
        }
    });
}
function gotocurrencyrate(me) {
    showloader("load")
    window.location.href = "/currencyrate/edit/" + $(me).attr("currid");
    removeloader();
    return
}

function formatDate(data) {


    var date = new Date(data);
    var day = date.getDate().toString().padStart(2, '0');
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var year = date.getFullYear();
    return day + '/' + month + '/' + year;



}
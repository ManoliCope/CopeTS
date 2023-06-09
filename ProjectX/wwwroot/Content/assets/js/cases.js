
var projectname = checkurlserver();

$(document).ready(function () {
    // special case for avaya (no gotopage, directly viewed here)
    drawcasetable();
    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname)
        resetdatatable("#cases");
    });

    $("#searchcase").click(function () {
        SearchCases()
    });

    $("input").attr("autocomplete", "off");

    triggertooltip("#searchcase")

    if ($("#notifications-bottom-right #phoneno").text() != "") {
        $("#phoneNo").val($("#notifications-bottom-right #phoneno").text().trim())
        SearchCases()
    }
});

function validatesearchcase(divname, me) {
    const requiredFields = $(divname + ' :input');
    var inputValues = [];

    requiredFields.each(function () {
        var field = $(this).val().trim();
        inputValues.push({ val: field }); // to return flag valid
        var id = $(this).attr("id");

        //if (field == undefined || field == '') {
        //    $("#" + id).css('border-color', 'red');
        //} else {
        //    //$("#" + id).css('border-color', '#e2e7f1');
        //}
    });

    const valid = inputValues.find(v => v.val != "");
    
    return valid;
}

function SearchCases() {
    //if (!validatesearchcase("#searchform")) {
    //    $("#searchcase").tooltip('toggle');
    //    return;
    //}

    showloader("load")

    var filter = {
        policyNo: $("#certificate").val(),
        CaseNumber: $("#caseNo").val(),
        idCaseStatus: $("#idStatus").val(),
        phoneNo: $("#phoneNo").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname +"/Case/GetAllCases",
        data: { req: filter },
        success: function (result) {
            //console.log(result)
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawcasetable(result.cases);
            }

            removeloader();
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

function drawcasetable(data) {
    //console.log(data, 'casedata')
    var table = $('#cases').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            {
                'data': 'idcase',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="View case" idcase="` + full.idCase + `"  class="text-black-50" onclick="viewcase(this)"><i class="fas fa-eye"/></a>`;
                }
            },
            { "title": "Case", "className": "text-center filter", "orderable": true, "data": "caseNumber" },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "caseType" },
            { "title": "Status", "className": "text-center filter", "orderable": true, "data": "caseStatus" },
            { "title": "Date", "className": "text-center filter", "orderable": true, "data": "accidentDate" },
            { "title": "Benefit", "className": "text-center filter", "orderable": true, "data": "benefit" },
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    //$("#example tbody tr #viewcase").on('click', function (event) {
    //    $("#example tbody tr").removeClass('row_selected');
    //    $(this).closest('tr').addClass('row_selected');
    //});

    triggerfiltertable(table, "cases")
}

function viewcase(me) {
    showloader("show");
    var idcase = $(me).attr("idcase");

    window.location.href = "/Case/GetCase/" + idcase;
    removeloader();
    return


    //$.ajax({
    //    type: 'Get',
    //    url: projectname +"/Case/GetCase",
    //    data: { parameter: idcase },
    //    success: function (result) {
    //        $('#partialscreen .content-page').html("").html(result).scrollTop(0)
    //        removeloader();

    //        //console.log(result)
    //    },
    //    failure: function (data, success, failure) {
    //        showresponsemodal("Error", "Bad Request")

    //        //alert("Error:" + failure);
    //    },
    //    error: function (data) {
    //        showresponsemodal("Error", "Bad Request")
    //        //alert("fail");
    //    }
    //});
}







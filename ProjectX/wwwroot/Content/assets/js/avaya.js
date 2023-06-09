var projectname = '';

$(document).ready(function () {
    projectname = checkurlserver();
    // special case for avaya (no gotopage, directly viewed here)
    removeloader();

    drawcasetable();
    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname)
        resetdatatable("#cases");
    });

    $("#searchcase").click(function () {
        SearchCases()
    });

    //drawcasetable();

    //SearchPolicies();
    $("input").attr("autocomplete", "off");

    triggertooltip("#searchcase")

    if ($("#phoneNo").val() != "")
        SearchCases()
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
    showloader("load")

    var filter = {
        policyNo: $("#certificate").val(),
        CaseNumber: $("#caseNo").val(),
        idCaseStatus: $("#idStatus").val(),
        phoneNo: $("#phoneNo").val(),
        lob: $("#lob").val()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Avaya/GetAllCases",
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
            { "title": "Line", "className": "text-center filter", "orderable": true, "data": "lob" },
            { "title": "Case", "className": "text-center filter ", "orderable": true, "data": "caseNumber" },
            { "title": "Policy", "className": "text-center filter", "orderable": true, "data": "policyNo" },
            {
                "title": "Status", "className": "text-center filter", "orderable": true, "data": "caseStatus",

                "render": function (data, type, full) {
                    if (full.lob == "Motor")
                        return "";

                    return full.caseStatus;
                }
            },
            { "title": "Date", "className": "text-center filter", "orderable": true, "data": "accidentDate" },
            { "title": "Operator", "className": "text-center filter", "orderable": true, "data": "operator" },

        ],
        //"columns": [
        //    {
        //        'data': 'idcase',
        //        className: "dt-center editor-edit",
        //        "render": function (data, type, full) {
        //            return `<a  href="#" title="View case" idcase="` + full.idCase + `"  class="text-black-50" onclick="viewcase(this)"><i class="fas fa-eye"/></a>`;
        //        }
        //    },
        //    { "title": "Case", "className": "text-center filter", "orderable": true, "data": "caseNumber" },
        //    { "title": "Type", "className": "text-center filter", "orderable": true, "data": "caseType" },
        //    { "title": "Status", "className": "text-center filter", "orderable": true, "data": "caseStatus" },
        //    { "title": "Date", "className": "text-center filter", "orderable": true, "data": "accidentDate" },
        //    { "title": "Benefit", "className": "text-center filter", "orderable": true, "data": "benefit" },
        //],
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

    var avaya = {
        CallDate: $("#calldate").text(),
        Callid: $("#cid").text(),
        CallNumber: $("#phoneno").text(),
        CallName: $("#phonename").text(),
        Caseid: idcase,
    }

    $.ajax({
        type: 'Post',
        url: projectname + "/Avaya/SaveCallID",
        data: { avayareq: avaya },
        success: function (result) {
            window.location.href = "login?cid=" + result;
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

function avayapopup() {
    $("#notifications-bottom-right-tab").toggle()
}

function checkurlserver() {
    var url = window.location.href
    if (url.indexOf("localhost") > -1) {
        return "";
    }
    else {
        var arr = url.split("/");
        return "/" + arr[3]
    }
}

var projectname = checkurlserver();

$(document).ready(function () {
    // special case for avaya (no gotopage, directly viewed here)

    //SearchCases()

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

    $(document).on('click', '.dropdown-menu button', function (e) {
        if ($(this).parent().find("#cancelreason").val() == "") {
            $(this).parent().find("#cancelreason").css("border-color", "red")
            e.stopPropagation();
            return false;
        }
    });

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
    var flagnofeedback = false;
    if ($("#srchnofeedback").is(':checked'))
        flagnofeedback = true;

    var filter = {
        policyNo: $("#certificate").val(),
        CaseNumber: $("#caseNo").val(),
        idCaseStatus: $("#idStatus").val(),
        phoneNo: $("#phoneNo").val(),
        nofeedback: flagnofeedback,
        lob: $("#lob").val()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Case/GetMdicalAllCases",
        data: { req: filter },
        success: function (result) {
            //console.log(result, 'myresult')
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
        //"createdRow": function (row, data, dataIndex) {
        //    //$(row).addClass('datatableselectedrow');
        //    if (data.hasPendingServices == `1`) {
        //        $(row).addClass('datatableselectedrow');
        //    }
        //},
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            //{
            //    className: "dt-center editor-edit",
            //    "render": function (data, type, full, meta) {
            //        return `<a href="javascript:void(0);" class="float-right mr-1 text-black-50" onclick="gotopage('case','MotorDeclaration',${full.idCase})">
            //            <i class="fas fa-book"></i>
            //        </a>`
            //    }
            //},
            {
                'data': 'idcase',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="View case" idcase="` + full.idCase + `"  class="text-black-50" onclick="viewcase(this)"><i class="fas fa-eye"/></a>`;
                }
            },
            { "title": "Case", "className": "text-center filter ", "orderable": true, "data": "caseNumber" },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "insuranceLine" },
            { "title": "Status", "className": "text-center filter", "orderable": true, "data": "caseStatus" },
            { "title": "Date", "className": "text-center filter", "orderable": true, "data": "accidentDate" },
          
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "cases")
}


function viewcase(me) {
    showloader("show");
    var idcase = $(me).attr("idcase");



    showloader("show");
    var idcase = $(me).attr("idcase");

    window.location.href = "/Case/GetMedicalCase/" + idcase;
    removeloader();
    return




    //$.ajax({
    //    type: 'Get',
    //    url: projectname + "/Case/GetMedicalCase",
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












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
        plate: $("#plateNo").val() + '/' + $("#plateLtr").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Case/GetAllTestCases",
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
        "createdRow": function (row, data, dataIndex) {
            //$(row).addClass('datatableselectedrow');
            if (data.hasPendingServices == `1`) {
                $(row).addClass('datatableselectedrow');
            }
        },
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
            { "title": "Case", "className": "text-center filter ",  "orderable": true, "data": "caseNumber" },
            {
                "title": "Name", "className": " filter ", "width": "5%","orderable": true, "data": "lastName",
                render: function (data, type, row) {
                    return row.firstName + ' ' + row.lastName ;
                }
            },
            { "title": "Mobile", "className": "text-center filter", "orderable": true, "data": "mobile" },
            //{ "title": "Status", "className": "text-center filter", "orderable": true, "data": "caseStatus" },
            { "title": "Plate", "className": "text-center filter", "orderable": true, "data": "plate" },
            { "title": "Comment", "className": "text-center filter", "orderable": true, "data": "comment" },
            { "title": "Date", "className": "text-center filter", "orderable": true, "data": "accidentDate" },
            { "title": "Operator", "className": "text-center filter", "orderable": true, "data": "operator" },
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    //return `<a  href="#" title="View Policy"  class="text-black-50" data-toggle="modal" 
                    //    data-target="#view-policy" polid="` + full.idPolicy + `"  onclick="viewpolicy(this)"><i class="fas fa-eye"/></a> <div id="thisloader" class="smloader hide"></div>`;
                    return `<a href="#" title="View Services" class="text-black-50" onclick="viewservices(this,'` + full.idCase + `','` + meta.row + `')">
                            <i class="fas fa-bars"></i></a>
                            <div id="thisloader" class="smloader hide"></div>`
                }

            },
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "cases")
}














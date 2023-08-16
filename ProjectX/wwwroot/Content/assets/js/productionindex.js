﻿var projectname = checkurlserver();
var travelinfo = {}
var addbenefits = []
$(document).ready(function () {
    $("#search").click(function () {

        //drawtable()
        Search();
    });
    drawtable();

    $(".isselect2").select2({
        tokenSeparators: [',', ' ']
    })

    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname);
        resetdatatable("#productiontable");
    });

    Search();

});



function drawtable(data) {
    console.log(data,'drawtable')

    var table = $('#productiontable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "Reference", "className": "text-center filter", "orderable": true, "data": "reference" },
            { "title": "Grand Total", "className": "text-center filter", "orderable": true, "data": "grandTotal" },
            { "title": "Duration", "className": "text-center filter", "orderable": true, "data": "duration" },
            {
                "title": "Type",
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    let typeLabel = "";

                    if (full.isIndividual) {
                        typeLabel = "Individual";
                    } else if (full.isGroup) {
                        typeLabel = "Group";
                    } else if (full.isFamily) {
                        typeLabel = "Family";
                    }
                    return `<label>${typeLabel}</label>`;
                }
            },
            {
                'data': 'policyID',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a   title="Edit" polid="` + full.policyID + `"  class="text-black-50" onclick="gotopol(this)"><i class="fas fa-book"/></a>`;
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
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
        reference: $("#referencename").val().trim(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Production/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            if (result.production)
            drawtable(result.production);
            else 
                drawtable();

            //if (result.statusCode.code != 1)
            //    showresponsemodal("error", result.statusCode.message)
            //else {
            //    drawtable(result.profiles);
            //}

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

           alert("Error:" + failure);
        },
        error: function (xhr,data) {
            var errorMessage;
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                errorMessage = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                errorMessage = xhr.responseText;
            } else {
                errorMessage = 'An error occurred.';
            }

            showresponsemodal("Error", errorMessage)
        }
    });
}

function removerow(me) {
    $(me).closest("tr").remove();
}

function gotopol(me) {
    showloader("load")
    window.location.href = "/production/edit/" + $(me).attr("polid");
    removeloader();
    return
}



















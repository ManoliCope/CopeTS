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
        resetdatatable("#packagetable");
        var dropdown = $('.select2-hidden-accessible');
        dropdown.val(null).trigger('change');
    });
    $(".isselect2").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    })
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
        deletepkg(this);
    });

   
});

function drawtable(data) {
    console.log(data)

    var table = $('#packagetable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "column-name text-center filter", "orderable": true, "data": "p_Id" },
            { "title": "Name", "className": "column-name text-center filter", "orderable": true, "data": "p_Name" },
            { "title": "Product", "className": "column-cob text-center filter", "orderable": true, "data": "pR_Id" },
            { "title": "Zone", "className": "column-cob-id text-center filter", "orderable": true, "data": "p_ZoneID" },
            { "title": "Remove Deductible", "className": "column-remove-deductible text-center filter", "orderable": true, "data": "p_Remove_deductable" },
            //{ "title": "Adult Number", "className": "column-adult-no text-center filter", "orderable": true, "data": "p_Adult_No" },
            //{ "title": "Children Number", "className": "column-children-no text-center filter", "orderable": true, "data": "p_Children_No" },
            {
                "title": "PA Included",
                "className": "column-pa-included text-center filter",
                "orderable": true,
                "data": "p_PA_Included",
                "render": function (data) {
                    return data ? "Yes" : "No";
                }
            },
            {
                'data': 'p_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Edit" pkgid="` + full.p_Id + `"  class="text-black-50" onclick="gotopkg(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'p_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="Delete" pkgid="` + full.p_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.p_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


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
        "Id": $("#Id").val(),
        "Name": $("#Name").val(),
        "ProductId": $("#ProductId").val(),
        "ZoneID": $("#ZoneID").val(),
        "Remove_deductable": $("#Remove_deductable").val(),
        "Adult_No": $("#Adult_No").val(),
        "Children_No": $("#Children_No").val(),
        "PA_Included": $("#PA_Included").prop('checked')
    };

    console.log(filter)
    $.ajax({
        type: 'POST',
        url: projectname + "/package/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.package);
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

    var pkgReq = {
        Name: $("#name").val(),
        ProductId: $("#product_id").val(),
        ZoneID: $("#zone_id").val(),
        Remove_deductable: $("#remove_deductible").val(),
        Adult_No: $("#adult_no").val(),
        Adult_Max_Age: $("#adult_max_age").val(),
        Children_No: $("#children_no").val(),
        Child_Max_Age: $("#child_max_age").val(),
        PA_Included: $("#pa_included").prop('checked'),
        Special_Case: $("#special_case").prop('checked'),
    };


    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/package/Createpackage",
        data: { req: pkgReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("package", "Edit", result.id);
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
    var pkgReq = {
        "id": $("#divinfo").attr("mid"),
        Name: $("#name").val(),
        ProductId: $("#product_id").val(),
        ZoneID: $("#zone_id").val(),
        Remove_deductable: $("#remove_deductible").val(),
        Adult_No: $("#adult_no").val(),
        Adult_Max_Age: $("#adult_max_age").val(),
        Children_No: $("#children_no").val(),
        Child_Max_Age: $("#child_max_age").val(),
        PA_Included: $("#pa_included").prop('checked'),
        Special_Case: $("#special_case").prop('checked'),
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/package/Editpackage",
        data: { req: pkgReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(1, result.statusCode.message)
            //showresponsemodal(1, result.statusCode.message, "package")
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deletepkg(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/package/Deletepackage",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#packagetable').length > 0) {
                    var myTable = $('#packagetable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "package")

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
function gotopkg(me) {
    showloader("load")
    window.location.href = "/package/edit/" + $(me).attr("pkgid");
    removeloader();
    return
}

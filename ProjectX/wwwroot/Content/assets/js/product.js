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
        resetdatatable("#producttable");
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
        deleteprod(this);
    });
});

function drawtable(data) {
    console.log(data)

    var table = $('#producttable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "pR_Id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            //{ "title": "Description", "className": "text-center filter", "orderable": true, "data": "pR_Title" },
            { "title": "Family", "className": "text-center filter", "orderable": true, "data": "pR_Is_Family" },
            {
                "title": "Activation Date", "className": "text-center filter", "orderable": true, "data": "pR_Activation_Date",
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        var date = new Date(data);
                        return date.toLocaleDateString();
                    }
                    return data;
                }
            },
            { "title": "Active", "className": "text-center filter", "orderable": true, "data": "pR_Is_Active" },
            //{ "title": "Is_Deductible", "className": "text-center filter", "orderable": true, "data": "pR_Is_Deductible" },
            //{ "title": "Sports Activities", "className": "text-center filter", "orderable": true, "data": "pR_Sports_Activities" },
            { "title": "Additional Benefits", "className": "text-center filter", "orderable": true, "data": "pR_Additional_Benefits" },
            {
                'data': 'PR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Edit" prodid="` + full.pR_Id + `"  class="text-black-50" onclick="gotoprod(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'PR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="Delete" prodid="` + full.pR_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.pR_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


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
        "description": $("#description").val(),
        "activation_date": $("#activation_date").val(),
        "is_deductible": $("#is_deductible").val(),
        "sports_activities": $("#sports_activities").val(),
        "additional_benefits": $("#additional_benefits").val(),
        "is_active": $("#is_active").prop('checked'),
        "is_family": $("#is_family").prop('checked')
    }


    $.ajax({
        type: 'POST',
        url: projectname + "/Product/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.products);
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
    var prodReq = {
        "title": $("#title").val(),
        "description": $("#description").val(),
        "activation_date": $("#activation_date").val(),
        "is_deductible": $("#is_deductible").val(),
        "sports_activities": $("#sports_activities").val(),
        "additional_benefits": $("#additional_benefits").val(),
        "is_active": $("#is_active").prop('checked'),
        "is_family": $("#is_family").prop('checked')
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Product/CreateProduct",
        data: { req: prodReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message, "Product")
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
    var prodReq = {
        "id": $("#title").attr("mid"),
        "title": $("#title").val(),
        "description": $("#description").val(),
        "activation_date": $("#activation_date").val(),
        "is_deductible": $("#is_deductible").val(),
        "sports_activities": $("#sports_activities").val(),
        "additional_benefits": $("#additional_benefits").val(),
        "is_active": $("#is_active").prop('checked'),
        "is_family": $("#is_family").prop('checked')
    }

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Product/EditProduct",
        data: { req: prodReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(result.statusCode.code, result.statusCode.message, "Product")
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deleteprod(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Product/DeleteProduct",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#producttable').length > 0) {
                    var myTable = $('#producttable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Product")

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
function gotoprod(me) {
    showloader("load")
    window.location.href = "/product/edit/" + $(me).attr("prodid");
    removeloader();
    return
}

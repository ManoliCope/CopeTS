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
                    return `<a  href="#" title="Edit" zneid="` + full.pR_Id + `"  class="text-black-50" onclick="gotozne(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                'data': 'PR_Id',
                className: "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a  href="#" title="Delete" zneid="` + full.pR_Id + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.pR_Id},${meta.row})" ><i class="fas fa-times red"></i></a>`;


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

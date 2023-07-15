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
        resetdatatable("#beneficiarytable");
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
        deletebeneficiary(this);
    });
});

function drawtable(data) {
    console.log(data)
    var table = $('#beneficiarytable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "bE_Id" },
            { "title": "First Name", "className": "text-center filter", "orderable": true, "data": "bE_FirstName" },
            { "title": "Middle Name", "className": "text-center filter", "orderable": true, "data": "bE_MiddleName" },
            { "title": "Last Name", "className": "text-center filter", "orderable": true, "data": "bE_LastName" },
            { "title": "Sex", "className": "text-center filter", "orderable": true, "data": "bE_SexName" },
            {
                "title": "Date of Birth",
                "className": "text-center filter",
                "orderable": true,
                "data": "bE_DOB",
                "render": function (data, type, full) {
                    if (type === 'display' || type === 'filter') {
                        var date = new Date(data);
                        var day = date.getDate().toString().padStart(2, '0');
                        var month = (date.getMonth() + 1).toString().padStart(2, '0');
                        var year = date.getFullYear();
                        return day + '/' + month + '/' + year;
                    }
                    return data;
                }
            },
            {
                "data": "bE_Id",
                "className": "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a title="Edit" beneficiaryid="${full.bE_Id}" class="text-black-50" onclick="gotobeneficiary(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                "data": "bE_Id",
                "className": "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a title="Delete" beneficiaryid="${full.bE_Id}" class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval', ${full.bE_Id}, ${meta.row})"><i class="fas fa-times red"></i></a>`;
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
        "FirstName": $("#firstName").val(),
        "LastName": $("#lastName").val(),
    }



    $.ajax({
        type: 'POST',
        url: projectname + "/Beneficiary/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.beneficiary);
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
    var beneficiaryReq = {
        "firstName": $("#firstName").val(),
        "middleName": $("#middleName").val(),
        "maidenName": $("#maidenName").val(),
        "lastName": $("#lastName").val(),
        "sex": $("#sex").val(),
        "passportNumber": $("#passportNumber").val(),
        "dateOfBirth": $("#dateOfBirth").val()
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Beneficiary/CreateBeneficiary",
        data: { req: beneficiaryReq },
        success: function (result) {
            console.log(result)
            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("beneficiary", "Edit", result.id);
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
    var beneficiaryReq = {
        "id": $("#divinfo").attr("mid"),
        "firstName": $("#firstName").val(),
        "middleName": $("#middleName").val(),
        "maidenName": $("#maidenName").val(),
        "lastName": $("#lastName").val(),
        "sex": $("#sex").val(),
        "passportNumber": $("#passportNumber").val(),
        "dateOfBirth": $("#dateOfBirth").val()
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Beneficiary/EditBeneficiary",
        data: { req: beneficiaryReq },
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

function deletebeneficiary(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Beneficiary/DeleteBeneficiary",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#beneficiarytable').length > 0) {
                    deletedatatablerowbyid(thisid, "bE_Id", "beneficiarytable")
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Beneficiary")

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
function gotobeneficiary(me) {
    showloader("load")
    window.location.href = "/beneficiary/edit/" + $(me).attr("beneficiaryid");
    removeloader();
    return
}


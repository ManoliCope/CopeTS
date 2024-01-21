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
        resetdatatable("#benefittable");
        var dropdown = $('.select2-hidden-accessible');
        dropdown.val(null).trigger('change');
    });
    $(".isselect2").select2({
        //tags: true,
        tokenSeparators: [',', ' ']
    })


    $("#samplefile").click(function () {

        downloadExcel()
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
        deletetariff(this);
    });


    var importbutton = $("#importupload");
    importbutton.click(function () {
        if (validateForm("#import-tariff-file .modal-body")) {
            return;
        }

        togglebtnloader(this)
        importtariff(this)
        //var importupload = $(this).parent().find(".file-upload");
        //importupload.click();
    });

});

function downloadExcel() {
    var fileName = "tariff.xlsx";
    var baseUrl = window.location.origin;
    var filePath = baseUrl + "/Samplefile/" + fileName;

    var a = document.createElement("a");
    a.style.display = "none";
    document.body.appendChild(a);
    a.href = filePath;
    a.download = fileName;
    a.click();
}





function drawtable(data) {
    console.log(data)

    var table = $('#tarifftable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "t_Id" },
            //{ "title": "Package ID", "className": "text-center filter", "orderable": true, "data": "p_Id" },
            { "title": "Package", "className": "text-center filter", "orderable": true, "data": "p_Name" },
            { "title": "Plan", "className": "text-center filter", "orderable": true, "data": "pL_Name" },
            { "title": "Start Age", "className": "text-center filter", "orderable": true, "data": "t_Start_Age" },
            { "title": "End Age", "className": "text-center filter", "orderable": true, "data": "t_End_Age" },
            { "title": "Number of Days", "className": "text-center filter", "orderable": true, "data": "t_Number_Of_Days" },
            { "title": "Amount", "className": "text-center filter", "orderable": true, "data": "t_Price_Amount" },
            //{ "title": "Net Premium Amount", "className": "text-center filter", "orderable": true, "data": "t_Net_Premium_Amount" },
            //{ "title": "PA Amount", "className": "text-center filter", "orderable": true, "data": "t_PA_Amount" },
            {
                "title": "Starting Date",
                "className": "text-center filter",
                "orderable": true,
                "data": "t_Tariff_Starting_Date",
                "render": function (data, type, row) {
                    if (type === "display" || type === "filter") {
                        var date = new Date(data);
                        var options = { day: '2-digit', month: '2-digit', year: 'numeric' };
                        return date.toLocaleDateString('en-GB', options);
                    }
                    return data;
                }
            },
            {
                "data": 't_Id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a title="Edit" tarid="` + full.t_Id + `" class="text-black-50" onclick="gototariff(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                "data": 't_Id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a title="Delete" tarid="` + full.t_Id + `" class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.t_Id},${meta.row})"><i class="fas fa-times red"></i></a>`;
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
        "id": $("#id").val(),
        "idPackage": $("#idPackage").val(),
        "planId": $("#idPlan").val(),
        "package": $("#package").val(),
        "start_age": $("#start_age").val(),
        "end_age": $("#end_age").val(),
        "number_of_days": $("#number_of_days").val(),
        "price_amount": $("#price_amount").val(),
        "net_premium_amount": $("#net_premium_amount").val(),
        "pa_amount": $("#pa_amount").val(),
        //"tariff_starting_date": new Date($("#tariff_starting_date").val()).toISOString()
    };


    $.ajax({
        type: 'POST',
        url: projectname + "/Tariff/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            console.log(result)
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.tariff);
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

    var tariffReq = {
        "idPackage": $("#idPackage").val(),
        "planId": $("#idPlan").val(),
        "start_age": $("#start_age").val(),
        "end_age": $("#end_age").val(),
        "number_of_days": $("#number_of_days").val(),
        "price_amount": $("#price_amount").val(),
        "net_premium_amount": $("#net_premium_amount").val(),
        "pa_amount": $("#pa_amount").val(),
        "Override_Amount": $("#Override_Amount").val(),
        "tariff_starting_date": new Date($("#tariff_starting_date").val()).toISOString()
    };


    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Tariff/CreateTariff",
        data: { req: tariffReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                gotopage("tariff", "Edit", result.id);
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
    var dateObj = new Date($("#tariff_starting_date").val());
    if (isNaN(dateObj.getTime())) {
        showresponsemodal(0, "Invalid Date")
        return false
    }


    showloader("load")
    var tariffReq = {
        "id": $("#divinfo").attr("mid"),
        "idPackage": $("#idPackage").val(),
        "planid": $("#idPlan").val(),
        "package": $("#package").val(),
        "start_age": $("#start_age").val(),
        "end_age": $("#end_age").val(),
        "number_of_days": $("#number_of_days").val(),
        "price_amount": $("#price_amount").val(),
        "net_premium_amount": $("#net_premium_amount").val(),
        "pa_amount": $("#pa_amount").val(),
        "Override_Amount": $("#Override_Amount").val(),
        "tariff_starting_date": new Date($("#tariff_starting_date").val()).toISOString()
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Tariff/EditTariff",
        data: { req: tariffReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
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

function deletetariff(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Tariff/DeleteTariff",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#tarifftable').length > 0) {
                    var myTable = $('#tarifftable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "Tariff")

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
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        }
    });
}
function gototariff(me) {
    showloader("load")

    window.location.href = "/tariff/edit/" + $(me).attr("tarid");
    removeloader();
    return
}
function browseForTariff() {

}
function showImportModel() {
    //var fileInput = document.getElementById("file");
    //fileInput.value = null;
    showresponsemodalbyid('import-tariff-file');
}



function importtariff(me) {
    var importupload = $(me).parent().find(".file-upload");
    var thisformData = new FormData();

    var $fileInput = $('#file');

    var selectedfiles = Getuploadedexcel($fileInput)
    if (selectedfiles) {
        thisformData = selectedfiles;
        thisformData.append("tarPackageid", $("#tarPackageid").val());
        thisformData.append("tarPlanid", $("#tarPlanid").val());
    }
    else
        return false;

    $.ajax({
        url: projectname + '/Tariff/exceltotable',
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (result) {
            if (result.statusCode.code != 1)
                showresponsemodal(0, result.statusCode.message)
            else {
                showresponsemodal(1, 'Tariff Uploaded')
                Search()
            }
            removebtnloader(me)

        },
        error: function (xhr, status, error) {
            //console.log('Error:'+ xhr.responseText + '. Try Again!'); 
            //var responseerror = 'Error in rows:' + xhr.responseText + '. Try Again!'
            var responseerror = 'Error. Try Again!'
            showresponsemodal(0, responseerror)
            removebtnloader(me)
        }
    });
}

function Getuploadedexcel(me) {
    var files = me.get(0).files;
    //var files = $(me)[0].files;
    var formData = new FormData();

    if (files.length > 0) {
        var allowedExtensions = ['xlsx', 'xls'];
        var valid = true;
        for (var i = 0; i != files.length; i++) {
            var path = files[i].name.split('.');
            var extension = path[path.length - 1]
            if ($.inArray(extension.toLowerCase(), allowedExtensions) < 0)
                if ($.inArray(extension, allowedExtensions) < 0)
                    valid = false;

            formData.append("files", files[i]);
        }

        //if (!valid) {
        //    removebtnloader($(".btnFileUpload"));
        //    $(me).closest(".modal").find(".importresponse").html('Not allowed file extension').css("color", "red")

        //    removebtnloader($("#importupload"));
        //    return;
        //}


        return formData;
    } else {
        return formData;
    }
}
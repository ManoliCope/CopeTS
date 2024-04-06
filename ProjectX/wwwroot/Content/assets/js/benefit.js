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
        deleteben(this);
    });
    checkBenFromat();


    var importbutton = $("#importupload");
    importbutton.click(function () {
        if (validateForm("#import-benefits-file .modal-body")) {
            return;
        }

        togglebtnloader(this)
        importbenefits(this)
        //var importupload = $(this).parent().find(".file-upload");
        //importupload.click();
    });
 
});

function importbenefits(me) {
    var importupload = $(me).parent().find(".file-upload");
    var thisformData = new FormData();

    var $fileInput = $('#file');

    var selectedfiles = Getuploadedexcel($fileInput)
    if (selectedfiles) {
        thisformData = selectedfiles;
        thisformData.append("packageid", $("#packageid").val());
        //thisformData.append("titleid", $("#titleid").val());
    }
    else
        return false;

    $.ajax({
        url: projectname + '/Benefit/exceltotable',
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (data) {
            showresponsemodal(1, 'Benefits Uploaded')
            removebtnloader(me)
            $('#search').click();
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


function drawtable(data) {
    console.log(data)

    var table = $('#benefittable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "b_Id" },
            { "title": "Title", "className": "text-center filter truncatetd ", "orderable": true, "data": "b_Title" },
            { "title": "Package", "className": "text-center filter truncatetd", "orderable": true, "data": "p_Name" },
            { "title": "Limit", "className": "text-center filter", "orderable": true, "data": "b_Limit" },
            {
                "data": 'id',
                "className": "dt-center editor-edit", "width": "20px",
                "render": function (data, type, full) {
                    return `<a href="#" title="Edit" benid="` + full.b_Id + `" class="text-black-50" onclick="gotoben(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                "data": 'id',
                "className": "dt-center editor-edit", "width": "20px",
                "render": function (data, type, full, meta) {
                    return `<a href="#" title="Delete" benid="` + full.b_Id + `" class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.b_Id})"><i class="fas fa-times red"></i></a>`;
                }
            }
        ],
        "orderCellsTop": true,
        "fixedHeader": true
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
        "title": $("#title").val(),
        "packageId": $("#packageId").val(),
        "limit": isNaN(parseFloat($("#limit").val())) ? $("#limit").val() : parseFloat($("#limit").val()),
        "additionalBenefits": isNaN(parseFloat($('#additional_benefits').val())) ? $("#additional_benefits").val() : parseFloat($("#additional_benefits").val()),
        "additionalBenefitsFormat": $('#additional_benefits_format').val(),
        "titleId": $('#titleId').val()

    };

    $.ajax({
        type: 'POST',
        url: projectname + "/benefit/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            console.log(result)
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.benefit);
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


    var benReq = {
        "id": $("#id").val(),
        "title": $("#title").val(),
        "packageId": $("#packageId").val(),
        "is_Plus": $("#is_Plus").prop('checked'),
        "limit": $("#limit").val(),
        "additionalBenefits": isNaN(parseFloat($('#additional_benefits').val())) ? $("#additional_benefits").val() : parseFloat($("#additional_benefits").val()),
        "additionalBenefitsFormat": parseFloat($('#additional_benefits_format').val()),
        "titleId": $('#titleId').val()

    };



    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/benefit/Createbenefit",
        data: { req: benReq },
        success: function (result) {
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            triggerresonseclick("benefit", "Edit", result.id)
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

    var benReq = {
        "id": $("#divinfo").attr("mid"),
        "title": $("#title").val(),
        "packageId": $("#packageId").val(),
        "is_Plus": $("#is_Plus").prop('checked'),
        "limit": $("#limit").val(),
        "additionalBenefits": parseFloat($('#additional_benefits').val()),
        "additionalBenefitsFormat": parseFloat($('#additional_benefits_format').val()),
        "titleId": $('#titleId').val()


    };


    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/benefit/Editbenefit",
        data: { req: benReq },
        success: function (result) {
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(result.statusCode.code, result.statusCode.message)
            //triggerresonseclick("benefit", "Edit", result.id)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deleteben(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/benefit/Deletebenefit",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#benefittable').length > 0) {
                    deletedatatablerowbyid(thisid, "b_Id","benefittable")
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "benefit")

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
function gotoben(me) {
    showloader("load")
    window.location.href = "/benefit/edit/" + $(me).attr("benid");
    removeloader();
    return
}
$('#is_Plus').change(function () {

    checkBenFromat();
});
function checkBenFromat() {
    if ($('#is_Plus').is(':checked')) {
        $('#addBenFormat').removeClass('hidden');
        $('#additional_benefits').attr('required', '');

    }
    else {
        $('#addBenFormat').addClass('hidden');
        $('#additional_benefits').removeAttr('required');

    }
}
function showImportModel() {
    //var fileInput = document.getElementById("file");
    //fileInput.value = null;
    showresponsemodalbyid('import-benefits-file');
}  

$("#samplefile").click(function () {

    downloadExcel()
});

function downloadExcel() {
    var fileName = "benefits.xlsx";
    var baseUrl = window.location.origin;
    var filePath = baseUrl + "/Samplefile/" + fileName;

    var a = document.createElement("a");
    a.style.display = "none";
    document.body.appendChild(a);
    a.href = filePath;
    a.download = fileName;
    a.click();
}

var projectname = checkurlserver();

function openimportpopup() {
    $("#partialContainer").html("");

    $.ajax({
        type: 'GET',
        url: projectname + "/Beneficiary/OpenPopUpImportBeneficiaries",
        success: function (result) {
            $("#partialContainer").html(result);

            $('#import-beneficiaries').find("#divinfo").find('input,select').css("border-color", "#e2e7f1").val('');
            $("#divbeneficiariestable").attr("hidden", "hidden")
            $("#import-beneficiaries").find(".modal-footer").attr("hidden", "hidden")
            $("#import-beneficiaries .modal-dialog").removeClass("modal-lg")

            showresponsemodalbyid('import-beneficiaries');
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
function importbeneficiariesbulk() {

    if (validateForm("#divinfo")) {
        return;
    }

    if ($('#beneficiariestable tbody tr').length == 0) {
        $(".importresponse").text("No file Uploaded!")
        return
    }
    else
        $(".importresponse").text("")

    var table = $('#beneficiariestable').DataTable();
    var importedbatch = []
    importedbatch = table.data().toArray();
    console.log(importedbatch)

    var stringifiedreq = JSON.stringify(importedbatch);

    $.ajax({
        type: 'POST',
        url: projectname + "/Beneficiary/importBeneficiaries",
        data: { importedbatch: stringifiedreq },
        success: function (result) {
            console.log(result)

            if (result.statusCode.code == 1) {
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                Search()
                $(".modal-footer").removeClass("hidden")
            }
            else {
                drawbeneficiariestable(result.beneficiaries)
                $(".importresponse").text("Unhandled errors!")
                $(".modal-footer").addClass("hidden")
            }
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
function importupload(me) {
    if (validateForm("#import-beneficiaries")) {
        return;
    }
    var importupload = $(me).parent().find(".file-upload");
    importupload.click();
    importupload.change(function () {
        togglebtnloader($("#importupload"));
        //var profileid = $("#prname").attr("name")

        importbeneficiaries(this)

        importupload.unbind();
    });
}
function importbeneficiaries(me) {
    $(".importresponse").text("")
    $(".modal-footer").removeClass("hidden")

    var thisformData = new FormData();

    var selectedfiles = GetBeneficiaries(me)

    if (selectedfiles)
        thisformData = selectedfiles;
    else
        return false;

    $.ajax({
        url: projectname + '/Beneficiary/exceltotable',
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (result) {
            $("#import-beneficiaries .modal-dialog").addClass("modal-lg")
            $("#divbeneficiariestable").removeAttr("hidden")
            drawbeneficiariestable(result)
            $(me).val('');
            $("#import-beneficiaries").find(".modal-footer").removeAttr("hidden");

            removebtnloader($("#importupload"));
            $(me).closest(".modal").find(".importresponse").html('').css("color", "Green")

            //$("#import-adherent").find(".modal-footer").show();

        },
        error: function (jqXHR, exception) {
            removebtnloader($("#importupload"));
            $(".importresponse").text("Error: Excel missing fields!")

            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            $('#post').html(msg);
        }
    });

}
function GetBeneficiaries(me) {
    var files = $(me)[0].files;
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

        if (!valid) {
            //addFileAlert('Not allowed file extension', 'danger')
            //alert('Not allowed file extension')
            removebtnloader($(".btnFileUpload"));
            $(me).closest(".modal").find(".importresponse").html('Not allowed file extension').css("color", "red")

            removebtnloader($("#importupload"));
            //$("#import-adherent").find(".modal-footer").hide();

            return;
        }


        return formData;
    } else {
        return formData;
    }
}
function drawbeneficiariestable(result) {
    var table = $('#beneficiariestable').DataTable({
        "data": result,
        "paging": true,
        "ordering": true, // Enable ordering
        "order": [[9, 'asc']], // Order based on the 'status' column (index 15) in ascending order

        "filter": false,
        "searching": true,
        "destroy": true,
        "lengthMenu": [[5, -1], [5, "All"]],
        "columns": [
            { "title": "FirstName", "className": "text-center filter", "orderable": true, "data": "firstName" },
            { "title": "MiddleName", "className": "text-center filter", "orderable": true, "data": "middleName" },
            { "title": "LastName", "className": "text-center filter", "orderable": true, "data": "lastName" },
            { "title": "PassportNumber", "className": "text-center filter", "orderable": true, "data": "passportNumber" },
            { "title": "DateOfBirth", "className": "text-center filter", "orderable": true, "data": "dateOfBirth" },
            { "title": "Nationality", "className": "text-center filter", "orderable": true, "data": "nationality" },
            { "title": "CountryOfResidence", "className": "text-center filter", "orderable": true, "data": "countryResidence" },
            { "title": "Gender", "className": "text-center filter", "orderable": true, "data": "gender" },
            { "title": "Reason", "className": "text-center filter", "orderable": true, "data": "reason" },
            { "visible": false, "data": "status" }

        ],
        orderCellsTop: true,
        fixedHeader: true,
        "rowCallback": function (row, data) {
            if (data.status == 0) {
                $(row).addClass("validaterow");
            }

        }
    });

} 
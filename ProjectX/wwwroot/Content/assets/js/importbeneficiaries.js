var projectname = checkurlserver();

function openimportpopup(isProduction) {
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

            $("#import-beneficiaries").attr("isProduction", isProduction)

            if (isProduction == 1)
                $("#downloadLink").attr("href", "/Samplefile/ProductionBeneficiaries.xlsx")
            else
                $("#downloadLink").attr("href", "/Samplefile/beneficiaries.xlsx")

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

    var isProduction = $("#import-beneficiaries").attr("isProduction")

    var table = $('#beneficiariestable').DataTable();
    var importedbatch = []
    importedbatch = table.data().toArray();

    var stringifiedreq = JSON.stringify(importedbatch);

    $.ajax({
        type: 'POST',
        url: projectname + "/Beneficiary/importBeneficiaries",
        data: { importedbatch: stringifiedreq, isProduction: isProduction },
        success: function (result) {
            if (result.statusCode.code == 1) {

                if (isProduction == 0) {
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    Search()
                }
                else {
                    closepopup()
                    addProductionBeneficiaries(result)
                }
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

    var isProduction = $("#import-beneficiaries").attr("isProduction")
    thisformData.append('isProduction', isProduction);

    $.ajax({
        type: "POST",
        url: projectname + '/Beneficiary/exceltotable',
        data: thisformData,
        processData: false,
        contentType: false,
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
    var isProduction = $("#import-beneficiaries").attr("isProduction")

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
            { "title": "First Name", "className": "text-center filter", "orderable": true, "data": "firstName" },
            { "title": "Middle Name", "className": "text-center filter", "orderable": true, "data": "middleName" },
            { "title": "Last Name", "className": "text-center filter", "orderable": true, "data": "lastName" },
            { "title": "Passport Number", "className": "text-center filter", "orderable": true, "data": "passportNumber" },
            {
                "title": "Date Of Birth", "className": "text-center filter", "orderable": true, "data": "dateOfBirth",
                "render": function (data, type, full, meta) {
                    return data.split('T')[0]
                }
            },
            { "title": "Nationality", "className": "text-center filter", "orderable": true, "data": "nationality" },
            { "title": "Country Of Residence", "className": "text-center filter", "orderable": true, "data": "countryResidence" },
            { "title": "Gender", "className": "text-center filter", "orderable": true, "data": "gender" },
            { "title": "Remove Deductible", "className": "text-center filter", "orderable": true, "data": "removeDeductible", visible: isProduction == 1 },
            { "title": "Add Sports Activities", "className": "text-center filter", "orderable": true, "data": "addSportsActivities", visible: isProduction == 1 },
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
function addProductionBeneficiaries(data) {
    var thistable = $('#beneficiary-table').DataTable();
    data.beneficiaries.forEach(function (item) {
        var thissex = item.gender === "M" ? 1 : 2;
        var thissexname = item.gender === "M" ? "Male" : "Female";

        var thisrow = {
            "bE_Id": 0,
            "bE_Sex": thissex,
            "bE_SexName": thissexname,
            "bE_FirstName": item.firstName,
            "bE_MaidenName": '',
            "bE_MiddleName": item.middleName,
            "bE_LastName": item.lastName,
            "bE_DOB": item.dateOfBirth.split('T')[0],
            "bE_PassportNumber": item.passportNumber,
            "bE_Nationalityid": item.nationalityId,
            "bE_CountryResidenceid": item.countryResidenceId,
            "bE_RemoveDeductible": item.removeDeductible,
            "bE_SportsActivities": item.addSportsActivities
        };

        thistable.row.add(thisrow).draw();
    });

    getQuotation()
}
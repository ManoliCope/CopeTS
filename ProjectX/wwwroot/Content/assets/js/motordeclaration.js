var projectname = checkurlserver();
var allexperts = [];
var thisorder = 0;
var selectedexperts = []

$(document).ready(function () {
    //FillcmbExperts()


    $("#btnsavedeclaration,#btnsubmiteclaration").click(function () {
        savedeclaration(this);
    });

    $(".attachfiles").click(function () {
        $($(this).attr("data-target")).modal('toggle');

        var documenttype = $(this).attr("documenttypeid")
        var popup = $(this).attr("data-target");

        $(popup).attr("documettypeid", documenttype)
    });

    var button = $(".btnFileUpload");
    button.click(function () {
        var fileupload = $(this).parent().find(".file-upload");
        fileupload.click();
        fileupload.unbind();

        fileupload.change(function () {
            togglebtnloader($(".btnFileUpload"));

            var documenttype = $(this).closest(".modal").attr("documettypeid")
            var caseid = $("#idmotordeclaration").attr("idcase")
            var filetype = $(this).closest(".modal").find("input[name='flexRadioDefault']:checked").val();

            uploadfiles(this, documenttype, caseid, filetype)
            fileupload.unbind();
        });


    });
    if ($("#accdate").attr('thisdate')) {
        document.getElementById('accdate').value = convertdate($("#accdate").attr('thisdate'));
    }
    else {
        document.getElementById('accdate').valueAsDate = new Date();
    }

    var i = 1;
    $("#divdeclarationinsured .thisinsured").each(function (index) {
        if ($(this).find(".insureddate").attr('thisdate')) {
            document.getElementById('thisdate' + i).value = convertdate($("#thisdate" + i).attr('thisdate'));
        }
        else {
            document.getElementById('thisdate' + i).valueAsDate = new Date();
        }

        //fix insured headers
        if (i != 1)
            $(this).find(".insuredheader").html(ordinal_suffix_of(i - 1) + ' PARTY');

        i++;
    });
});

function addparty() {
    var cloneCount = $('.thisinsured').length;
    cloneCount++;
    $("#divinsured1").clone().prop('id', 'divinsured' + cloneCount).appendTo("#divdeclarationinsured");
    $("#divinsured" + cloneCount + " .insuredheader").html(ordinal_suffix_of(cloneCount - 1) + ' PARTY');
    $("#divinsured" + cloneCount).find(".insureddate").removeAttr("id").attr("id", "thisdate" + cloneCount)

    $("#divinsured" + cloneCount + " textarea").val('');
    $("#divinsured" + cloneCount).removeAttr("iddeclarationinsured").removeAttr("idmotordeclaration")

    var colspan = "";
    if (cloneCount == 1)
        colspan = "col-md-12";
    else if (cloneCount == 2)
        colspan = "col-md-6";
    else if (cloneCount == 3)
        colspan = "col-md-4";
    else if (cloneCount == 5)
        colspan = "col-md-4";
    else if (cloneCount == 6)
        colspan = "col-md-4";
    else
        colspan = "col-md-6";

    $(".thisinsured").each(function (index) {
        $(this).removeClass("col-md-4").removeClass("col-md-6").removeClass("col-md-12")
        $(this).addClass(colspan)
    });

}

function savedeclaration(me) {
    showloader("show");
    var sendmail = $(me).attr("sendmail")
    if (sendmail == 1)
        sendmail = true;

    var allinsureds = [];
    $("#divdeclarationinsured .thisinsured").each(function (index) {
        var insured = {
            IdDeclarationInsured: $(this).attr("IdDeclarationinsured"),
            IdMotorDeclaration: $(this).attr("IdMotorDeclaration"),
            Name: $(this).find(".insuredname").val(),
            Mobile: $(this).find(".insuredphone").val(),
            Address: $(this).find(".insuredaddress").val(),
            VehicleType: $(this).find(".insuredcarnature").val(),
            VehicleMake: $(this).find(".insuredcarmake").val(),
            PlateNo: $(this).find(".insuredcarplate").val(),
            DriverName: $(this).find(".insuredconductorname").val(),
            DrivingLicenseNo: $(this).find(".insuredpermit").val(),
            DeclarationDate: $(this).find(".insureddate").val(),
        }

        if ($(this).find(".insuredname").val() != "")
            allinsureds.push(insured)
    });





    var thisdeclaration =
    {
        "idMotorDeclaration": $("#idmotordeclaration").attr("idmotordeclaration"),
        "idCase": $("#idmotordeclaration").attr("idcase"),
        "idBranch": $("#idbranch").val(),
        "casenumber": $("#casenumber").text(),
        "expertname": $("#expertname").text(),
        "policynumber": $("#idmotordeclaration").text(),
        "accidentDate": $("#accdate").val(),
        "accidentTime": $("#acctime").val(),
        "location": $("#location").val(),
        "from": $("#from").val(),
        "to": $("#to").val(),
        "hasExpertReport": $("#chekexportreport").prop("checked") ? "1" : "0",
        "hasPolicyReport": $("#chkpolicyreport").prop("checked") ? "1" : "0",
        "noOfParties": $("#idnumparties").val(),
        "insureds": allinsureds,
        sendmail: sendmail
    }


    //console.log(thisdeclaration)
    $.ajax({
        type: 'POST',
        url: projectname + "/case/SaveMotorDeclaration",
        data: { req: thisdeclaration },
        success: function (result) {
            //console.log(result)
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                     gotopage('Case', 'MotorDeclaration', $("#idmotordeclaration").attr("idcase"))
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")

           // removeloader()
            //if ($(me).attr("IdAction") == 2)
            //    removebtnloader(me)

            //$("#btnsavecase").attr("caseid", result.idCase)
            //$("#btnsavecase span").html("Edit");

            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
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
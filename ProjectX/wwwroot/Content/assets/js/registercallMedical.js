var projectname = checkurlserver();
var allexperts = [];
var thisorder = 0;
var selectedexperts = []

function testemail() {
    $.ajax({
        type: 'POST',
        url: projectname + "/Case/testemail",
        success: function (result) {

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


$(document).ready(function () {

    //FillcmbExperts()
    $("input").attr("autocomplete", "off");
    $(".viewhistory").click(function () {
        $("#calls").toggle()
    });

    $("#testbtn").click(function () {
        testemail()
    });
    
    $(".viewhistory").click() 

    $("#btnsavecase , #btnproceedcase").click(function () {
        savecase(this);
    });

    //$("#btnupload").click(function () {
    //    updloadgop(this);
    //});

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
            var caseid = $("#btnsavecase").attr("caseid")
            var filetype = $(this).closest(".modal").find("input[name='flexRadioDefault']:checked").val();

            uploadfiles(this, documenttype, caseid, filetype)
            fileupload.unbind();
        });
    });


    $("#validatecase").click(function () {
        validatecase(this);
    });

    getavayainfo();
});


function validatecase(me) {
    if (validateForm(".validateme")) {
        return;
    }
    $("#confirm-submit").modal("toggle")
}

function savecase(me) {
    if (validateForm(".validateme")) {
        return;
    }
    localStorage.removeItem("cid");


    if ($(me).attr("IdAction") == 2)
        togglebtnloader(me)
    else
        showloader("load")

    var call =
    {
        "AvayaCall_ID": $("#callnumber").attr("callid"),
        "CallerPhoneNo": $("#callnumber").val(),
        "CallerName": $("#callname").val(),
        "InsuredPhoneNo": $("#callphone").val(),
        "comment": $("#callcomment").val(),
    }


    var thiscase =
    {
        "idCase": $("#btnsavecase").attr("caseid"),
        "idCustomerType": $("#casetype").attr("idcustomertype"),
        "idPolicy": $("#policy").attr("policyid"),
        "idProfile": $("#policy").attr("profileid"),
        "idInsured": $("#insured").attr("insuredid"),
        "isOnline": $("#insured").attr("isOnline"),

        "callType": $("#casetype").val(),
        "callCategory": $("#casecategory").val(),
        "sendEmailTo": $("#sendemail").val(),
        "sendEmailToCode": $("#sendemail option:selected").attr("code"),
        "status": $("#casestatus").val(),
        "comments": $("#comments").val(),
        "call": call,
        "lob": $("#btnsavecase").attr("lob"),
    }

    showloader("show");

    var thislob = $("#btnsavecase").attr("lob")
    var casepage;
    if (thislob == "OT")
        casepage = "GetOtherCase"
    else
        casepage = "GetMedicalCase"

    var UpdateActions = 1;


    $.ajax({
        type: 'POST',
        url: projectname + "/case/SaveMedicalCase",
        data: { req: thiscase, IdCaseUpdateActions: UpdateActions },
        success: function (result) {
            //console.log(result)
            resetavayapopup($("#callnumber").attr("callid"));

            //resetavayapopup();
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    gotopage('Case', casepage, result.idCase)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")

            if ($(me).attr("IdAction") == 2)
                removebtnloader(me)
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

function getavayainfo() {
    var avayabox = $("#notifications-bottom-right");

    $("#callnumber").val(avayabox.find("#phoneno").text())
    $("#callname").val(avayabox.find("#phonename").text())
    $("#callnumber").attr("callid", avayabox.find("#cid").text())
}


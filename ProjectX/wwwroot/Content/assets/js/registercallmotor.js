var projectname = checkurlserver();
var allexperts = [];
var thisorder = 0;
var selectedexperts = []

$(document).ready(function () {
    //FillcmbExperts()
    triggerservice($("#calldiv").attr("serviceid"))

    $('#allexperts option').each(function () {
        var $this = $(this);
        var thisexpert = {};

        thisexpert = {
            IdExpert: $this.val(),
            ord: $this.attr('ord'),
            IdRegion: $this.attr('regionid'),
            info: $this.text(),
            IdReason: 0,
            IsUsed: false,
            IdExpertStatus: 1
        }

        allexperts.push(thisexpert)
    });



    $("#selectPosComm,#selectNegComm").select2({
        tags: true,
        closeOnSelect: false
    })
    $(".select2").select2()



    $(".emailselection").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    })

    $("input").attr("autocomplete", "off");

    $(".viewhistory").click(function () {
        $("#calls").toggle()
    });

    $("#btnsavecase , #btnproceedcase").click(function () {
        savecase(this);
    });

    //$("#btnupload").click(function () {
    //    updloadgop(this);
    //});

    $("#btnreopen").click(function () {
        reopen(this);
    });

    $(".towingstatus").click(function () {
        updatetowingstatus(this);
    });

    $("#btnsaveaudit , #btnproceedaudit").click(function () {
        saveaudit(this);
    })

    $("#btnproceedapproval , #btnproceedmedical, #btnsendrequestemail").click(function () {
        sendEmail(this);
    })

    $("#validatecase").click(function () {
        validatecase(this);
    });

    $("#validateaudit").click(function () {
        validateaudit(this);
    });

    $(".btnrequestemail").click(function () {
        $($(this).attr("data-target")).modal('toggle');
    });

    $(".attachfiles").click(function () {
        $($(this).attr("data-target")).modal('toggle');

        var documenttype = $(this).attr("documenttypeid")
        var popup = $(this).attr("data-target");

        $(popup).attr("documettypeid", documenttype)
    });

    $(document).on('click', '.dropdown-menu button', function (e) {
        if ($(this).parent().find("#cancelreason").val() == "") {
            $(this).parent().find("#cancelreason").css("border-color", "red")
            e.stopPropagation();
            return false;
        }
    });

    //if ($("#casedate").attr('thisdate')) {
    //    document.getElementById('casedate').value = convertdate($("#casedate").attr('thisdate'));
    //}
    //else
    //    document.getElementById('casedate').valueAsDate = new Date();

    if ($("#invoicedate").attr('thisdate')) {
        document.getElementById('invoicedate').value = convertdate($("#invoicedate").attr('thisdate'));
    }

    seturlgop();

    var button = $(".btnFileUpload");
    button.click(function () {
        var fileupload = $(this).parent().find(".file-upload");
        fileupload.click();
        fileupload.unbind();

        fileupload.change(function () {
            togglebtnloader($(".btnFileUpload"));

            var documenttype = $(this).closest(".modal").attr("documettypeid")
            var caseid = $("#calldiv").attr("caseid")
            var filetype = $(this).closest(".modal").find("input[name='flexRadioDefault']:checked").val();

            uploadfiles(this, documenttype, caseid, filetype)
            fileupload.unbind();
        });
    });

    getavayainfo();
    checkpartner();
    triggerpartneremails();


    //checktowinglocation('from')
    //checktowinglocation('to')
});


function updatetowingstatus(me) {
    var Reason = $(me).parent().find("#cancelreason").val();
    if ($(me).attr("IdStatus") == 3 && Reason == "")
        return false

    if ($(me).attr("IdAction") == 2)
        togglebtnloader(me)
    else
        showloader("load")

    var idcase = $(me).attr("caseid");
    var towing = {
        "IdCase": idcase,
        "IdReference": $(me).attr("idcaseservicerow"),
        "IdService": $(me).attr("idcaseservice"),
        "IdStatus": $(me).attr("IdStatus"),
        "Reason": Reason,
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/case/UpdateServiceStatus",
        data: { req: towing },
        success: function (result) {
            if (result.statusCode) {

                if (result.statusCode.code == 1) {
                    gotopage('Case', 'GetMotorCase', idcase)
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")


            if ($(me).attr("IdAction") == 2)
                removebtnloader(me)


        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}



function GetImages() {
    var files = $("#file-upload")[0].files;
    var formData = new FormData();


    if (files.length > 0) {
        var allowedExtensions = ['gif', 'jpeg','jfif', 'png', 'jpg', 'pdf', 'xlsx', 'xls', 'docx', 'doc', 'txt'];
        var valid = true;
        for (var i = 0; i != files.length; i++) {
            var path = files[i].name.split('.');
            var extension = path[path.length - 1]
            if ($.inArray(extension.toLowerCase(), allowedExtensions) < 0)
                valid = false;

            //console.log(files[i], 'file')

            formData.append("files", files[i]);
        }

        if (!valid) {
            addFileAlert('Not allowed file extension', 'danger')
            return;
        }


        for (var pair of formData.entries()) {
            //console.log(pair[0], pair[1]);
            //console.log(pair);
        }


        //console.log(formData,'formdata')


        return formData;
    } else {
        return formData;
    }
}
function reopen(me) {
    showloader("load")

    $.ajax({
        type: 'Get',
        url: projectname + "/case/Reopen",
        data: {
            idCase: $(me).attr("caseid"),
        },
        success: function (result) {
            //console.log(result)
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    gotopage('Case', 'GetCase', result.idCase)
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")

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

function validatecase(me) {
    if (validateForm(".validateme")) {
        return;
    }
    $("#confirm-submit").modal("toggle")
}

function validateaudit(me) {
    if (validateForm("#auditdiv")) {
        return;
    }
    $("#confirm-audit-submit").modal("toggle")
}

function savecase(me) {

    if (validateForm(".validateme")) {
        return;
    }
    localStorage.removeItem("cid");

    var towing = {
        "IdCaseTowing": $("#divservicetowing").attr("idtowing"),
        //"IdCase": $("#callnumber").val(),
        "IdCase": $("#calldiv").attr("caseid"),
        "IdTowingType": $("#selecttypetow").val(),
        "IdTowingCompany": $("#selectCompany").val(),
        "FromGovernorate": $("#SelectLocationFrom option:selected").attr("region"),
        "ToGovernorate": $("#SelectLocationTo option:selected").attr("region"),
        "IdRegionFrom": $("#SelectLocationFrom").val(),
        "IdRegionTo": $("#SelectLocationTo").val(),
        "IdTowingStatus": 1,
        "exGratiaTow":$("#exGratiaTow").prop("checked")
        //"TowingDate": $("#callnumber").val(),
        //"TowingTime": $("#callnumber").val(),
    }

    var attr = $("#divservicetowing").attr('hidden');
    if (typeof attr !== 'undefined' && attr !== false) {
        towing = null;
    }
    else {
        if (towing.IdTowingType != "" || towing.IdTowingCompany != "" ||
            towing.IdRegionFrom != "" || towing.IdRegionTo != "") {
            if (validateForm("#divservicetowing"))
                return;
        }


        if (towing.IdTowingType == "" || towing.IdTowingCompany == "" || towing.IdRegionFrom == "" || towing.IdRegionTo == "") {
            towing = null;
        }
    }

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


    if ($("#divserviceexpert").attr("idexpert") > 0) {
        selectedexperts.push({
            IdCaseExpert: $("#divserviceexpert").attr("idexpert"),
            //IdExpert: 1,
            //ord: 1,
            //IdRegion: 1,
            //info: 1,
            //IdReason: 0,
            //IsUsed: false
        })
    }

    var allowtowing = 0;
    if ($("#servicelist").attr("towing") == "1")
        allowtowing = 1;

    var thiscase =
    {
        "idCase": $("#btnsavecase").attr("caseid"),
        "idPolicy": $("#policy").attr("policyid"),
        "idProfile": $("#policy").attr("profileid"),
        "idInsured": $("#insured").attr("insuredid"),

        "allowtowing": allowtowing,
        "call": call,
        "towing": towing,
        "expert": selectedexperts
    }

    //console.log(thiscase, 'thiscase')
    //remov(me)
    $.ajax({
        type: 'POST',
        url: projectname + "/case/SaveMotorCase",
        data: { req: thiscase },
        success: function (result) {
            console.log(result)
            resetavayapopup($("#callnumber").attr("callid"));

            //resetavayapopup();
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    gotopage('Case', 'GetMotorCase', result.idCase)
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
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

function saveaudit(me) {
    if (validateForm("#auditdiv")) {
        return;
    }

    if ($(me).attr("IdAction") == 2)
        togglebtnloader(me)
    else
        showloader("load")

    var caseid = $("#btnsaveaudit").attr("caseid")
    var thiscase =
    {
        "idCase": caseid,
        "idPaymentCurrency": $("#auditcurrency").val(),
        "invoiceDate": $("#invoicedate").val(),
        "invoiceAmount": $("#invoiceAmount").val(),
        "discount": $("#discount").val(),
        "assistanceFees": $("#assitanceFees").val(),
        "otherFees": $("#otherfees").val(),
        "approvedAmount": $("#approvedAmount").val(),
        "netAmount": $("#netAmount").val(),
        "auditNotes": $("#auditNotes").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/case/AuditCase",
        data: { req: thiscase, IdCaseUpdateActions: $(me).attr("IdAction") },
        success: function (result) {
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    gotopage('Case', 'GetCase', caseid)
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")



            if ($(me).attr("IdAction") == 2)
                removebtnloader(me)
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

function refreshNetAmount() {
    var netAmount = $('#invoiceAmount').val() - $('#discount').val();
    $('#netAmount').val(netAmount);
}

function checkpartner(me) {
    var selectedcountry = $("#casecountry").val();
    var selectedtype = $("#casetype").val();

    //alert(selectedcountry)
    //alert(selectedtype)

    $('#casepartner > option').each(function () {
        if ($(this).val() != '') {

            if (($(this).attr("idcountry") == 0 && $(this).attr("idtype").trim() == selectedtype) || ($(this).attr("idcountry") == selectedcountry && $(this).attr("idtype").trim() == selectedtype)) {
                $(this).removeClass("hidden")
            }
            else
                $(this).addClass("hidden")
        }
    });

    if ($('#casepartner :selected').hasClass("hidden")) {
        $('#casepartner').val("");
        seturlgop();
    }
}

function checkbenefit(me) {
    checkpartner();

    var selectedtype = $(me).val();
    $('#casebenefit > option').each(function () {
        if ($(this).val() != '') {
            if ($(this).attr("idtype") == selectedtype)
                $(this).removeClass("hidden")
            else
                $(this).addClass("hidden")
        }
    });

    if ($('#casebenefit :selected').hasClass("hidden")) {
        $('#casebenefit').val("");
    }
}

function seturlgop() {
    $("#goplink").removeAttr("href").attr("href", $('#casepartner :selected').attr("url"))
    var linkattr = $("#goplink").attr("href")

    if (typeof linkattr !== 'undefined' && linkattr !== false)
        $("#goplink").removeClass("hide")
    else
        $("#goplink").addClass("hide")

}

function getavayainfo() {
    var avayabox = $("#notifications-bottom-right");

    $("#callnumber").val(avayabox.find("#phoneno").text())
    $("#callname").val(avayabox.find("#phonename").text())
    $("#callnumber").attr("callid", avayabox.find("#cid").text())
}

function sendEmail(me) {
    var idAction = $(me).attr("IdAction");

    if (idAction == 3) {
        if (!validateemailpopup()) {
            return;
        }
        if (validateForm("#request-email")) {
            return;
        }
        var to = $("#emailto").val().join(";");
        var cc = $("#emailcc").val().join(";");
        var subject = $("#emailsubject").val();
        var body = $("#emailbody").val();
    }

    var email =
    {
        "recipients": to,
        "ccRecipients": cc,
        "subject": subject,
        "body": body,
        "idCase": $("#calldiv").attr("caseid")
    }

    //console.log(email)

    togglebtnloader($("#btnsendrequestemail"))

    $.ajax({
        type: 'Post',
        url: projectname + "/case/SendEmail",
        data: {
            req: email, IdCaseEmailOption: idAction
        },
        success: function (result) {
            showresponsemodal(result.statusCode.code, result.statusCode.message)
            removebtnloader($("#btnsendrequestemail"))
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

function triggerpartneremails() {
    if ($("#policy").attr("profileid") != '' && $("#casepartner").val() != '' && $("#casecountry").val() != '') {
        $.ajax({
            type: 'Post',
            url: projectname + "/case/GetContactEmails",
            data: {
                idProfile: $("#policy").attr("profileid"),
                idPartner: $("#casepartner").val(),
                idCaseCountry: $("#casecountry").val()
            },
            success: function (result) {
                $("#emailto").html("");
                $("#emailcc").html("");

                var $optgrouppayer = $("<optgroup label='Payer'>");
                var $optgrouppartner = $("<optgroup label='Partner'>");

                $(result).each(function () {
                    if (this.idProfileType == 1)
                        $optgrouppayer.append(`<option value="${this.email}">${this.email} (${this.department}) </option>`);
                    else
                        $optgrouppartner.append(`<option value="${this.email}">${this.email} (${this.department}) </option>`);
                });
                $("#emailcc, #emailto").append($optgrouppayer, $optgrouppartner);
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
}

function validateemailpopup() {
    if (validateMultipleEmailsCommaSeparated('to', $("#emailto").val().join(";"), ';')) {
        $("#emailto").parent().find(".select2-container").removeClass("select2-borderred");
    }
    else {
        $("#emailto").parent().find(".select2-container").addClass("select2-borderred");
        return false;
    }

    if (validateMultipleEmailsCommaSeparated('cc', $("#emailcc").val().join(";"), ';')) {
        $("#emailcc").parent().find(".select2-container").removeClass("select2-borderred");
    }
    else {
        $("#emailcc").parent().find(".select2-container").addClass("select2-borderred");
        return false;
    }

    return true;


    function validateEmail(field) {
        var regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$/;
        return (regex.test(field)) ? true : false;
    }
    function validateMultipleEmailsCommaSeparated(type, emailcntl, seperator) {
        if (emailcntl == '' && type == "cc")
            return true;

        if (emailcntl != '') {
            var result = emailcntl.split(seperator);
            for (var i = 0; i < result.length; i++) {
                if (result[i] != '') {
                    if (!validateEmail(result[i])) {
                        //alert('Please check, `' + result[i] + '` email addresses not valid!');
                        return false;
                    }
                }
            }
            return true;
        }
        return false;
    }
}

//function FillcmbExperts() {
//$.ajax({
//    type: 'POST',
//    url: projectname + "/Motor/Fill_cmbExperts",
//    success: function (result) {
//        //$("#allexperts").empty();
//        //$(result.experts).each(function () {
//        //    $("#allexperts").append(`<option value="${this.code}">${this.expRegDescription} - ${this.userFullName} (${this.tel1}) </option>`);
//        //});

//    },
//    error: function (xhr, status, error) {

//    }
//});
//}
function triggerservice(serviceid) {
    //var serviceid = $(me).attr("serviceid");
    if (serviceid == 1)
        $("#divserviceexpert").removeAttr("hidden")
    else if (serviceid == 2) {
        $("#divservicetowing").removeAttr("hidden")
        $("#divserviceexpert").removeAttr("hidden")
    }
    else if (serviceid == 6) {
        $("#divservicetowing").removeAttr("hidden")
    }
}

////// setting experts   ////////
function triggerexpertreason(visibility) {
    if (visibility == "show")
        $('#divreason').removeClass("hidden");
    else
        $('#divreason').addClass("hidden");

    $("#expreason").val("")
}

function triggerallexperts(visibility) {
    if (visibility == "show") {
        $('#divallexperts').removeClass("hidden");
        $('#divthisexpert').addClass("hidden");
    }
    else {
        $('#divallexperts').addClass("hidden");
        $('#divthisexpert').removeClass("hidden");
    }
}

function updateexpertreason() {
    $("#expreason").css('border-color', '#e2e7f1');
    var region = $("#SelectCity").val()
    for (var i in allexperts) {
        if (allexperts[i].IdRegion == region && allexperts[i].ord == thisorder) {
            allexperts[i].IsUsed = true;
            allexperts[i].IdReason = ($("#expreason").val() == "") ? "0" : $("#expreason").val()
            break;
        }
    }
}

function getnextinline() {
    if ($("#expreason").val() == "") {
        $("#expreason").css('border-color', 'red');
        return false;
    }
    else {
        $("#expreason").css('border-color', '#e2e7f1');
        updateexpertreason();
        setnextexpert()
        triggerexpertreason('hide');
    }
}

function resetexpertcycle() {
    thisorder = 0;

    $('#divthisexpert').removeClass("hidden");
    $('#divreason').addClass("hidden");
    $('#divallexperts').addClass("hidden");

    $("#allexperts").val("")
    $("#selectedexp").val("")
    $("#selectedexp").removeAttr("thisval")
    $("#selectedexp").removeClass("expertshadow")

    triggerexpertreason()
    triggerallexperts()

    allexperts.map(function (e, index) {
        e.IdReason = 0;
        e.IsUsed = false;
        return Object(e)
    });
}

function setnextexpert(type) {
    if (type == 'N') {
        resetexpertcycle()
    }

    if ($("#SelectCity").val() == "") {
        $("#SelectCity").css('border-color', 'red');
        return false;
    }
    else {
        $("#SelectCity").css('border-color', '#e2e7f1');
        $('#show-expert').modal('show').find(".modal-header").removeAttr("style");
    }

    var region = $("#SelectCity").val()
    var thisexpert = []
    thisorder++;

    thisexpert = allexperts.filter(function (element) { return (element.IdRegion == region && element.ord == thisorder) })

    if (thisexpert.length === 0) {
        triggerallexperts("show")
    }
    else {
        $("#thisexpert").val(thisexpert[0].info)
        $("#thisexpert").attr("thisval", thisexpert[0].id)
    }
}

function selectexpert() {
    var thisexpert = $("#thisexpert");
    $("#selectedexp").val(thisexpert.val())
    $("#selectedexp").attr('thisval', thisexpert.attr("thisval"))
    $("#selectedexp").addClass("expertshadow")

    $("#expreason").val("")
    updateexpertreason()

    $("#show-expert").modal("hide")

    selectedexperts = allexperts.filter(function (element) { return (element.IsUsed == true) })
    //console.log(selectedexperts)
}

function selectfromallexpert() {
    if ($("#allexperts").val() == "") {
        $("#allexperts").css('border-color', 'red');
        $("#selectedexp").removeClass("expertshadow")
        return false;
    }
    else {
        var region = $("#SelectCity").val()
        $("#allexperts").css('border-color', '#e2e7f1');

        $("#selectedexp").val($("#allexperts option:selected").text())
        $("#selectedexp").attr('thisval', $("#allexperts").val())
        $("#selectedexp").addClass("expertshadow")


        $("#show-expert").modal("hide")

        for (var i in allexperts) {
            if (allexperts[i].IdExpert == $("#allexperts").val()) {
                allexperts[i].IsUsed = true;
                allexperts[i].IdReason = 0;
                break;
            }
        }

        selectedexperts = allexperts.filter(function (element) { return (element.IsUsed == true) })


        selectedexperts.map(function (e, index) {
            e.regionid = region;

            return Object(e)
        });
        //console.log(selectedexperts);
    }
}
////// setting experts   ////////


function checktowinglocation(type) {
    var selectedlocation;
    var selectedregion;

    if (type == "from") {
        selectedregion = $("#SelectRegionFrom option:selected").text().trim();
        selectedlocation = "#SelectLocationFrom";
    }
    else {
        selectedregion = $("#SelectRegionTo option:selected").text().trim();
        selectedlocation = "#SelectLocationTo";
    }

    $(selectedlocation + '> option').each(function () {
        if ($(this).val() != '') {

            if ($(this).attr("region").trim() == selectedregion)
                $(this).removeClass("hidden")
            else
                $(this).addClass("hidden")
        }
    });

    if ($(selectedlocation + ' :selected').hasClass("hidden")) {
        $(selectedlocation).val("");
        seturlgop();
    }
}

function savefeedback(me) {
    var idservice = $(me).attr("idservice");
    var rate;
    var radios = document.getElementsByName('rating-' + idservice);
    for (var i = 0, length = radios.length; i < length; i++) {
        if (radios[i].checked) {
            rate = radios[i].value;
            break;
        }
    }

    var thisfeedback;
    var popfeedback = $("#pop-feedback-" + idservice);

    if (!validatefeedback(popfeedback, rate)) {
        return;
    }



    if (popfeedback.find("#chknofeedback").is(':checked'))
        thisfeedback = {
            "IdCase": $("#calldiv").attr("caseid"),
            "IdService": idservice,
            "IdNoFeedback": popfeedback.find("#selectStatus").val(),
            "NoFeedback": 1,
        }
    else
        thisfeedback = {
            "IdCase": $("#calldiv").attr("caseid"),
            "IdService": idservice,
            "Rate": rate,
            "Comments": popfeedback.find("#feedbackcomments").val(),
            "Positive": popfeedback.find("#selectPosComm").val(),
            "Negative": popfeedback.find("#selectNegComm").val(),
            "IdNoFeedback": 0,
            "NoFeedback": 0,
        }

    $.ajax({
        type: 'POST',
        url: projectname + "/case/SaveFeedback",
        data: { req: thisfeedback },
        success: function (result) {
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    //gotopage('Case', 'GetMotorCase', idcase)
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    $("#feedbackicon-" + idservice).removeClass().addClass("fas fa-star");
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")


            if ($(me).attr("IdAction") == 2)
                removebtnloader(me)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}


function validatefeedback(popfeedback, rate) {
    var returnedvalue = true;

    if (popfeedback.find("#chknofeedback").is(':checked')) {

        if (popfeedback.find("#selectStatus").val() == '') {
            popfeedback.find("#selectStatus").css('border-color', 'red');
            popfeedback.find("#selectStatus").parent().find(".select2-container").addClass("select2-borderred");
            returnedvalue = false
        } else {
            popfeedback.find("#selectStatus").css('border-color', '#e2e7f1');
            popfeedback.find("#selectStatus").parent().find(".select2-container").removeClass("select2-borderred");
        }
    }
    else {
        if (popfeedback.find("#selectPosComm").val() == '' && popfeedback.find("#selectNegComm").val() == '') {
            if (popfeedback.find("#selectPosComm").val() == '') {
                popfeedback.find("#selectPosComm").css('border-color', 'red');
                popfeedback.find("#selectPosComm").parent().find(".select2-container").addClass("select2-borderred");
                returnedvalue = false
            } else {
                popfeedback.find("#selectPosComm").css('border-color', '#e2e7f1');
                popfeedback.find("#selectPosComm").parent().find(".select2-container").removeClass("select2-borderred");
            }


            if (popfeedback.find("#selectNegComm").val() == '') {
                popfeedback.find("#selectNegComm").css('border-color', 'red');
                popfeedback.find("#selectNegComm").parent().find(".select2-container").addClass("select2-borderred");
                returnedvalue = false
            } else {
                popfeedback.find("#selectNegComm").css('border-color', '#e2e7f1');
                popfeedback.find("#selectNegComm").parent().find(".select2-container").removeClass("select2-borderred");
            }
        }
        else {
            popfeedback.find("#selectPosComm").css('border-color', '#e2e7f1');
            popfeedback.find("#selectPosComm").parent().find(".select2-container").removeClass("select2-borderred");

            popfeedback.find("#selectNegComm").css('border-color', '#e2e7f1');
            popfeedback.find("#selectNegComm").parent().find(".select2-container").removeClass("select2-borderred");
        }

        if (rate == '' || rate == undefined) {
            popfeedback.find("#errorlabel").html("Rate Us!")
            returnedvalue = false
        } else {
            popfeedback.find("#errorlabel").html("")
        }
    }
    return returnedvalue
}

function triggernofeedback(me) {
    if ($(me).is(':checked')) {
        $(me).closest('.modal').find("#divwithfeedback").attr("hidden", "hidden")
        $(me).closest('.modal').find("#feedbackreason").removeAttr("hidden")
    }
    else {
        $(me).closest('.modal').find("#feedbackreason").attr("hidden", "hidden")
        $(me).closest('.modal').find("#divwithfeedback").removeAttr("hidden")
    }
}

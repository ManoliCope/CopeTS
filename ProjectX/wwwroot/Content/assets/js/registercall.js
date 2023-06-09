var projectname = checkurlserver();
$(document).ready(function () {
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

    $("#btncancel").click(function () {
        cancelcase(this);
    });


    $("#btnreopen").click(function () {
        reopen(this);
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
    $("#completecase").click(function () {
        completecase(this);
    });

    $("#validateaudit").click(function () {
        validateaudit(this);
    });

    $(".btnrequestemail").click(function () {
        $($(this).attr("data-target")).modal('toggle');
    });

    $(".attachfiles").click(function () {
        // alert('motordeclaration');
        $($(this).attr("data-target")).modal('toggle');

        var documenttype = $(this).attr("documenttypeid")
        var popup = $(this).attr("data-target");

        $(popup).attr("documettypeid", documenttype)
    });


    if ($("#casedate").attr('thisdate')) {
        document.getElementById('casedate').value = convertdate($("#casedate").attr('thisdate'));
    }
    else
        document.getElementById('casedate').valueAsDate = new Date();

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
    checkmedicalfields()
    checkpartner();
    triggerpartneremails();
});

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

function cancelcase(me) {
    showloader("load")

    $.ajax({
        type: 'Get',
        url: projectname + "/case/CancelCase",
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
function completecase(me) {
    //if (validateForm(".validateme")) {
    //    return;
    //}
    $("#complete-submit").modal("toggle")
}

function validateaudit(me) {
    if (validateForm("#auditdiv")) {
        return;
    }
    $("#confirm-audit-submit").modal("toggle")
}

$("#btncompletefincase").click(function () {
    casecomplete(this);
});
function casecomplete(me) {
    if ($(me).attr("IdAction") == "6")
        togglebtnloader(me)
    else
        showloader("load")

    var thiscase =
    {
        "idCase": $("#completecase").attr("caseid"),
        "accidentDate": $("#casedate").val(),
        "idCustomerType": $("#casetype").attr("idcustomertype"),
        "idPolicy": $("#policy").attr("policyid"),
        "idProfile": $("#policy").attr("profileid"),
        "idInsured": $("#insured").attr("insuredid"),
        "idBenefit": $("#casebenefit").val(),
        "idCaseType": $("#casetype").val(),
        "idCaseComplexity": $("#caselevel").val(),
        "idApprovalStatus": $("#approvalstatus").val(),

        "idCaseCountry": $("#casecountry").val(),
        "idPartner": $("#casepartner").val(),
        "estimatedAmount": $("#estimatedamnt").val(),
        "idCaseCurrency": $("#casecurrency").val(),
       
        "limitation": $("#limitationamnt").val(),
        "excess": $("#excessamnt").val(),

        "hospital": $("#hospitalname").val(),
        "physician": $("#physicianname").val(),
        "idDiagnosis": $("#casediagnosis").val(),

        "comments": $("#comment").val(),
        "additionalNotes": $("#saComment").val(),
        "isOnline": $("#insured").attr("isOnline")
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/case/FinanceCompleteCase",
        data: { req: thiscase, IdCaseUpdateActions: $(me).attr("IdAction") },
        success: function (result) {
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    gotopage('Case', 'GetCase', result.idCase)
                }
                else
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(0, "Error")
            if ($(me).attr("IdAction") == "6")
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
function savecase(me) {
    //alert('here')
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

    var flagexgracia = false;
    if ($("#chkexgracia").is(':checked'))
        flagexgracia = true;


    var thiscase =
    {
        "idCase": $("#btnsavecase").attr("caseid"),
        "accidentDate": $("#casedate").val(),
        "idCustomerType": $("#casetype").attr("idcustomertype"),
        "idPolicy": $("#policy").attr("policyid"),
        "idProfile": $("#policy").attr("profileid"),
        "idInsured": $("#insured").attr("insuredid"),
        "idBenefit": $("#casebenefit").val(),
        "idCaseType": $("#casetype").val(),
        "idCaseComplexity": $("#caselevel").val(),
        "idApprovalStatus": $("#approvalstatus").val(),

        "idCaseCountry": $("#casecountry").val(),
        "idPartner": $("#casepartner").val(),
        "estimatedAmount": $("#estimatedamnt").val(),
        "idCaseCurrency": $("#casecurrency").val(),

        //new
        "limitation": $("#limitationamnt").val(),
        "excess": $("#excessamnt").val(),

        "hospital": $("#hospitalname").val(),
        "physician": $("#physicianname").val(),
        "idDiagnosis": $("#casediagnosis").val(),
        "isexgracia": flagexgracia,

        "comments": $("#comment").val(),
        "additionalNotes": $("#saComment").val(),
        "isOnline": $("#insured").attr("isOnline"),
        "call": call
    }
    //console.log(thiscase);

    $.ajax({
        type: 'POST',
        url: projectname + "/case/SaveCase",
        data: { req: thiscase, IdCaseUpdateActions: $(me).attr("IdAction") },
        success: function (result) {
            //console.log(result)
            resetavayapopup($("#callnumber").attr("callid"));
            //resetavayapopup();
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    gotopage('Case', 'GetCase', result.idCase)
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
        "auditFinanceID": $("#auditFinance").val()
    }
    //console.log(thiscase)
    $.ajax({
        type: 'POST',
        url: projectname + "/case/AuditCase",
        data: { req: thiscase, IdCaseUpdateActions: $(me).attr("IdAction") },
        success: function (result) {
            if (result.statusCode) {
                if (result.statusCode.code == 1) {
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                    gotopage('Case', 'GetCase', caseid)
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

    $('#casepartner > option').each(function () {
        if ($(this).val() != '') {

            if (($(this).attr("idcountry") == 0 && $(this).attr("idtype").trim() == selectedtype)
                || ($(this).attr("idcountry") == selectedcountry && $(this).attr("idtype").trim() == selectedtype)) {

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
function checkmedicalfields() {
    $('.medical').each(function () {
        if ($("#casetype").val() != "8") {
            $(this).attr("hidden", "hidden")
        }
        else
            $(this).removeAttr("hidden")
    });
}
function checkbenefit(me) {

    checkmedicalfields();
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
    var files = $("#file-upload").files;

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
        var withAttachments = $('#withAttachments').is(':checked');
    }

    var email =
    {
        "recipients": to,
        "ccRecipients": cc,
        "subject": subject,
        "body": body,
        "idCase": $("#calldiv").attr("caseid"),
        "withAttachments": withAttachments
    }

    togglebtnloader($(me))

    $.ajax({
        type: 'Post',
        url: projectname + "/case/SendEmail",
        data: {
            req: email, IdCaseEmailOption: idAction
        },
        success: function (result) {
            showresponsemodal(result.statusCode.code, result.statusCode.message)
            removebtnloader($(me))
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

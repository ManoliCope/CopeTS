var projectname = checkurlserver();

var reporturl = getreporturl()



function getreporturl() {
    var url = window.location.href
    if (url.indexOf("localhost") > -1) {
        return "https://localhost:44378";
    }
    else {
    }
}




function testme() {

    //alert('a')
    //let a = document.createElement('a');
    //a.target = '_blank';
    //a.href = 'https://localhost:44378/Reports/Testhere';
    //a.click();
    var thiscase = {
        id: 1,
        text: 'test',
    }
    var url = "https://localhost:44378/Reports/Testheree";
    $.redirect(url, thiscase);
    removeloader();
    return
}

function validatefilterreport(divname, me) {
    const requiredFields = $(divname + ' .required:input');
    var inputValues = [];

    requiredFields.each(function () {
        var field = $(this).val().trim();
        inputValues.push({ val: field });
        var id = $(this).attr("id");
    });

    const valid = inputValues.find(v => v.val == "");
    return valid;
}

$(document).ready(function () {

    triggertooltip("#generatecallsreport")
    triggertooltip("#generateexpreport")
    triggertooltip("#generateunassexpsreport")
    triggertooltip("#generatetowsreport")
    triggertooltip("#generatefeedbackreport")
    triggertooltip("#generateprdbordreport")
    triggertooltip("#generatepaymentbydate")
    triggertooltip("#generatemotordecreport")
    triggertooltip("#generateTravelCasesReport")
    triggertooltip("#generateAuditTravelCasesReport")

    $("#generatecallsreport").click(function () {
        generatecallsreport()
    });

    $("#generatemotordecreport").click(function () {
        generatemotordecreport()
    });
    $("#generateTravelCasesReport").click(function () {
        generateTravelCasesReport()
    });

    $("#generateexpreport").click(function () {
        generateexpreport()
    });

    $("#generateunassexpsreport").click(function () {
        generateunassexpsreport()
    });

    $("#generatefeedbackreport").click(function () {
        generatefeedbackreport()
    });

    $("#generatetowsreport").click(function () {
        generatetowsreport()
    });

    $("#generateprdbordreport").click(function () {
        generateprdbordreport()
    });
    
    $("#generatepaymentbydate").click(function () {
        generatepaymentbydate()
    });
    $("#generateAuditTravelCasesReport").click(function () {
        generateAuditTravelCasesReport()
    });

    // special case for avaya (no gotopage, directly viewed here)

    //SearchCases()

    //drawcasetable();
    //$(".resetdiv").click(function () {
    //    var divname = $(this).closest(".card-body").attr("id")
    //    resetAllValues(divname)
    //    resetdatatable("#cases");
    //});

    //$("#searchcase").click(function () {
    //    SearchCases()
    //});

    //$("input").attr("autocomplete", "off");

    //triggertooltip("#searchcase")


    //if ($("#notifications-bottom-right #phoneno").text() != "") {
    //    $("#phoneNo").val($("#notifications-bottom-right #phoneno").text().trim())
    //    SearchCases()
    //}

    //$(document).on('click', '.dropdown-menu button', function (e) {
    //    if ($(this).parent().find("#cancelreason").val() == "") {
    //        $(this).parent().find("#cancelreason").css("border-color", "red")
    //        e.stopPropagation();
    //        return false;
    //    }
    //});

});

function tttttttt() {
    var req = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }


    var url = "https://localhost:44378/Reports/GeneratecallsReport";
    $.redirect(url, thisobj);
}

function generatecallsreport() {
    if (validatefilterreport("#searchform")) {
        $("#generatecallsreport").tooltip('toggle');
        return;
    }
    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/Generatecalls",
        data: { req: request },
        success: function (result) {
          console.log(result)


            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'CasesReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                //$.redirect(url, req);
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }
            removeloader();
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

function generatemotordecreport() {
    //alert('test')
    if (validatefilterreport("#searchform")) {
        $("#generatemotordecreport").tooltip('toggle');
        return;
    }
    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateMotorDeclaration",
        data: { req: request },
        success: function (result) {
            console.log(result)
            //alert('success')

            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'MotorDeclarationReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                //$.redirect(url, req);
                //alert(url)
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }
            removeloader();
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
//generateTravelCasesReport
function generateTravelCasesReport() {
    //alert('test')
    if (validatefilterreport("#searchform")) {
        $("#generateTravelCasesReport").tooltip('toggle');
        return;
    }
    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateTravelCasesReport",
        data: { req: request },
        success: function (result) {
            console.log(result)

            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'TravelCasesReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }
            removeloader();
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

function generateAuditTravelCasesReport() {
    //alert('test')
    if (validatefilterreport("#searchform")) {
        $("#generateAuditTravelCasesReport").tooltip('toggle');
        return;
    }
    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateAuditTravelCasesReport",
        data: { req: request },
        success: function (result) {
            console.log(result)

            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'AuditedTravelCasesReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }
            removeloader();
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




function generateprdbordreport() {
    if (validatefilterreport("#searchform")) {
        $("#generateprdbordreport").tooltip('toggle');
        return;
    }

    var req = {
        from: $("#datefrom").val(),
        to: $("#dateto").val(),
        filename: 'ProductionBordereauReport'
    }

    var url = reporturl + "/Reports/GenerateProductionBordReport";
    $.redirect(url, req, "POST", "_blank");

}
function generateexpreport() {
    if (validatefilterreport("#searchform")) {
        $("#generateexpreport").tooltip('toggle');
        return;
    }

    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateExperts",
        data: { req: request },
        success: function (result) {

            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'ExpertsReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                //$.redirect(url, req);
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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
function generateunassexpsreport() {
    if (validatefilterreport("#searchform")) {
        $("#generateunassexpsreport").tooltip('toggle');
        return;
    }

    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateUnassignedExperts",
        data: { req: request },
        success: function (result) {
            
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);

                var req = {
                    data: myJSON,
                    filename:'UnassignedExpertsReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                //$.redirect(url, req);
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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
function generatefeedbackreport() {
    if (validatefilterreport("#searchform")) {
        $("#generatefeedbackreport").tooltip('toggle');
        return;
    }

    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val(),
        towcompany: $("#towcompany").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateFeedbacks",
        data: { req: request },
        success: function (result) {
           
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                
                var req = {
                    data: myJSON,
                    filename:'FeedbacksReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                //$.redirect(url, req);
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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
function generatetowsreport() {
    if (validatefilterreport("#searchform")) {
        $("#generatetowsreport").tooltip('toggle');
        return;
    }

    var request = {
        from: $("#datefrom").val(),
        to: $("#dateto").val(),
        towcompany: $("#towcompany").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateTows",
        data: { req: request },
        success: function (result) {
        
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);

                var req = {
                    data: myJSON,
                    filename:'TowingsReport'
                }
                var url = reporturl + "/Reports/GenerateReport";
                //$.redirect(url, req);
                $.redirect(url, req, "POST", "_blank");
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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
function generatepaymentbydate() {
    if (validatefilterreport("#searchform")) {
        $("#generatepaymentbydate").tooltip('toggle');
        return;
    }
    var line = $("input[name='paybydate']:checked").val();
    var req = {
        from: $("#datefrom").val(),
        to: $("#dateto").val(),
        payline: line,
        filename: 'PaymentbydateReport'
    }
    var url = reporturl + "/Reports/GeneratePaymentByDateReport";
    $.redirect(url, req, "POST", "_blank");
}








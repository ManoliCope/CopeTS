var projectname = checkurlserver();
////$(document).ready(function () {
////    var table = $('table').DataTable({
////        responsive: true
////    });
////});


$(document).ready(function () {
    //showresponsemodal("success", "Data Saved!")
    //showresponsemodal("s", "Data Saved!")

    //showloader("ttt")
    //showloader()

    drawpolicytable();

    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname)
        resetdatatable("#policy");
    });

    $("#searchpolicy").click(function () {
        SearchPolicies()
    });

    //drawpolicytable();

    //SearchPolicies();
    $("input").attr("autocomplete", "off");



    triggertooltip("#searchpolicy")


});

function validatesearchpolicy(divname, me) {
    const requiredFields = $(divname + ' :input');
    var inputValues = [];

    requiredFields.each(function () {
        var field = $(this).val().trim();
        inputValues.push({ val: field }); // to return flag valid
        var id = $(this).attr("id");

        //if (field == undefined || field == '') {
        //    $("#" + id).css('border-color', 'red');
        //} else {
        //    //$("#" + id).css('border-color', '#e2e7f1');
        //}
    });

    const valid = inputValues.find(v => v.val != "");
    return valid;
}

function SearchPolicies() {
    if (!validatesearchpolicy("#searchform")) {
        $("#searchpolicy").tooltip('toggle');
        return;
    }

    showloader("load")

    var filter = {
        certificate: $("#certificate").val(),
        policyNo: $("#policyNo").val(),
        name: $("#name").val().trim(),
        dob: $("#dob").val(),
        phoneNo: $("#phoneNo").val(),
        idCustomerType: $("#idCustomerType").val()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Travel/SearchPolicy",
        data: { req: filter },
        success: function (result) {
            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawpolicytable(result.policies);
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

function drawpolicytable(data) {
    console.log(data);
    var table = $('#policy').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [

            { "title": "Policy", "className": "text-center filter", "orderable": true, "data": "policyNo" },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "customerType" },
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "name" },
            { "title": "Inception", "className": "text-center filter", "orderable": true, "data": "inceptionDate" },
            { "title": "Expiry", "className": "text-center filter", "orderable": true, "data": "expiryDate" },
            { "title": "Sports Activities", "className": "text-center filter", "orderable": true, "data": "sportsActivities" },
            { "title": "Deductible", "className": "text-center filter", "orderable": true, "data": "deductable" },
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {

                    return `<a id="gotocase${full.idPolicy}"  href="#" data-toggle="tooltip" data-original-title="No Case History!" custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" class="text-black-50" onclick="getcasehistory(this)"><i class="fas fa-list"/></a>`;
                }
            },
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {

                    return `<a href="#" title="View Policy" polid="` + full.idPolicy + `" certificate="` + full.certificate + `" sc_ser="` + full.sc_ser + `" insid="` + full.idInsured + `" polnum="` + full.policyNo + `" brk="` + full.bK_NO + `" cob="` + full.cob + `" isOnline="` + full.isOnline + `"  class="text-black-50" title="View Policy" class="text-black-50"    onclick="viewpolicy(this)">
                                   <i class="fas fa-eye"></i></a>
                            <div id="thisloader" class="smloader hide"></div>`
                }
            },
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Register" custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" isOnline="` + full.isOnline + `" class="text-black-50" onclick="register(this)"><i class="fas fa-book"/></a>`;
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggertooltip("#testme")
    triggerfiltertable(table, "policy")
}


function viewpolicy(me) {

    togglebtnloader(me);
    var polid = $(me).attr('polid');
    var policynum = $(me).attr('polnum');
    var cob = $(me).attr('cob');
    var brokercode = $(me).attr('brk');
    var sc_ser = $(me).attr('sc_ser');
    var sc_hpins = $(me).attr('insid');
    var certificate = $(me).attr('certificate');
    var isOnline = $(me).attr('isOnline');
    if (isOnline == 1) {
        var link = "https://www.isatravelonline.com:9088/api/PrintPolicy/Print?PolicyCode=" + polid + "&ProdCode=0&beneficiaryCode=1&ShowTotal=1";
        $("#view-policy").find('.modal-body').html("").append(`<iframe src="` + link + `" width="100%" style="height:73vh"></iframe>`);
        $("#view-policy").modal('show').find(".modal-header").removeAttr("style");
        removebtnloader(me);
        return;
    } else {
        var polobj = {
            "polType": "T",
            "Data":
            {
                "Message": "",
                "status": "1",
                "Data":
                {
                    "policyID": polid,
                    "PolicyNumber": policynum,
                    "InceptionDate": "",
                    "ExpiryDate": "",
                    "TotalAmount": 0.0,
                    "PaidAmount": 0.0,
                    "Currency": "",
                    "Name": "",
                    "Status": "1",
                    "PdfLink": "",
                    "BrokerName": brokercode,
                    "Claims": "",
                    "COB": cob,
                    "PT_INCOD": "",
                    "polNum": "",
                    "SNO": "000",
                    "sc_ser": sc_ser,
                    "sc_hpins": sc_hpins,
                    "pt_brcod": "",
                    "sc_tpa": certificate,
                    "TotalPremiumMCD": 0.0,
                    "TotalUSD": 0.0,
                    "paymentType": ""
                }
            }
        }
        $.ajax({
            type: 'POST',
             url: "https://www.securiteapps.com/vpapi/GetPolicy/Post",
            data: polobj,
            success: function (result) {
                if (result != "") {

                    var url = "https://" + result;
                    $("#view-policy").find('.modal-body').html("").append(`<iframe src="` + url + `" width="100%" style="height:73vh"></iframe>`);
                    $("#view-policy").modal('show').find(".modal-header").removeAttr("style");
                }
                else
                    showresponsemodal("Error", "Policy Error")

                removebtnloader(me);
            },
            failure: function (data, success, failure) {
                showresponsemodal("Error", "Bad Request")
            },
            error: function (data) {
                showresponsemodal("Error", "Bad Request")
            }
        });
    }
   

}













function register(me) {
    showloader("hide");
    var thiscase = {
        idCustomerType: $(me).attr("custtype"),
        idPolicy: $(me).attr("polid"),
        idInsured: $(me).attr("insid"),
        idProfile: $(me).attr("profid"),
        isOnline: $(me).attr("isOnline")
    }

    var url = projectname + "/Case/GetPolicyCase";

    $.redirect(url, thiscase);
    removeloader();
    return

    //$.ajax({
    //    type: 'POST',
    //    url: projectname +"/Case/GetPolicyCase",
    //    data: { req: thiscase },
    //    success: function (result) {
    //        $('#partialscreen .content-page').html("").html(result).scrollTop(0)
    //        removeloader();

    //        //console.log(result)
    //    },
    //    failure: function (data, success, failure) {
    //        showresponsemodal("Error", "Bad Request")

    //        //alert("Error:" + failure);
    //    },
    //    error: function (data) {
    //        showresponsemodal("Error", "Bad Request")
    //        //alert("fail");
    //    }
    //});
}

function getcasehistory(me) {
    var thiscase = {
        idCustomerType: $(me).attr("custtype"),
        idPolicy: $(me).attr("polid"),
        idInsured: $(me).attr("insid"),
        idProfile: $(me).attr("profid")
    }


    $.ajax({
        type: 'POST',
        url: projectname + "/Case/GetCaseHistory",
        data: { req: thiscase },
        success: function (result) {
            if (result != "") {
                $("#view-history .modal-body").html("").html(result).scrollTop(0);
                $('#view-history').modal('show').find(".modal-header").removeAttr("style");
            }
            else {
                $("#gotocase" + $(me).attr("polid")).tooltip('toggle');
            }
            //$('#partialscreen .content-page').html("").html(result)
            //removeloader();

            ////console.log(result)
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

function viewcase(me) {
    //showloader("show");
    togglebtnloader(me);

    var idcase = $(me).attr("idcase");

    window.location.href = "/Case/GetCase/" + idcase;
    removeloader();
    return



    //$.ajax({
    //    type: 'Get',
    //    url: projectname +"/Case/GetCase",
    //    data: { parameter: idcase },
    //    success: function (result) {
    //        $("#view-history").modal("hide");

    //        $('#partialscreen .content-page').html("").html(result).scrollTop(0);
    //        triggerBody();
    //        //removeloader();

    //        //console.log(result)
    //    },
    //    failure: function (data, success, failure) {
    //        showresponsemodal("Error", "Bad Request")

    //        //alert("Error:" + failure);
    //    },
    //    error: function (data) {
    //        showresponsemodal("Error", "Bad Request")
    //        //alert("fail");
    //    }
    //});
}









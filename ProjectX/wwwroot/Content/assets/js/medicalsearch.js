var projectname = checkurlserver();


$(document).ready(function () {
    drawpolicytable();

    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname)
        resetdatatable("#policy");
    });

    $("#searchpolicy").click(function () {
        SearchPolicies()
    });

    $("input").attr("autocomplete", "off");
    triggertooltip("#searchpolicy")
    ////SearchPolicies()

    //$('a[data-toggle="tooltip"]').tooltip({
    //    animated: 'fade',
    //    placement: 'bottom',
    //    trigger: 'click'
    //});

});

function validatesearchpolicy(divname, me) {
    const requiredFields = $(divname + ' :input');
    var inputValues = [];

    requiredFields.each(function () {
        var field = $(this).val().trim();
        inputValues.push({ val: field });
        var id = $(this).attr("id");

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
        pinNo: $("#pinNo").val(),
        policyNo: $("#policyNo").val(),
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/Medical/SearchPolicy",
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

        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function drawpolicytable(data) {
    //console.log(data)
    //age: 0
    //bK_NO: "07237"
    //benefits: null
    //cob: "PCH"

    var table = $('#policy').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a href="#" title="View Policy" polid="` + full.idPolicy + `" polnum="` + full.policyNo + `" brk="` + full.bK_NO + `" cob="` + full.cob + `"  class="text-black-50" onclick="viewpolicy(this)">
                            <i class="fas fa-eye"></i></a>
                            <div id="thisloader" class="smloader hide"></div>`

                }
            },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "policytype" },
            { "title": "Policy", "className": "text-center filter", "orderable": true, "data": "policyNo" },
            //{ "title": "Type", "className": "text-center filter", "orderable": true, "data": "customerType" },
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "name" },
            { "title": "Inception", "className": "text-center filter", "orderable": true, "data": "inceptionDate" },
            { "title": "Expiry", "className": "text-center filter", "orderable": true, "data": "expiryDate" },
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  href="#" title="Register" custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `"  polid="` + full.idPolicy + `" lob="` + full.idpolicytype + `"  profid="` + full.idProfile + `" isOnline="` + full.isOnline + `" class="text-black-50" onclick="medicalregister(this)"><i class="fas fa-book"/></a>`;
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
    var cob = $(me).attr('cob');
    var brokercode = $(me).attr('brk');
    var policynum = $(me).attr('polnum');

    var polobj = {
        "polType": "O",
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
                "sc_ser": "",
                "sc_hpins": "",
                "pt_brcod": "",
                "sc_tpa": "",
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
                var url = "http://" + result;
                window.open(url, '_blank').focus();
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



    /*  $("#view-policy").find('.modal-body').html("").append(`<iframe src="Content/assets/images/travel2021.pdf" width="100%" style="height:73vh"></iframe>`)*/
}

function medicalregister(me) {
    showloader("hide");
    var thiscase = {
        idCustomerType: 1,
        idPolicy: $(me).attr("polid"),
        idInsured: $(me).attr("insid"),
        idProfile: 1,
        isOnline: 0,
        lob: $(me).attr("lob"),
    }


    var url = projectname + "/Case/GetPolicyMedicalCase";

    $.redirect(url, thiscase);
    removeloader();
    return


    //$.ajax({
    //    type: 'POST',
    //    url: projectname + "/Case/GetPolicyMedicalCase",
    //    data: { req: thiscase},
    //    success: function (result) {
    //        $('#partialscreen .content-page').html("").html(result).scrollTop(0)
    //        removeloader();
    //        $(".modal-backdrop").remove();

    //    },
    //    failure: function (data, success, failure) {
    //        showresponsemodal("Error", "Bad Request")
    //    },
    //    error: function (data) {
    //        showresponsemodal("Error", "Bad Request")
    //    }
    //});



}


function viewcase(me) {
    togglebtnloader(me);
    var idcase = $(me).attr("idcase");
    $.ajax({
        type: 'Get',
        url: projectname + "/Case/GetCase",
        data: { parameter: idcase },
        success: function (result) {
            $("#view-history").modal("hide");

            $('#partialscreen .content-page').html("").html(result).scrollTop(0);
            triggerBody();
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}





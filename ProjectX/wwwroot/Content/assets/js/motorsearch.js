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

    $(".viewtowinghistory").click(function () {
        $("#towinghistory").toggle()
    });
    //SearchPolicies()
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
        plateNo: $("#plateNo").val(),
        plateLtr: $("#plateLtr").val(),
        policyNo: $("#policyNo").val(),
        cob: $("#policyCob").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Motor/SearchPolicy",
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

    var table = $('#policy').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "order": [[7, "desc"]],
        "filter": true,
        "destroy": true,
        "columns": [
            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    //return `<a  href="#" title="View Policy"  class="text-black-50" data-toggle="modal" 
                    //    data-target="#view-policy" polid="` + full.idPolicy + `"  onclick="viewpolicy(this)"><i class="fas fa-eye"/></a> <div id="thisloader" class="smloader hide"></div>`;
                    if (full.validity == "Expired")
                        return '';
                    return `<a href="#" title="View Policy" custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" isOnline="` + full.isOnline + `" class="text-black-50" onclick="viewpolicy(this)">
                            <i class="fas fa-eye"></i></a>
                            <div id="thisloader" class="smloader hide"></div>`

                }
            },
            { "title": "Policy", "className": "text-center filter", "orderable": true, "data": "policyNo" },
            //{ "title": "Type", "className": "text-center filter", "orderable": true, "data": "customerType" },
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "name" },
            { "title": "Inception", "className": "text-center filter", "orderable": true, "data": "inceptionDate" },
            { "title": "Expiry", "className": "text-center filter", "orderable": true, "data": "expiryDate" },
            { "title": "Cancelled", "className": "text-center filter", "orderable": true, "data": "isCancelled" },
            { "title": "Company", "className": "text-center filter", "orderable": true, "data": "tcomp" },
            { "title": "Validity", "className": "text-center filter", "orderable": true, "data": "validity" },
            //{
            //    'data': 'idPolicy',
            //    className: "dt-center editor-edit hidden",
            //    "render": function (data, type, full) {
            //        return `<a  href="#" title="Register" custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" isOnline="` + full.isOnline + `" class="text-black-50" onclick="register(this)"><i class="fas fa-truck"/></a>`;
            //    }
            //},

            {
                'data': 'idPolicy',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    var hidden = ""; if (full.tvisible == 0) hidden = "hidden";
                    //if (full.validity == "Expired")
                    //    return '';
                    if (full.policyNo.includes("MOC"))
                        return '';
                    if (full.isCancelled == "Yes")
                        return '';

                    if (full.validity == "Valid")
                        return `<div class="dropdown">
                            <a href="#"   class="dropdown-toggle arrow-none text-muted" data-toggle="dropdown" aria-expanded="false">
                                <i class="fas fa-bars"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right" x-placement="bottom-end" style="position: absolute; will-change: transform; top: 0px; left: 0px; transform: translate3d(-164px, 20px, 0px);">
                                <a custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" isOnline="` + full.isOnline + `" tvisible="` + full.tvisible + `"  href="javascript:void(0);" class="dropdown-item" onclick="register(this,1)" >  <i class="uil uil-user mr-3"></i>Expert</a>
                             <a custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" isOnline="` + full.isOnline + `"  tvisible="` + full.tvisible + `"  href="javascript:void(0);" class="dropdown-item" onclick="register(this,6)" ` + hidden + `> <i class="uil uil-truck mr-3"></i>Towing</a>
                                <a custtype="` + full.idCustomerType + `" insid="` + full.idInsured + `" polid="` + full.idPolicy + `" profid="` + full.idProfile + `" isOnline="` + full.isOnline + `"  tvisible="` + full.tvisible + `"  href="javascript:void(0);" class="dropdown-item" onclick="register(this,2)" ` + hidden + `> <i class="uil uil-users-alt mr-3"></i>Towing/Expert</a>
                                <!--   <div class="dropdown-divider"></div>  -->
                            </div>
                        </div>`;
                    else
                        return '';


                }
            }


        ],
        orderCellsTop: true,
        fixedHeader: true,
        fnRowCallback: function (nRow, aData) {
            
            if (aData.isCancelled == "Yes") {
                 $('td', nRow).css('background-color', '#d01b1b').css('color','white');
            }
        }
    });


    triggertooltip("#testme")

    triggerfiltertable(table, "policy")
}


function viewpolicy(me) {
    togglebtnloader(me);

    var id = $(me).attr('polid');
    //showloader("hide");
    $("#benefits").html("")
    $("#conditions").html("")
    $(".imgtaxi").attr("hidden", "hidden")
    $(".imgtow").attr("hidden", "hidden")


    $.ajax({
        type: 'POST',
        url: projectname + "/Motor/ViewPolicy",
        data: { idpolicy: id },
        success: function (result) {
            console.log(result)
           
            var currentRow = $(me).closest("tr");
            $(".insuredname").html(currentRow.find("td:eq(2)").text())
            $(".phone").html(result.insured.phone)
            $(".broker").html(result.broker.code + " - " + result.broker.name)
            $(".towing").html(result.towing.tow)
            $(".mechanical").html('Mechanical: ' + result.towing.usedMech)
            $(".accident").html('Accident: ' + result.towing.usedAcc)

            //alert($(".cancelled").html(currentRow.find("td:eq(5)").text()))

            $(".platenumber").html(result.motorinfo.plate)
            $(".make").html(result.motorinfo.make)
            $(".model").html(result.motorinfo.model)
            $(".chassis").html(result.motorinfo.chassis)
            $(".bodytype").html(result.motorinfo.bodyType)

            $(".policynumber").html(currentRow.find("td:eq(1)").text())
            $(".inception").html(currentRow.find("td:eq(3)").text())
            $(".expiry").html(currentRow.find("td:eq(4)").text())
            $(".cancelled").html(currentRow.find("td:eq(5)").text())
            $(".company").html(currentRow.find("td:eq(6)").text())
            $(".policystatus").html(result.policytype)

            //hide new cases dropdown if the policy is cancelled or not valid
            if (currentRow.find("td:eq(5)").text() == "Yes" || currentRow.find("td:eq(7)").text()!='Valid')
                $(".dropdown").hide()
            else $(".dropdown").show()

            if (result.hastaxi)
                $(".imgtaxi").removeAttr("hidden")
            if (result.hastowing) {
                $(".imgtow").removeAttr("hidden")
                $(".togtowing").removeAttr("hidden")
            }
            else {
                $(".togtowing").attr("hidden", "hidden")
            }

            $(".platenumber").html(result.motorinfo.plate)
            $(".make").html(result.motorinfo.make)
            $(".model").html(result.motorinfo.model)
            $(".chassis").html(result.motorinfo.chassis)
            $(".bodytype").html(result.motorinfo.bodyType)
            

            var bentable = $('#benefits').DataTable({
                "data": result.benefits,
                "paging": false,
                "ordering": true,
                "filter": false,
                "destroy": true,
                "bInfo": false,
                "columns": [
                    { "title": "Code", "className": "text-center filter", "orderable": true, "data": "idBenefit" },
                    //{ "title": "Type", "className": "text-center filter", "orderable": true, "data": "customerType" },
                    { "title": "Benefit", "className": "filter", "orderable": true, "data": "description" },
                    { "title": "Limit", "className": "text-center filter", "orderable": true, "data": "limit" },
                    { "title": "Deductible", "className": "text-center filter", "orderable": true, "data": "deductible" },
                ],
                orderCellsTop: true,
                fixedHeader: true
            });

            var condtable = $('#conditions').DataTable({
                "data": result.conditions,
                "paging": false,
                "ordering": true,
                "filter": false,
                "destroy": true,
                "bInfo": false,
                "columns": [
                    { "title": "Code", "className": "text-center filter", "width": "15px", "orderable": true, "data": "code" },
                    { "title": "Description", "className": "filter", "orderable": true, "data": "description" },
                ],
                orderCellsTop: true,
                fixedHeader: true
            });
            $($.fn.dataTable.tables(true)).DataTable().columns.adjust();



            //// adding onclick attributes for services

            //custtype = "` + full.idCustomerType + `" insid = "` + full.idInsured + `" polid = "` + full.idPolicy + `" profid = "` + full.idProfile + `" isOnline = "` + full.isOnline + `"

            //$("#policy-popup-services").find('a').attr("custtype","")
            //$("#policy-popup-services").find('a').attr("insid","")
            //$("#policy-popup-services").find('a').attr("polid","")
            //$("#policy-popup-services").find('a').attr("profid", "")
            //$("#policy-popup-services").find('a').attr("isOnline", "")
            /////
             
            $("#policy-popup-services a").each(function (index) {
                var tvisible = 0;
                if (result.hastowing)
                    tvisible = 1;

                $(this).attr("custtype", $(me).attr("custtype"))
                $(this).attr("polid", $(me).attr("polid"))
                $(this).attr("insid", $(me).attr("insid"))
                $(this).attr("profid", $(me).attr("profid"))
                $(this).attr("isOnline", $(me).attr("isOnline"))
                $(this).attr("tvisible", tvisible)

                if ($(this).attr('serv') != 1 && !result.hastowing)
                    $(this).attr("hidden", "hidden")
                else
                    $(this).removeAttr("hidden")
            });


            $('#towinghistory tbody').html("")
            if (result.towing.towingDetails.length > 0)
                for (g in result.towing.towingDetails) {
                    var $row = $('<tr>' +
                        '<td>' + result.towing.towingDetails[g].towdate + '</td>' +
                        '<td>' + result.towing.towingDetails[g].from + '</td>' +
                        '<td>' + result.towing.towingDetails[g].to + '</td>' +
                        '</tr>');
                    $('#towinghistory tbody').append($row);
                }
            else {
                var $row = $('<tr>' +
                    '<td colspan="3"> No Towing Found!</td>' +
                    '</tr>');
                $('#towinghistory tbody').append($row);
            }
            //var towtable = $('#towinghistory').DataTable({
            //    "data": result.towing.towingDetails,
            //    "paging": false,
            //    "ordering": false,
            //    "filter": false,
            //    "destroy": true,
            //    "bInfo": false,
            //    "columns": [
            //        { "title": "Towing Date", "data": "towdate" },
            //        { "title": "From ", "data": "from" },
            //        { "title": "To ", "data": "to" },
            //    ],
            //});


            $('#view-policy').modal('show').find(".modal-header").removeAttr("style");

            $($.fn.dataTable.tables(true)).DataTable().columns.adjust();

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

function register(me, serviceid) {
    showloader("hide");

    var thiscase = {
        idCustomerType: 1,
        idPolicy: $(me).attr("polid"),
        idInsured: $(me).attr("insid"),
        idProfile: 1,
        isOnline: 0,
        Tvisible: $(me).attr("tvisible"),
        Serviceid: serviceid
    }

    var url = projectname + "/Case/GetPolicyMotorCase";

    $.redirect(url, thiscase);
    removeloader();
    return



    //$.ajax({
    //    type: 'POST',
    //    url: projectname + "/Case/GetPolicyMotorCase",
    //    data: { req: thiscase, serviceid: serviceid },
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
                $("#gotocase").tooltip('toggle');
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



//function checktowinglocation(type) {
//    var selectedlocation;
//    var selectedregion;

//    if (type == "from") {
//        selectedregion = $("#SelectRegionFrom option:selected").text().trim();
//        selectedlocation = "#SelectLocationFrom";
//    }
//    else {
//        selectedregion = $("#SelectRegionTo option:selected").text().trim();
//        selectedlocation = "#SelectLocationTo";
//    }

//    $(selectedlocation + '> option').each(function () {
//        if ($(this).val() != '') {

//            if ($(this).attr("region").trim() == selectedregion)
//                $(this).removeClass("hidden")
//            else
//                $(this).addClass("hidden")
//        }
//    });

//    if ($(selectedlocation + ' :selected').hasClass("hidden")) {
//        $(selectedlocation).val("");
//        seturlgop();
//    }
//}


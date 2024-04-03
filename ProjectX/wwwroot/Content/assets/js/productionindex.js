var projectname = checkurlserver();
var travelinfo = {}
var addbenefits = []
$(document).ready(function () {
    $("#search").click(function () {
        //drawtable()
        Search();
    });
    drawtable();

    $(".isselect2").select2({
        tokenSeparators: [',', ' ']
    })

    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        var tablename = $(this).closest(".tab-pane").find('table').attr("id")
        resetAllValues(divname);
        if ($('#' + tablename + ' tbody tr').length > 0)
            resetdatatable("#" + tablename);
    });

    Search();
    //Search2();
    //Search3();
    //Search4();
    //Search5();


    var isadmin = $(".prodadm").attr("prodadm")
    //alert(isadmin)

    $('.nav-tabs a').on('click', function (e) {
        //e.preventDefault();
        var tabcontainer = $($(this).attr('href'))

        if (tabcontainer.find('table tbody tr').length == 0) {
            tabcontainer.find('button[id^="search"]').click();
        }
    });

});

function getCancellationControl(data, status) {
    var isAdmin = data[0].isAdmin;
    var canCancel = data[0].canCancel;

    if ((isAdmin == true || canCancel == true) && (status == 1 || status == 3))
        return 1;
    
    else return 0;
}
function getEditableControl(data, status) {
    var isAdmin = data.isAdmin;
    var canEdit = data.canEdit;

    if (data.status == 4 || data.source == 'M')
        return 0;
    else if (isAdmin == true)
        return 1;
    else if (canEdit == true)
        return 1;
    else if (data.isEditable == true)
        return 1;
    
    else return 0;
}

function drawtable(data, status) {
   
    console.log(data, 'policy data');
    if (status == null || status == 'undefined')
        status = 1;
    var tableid = '#productiontable' + status;
    var isAdmin = false;
    var canEdit = false;
    var cancelAllow = 0;
    if (data != undefined && data.length>0) {
        isAdmin = data[0].isAdmin;
        canEdit = data[0].canEdit;
         cancelAllow = getCancellationControl(data, status);
    }
   

    var table = $(tableid).DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "order": [[1, 'desc']],
        "columns": [
            { "title": "Reference", "className": "text-center filter truncatetd", "orderable": true, "data": "reference" },
            {
                "title": "Production Date", "className": "text-center filter", "orderable": true, "data": "createdOn",
                "render": function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // Format the date using JavaScript's toLocaleDateString function
                        var date = new Date(data);
                        var formattedDate = formatDate_DdMmYyyy(date);
                        return formattedDate;
                    }
                    return data;
                }
            },
            { "title": "Client Name", "className": "text-center filter truncatetd", "orderable": true, "data": "mainname" },
            {
                "title": "Inception", "className": "text-center filter", "orderable": true, "data": "fromdate",
                "render": function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // Format the date using JavaScript's toLocaleDateString function
                        var date = new Date(data);
                        var formattedDate = formatDate_DdMmYyyy(date);
                        return formattedDate;
                    }
                    return data;
                }
            },
           
            //{ "title": "Nb isCanceled", "className": "text-center filter", "orderable": true, "data": "isCanceled" },
            { "title": "# Clients", "className": "text-center filter", "orderable": true, "data": "nbofclients" },
            { "title": "Total $", "className": "text-center filter", "orderable": true, "data": "grandTotal" },
            { "title": "Created by", "className": "text-center filter", "orderable": true, "data": "createdByName" },

            {
                title: "Editable",
                className: "text-center filter",
                orderable: true,
                visible: isAdmin == true ||(( canEdit==true) && (status == 1 || status == 3)),
                data: "isEditable",
                render: function (data, type, full, meta) {

                    // if (full.status == 3) {
                    if (isAdmin == true || (full.status != 4 && full.source!='M')) {
                        if(isAdmin==true|| (type === 'display' || type === 'filter')) {
                            // Assuming "IsEditable" is a boolean property
                            if (data) {
                                var checkbox = $(`<input id="chckbox` + full.policyID + `" type="checkbox" onclick="responsemodalcheckbox('confirm-edit-approval',${full.policyID},${meta.row},this); triggerclose(this)" checked>`);
                            }
                            else
                                var checkbox = $(`<input id="chckbox` + full.policyID + `" type="checkbox" onclick="responsemodalcheckbox('confirm-edit-approval',${full.policyID},${meta.row},this);triggerclose(this)">`);

                            return checkbox[0].outerHTML;
                        }
                    }
                    //return data; // For other types, return the original data
                    // }
                     return '';
                }
            },

            {
                'data': 'policyID',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    var icon = "";

                    //if (isAdmin == 'True') {
                    //    icon = "book";
                    //    return `<a   title="Edit" polid="` + full.policyID + `" stat="` + status + `" polstat="` + full.status + `" class="text-black-50" onclick="gotopol(this)"><i class="fas fa-${icon}"/></a>`;

                    //}
                    //else
                    var editAllow = getEditableControl(full, status);
                    //if (full.status == 4 || full.source == 'M') {
                    if (editAllow==0 && isAdmin==false) {
                        icon = "eye";
                        return `<a   title="View" polid="` + full.policyGuid + `" stat="` + status + `" polstat="` + full.status + `"  src="` + full.source + `" class="text-black-50" onclick="gotopol(this)"><i class="fas fa-${icon}"/></a>`;
                    }
                    else{
                        icon = "book";
                        return `<a   title="Edit" polid="` + full.policyGuid + `" stat="` + status + `" polstat="` + full.status + `"  src="` + full.source + `" class="text-black-50" onclick="gotopol(this)"><i class="fas fa-${icon}"/></a>`;
                    }

                    //  {
                    // if (full.status == 3) icon = "book"; else icon = "eye"
                    //  {
                    //   }
                    //  }
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            },
            {
                'data': 'policyID',
                className: "dt-center editor-edit",
                //visible: isAdmin == 'True' && (status == 1 || status == 3),
                visible: isAdmin == true || (cancelAllow == 1 || (isAdmin == true && data.source == 'M')),
                "render": function (data, type, full, meta) {   
                    //if (full.status == 3)
                   // var cancelAllow = getCancellationControl(data, status);
                    if (full.status != 5 || isAdmin==true) {
                        if (full.isCanceled) {

                            return `<a  title="Activate" prodid="` + full.policyID + `"  class="text-black-50" onclick="changestatus('confirm-delete-production',${full.policyID},${meta.row},${full.isCanceled})" ><i class="fas fa-check green"></i></a>`;
                        }
                        else {

                            return `<a  title="Cancel" prodid="` + full.policyID + `"  class="text-black-50" onclick="changestatus('confirm-delete-production',${full.policyID},${meta.row},${full.isCanceled})" ><i class="fas fa-times red"></i></a>`;
                        }
                    }
                     else return '';

                }
            },
            //{ "title": "Source", "className": "text-center filter", "orderable": true, "data": "source" },
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        console.log(data.isCanceled)

        if (data.isCanceled == true) {
            $(this.node()).addClass('cancelled-row');
        }
    });

    triggerfiltertable(table, "profile")
}

//$('#closeconfirmeditbtn').click(function () {
//    returncheckbox(this);

//});


function changestatus(popupname, polid, metarow, isCanceled) {
    if (isCanceled)
        $(`#${popupname} .modal-body.msgtxt`).text("Are you sure you want to activate this record ?")
    else
        $(`#${popupname} .modal-body.msgtxt`).text("Are you sure you want to cancel this record ?")

    showresponsemodalbyid(popupname, polid, metarow);
}
function triggerclose(me) {
    $("#closeconfirmeditbtn").off("click");

    $('#closeconfirmeditbtn').click(function () {
        var status = $(me).is(':checked');
        if (status) {
            $(me).prop("checked", false)
        }
        else {
            $(me).prop("checked", true)
        }
    });
}

function returncheckbox(me) {
    var isEditable = $(me).closest("#confirm-edit-approval").attr("chckbox");
    var polid = $(me).closest("#confirm-edit-approval").attr("actid");
    var rowid = '#chckbox' + polid;
    if (isEditable == '0') {
        $(rowid).prop("checked", false)
    } else {
        $(rowid).prop("checked", true)
    }
}
function responsemodalcheckbox(popupid, thisid, trindex, me) {
    showresponsemodalbyid(popupid, thisid, trindex)
    var status = $(me).is(':checked');
    if (status)
        $('#' + popupid).attr('chckbox', 1);
    else
        $('#' + popupid).attr('chckbox', 0);
}

function Search() {
    if (validateForm("#searchform")) {
        return;
    }
    showloader("load")
    var status = $('#tab1default').attr('status');
    //alert(status)
    var filter = {
        status: status,
        reference: $("#referencename").val().trim(),
        beneficiarys: $("#beneficiaryname").val().trim(),
        passportno: $("#passportno").val().trim(),
        agentid: $("#agentid1").val().trim()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Production/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            if (result.production)
                drawtable(result.production);
            else
                drawtable();

            //if (result.statusCode.code != 1)
            //    showresponsemodal("error", result.statusCode.message)
            //else {
            //    drawtable(result.profiles);
            //}

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            alert("Error:" + failure);
        },
        error: function (xhr, data) {
            var errorMessage;
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                errorMessage = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                errorMessage = xhr.responseText;
            } else {
                errorMessage = 'An error occurred.';
            }

            showresponsemodal("Error", errorMessage)
        }
    });
}
function removerow(me) {
    $(me).closest("tr").remove();
}
function gotopol(me) {
    showloader("load")
    sessionStorage.setItem('status', $(me).attr("stat"));

    if ($(me).attr('src') == 'M')
        window.location.href = "/production/EditManual/" + $(me).attr("polid");
    else
        window.location.href = "/production/edit/" + $(me).attr("polid");
    removeloader();
    return
}
$("#confirmdeletebtn").click(function () {
    cancelproduction(this);
});
$("#confirmeditbtn").click(function () {
    editableProduction(this);
});
function editableProduction(me) {
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-edit-approval").attr("actid");
    var isEditable = $(me).closest("#confirm-edit-approval").attr("chckbox");

    //deductible: insuredSection.find("input[name='name'][data-dedprice]").prop("checked"),
    //table.find('input[data-dedprice]').prop('checked', item.deductible);
    //alert(isEditable)


    removebtnloader(me);

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Production/EditableProduction",
        data: { polId: thisid, isEditable: isEditable },
        success: function (result) {

            if (result.statusCode.code == 1) {

                removebtnloader(me);
                showresponsemodal(result.statusCode.code, result.statusCode.message)
            }
            else
                showresponsemodal(result.statusCode.code, result.statusCode.message, "Production")

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}
$("#search2").click(function () {
    Search2();
});
$("#search3").click(function () {
    Search3();
});
$("#search4").click(function () {
    Search4();
});
$("#search5").click(function () {
    Search5();
});
function Search2() {
    if (validateForm("#searchform2")) {
        return;
    }
    showloader("load")
    var status = $('#tab2default').attr('status');
    //alert(status)
    var filter = {
        status: status,
        reference: $("#referencename2").val().trim(),
        beneficiarys: $("#beneficiaryname2").val().trim(),
        passportno: $("#passportno2").val().trim(),
        agentid: $("#agentid2").val().trim()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Production/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            if (result.production)
                drawtable(result.production, status);
            else
                drawtable();

            //if (result.statusCode.code != 1)
            //    showresponsemodal("error", result.statusCode.message)
            //else {
            //    drawtable(result.profiles);
            //}

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            alert("Error:" + failure);
        },
        error: function (xhr, data) {
            var errorMessage;
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                errorMessage = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                errorMessage = xhr.responseText;
            } else {
                errorMessage = 'An error occurred.';
            }

            showresponsemodal("Error", errorMessage)
        }
    });
}
function Search3() {
    if (validateForm("#searchform3")) {
        return;
    }
    showloader("load")
    var status = $('#tab3default').attr('status');
    var filter = {
        status: status,
        reference: $("#referencename3").val().trim(),
        beneficiarys: $("#beneficiaryname3").val().trim(),
        passportno: $("#passportno3").val().trim(),
        agentid: $("#agentid3").val().trim()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Production/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            if (result.production)
                drawtable(result.production, status);
            else
                drawtable();

            //if (result.statusCode.code != 1)
            //    showresponsemodal("error", result.statusCode.message)
            //else {
            //    drawtable(result.profiles);
            //}

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            alert("Error:" + failure);
        },
        error: function (xhr, data) {
            var errorMessage;
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                errorMessage = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                errorMessage = xhr.responseText;
            } else {
                errorMessage = 'An error occurred.';
            }

            showresponsemodal("Error", errorMessage)
        }
    });
}
function Search4() {
    if (validateForm("#searchform4")) {
        return;
    }
    showloader("load")
    var status = $('#tab4default').attr('status');
    var filter = {
        status: status,
        reference: $("#referencename4").val().trim(),
        beneficiarys: $("#beneficiaryname4").val().trim(),
        passportno: $("#passportno4").val().trim(),
        agentid: $("#agentid4").val().trim()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Production/Search",
        data: { req: filter },
        success: function (result) {
            console.log(result.production)
            removeloader();
            if (result.production)
                drawtable(result.production, status);
            else
                drawtable();

            //if (result.statusCode.code != 1)
            //    showresponsemodal("error", result.statusCode.message)
            //else {
            //    drawtable(result.profiles);
            //}

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            alert("Error:" + failure);
        },
        error: function (xhr, data) {
            var errorMessage;
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                errorMessage = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                errorMessage = xhr.responseText;
            } else {
                errorMessage = 'An error occurred.';
            }

            showresponsemodal("Error", errorMessage)
        }
    });
}
function Search5() {
    if (validateForm("#searchform5")) {
        return;
    }
    showloader("load")

    var status = $('#tab5default').attr('status');
    var filter = {
        status: status,
        reference: $("#referencename5").val().trim(),
        beneficiarys: $("#beneficiaryname5").val().trim(),
        passportno: $("#passportno5").val().trim(),
        agentid: $("#agentid5").val().trim()
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Production/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();
            if (result.production)
                drawtable(result.production, status);
            else
                drawtable();

            //if (result.statusCode.code != 1)
            //    showresponsemodal("error", result.statusCode.message)
            //else {
            //    drawtable(result.profiles);
            //}

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

            alert("Error:" + failure);
        },
        error: function (xhr, data) {
            var errorMessage;
            if (xhr.responseJSON && xhr.responseJSON.Message) {
                errorMessage = xhr.responseJSON.Message;
            } else if (xhr.responseText) {
                errorMessage = xhr.responseText;
            } else {
                errorMessage = 'An error occurred.';
            }

            showresponsemodal("Error", errorMessage)
        }
    });
}
function cancelproduction(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    console.log(me)

    togglebtnloader(me)
    //var thisid = $(me).attr("userid")
    var thisid = $(me).closest("#confirm-delete-production").attr("actid")
    //var thisid = $("#userstable").closest("row").attr("userid")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Production/CancelProduction",
        data: { polId: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                removebtnloader(me);
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                Search();     
            }
            else
                showresponsemodal(result.statusCode.code, result.statusCode.message)

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}




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
});



function drawtable(data, status) {
    console.log(data, 'drawtable')

    if (status == null || status == 'undefined')
        status = 1;
    var tableid = '#productiontable' + status;
    var isAdmin = $(tableid).attr('admn');

    var table = $(tableid).DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "Reference", "className": "text-center filter", "orderable": true, "data": "reference" },
            { "title": "Client Name", "className": "text-center filter", "orderable": true, "data": "mainname" },
            {
                "title": "Inception", "className": "text-center filter", "orderable": true, "data": "fromdate",
                "render": function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // Format the date using JavaScript's toLocaleDateString function
                        var date = new Date(data);
                        //var formattedDate = date.toLocaleDateString('en-US'); // Adjust the locale as needed
                        var formattedDate = formatDate_DdMmYyyy(date);
                        return formattedDate;
                    }
                    return data; // For other types, return the original data
                }
            },
            //{ "title": "Nb isCanceled", "className": "text-center filter", "orderable": true, "data": "isCanceled" },
            { "title": "Nb of Clients", "className": "text-center filter", "orderable": true, "data": "nbofclients" },
            { "title": "Total", "className": "text-center filter", "orderable": true, "data": "grandTotal" },

            {
                title: "Editable",
                className: "text-center filter",
                orderable: true,
                visible: isAdmin == 'True' && (status == 1 || status == 3),
                data: "isEditable",
                render: function (data, type, full, meta) {
                    
                   // if (full.status == 3) {
                        if (type === 'display' || type === 'filter') {
                            // Assuming "IsEditable" is a boolean property
                            if (data) {
                                var checkbox = $(`<input id="chckbox` + full.policyID + `" type="checkbox" onclick="responsemodalcheckbox('confirm-edit-approval',${full.policyID},${meta.row},'1'); triggerclose(this)" checked>`);
                            }
                            else
                                var checkbox = $(`<input id="chckbox` + full.policyID + `" type="checkbox" onclick="responsemodalcheckbox('confirm-edit-approval',${full.policyID},${meta.row},'0');triggerclose(this)">`);

                            return checkbox[0].outerHTML;
                        }
                        return data; // For other types, return the original data
                   // }
                   // return '';
                }
            },

            {
                'data': 'policyID',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    var icon = "";
                    if (isAdmin == 'True') {
                        icon = "book";
                        return `<a   title="Edit" polid="` + full.policyID + `" stat="` + status + `" polstat="` + full.status + `" class="text-black-50" onclick="gotopol(this)"><i class="fas fa-${icon}"/></a>`;

                    }
                    else if (status == 1 || status == 3) {
                        icon = "book";
                        return `<a   title="Edit" polid="` + full.policyID + `" stat="` + status + `" polstat="` + full.status + `" class="text-black-50" onclick="gotopol(this)"><i class="fas fa-${icon}"/></a>`;

                    }
                    else {
                        icon = "eye";
                        return `<a   title="Edit" polid="` + full.policyID + `" stat="` + status + `" polstat="` + full.status + `" class="text-black-50" onclick="gotopol(this)"><i class="fas fa-${icon}"/></a>`;

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
                visible: isAdmin == 'True' && (status == 1 || status == 3),
                "render": function (data, type, full, meta) {
                    //if (full.status == 3)
                        return `<a  title="Delete" prodid="` + full.policyID + `"  class="text-black-50" onclick="showresponsemodalbyid('confirm-delete-production',${full.policyID},${meta.row})" ><i class="fas fa-times red"></i></a>`;
                  //  else return '';

                }
            }
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

$('#closeconfirmeditbtn').click(function () {
    returncheckbox(this);

});
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
//function triggerclose(me) {

//    $('#closeconfirmeditbtn').click(function () {
//        var status = $(me).is(':checked');
//        if (status) {
//            $(me).prop("checked", false)
//        }
//        else {
//            $(me).prop("checked", true)
//        }
//    });
//}
function responsemodalcheckbox(popupid, thisid, trindex, chkbx) {
    showresponsemodalbyid(popupid, thisid, trindex)
    $('#' + popupid).attr('chckbox', chkbx);
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
                Search();      ///////1557///////    why do u need to get all users after deleting?
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




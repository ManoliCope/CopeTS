
//var projectname = checkurlserver();
//var screentype = $("#rcid").attr("screentype")

//var url_string = window.location.href
//var url = new URL(url_string);
//var selectedcid = url.searchParams.get("cid");



//if ($("#rcid").attr("cid")) {
//    var caseid = $("#rcid").attr("cid")
//    gotopage('Case', 'GetGlobalCase', caseid)
//}

//else if (screentype == "home" & selectedcid != null) {
//    localStorage.setItem("cid", selectedcid);
//    gotopage('Case', 'AllCaseSearch')
//}
//else {
//    var url = window.location.pathname;
//    if (url.toLowerCase().includes('home'))
//        $("body").find(".defaultscreen:first").click();

//    $('.dropdown-menu-right').on('hide.bs.dropdown', function () {
//        return false;
//    });


//    togglemenucolor()

//    var avayacid = localStorage.getItem("cid");
//    if (avayacid) {
//        triggeravayaselect(avayacid)
//    }
//    showavayapopup();

//}
function deletedatatablerowbyid(idToDelete, conditionFieldName, tablename) {
    //table.rows().eq(0).each(function (index) {
    //    var row = table.row(index);
    //    var rowData = row.data();

    //    // Check the condition using the specified field name and value
    //    if (rowData[conditionFieldName] === conditionID) {
    //        row.remove().draw();
    //        return false; // Exit the loop once the row is deleted
    //    }
    //});



    var table = $('#' + tablename).DataTable();
    var row = table.row(function (index, data, node) {
        return data[conditionFieldName] == idToDelete;
    });
    row.remove().draw();


    //var table = $('#' + tablename).DataTable();
    //var rowToDelete = table.rows().eq(0).filter(function (rowIdx) {
    //    return table.cell(rowIdx, 0).data() == idToDelete;
    //});
    //console.log(rowToDelete.data())
    //table.row(rowToDelete).remove().draw();



}
function triggerselectedmenu() {
    var menuholder = $('.sidebar-content').find(".active").parent().closest(".sub-menu")
    if (menuholder.length > 0)
        menuholder.find('a').first()[0].click()

}

triggerselectedmenu()


function showloader(inside) {

    if ($("#loader").length == 0) {
        if (inside) {
            //$('#cover-spin').show(0)
            //$("#cover-spin").css("display", "block !important")
            $("#partialscreen .container-fluid").parent().prepend(` <div class="myloader" >
                <div class="loader" style="background:none !important"></div>
            </div>`);

            $("#partialscreen .container-fluid").css("opacity", "0.2");
        }
        else {
            $("body").append(`<div style="width:100%;height:500px; background:transparent"><div id="loader"></div></div>`)
            $('#partialscreen .content-page').css("display", "none")
        }
    }
    $('body').css('overflow-y', 'hidden');

}

function removeloader() {
    $("#loader").parent().remove();
    $(".myloader").remove();
    $('body').css('overflow-y', 'auto');
    $("#partialscreen .container-fluid").css("opacity", "unset")
    //$(".modal-backdrop").remove();
}



function togglebtnloader(me) {
    $(me).removeClass("show").addClass("hide");
    $(me).parent().find("#thisloader").removeClass("hide").addClass("show");
}

function removebtnloader(me) {
    $(me).removeClass("hide").addClass("show");
    $(me).parent().find("#thisloader").removeClass("show").addClass("hide");
}

//// validating source request if eligable to pass
//window.addEventListener('message', event => {
//    $.ajax({
//        type: 'Get',
//        url: projectname + '/Navigate/geturl',
//        success: function (result) {
//            var url = result.slice(0, -1)
//            var thisorigin = event.origin;
//            if (projectname != '')
//                thisorigin = thisorigin + '/wb'

//            if (thisorigin.startsWith(url)) {
//                removeloader();
//                $('#partialscreen .content-page').css("display", "block")
//            } else {
//                alert('error')

//                return;
//            }
//        }
//    });
//});


function togglefilter() {
    $("#rowfilter").toggleClass("hidden")
}


function triggerfiltertablebackup(datatable, tablename) {

    $('#' + tablename + ' thead tr').clone(true).appendTo('#' + tablename + ' thead').attr("id", "rowfilter").removeClass().addClass("hidden").find('th').wrapInner('<td />').contents().unwrap();
    $('#' + tablename + ' thead tr:eq(1) td').each(function (i) {
        var title = $(this).text();
        if ($('#' + tablename + ' thead tr th:eq(' + i + ')').hasClass("filter")) {
            $(this).html('<input type="text" class="form-control datatable-input" placeholder="Search ' + title + '" />');
        }


        $('input', this).on('keyup change', function () {
            console.log(this.value)
            console.log(i)
            if (datatable.column(i).search() !== this.value) {
                datatable
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });

    $("#" + tablename + "_wrapper").find("#" + tablename + "_length").append(`<span  class='pl-4 noselect'><a  onclick='togglefilter()' style='cursor:pointer'>
                        <i class="fas fa-filter" aria-hidden="true"></i> <span>Filter</span>
                    </a></span>
        `);

}


function triggerfiltertable(datatable, tablename) {
    //$("#" + tablename + "_wrapper").find("#" + tablename + "_length").append(`
    //        <div class="btn-group dropright">
    //              <span  class="dropdown align-self-center profile-dropdown-menu pl-4 noselect" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style='cursor:pointer'>
    //                             <i id="filtericon" class="fas fa-filter" aria-hidden="true"></i> <span>Filter</span>
    //              </span>
    //              <div id='datatablefilterlist' class="dropdown-menu profile-dropdown " ">
    //              </div>
    //        </div>
    //`);
    //$('#' + tablename + ' thead tr th').each(function (i) {
    //    var title = $(this).text();
    //    // alert(title);
    //    if ($('#' + tablename + ' thead tr th:eq(' + i + ')').hasClass("filter")) {
    //        $("#datatablefilterlist").append('<div class="row p-1"><div class="col-md-4" style="text-align: center;padding-top: 5px"><span>' + title + '</span></div><div class="col-md-8"><input type="text" name=' + i + ' class="form-control datatable-input" placeholder="Search ' + title + '" /></div></div>');
    //    }
    //});
    //$("#datatablefilterlist").append('<div class="row p-1"><div class="col-md-4" ></div><div class="col-md-8" > <button id="resettablefilters" class= "btn btn-info" id = "kt_reset" style = "width:100%" ><span>Reset</span></button></div ></div>');


    //$('input', '#datatablefilterlist').on('keyup change', function () {
    //    $("#filtericon").css("color", "red");
    //    if (datatable.column(this.name).search() !== this.value) {
    //        datatable
    //            .column(this.name)
    //            .search(this.value)
    //            .draw();
    //    }
    //});

    //$('#resettablefilters').on('click', function () {
    //    $(this).closest('#datatablefilterlist').find('input').val('');
    //    $("#filtericon").css("color", "inherit");
    //    datatable
    //        .columns().search("")
    //        .draw();
    //});

    var buttonsContainer = datatable.buttons().container();
    var filterElement = $('.dataTables_filter');

    buttonsContainer.prependTo(filterElement).addClass('dt-buttons-left').addClass('float-left').find('button').addClass('btn btn-info'); // Adjust positioning for left side
}


function triggerresonseclick(PageName, action, parameter) {

    $("#responsemodal button").click(function () {
        gotopage(PageName, action, parameter);
    });

}
function showresponsemodal(type, msg, pagename) {

    removeloader();
    $(".modal-backdrop").remove();

    //$('#responsemodal').modal({ backdrop: 'static', keyboard: false })
    $("body").find(".modal").modal("hide");
    $(".close").click();

    if (pagename) {
        $("#responsemodal button").click(function () {
            gotopage(pagename, "Index");
        });
    }


    $("body").find("#AddPatient").css("display", "none");

    if (type == "1") {
        $('#responsemodal .modal-confirm').removeClass("popup-success popup-failure").addClass("popup-success")
        //$('#responsemodal .btn').removeClass("popup-success popup-failure").addClass("popup-success")
        $('#responsemodal .icon-box i').removeClass().addClass("fas fa-check")

        $('#responsemodal h4').text("Success")
    } else {
        $('#responsemodal .modal-confirm').removeClass("popup-success popup-failure").addClass("popup-failure")
        //$('#responsemodal .btn').removeClass("popup-success popup-failure").addClass("popup-failure")
        $('#responsemodal .icon-box i').removeClass().addClass("fas fa-times")

        $('#responsemodal h4').text("Failure")
    }

    $(".modal-backdrop").removeClass("underlay")

    $('#responsemodal p').text(msg)
    $('#responsemodal').modal('show');

    $(".scrollpartialscreen").scrollTop(0)


}



function showresponsemodalbyid(popupid, thisid, trindex) {
    removeloader();
    $(".modal-backdrop").remove();

    $("body").find(".modal").modal("hide");
    $(".close").click();

    $('#' + popupid).addClass("transpback").attr("actid", thisid).attr("trindex", trindex);
    $('#' + popupid).modal('show');
    $("body .modal-backdrop").removeClass("show").addClass("underlay");

    $(".scrollpartialscreen").scrollTop(0)

}

//$(".loadingthis").on(function () {
//    $('#cover-spin').show(0)

//    //searchtable();
//    setTimeout(function () { removeloader(); }, 2000);

//});


function resetAllValues(divname) {
    $("#" + divname).find('input').val('');
    $("#" + divname).find("select").each(function () {
        this.selectedIndex = 0;
        this.selectedIndex = "";
    });
    $("#" + divname).find('input:checkbox').prop("checked", false);

}


function triggertooltip(id) {
    $(id).tooltip({
        animated: 'fade',
        placement: 'left',
        trigger: 'manual'
    });
    $(id).mouseleave(function () {
        $(this).tooltip('hide');
    });
}

function resetdatatable(tablename) {
    var table = $(tablename).DataTable();
    if (table.data().any()) {
        table.clear();
        table.draw();
        table.destroy();
        $(tablename).DataTable();
    }
}

function triggerBody() {
    $(".modal-backdrop").remove();
    $('body').removeClass("modal-open");
    $('body').css('overflow-y', 'auto');
}

function convertdate(thisdate) {
    var input = thisdate;
    var output = input.replace(/(\d\d)\/(\d\d)\/(\d{4})/, "$3-$2-$1");
    return output;
}

function gotopage(PageName, action, parameter) {
    showloader();
    if (!action)
        action = '';

    var link;
    if (parameter)
        link = PageName + "/" + action + "?id=" + parameter;
    else
        link = PageName + "/" + action;


    removeloader();
    $('#partialscreen .content-page').css("display", "block")

    window.location.href = "/" + link;


}






////  upload files
function uploadfiles(me, iddirectory, idreference, documenttype) {
    var thisformData = new FormData();

    var selectedfiles = Getfiles(me)
    if (selectedfiles)
        thisformData = selectedfiles;
    else
        return false;

    thisformData.append("fd", iddirectory);
    thisformData.append("reference", idreference);
    thisformData.append("dt", documenttype);


    $.ajax({
        url: projectname + '/File/Upload',
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (result) {
            $(me).val('');

            if (typeof (result.statusCode) == 'undefined') {
                $(me).closest(".modal").find(".uploadresponse").html("File Not Uploaded!").css("color", "Red")
            }
            else {
                if (result.statusCode.code == 1)
                    $(me).closest(".modal").find(".uploadresponse").html('File Uploaded').css("color", "Green")
                else {
                    if (result.statusCode.message == "" || result.statusCode.message == null)
                        $(me).closest(".modal").find(".uploadresponse").html("File Not Uploaded!").css("color", "Red")
                    else
                        $(me).closest(".modal").find(".uploadresponse").html(result.statusCode.message).css("color", "Red")
                }
            }

            removebtnloader($(".btnFileUpload"));

            $.each(result.uploadedFiles, function (key, value) {

                if (value.idDocumentType == 1)
                    $(me).closest(".modal").find(".file-list-tab").find("[idDocumenttype='1']").parent().remove();

                $(me).closest(".modal").find(".file-list-tab").append(`
                     <li class="list-group-item clearfix">
                        <a class="text-black-50 font-weight-bold font-size-16" idDocumenttype="` + value.idDocumentType + `"  href="` + value.filePath + `" imgurl="` + value.filePath + `" download style="display:inline"> ` + value.documentType + ` -- ` + value.fileName + `</a>
                        <button idAttachment="`+ value.idAttachment + `" type="button" class="btn btn-danger float-right font-size-11 filedelete" style="border-radius:50%;width: 30px;" onclick= "deletefileatt(this)"><span class="glyphicon glyphicon-remove" ></span> X</button>
                    </li>`)

            });
            //$("#filediv").find('img').attr('src', `/Content/assets/images/salogo.png`)

        },
        error: function (jqXHR, exception) {
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            $('#post').html(msg);
        }
    });

}

function deletefileatt(me) {
    var idAttachment = $(me).attr("idAttachment");

    var req = {
        idAttachment: idAttachment
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/File/Clear",
        data: { req: req },
        success: function (result) {
            $(me).closest(".modal").find(".uploadresponse").html('File Deleted').css("color", "Red")
            $(me).closest("li").remove();
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")

        },
    });

}

function Getfiles(me) {
    var files = $(me)[0].files;
    var formData = new FormData();

    if (files.length > 0) {
        var allowedExtensions = ['gif', 'jpeg', 'jfif', 'png', 'jpg', 'pdf', 'xlsx', 'xls', 'docx', 'doc', 'txt'];
        var valid = true;
        for (var i = 0; i != files.length; i++) {
            var path = files[i].name.split('.');
            var extension = path[path.length - 1]
            if ($.inArray(extension.toLowerCase(), allowedExtensions) < 0)
                valid = false;

            formData.append("files", files[i]);
        }

        if (!valid) {
            //addFileAlert('Not allowed file extension', 'danger')
            //alert('Not allowed file extension')
            removebtnloader($(".btnFileUpload"));
            $(me).closest(".modal").find(".uploadresponse").html('Not allowed file extension').css("color", "red")

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

function triggermultipleupload(me) {
    if ($(me).val() == 1)
        $("#file-upload").removeAttr("multiple")
    else
        $("#file-upload").attr("multiple", "")
}


function validateEmail() {
    var emailRegEx = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    str = document.getElementById("txtContactEmail").value;
    var array = str.split(';');
    for (var i = 0; i < array.length; i++) {
        var email = array[i];
        if (!email.match(emailRegEx)) {
            alert('The email address ' + ' "' + email + '" ' + ' is invalid');
            return false;
        }
    }
    return true
}

function validateForm(divname, exception) {
    var inputValues = [];

    var requiredFields;
    if (exception)
        requiredFields = $(divname + ' :input[required]:not("' + exception + ' :input")');
    else
        requiredFields = $(divname + ' :input[required]');

    requiredFields.each(function () {
        var field = $(this).val();
        inputValues.push({ val: field }); // to return flag valid

        var id = $(this).attr("id");

        if (field == undefined || field == '') {
            $("#" + id).css('border-color', 'red');
            $("#" + id).parent().find(".select2-container").addClass("select2-borderred");
        } else {
            $("#" + id).css('border-color', '#e2e7f1');
            $("#" + id).parent().find(".select2-container").removeClass("select2-borderred");
        }
    });

    if (!validateField(divname, '.hasemail', /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/)) {
        inputValues.push({ val: "" })
    }

    if (!validateField(divname, '.hasnumber', '^[0-9]+(.[0-9]+)?$')) {
        inputValues.push({ val: "" })
    }

    if (!validateField(divname, '.hasdecimal', '^[0-9]+(.[0-9]+)?$')) {
        inputValues.push({ val: "" })
    }

    //if (!validateField(divname, '.hasphone', '^\\+?[0-9]{8,}$')) {
    //    inputValues.push({ val: "" })
    //}

    const valid = inputValues.find(v => v.val == "");
    //console.log(valid)
    return valid;
}



function validateField(divname, classtype, pattern) {
    var valid = true;
    if ($(divname).find(classtype).length !== 0) {
        //var requiredFields = $(divname + ' :not("' + exception + '")' ).find(classtype);
        var requiredFields = $(divname).find(classtype)

        var inputValues = [];

        requiredFields.each(function () {

            var field = $(this).val();
            inputValues.push({ val: field }); // to return flag valid

            var id = $(this).attr("id");
            if (field == undefined || field == '') {
                if ($(this).attr('required')) {
                    $("#" + id).css('border-color', 'red');
                    valid = false;
                }
                else {
                    $("#" + id).css('border-color', '#e2e7f1');
                }
            } else {
                var allowedChar;
                if (classtype != ".hasemail") {
                    allowedChar = new RegExp(pattern);
                    if (!allowedChar.test(field)) {
                        $("#" + id).css('border-color', 'red');
                        valid = false;
                    }
                    else {
                        $("#" + id).css('border-color', '#e2e7f1');
                    }
                }
                else {
                    var allemails = field.split(';');
                    for (var i = 0; i < allemails.length; i++) {
                        var email = allemails[i].trim();
                        if (!email.match(pattern)) {
                            valid = false;
                            $("#" + id).css('border-color', 'red');
                        }
                        else {
                            $("#" + id).css('border-color', '#e2e7f1');
                        }
                    }
                }
            }
        });
    }

    return valid;
}

//function avayapopup() {
//    $("#notifications-bottom-right").html();
//    var bottom_center = ` <div id="notifications-bottom-right-tab" class="animated bounce">
//        <div id="notifications-bottom-right-tab-close" class="close">
//            <span class="iconb" data-icon="&#xe20e;"></span>
//        </div>
//        <div id="notifications-bottom-right-tab-right">
//            <div id="notifications-bottom-right-tab-right-title"><span>New Call</span><br /> Call ID:<br /> 15248</div>
//            <div id="notifications-bottom-right-tab-right-text">
//                Caller Phone Number: 03458914
//<br>
//<br>
//27/07/2021 15:30
//            </div>
//        </div>
//    </div >`;

//    $("#notifications-bottom-right").html(bottom_center);
//    $("#notifications-bottom-right-tab").addClass('animatedbounce');
//    refresh_close();
//}


//function refresh_close() {
//    $('.close').click(function () { $(this).parent().fadeOut(200); });
//}

//refresh_close();

function hideavayapopup() {
    $("#notifications-bottom-right-tab").hide()
}
function showavayapopup() {
    if ($("#notifications-bottom-right #cid").text() != "" /*&& isBlackListed!="True"*/) {
        $("#notifications-bottom-right-tab").show()
    }
}


function getunregcalls(me) {


    $("#avayalist").find(".fa-sync-alt").toggleClass("down");

    $("#unregisteredcalls").html("")
    cancelavayapopup();

    $.ajax({
        type: 'Get',
        url: projectname + "/Home/GetunRegisteredCalls",
        success: function (result) {
            $("#unregisteredcalls").append(result)

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



function triggeravayaselect(avayacid) {

    var selectedcall = $("#unregisteredcalls").find("[bakcid='" + avayacid + "']").closest("p")
    var isBlackList = $(selectedcall).find(".selectnumber").attr("isBlackListed");

    // updated to get the bakcid instead of original cid to cover special duplicate avayaid cases
    $("#notifications-bottom-right #cid").text($(selectedcall).find(".selectnumber").attr("cid"));
    $("#notifications-bottom-right #calldate").text($(selectedcall).find(".selectdate").text());
    $("#notifications-bottom-right #phoneno").text($(selectedcall).find(".selectnumber").text());
    $("#notifications-bottom-right #phonename").text($(selectedcall).find(".selectname").text());

    $("#notifications-bottom-right #phonename").text($(selectedcall).find(".selectname").text());

    $("#unregisteredcalls").find(".flagselected").attr("hidden", "hidden");
    $(selectedcall).find(".flagselected").removeAttr("hidden");



    if (isBlackList == "True") {
        $("#notifications-bottom-right-tab").addClass("blacklisted");
        $("#notifications-bottom-right-tab-right-title span").text("Blacklisted Call")
    }
    else {
        $("#notifications-bottom-right-tab").removeClass("blacklisted");
        $("#notifications-bottom-right-tab-right-title span").text("Active Call")
    }

    $("#notifications-bottom-right-tab").hide().show("slow");

    if ($("#searchform #phoneNo").length > 0) {
        $("#searchform  #phoneNo").val($("#notifications-bottom-right #phoneno").text().trim())
        //resetdatatable("#cases");
    }
    try {
        //getavayainfo();
    } catch (e) {

    }
}



function setactiveavayacall(me) {
    var isBlackListed = "";
    localStorage.removeItem("cid");
    localStorage.setItem("cid", $(me).find(".selectnumber").attr("bakcid"));

    isBlackListed = $(me).find(".selectnumber").attr("isBlackListed")

    $("#notifications-bottom-right #cid").text($(me).find(".selectnumber").attr("cid"));
    $("#notifications-bottom-right #calldate").text($(me).find(".selectdate").text());
    $("#notifications-bottom-right #phoneno").text($(me).find(".selectnumber").text());
    $("#notifications-bottom-right #phonename").text($(me).find(".selectname").text());


    $("#unregisteredcalls").find(".flagselected").attr("hidden", "hidden");
    $(me).find(".flagselected").removeAttr("hidden");

    if (isBlackListed == "True") {
        $("#notifications-bottom-right-tab").addClass("blacklisted");
        $("#notifications-bottom-right-tab-right-title span").text("Blacklisted Call")
    }
    else {
        $("#notifications-bottom-right-tab").removeClass("blacklisted");
        $("#notifications-bottom-right-tab-right-title span").text("Active Call")

    }
    $("#notifications-bottom-right-tab").hide().show("slow");

    if ($("#searchform #phoneNo").length > 0) {
        $("#searchform  #phoneNo").val($("#notifications-bottom-right #phoneno").text().trim())
        resetdatatable("#cases");
    }

    try {
        getavayainfo();
    } catch (e) {

    }

    //if (screentype == 'avaya') {
    //    SearchCases()
    //}
    //else
    //    gotopage('case', '')
}


function resetavayapopup(callid) {
    var avayabox = $("#notifications-bottom-right");
    avayabox.find("#phoneno").text("")
    avayabox.find("#phonename").text("")
    avayabox.find("#cid").text("")

    $("#unregisteredcalls .thiscallselect").each(function () {
        if ($(this).find(".selectnumber").attr('cid') == callid)
            $(this).closest(".thiscallselect").remove()
    });
}


function cancelavayapopup() {
    var avayabox = $("#notifications-bottom-right");
    avayabox.find("#phoneno").text("")
    avayabox.find("#phonename").text("")
    avayabox.find("#cid").text("")

    //$("#unregisteredcalls .thiscallselect").each(function () {
    //    $("#unregisteredcalls").find(".flagselected").attr("hidden", "hidden");
    //});

    localStorage.removeItem("cid");

    hideavayapopup()
}





function checkurlserver() {
    return "";
    var url = window.location.href
    if (url.indexOf("localhost") > -1) {
        var arr = url.split("/");
        return "/" + arr[3]
        //return "";
    }
    else {
        var arr = url.split("/");
        return "/" + arr[3]
    }
}

function ordinal_suffix_of(i) {
    var j = i % 10,
        k = i % 100;
    if (j == 1 && k != 11) {
        return i + "st";
    }
    if (j == 2 && k != 12) {
        return i + "nd";
    }
    if (j == 3 && k != 13) {
        return i + "rd";
    }
    return i + "th";
}



function formatDate_DdMmYyyy(inputDate) {
    // Parse the input date string (assuming it's in a standard format)
    var date = new Date(inputDate);

    // Extract day, month, and year components
    var day = date.getDate();
    var month = date.getMonth() + 1; // Months are zero-based
    var year = date.getFullYear();

    // Ensure two-digit formatting for day and month
    var formattedDay = day < 10 ? '0' + day : day;
    var formattedMonth = month < 10 ? '0' + month : month;

    // Format the date as "dd/mm/yyyy"
    var formattedDate = formattedDay + '/' + formattedMonth + '/' + year;

    return formattedDate;
}





////// country flag full use
//var input = document.querySelector("#testme");
//window.intlTelInput(input, {
//    // allowDropdown: false,
//    // autoHideDialCode: false,
//    autoPlaceholder: "on",
//    // dropdownContainer: document.body,
//    // excludeCountries: ["us"],
//    // formatOnDisplay: false,
//    // geoIpLookup: function(callback) {
//    //   $.get("http://ipinfo.io", function() {}, "jsonp").always(function(resp) {
//    //     var countryCode = (resp && resp.country) ? resp.country : "";
//    //     callback(countryCode);
//    //   });
//    // },
//    // hiddenInput: "full_number",
//    // initialCountry: "auto",
//    // localizedCountries: { 'de': 'Deutschland' },
//    // nationalMode: false,
//    onlyCountries: ['lb', 'gb', 'ch', 'ca', 'do'],
//    // placeholderNumberType: "MOBILE",
//    preferredCountries: ['lb'],
//    // separateDialCode: true,
//    utilsScript: "/countryflags/js/utils.js",
//});
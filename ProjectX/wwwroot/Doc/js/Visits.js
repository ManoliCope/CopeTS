var PatientDetials = [];

$(document).ready(function () {
    $("#btnpayment").on("click", function () {
        togglepayment();
    });

    var ID = $("#patientid").val();
    if (ID) {
        $.ajax({
            type: 'Get',
            url: '/Profile/Getpatientdetails',
            data: {
                "ID": ID
            },
            success: function (result) {
                PatientDetials = result;
                //console.log(PatientDetials, 'get visits')
            }
        })
    }
});

//alert('click visit')


function converdatetostring(thisdate) {
    //thisdate = thisdate.split(' ')[0]
    var output = thisdate.replace(/(\d\d)\/(\d\d)\/(\d{4})/, "$3-$2-$1");
    return output;

    //var input = "03/07/2016";

    //alert(output)

    //var today = new Date(thisdate);
    //var dd = String(today.getDate()).padStart(2, '0');
    //var mm = String(today.getMonth() + 1).padStart(2, '0');
    //var yyyy = today.getFullYear();
    //today = yyyy + '-' + mm + '-' + dd;
    //alert('a')
    //alert(today)
}

function fillvisittab(visitid, me) {
    var patientsvisits = PatientDetials["visits"];

    var thislist = {};
    resetvisitinputs();

    for (g in patientsvisits) {
        if (patientsvisits[g].visitID == visitid)
            thislist = patientsvisits[g];
    }

    //$("#visitdate").val(thislist.to);
    var thisvisitdate = $(me).html().trim()
    //console.log(thislist.to)
    //console.log(thisvisitdate)

    $("#visitdate").val(converdatetostring(thisvisitdate));

    $("#Reason").val(thislist.reason);

    if (thislist.firstVisit)
        $("#newpatient").prop('checked', true);
    else
        $("#newpatient").prop('checked', false);

    if (thislist.surgery == 's')
        $("#surgery").prop('checked', true);
    else
        $("#surgery").prop('checked', false);

    $("#complains").val(thislist.complains);
    $("#findings").val(thislist.findings);
    $("#diognoses").val(thislist.diognoses);
    $("#medications").val(thislist.medications);
    $("#labresults").val(thislist.imaging);
    $("#payment").val(thislist.payment);

    $("#Savevisit").text("Edit");
    $("#Savevisit").attr("onclick", "").unbind("click");
    $('#Savevisit').attr('onclick', `checkvisit(${thislist.patienID},${thislist.visitID})`);
    //console.log(thislist, 'thiislist')

    togglevisitstab();

}
function togglepayment() {
    $('#payment').toggle("hide")
}

function togglevisitstab() {
    $('#visittab')[0].click();
}

function toggleNewvisitstab() {
    var patientid = PatientDetials["patients"].id;
    $('#payment').css("display", "none")
    $("#Savevisit").attr("onclick", "").unbind("click");
    $('#Savevisit').attr('onclick', `checkvisit(${patientid})`);
    $("#Savevisit").text("Add");
    $('#visittab')[0].click();
    resetvisitinputs();
}

function resetvisitinputs() {
    $('#visitcontainer').find('input:checkbox').removeAttr('checked');
    $('#visitcontainer').find('select').val('');
    $('#visitcontainer').find('input').val('');
    $('#visitcontainer').find('textarea').val('');
    $('#visitcontainer').find('input:text').val('');
    $('#payment').css("display", "none")

    $("#visitdate").val(converdatetostring(gettodaydate()));
}

function gettodaydate() {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    today = dd + '/' + mm + '/' + yyyy;
    return today;
}

function checkvisit(PatientID, VisitID) {
    showloader();
    var surgery;
    if ($("#surgery").prop('checked'))
        surgery = 's'
    else
        surgery = 'v'

    var visit = {
        PatienID: PatientID,
        visitid: VisitID,
        from: $("#visitdate").val(),
        to: $("#visitdate").val(),
        Reason: $("#Reason").val(),
        FirstVisit: $("#newpatient").prop('checked'),
        Surgery: surgery,
        Complains: $("#complains").val(),
        findings: $("#findings").val(),
        diognoses: $("#diognoses").val(),
        medications: $("#medications").val(),
        Imaging: $("#labresults").val(),
        Payment: $("#payment").val(),
    }

    var thisformData = new FormData();
    thisformData = GetImages();
    thisformData.append("patientid", visit.PatienID);
    thisformData.append("visitid", visit.visitid);
    thisformData.append("from", visit.from);
    thisformData.append("to", visit.to);
    thisformData.append("Reason", visit.Reason);
    thisformData.append("FirstVisit", visit.FirstVisit);
    thisformData.append("Surgery", visit.Surgery);
    thisformData.append("Complains", visit.Complains);
    thisformData.append("findings", visit.findings);
    thisformData.append("diognoses", visit.diognoses);
    thisformData.append("medications", visit.medications);
    thisformData.append("Imaging", visit.Imaging);
    thisformData.append("Payment", visit.Payment);



    $.ajax({
        url: '/Profile/addpatientfiles',
        data: thisformData,
        processData: false,
        contentType: false,
        type: "POST",
        success: function (result) {
            setTimeout(function () {
                hideloader()
                if (VisitID > 0)
                    showresponsemodal("success", "Visit Updated!")
                else
                    showresponsemodal("success", "Visit Created!")
            }, 1000);


            //console.log(result,"error")
            //window.location.href = "/Profile/GetPatientsID?id=" + PatientID;
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

function openpopup(me) {
    var imagename = $(me).attr("name");
    $("#imagename").text(imagename.split('/').pop())
    $("#popupimage").attr("src", imagename);
    $("#showimageModal").modal();
}



////old add edit
//function checkvisit(PatientID, VisitID) {
//    if (VisitID)
//        editvisit(PatientID, VisitID);
//    else
//        addvisit(PatientID, VisitID);
//}

//function addvisit(PatientID, VisitID) {
//    var visit = {
//        PatienID: PatientID,
//        from: $("#visitdate").val(),
//        to: $("#visitdate").val(),
//        Reason: $("#Reason").val(),
//        FirstVisit: $("#newpatient").prop('checked'),
//        Surgery: $("#surgery").prop('checked'),
//        Complains: $("#complains").val(),
//        Findings: $("#findings").val(),
//        Diognoses: $("#diognoses").val(),
//        medications: $("#medications").val(),
//        Imaging: $("#labresults").val(),
//        Payment: $("#payment").val(),
//    }

//    $.ajax({
//        type: 'Post',
//        url: '/Profile/AddVisitDetails',
//        data: {
//            "Newvisit": visit
//        },
//        success: function (result) {
//            var Patientid = result;
//            window.location.href = "/Profile/GetPatientsID?id=" + PatientID;
//        }
//    });
//}

//function editvisit(PatientID, VisitID) {
//    var visit = {
//        PatienID: PatientID,
//        visitid: VisitID,
//        from: $("#visitdate").val(),
//        to: $("#visitdate").val(),
//        Reason: $("#Reason").val(),
//        FirstVisit: $("#newpatient").prop('checked'),
//        Surgery: $("#surgery").prop('checked'),
//        Complains: $("#complains").val(),
//        findings: $("#findings").val(),
//        diognoses: $("#diognoses").val(),
//        medications: $("#medications").val(),
//        Imaging: $("#labresults").val(),
//        Payment: $("#payment").val(),
//    }

//    var thisformData = new FormData();
//    thisformData = GetImages();
//    thisformData.append("patientid", visit.PatienID);
//    thisformData.append("visitid", visit.visitid);
//    thisformData.append("from", visit.from);
//    thisformData.append("to", visit.to);
//    thisformData.append("Reason", visit.Reason);
//    thisformData.append("FirstVisit", visit.FirstVisit);
//    thisformData.append("Surgery", visit.Surgery);
//    thisformData.append("Complains", visit.Complains);
//    thisformData.append("findings", visit.findings);
//    thisformData.append("diognoses", visit.diognoses);
//    thisformData.append("medications", visit.medications);
//    thisformData.append("Imaging", visit.Imaging);
//    thisformData.append("Payment", visit.Payment);

//    $.ajax({
//        url: '/Profile/addpatientfiles',
//        data: thisformData,
//        processData: false,
//        contentType: false,
//        type: "POST",
//        success: function (result) {
//            alert("done")
//        }
//    });


//    //$.ajax({
//    //    type: 'Post',
//    //    url: '/Profile/UpdateVisitDetails',
//    //    data: {
//    //        visit
//    //    },
//    //    success: function (result) {
//    //        window.location.href = "/Profile/GetPatientsID?id=" + PatientID;
//    //    }
//    //});


//}






function GetImages() {
    var files = $("#file-upload")[0].files;
    var formData = new FormData();

    if (files.length > 0) {
        var allowedExtensions = ['gif', 'jpeg', 'png', 'jpg', 'pdf', 'xlsx', 'xls', 'docx', 'doc', 'txt'];
        var valid = true;
        for (var i = 0; i != files.length; i++) {
            var path = files[i].name.split('.');
            var extension = path[path.length - 1]
            if ($.inArray(extension, allowedExtensions) < 0)
                valid = false;
            formData.append("files", files[i]);
        }

        if (!valid) {
            addFileAlert('Not allowed file extension', 'danger')
            return;
        }
        return formData;
    } else {
        return formData;
    }
}


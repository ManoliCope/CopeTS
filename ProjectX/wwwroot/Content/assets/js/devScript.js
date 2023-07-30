var projectname = '';

function LogintoCMS() {
    //window.location.href = 'Home';
    //return;

    localStorage.removeItem("cid");

    $("#incorrectCredMsg").removeClass("show");
    $("#incorrectCredMsg").addClass("hide");

    var valid = true;
    var username = $("#username").val();
    var password = $("#password").val();

    if (username == "" || username == null) {
        $("#username").addClass("errorinput");
        valid = false;
    }
    if (password == "" || password == null) {
        $("#password").addClass("errorinput");
        valid = false;
    }



    if (valid == true) {
        $("#loginbutton").removeClass("show");
        $("#loginbutton").addClass("hide");
        $("#loginloader").removeClass("hide");
        $("#loginloader").addClass("show");

        var model = {
            username: username,
            password: password,
        }

        $.ajax({
            type: 'POST',
            url: '/login/login',
            data: model,

            success: function (result) {
                setTimeout(function () {
                    if (result.statusCode) {
                        if (result.statusCode.code == 1) {
                            var url_string = window.location.href
                            var url = new URL(url_string);
                            //var cid = url.searchParams.get("cid");
                            //var caseid = url.searchParams.get("csid");
                            //alert(url_string);
                            window.location.href = "/production";
                            
                            //if (cid)
                            //    window.location.href = "/Home?cid=" + cid;
                            //else if (caseid) {
                            //    window.location.href = "/Home?csid=" + caseid;
                            //}
                            //else
                            //    window.location.href = "/Home";

                        }
                        else {
                            $("#incorrectCredMsg").removeClass("hide");
                            $("#incorrectCredMsg").addClass("show");
                            $("#loginbutton").removeClass("hide");
                            $("#loginbutton").addClass("show");
                            $("#loginloader").removeClass("show");
                            $("#loginloader").addClass("hide");
                        }
                    }
                    else {
                        $("#incorrectCredMsg").removeClass("hide");
                        $("#incorrectCredMsg").addClass("show");
                        $("#loginbutton").removeClass("hide");
                        $("#loginbutton").addClass("show");
                        $("#loginloader").removeClass("show");
                        $("#loginloader").addClass("hide");
                    }

                }, 1000);
            }
        });
    }

    return false;

}

//Register Call
function fillCallCategory() {
    $('#callCat').empty();
    var ctype = $('#callregType :selected').val();
    if (ctype == "Travel") {
        $('#callCat').append('<option value="Coverage (Benefits)">Coverage (Benefits)</option>');
        $('#callCat').append('<option value="Zone Of Coverage (Geography)">Zone Of Coverage (Geography)</option>');
        $('#callCat').append('<option value="Documents needed to issue a policy">Documents needed to issue a policy</option>');
        $('#callCat').append('<option value="Pricing">Pricing</option>');
        $("#plate").hide();
    }
    else {
        if (ctype == "Motor") $("#plate").show();
        else $("#plate").hide();
        $('#callCat').append('<option value="Price Or Offer Request">Price Or Offer Request</option>');
        $('#callCat').append('<option value="General Clarification">General Clarification</option>');
        $('#callCat').append('<option value="Policies Conditions">Policies Conditions</option>');
        $('#callCat').append('<option value="Working Hours">Working Hours</option>');
        $('#callCat').append('<option value="Location">Location</option>');
        $('#callCat').append('<option value="Complaint">Complaint</option>');
        $('#callCat').append('<option value="Concierge">Concierge</option>');
    }
    $('#callCat').append('<option value="Others">Others</option>');
}
function crudRegisterCall(PolId, user) {
    var form = document.getElementById('addRegisterCallForm');
    $("#cover-spin").addClass("show");

    //var call = {
    //    Policy_Id: PolId,
    //    CallerName: document.getElementById('validationCustom01').value,//txtCaller.Value,
    //    Tel: document.getElementById('validationCustom03').value,//txtTel.Value,
    //    ClaimStatus: document.getElementById('').Value,//claimStatus.Value,
    //    Operator: user,
    //    //  CallDate = DateTime.Now,
    //    Comment: document.getElementById('comment').value,//txtComment.Value,
    //    CallerID: document.getElementById('validationCustom02').value,//this.txtcallerId.Value,
    //    //CallerMail: document.getElementById('emailid').Value,//mail,
    //    CallType: '3',
    //    category: document.getElementById('callCat').value,//selectcallCategory.Value,
    //    type: document.getElementById('callregType').value,//selectcallType.Value,
    //    CallerMail: document.getElementById('').Value,//email.Value,
    //    status: '1',// document.getElementById('').Value,//selectStatus.Items[selectStatus.SelectedIndex].Text,
    //    //statusStart = DateTime.Now,
    //    SAComments: document.getElementById('saComment').value,//sacmt.Value,
    //    mailto: document.getElementById('emailid').value,//selectmail.Value,
    //    plateNum: document.getElementById('pnumber').value,//plate.Value,
    //    user: user,
    //     if(selectStatus.Items[selectStatus.SelectedIndex].Text == "Closed")  statusEnd = DateTime.Now;
    //    act = 'Add'
    //};

    var call = {
        Policy_Id: PolId,
        CallerName: '',
        Tel: '',
        Operator: '',
        CallDate: '',
        Comment: '',
        CallerID: '',
        //CallType: 3,
        category: '',
        type: '',
        //status: '1',
        SAComments: '',
        mailto: '',
        plateNum: '',//,
        //user: '',
        //act = 'Add'
    };


    $.ajax({
        cache: false,
        type: "POST",
        url: projectname + "RegisterCall/CRUDForm",
        data: JSON.stringify(call),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#cover-spin").removeClass("show");
            swal({
                title: "Success!",
                html: result.responseText,
                type: "success",
                showConfirmButton: false
            })
            setTimeout(function () {
                //goToPage('cob')
            }, 2000);

        },
        failure: function (result) {
            $("#cover-spin").removeClass("show");
            swal({
                title: "Success!",
                html: result.responseText,
                type: "success",
                showConfirmButton: false
            })
            setTimeout(function () {
                //goToPage('cob')
            }, 2000);
        },
        error: function (result) {
            $("#cover-spin").removeClass("show");
            swal({
                title: "Success!",
                html: result.responseText,
                type: "success",
                showConfirmButton: false
            })
            setTimeout(function () {
                //goToPage('cob')
            }, 2000);
        }
    });
    form.classList.add('was-validated');
}




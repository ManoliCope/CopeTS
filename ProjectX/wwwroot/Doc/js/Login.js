

$(document).ready(function () {
    //$("#btnlogin").on("click", function () {
    //   return  checkuser();
    //});

});


function checkuser(me) {

    var val = 0;
    var name = document.getElementById("txtusername").value;
    var pass = document.getElementById("txtPassword").value;

    if (name == '') {
        document.getElementById("txtusername").style.borderColor = 'red';
        val = 1;
    }
    else
        document.getElementById("txtusername").style.borderColor = '#ced4da';

    if (pass == '') {
        document.getElementById("txtPassword").style.borderColor = 'red';
        val = 1;
    }
    else
        document.getElementById("txtPassword").style.borderColor = '#ced4da';


    if (val == 1) { 
        return false;
    }
    else {
        toggleLoaderButton(me, 'show');

        var User = {
            Username: name,
            Password: pass,
        }
        alert('a')
        $.ajax({
            type: 'POST',
            url: '/Login/CheckUser',
            data: User,
            success: function (result) {
                console.log(result);
                if (result == 0) {
                    setTimeout(function () {
                        document.getElementById("txtFailure").innerText = "Username and/or password is incorrect.";
                        toggleLoaderButton(me, 'hide');
                    }, 200);
                }
                else {
                    window.location.href = "../Calendar/Calendar";
                }
            }
        });
        return false;
    }
}



function toggleLoaderButton(me, toggle) {
    if (toggle == 'show') {
        $(me).append(`<div class="loader"></div>`);
        $(me).find('span').hide();
        $(me).addClass('disabled');
    } else {
        $(me).find('.loader').remove();
        $(me).find('span').show();
        $(me).removeClass('disabled');
    }
}


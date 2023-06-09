// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function showresponsemodal(type, msg) {
    //$('#responsemodal').modal({ backdrop: 'static', keyboard: false })
    $("body").find(".modal").modal("hide");
    $(".close").click();

    $('#responsemodal').on('hidden.bs.modal', function () {
        location.reload();
    })

    $("body").find("#AddPatient").css("display","none");

    if (type == "success") {
        $('#responsemodal .responseimage').attr("src", "/images/Green-Round-Tick.png")
        $('#responsemodal h1').text("Success")
    } else {
        $('#responsemodal .responseimage').attr("src", "/images/failureicon.png")
        $('#responsemodal h1').text("Failure")
    }

    $('#responsemodal p').text(msg)
    $('#responsemodal').modal('show');
}

function showloader() {
        $("body").append(`<div class="loading">Loading</div>`);
}

function hideloader() {
        $("body .loading").remove();
}





$(document).ready(function () {
    //showloader()
    //setTimeout(function () {
    //    hideloader()

    //    showresponsemodal("success", "Patient Added!")

    //}, 2000);

   // showloader()

   //showresponsemodal('success', 'Patient Added!')
    //showresponsemodal('error', 'Patient cannot be added!')

        //setTimeout(loader("show")
    //    , 3000);

});
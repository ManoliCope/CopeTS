$(document).ready(function () {


    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0');
    var yyyy = today.getFullYear();
    today = yyyy + '-' + mm + '-' + dd;

    $("#visitdate").val(today)


    $("#patientname").select2({
        placeholder: "Select a state",
        allowClear: true
    });

    paginate();

    //$('#example').DataTable({
    //    "order": [[0, "desc"]]
    //});

    $('#example').DataTable({
        "order": [[3, "desc"]], //or asc 
        "columnDefs": [{ "targets": 3, "type": "date" }],
        "createdRow": function (row, data, dataIndex) {
            if (data[0] == 'True') {
                $(row).addClass('backcolorapp');
            }
          
        }
    });



});


function paginate() {
    //Pagination
    pageSize = 20;
    incremSlide = 5;
    startPage = 0;
    numberPage = 0;

    var pageCount = $(".line-content").length / pageSize;
    var totalSlidepPage = Math.floor(pageCount / incremSlide);

    if (pageCount > 0)
        $("#pagin").append('<li class="page-item"><a class="page-link" href="#" aria-label="Previous"><span aria-hidden="true">«</span></a></li> ');


    for (var i = 0; i < pageCount; i++) {
        $("#pagin").append('<li class="page-item"><a class="page-link" href="#">' + (i + 1) + '</a></li> ');
        if (i > pageSize) {
            $("#pagin li").eq(i).hide();
        }
    }

    if (pageCount > 0)
        $("#pagin").append('<li class="page-item"><a class="page-link" href="#" aria-label="Next"><span aria-hidden="true">»</span> </a></li>');


    var prev = $("<li/>").addClass("prev").html("Prev").click(function () {
        startPage -= 5;
        incremSlide -= 5;
        numberPage--;
        slide();
    });

    prev.hide();

    var next = $("<li/>").addClass("next").html("").click(function () {
        startPage += 5;
        incremSlide += 5;
        numberPage++;
        slide();
    });

    $("#pagin").prepend(prev).append(next);

    $("#pagin li").first().find("a").addClass("current");

    slide = function (sens) {
        $("#pagin li").hide();

        for (t = startPage; t < incremSlide; t++) {
            $("#pagin li").eq(t + 1).show();
        }
        if (startPage == 0) {
            next.show();
            prev.hide();
        } else if (numberPage == totalSlidepPage) {
            next.hide();
            prev.show();
        } else {
            next.show();
            prev.show();
        }


    }

    showPage = function (page) {
        $(".line-content").hide();
        $(".line-content").each(function (n) {
            if (n >= pageSize * (page - 1) && n < pageSize * page)
                $(this).show();
        });
    }

    showPage(1);
    $("#pagin li a").eq(0).addClass("current");

    $("#pagin li a").click(function () {
        $("#pagin li a").removeClass("current");
        $(this).addClass("current");
        showPage(parseInt($(this).text()));
    });
}


function AddPatient() {
    showloader();
    var NewPatients = {
        firstname: $("#firstname").val(),
        middlename: $("#middlename").val(),
        lastname: $("#lastname").val(),
        firstnamear: $("#firstnamear").val(),
        middlenamear: $("#middlenamear").val(),
        lastnamear: $("#lastnamear").val(),
        phone: $("#phone").val(),
        mobile: $("#mobile").val(),
        email: $("#email").val(),
        location: $("#location").val(),
        dob: $("#dob").val(),
        sex: $("#sex").val(),
        allergies: $("#allergies").val(),
        Insurance: $("#insurance option:selected").val(),
        height: $("#height").val(),
        weight: $("#weight").val(),
        chronicdisease: $("#chronicdisease").val()
    }

    $.ajax({
        type: 'Post',
        url: '/Profile/AddPatient',
        data: {
            NewPatients
        },
        success: function (result) {
            var Patientid = result;

            setTimeout(function () {
                hideloader()
                showresponsemodal("success", "Patient Added!")
            }, 1000);


            //window.location.href = "/Profile/GetPatientsID?id=" + Patientid;
        }
    });


}

function editPatient(ID) {
    showloader();
    var Patients = {
        ID: ID,
        firstname: $("#firstname").val(),
        middlename: $("#middlename").val(),
        lastname: $("#lastname").val(),
        firstnamear: $("#firstnamear").val(),
        middlenamear: $("#middlenamear").val(),
        lastnamear: $("#lastnamear").val(),
        phone: $("#phone").val(),
        mobile: $("#mobile").val(),
        email: $("#email").val(),
        location: $("#location").val(),
        dob: $("#dob").val(),
        sex: $("#sex").val(),
        allergies: $("#allergies").val(),
        Insurance: $("#Insurance option:selected").val(),
        height: $("#height").val(),
        weight: $("#weight").val(),
        chronicdisease: $("#chronicdisease").val()
    }


    $.ajax({
        type: 'Post',
        url: '/Profile/editPatient',
        data: {
            Patients
        },
        success: function (result) {
            setTimeout(function () {
                hideloader()
                showresponsemodal("success", "Patient info Updated!")
            }, 1000);

            //window.location.href = "/Profile/GetPatientsID?id=" + ID;
        }
    });


}

function gotopatient(Patientid) {
    window.location.href = "/DProfile/GetPatientsID?id=" + Patientid;
}

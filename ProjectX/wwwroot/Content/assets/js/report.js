
var projectname = checkurlserver();

$(document).ready(function () {

});



$('#generateproduction').click(function () {
    generateproduction();
});

$('#generatebenefits').click(function () {
    generatebenefits();
});
$('#generatebeneficiaries').click(function () {
    generatebeneficiaries();
});
function generatebenefits() {
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateBenefits",
        //data: { req: request },
        success: function (result) {
            console.log(result)
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'BenefitsReport'
                }
                var url = projectname + "/Report/GenerateReport";

                //$.redirect(url, req, "POST", "_blank");
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { req: req },
                    success: function (result) {
                        alert('done')
                    }
                });
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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
function generatebeneficiaries() {
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateBeneficiaries",
       //data: { req: request },
        success: function (result) {
            console.log(result)
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'BeneficiariesReport'
                }
                var url = projectname + "/Report/GenerateReport";

                //$.redirect(url, req, "POST", "_blank");
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { req: req },
                    success: function (result) {
                        alert('done')
                    }
                });
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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
function generateproduction() {
   

    var request = $("#prodId").val();
       
    

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateProduction",
        data: { req: request },
        success: function (result) {
            console.log(result)
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'ProductionReport'
                }
                var url = projectname + "/Report/GenerateReport";
                
                //$.redirect(url, req, "POST", "_blank");
                $.ajax({
                    type: 'POST',
                    url: url,
                    data: { req: req },
                    success: function (result) {
                        alert('done')
                    }
                });
            }
            else {
                showresponsemodal(0, result.statusCode.message)
            }

            removeloader();
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


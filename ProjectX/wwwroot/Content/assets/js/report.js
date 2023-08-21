var projectname = checkurlserver();

$(document).ready(function () {

});

function getprofileclients(me) {

    var profid = $(me).attr("profid");

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateProfileViewClients",
        data: { idProfile: profid },
        success: function (result) {
            if (result.statusCode.code == 1) {
                const myJSON = JSON.stringify(result.reportData);
                var req = {
                    data: myJSON,
                    filename: 'AdherentProfileClients'
                }
                console.log(req);
                var url = reporturl + "/Reports/GenerateReport";
                //    alert(url);
                //$.redirect(url, req);
                //alert(url)
                $.redirect(url, req, "POST", "_blank");
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




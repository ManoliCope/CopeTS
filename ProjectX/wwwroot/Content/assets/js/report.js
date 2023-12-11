
var projectname = checkurlserver();

$(document).ready(function () {
    $('#generateproduction').click(function () {
        generateproduction();
    });

    $('#generatebenefits').click(function () {
        generatebenefits();
    });
    $('#generatebeneficiaries').click(function () {
        generatebeneficiaries();
    });
    $('#generatecurrencies').click(function () {
        generatecurrencies();
    });
});

function generatebenefits() {
    //var datefrom = $("#datefrom").val();
    //var dateto = $("#dateto").val();
    var userid = $("#userid").val();
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateBenefits",
        data: { userid: userid},
        xhrFields: {
            responseType: 'blob'
        },
        success: function (response) {
            var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var blobUrl = URL.createObjectURL(blob);

            var link = document.createElement('a');
            link.href = blobUrl;
            link.download = 'report.xlsx';
            link.click();

            URL.revokeObjectURL(blobUrl);
        },
        error: function (error) {
            console.error("Error generating report:", error);
        }
    });
}

function generateproduction() {
    var prod = {};
     prod['request'] = $("#prodId").val();
     prod['datefrom'] = $("#datefrom").val();
     prod['dateto'] = $("#dateto").val();
     prod['agentId'] = $("#agentId").val();
     prod['subAgentId'] = $("#subAgentId").val();
     prod['policyStatus'] = $("#policyStatus").val();
     prod['policyNumber'] = $("#policyNumber").val();
     prod['clientFirstName'] = $("#clientFirstName").val();
     prod['clientLastName'] = $("#clientLastName").val();
     prod['passportNumber'] = $("#passportNumber").val();
    

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateProduction",
        data: { req: prod },
        xhrFields: {
            responseType: 'blob'
        },
        success: function (response) {
            var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var blobUrl = URL.createObjectURL(blob);

            var link = document.createElement('a');
            link.href = blobUrl;
            link.download = 'report.xlsx';
            link.click();

            URL.revokeObjectURL(blobUrl);
        },
        error: function (error) {
            console.error("Error generating report:", error);
        }
    });


}

function generatecurrencies  () {
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateCurrencies",
        //data: { req: request, datefrom: datefrom, dateto: dateto },
        xhrFields: {
            responseType: 'blob'
        },
        success: function (response) {
            var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var blobUrl = URL.createObjectURL(blob);

            var link = document.createElement('a');
            link.href = blobUrl;
            link.download = 'report.xlsx';
            link.click();

            URL.revokeObjectURL(blobUrl);
        },
        error: function (error) {
            console.error("Error generating report:", error);
        }
    });

}



function generatebeneficiaries() {
    //var request = $("#prodId").val();
    //var datefrom = $("#datefrom").val();
    //var dateto = $("#dateto").val();
    var prod = {};
    prod['request'] = $("#prodId").val();
    prod['agentId'] = $("#agentId").val();
    prod['subAgentId'] = $("#subAgentId").val();
    //prod['policyStatus'] = $("#policyStatus").val();
    //prod['policyNumber'] = $("#policyNumber").val();
    prod['clientFirstName'] = $("#clientFirstName").val();
    prod['clientLastName'] = $("#clientLastName").val();
    prod['passportNumber'] = $("#passportNumber").val();


    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateBeneficiaries",
        data: { req:prod },
        xhrFields: {
            responseType: 'blob'
        },
        success: function (response) {
            var blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });

            var blobUrl = URL.createObjectURL(blob);

            var link = document.createElement('a');
            link.href = blobUrl;
            link.download = 'report.xlsx';
            link.click();

            URL.revokeObjectURL(blobUrl);
        },
        error: function (error) {
            console.error("Error generating report:", error);
        }
    });

}


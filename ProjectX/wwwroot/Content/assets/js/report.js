
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
    var datefrom = $("#datefrom").val();
    var dateto = $("#dateto").val();
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateBenefits",
        data: { datefrom: datefrom, dateto: dateto },
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
    var request = $("#prodId").val();
    var datefrom = $("#datefrom").val();
    var dateto = $("#dateto").val();

    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateProduction",
        data: { req: request,datefrom: datefrom,dateto:dateto },
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

function x  () {
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
    var request = $("#prodId").val();
    var datefrom = $("#datefrom").val();
    var dateto = $("#dateto").val();
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateBeneficiaries",
        data: { req: request, datefrom: datefrom, dateto: dateto },
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



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
    $('#generatetariff').click(function () {
        generatetariff();
    });
    $('#assignedUserId').change(function () {
        var prid = $('#assignedUserId option:selected').attr('prid');
        $('#tproductId option').each(function () {
            if ($(this).val() == prid) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
    });
    $('#tproductId').change(function () {
        var prid = $(this).val();
        $('#assignedUserId option').each(function () {
            if ($(this).attr('prid') == prid) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
        $('#tpackageId option').each(function () {
            if ($(this).attr('prid') == prid) {
                $(this).show();
            } else {
                $(this).hide();
            }
        });
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

function generatecurrencies() {

    var curr = $('#currencyId').val();
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateCurrencies",
        data: { req: curr },
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
function generatetariff() {

    var plan = $('#pplanId').val();
    var package = $('#ppackageId').val();
    var user = $('#assignedUser').val();
    var product = $('#pproductId').val();
    $.ajax({
        type: 'POST',
        url: projectname + "/Report/GenerateTariff",
        data: { planid: plan, packageid: package, assignedid:user,productid:product },
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

function loadChildren() {
    var mainUserDropdown = document.getElementById("agentId");
    var childrenDropdown = document.getElementById("subAgentId");
    var selectedUserId = mainUserDropdown.value;

    // Clear previous options in children dropdown
    childrenDropdown.innerHTML = "<option value=''>Loading...</option>";

    if (selectedUserId) {

        $.ajax({
            type: 'GET',
            url: projectname + "/Report/getChildren",
            data: { userid: selectedUserId },
            success: function (response) {
                childrenDropdown.innerHTML = "<option value=''>Select Sub Agent</option>";

                var children = response.loadedData.subagents;
                children.forEach(child => {
                    var option = document.createElement("option");
                    option.value = child.lK_ID;
                    option.text = child.lK_TableField;
                    childrenDropdown.add(option);
                });
            },
            error: function (error) {
                console.error("Error getting sub agents:", error);
            }
        });
    } else 
        childrenDropdown.innerHTML = "<option value=''>Select Sub Agent</option>";

}



var projectname = checkurlserver();

$(document).ready(function () {
    drawtable();

    
    $('#editAmountBtn').click(function () {
        //resetAmountPopup();
        showresponsemodalbyid('edit-balance')
        
    });
    $('.editbalance').click(function () {
        editAmount(this);
    });
   
});
function editAmount(me) {
    if (validateForm("#edit-balance .container-fluid")) {
        return;
    }
    showloader("load");
    /*var type = $(me).attr("typ");*/
    var type = 0;
    var amount = $('#newAmount').val();
    var userid = $('#userId').val();
    $.ajax({
        type: 'POST',
        url: projectname + "/PrepaidAccounts/EditBalance",
        data: { action: type, amount: amount, userid: userid },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                loadUserPreBalance();
                showresponsemodal(result.statusCode.code, result.statusCode.message);
            }
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
function drawtable(data) {
    console.log(data)
    var table = $('#transactionstable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "", "className": "text-center filter", "orderable": true, "data": "paT_Id", "visible": false},
            //{ "title": "Action", "className": "text-center filter", "orderable": true, "data": "paT_Action" },
            { "title": "Details", "className": "text-center filter", "orderable": true, "data": "paT_Details" },
            { "title": "Amount", "className": "text-center filter", "orderable": true, "data": "paT_ActionName" },
            { "title": "Date", "className": "text-center filter", "orderable": true, "data": "paT_CreationDate" },
           
        ], order: [[0, "desc"]],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "transactionstable")
}


function formatDate(data) {


    var date = new Date(data);
    var day = date.getDate().toString().padStart(2, '0');
    var month = (date.getMonth() + 1).toString().padStart(2, '0');
    var year = date.getFullYear();
    return day + '/' + month + '/' + year;



}

function loadUserPreBalance() {
    showloader("load")
    var userid = $('#userId').val();
    if (userid == undefined || userid == null || userid == '') {
        $('#editAmountBtn').addClass('hidden');
        $('#currentAmount').text('');
        $('#currAmount').text('');
        drawtable({});
        removeloader();

        return;
    }
    $.ajax({
        type: 'POST',
        url: projectname + "/PrepaidAccounts/GetUserBalance",
        data: { userid: userid },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                $('#editAmountBtn').removeClass('hidden');
                $('#currentAmount').text('Current Balance: ' + result.pA_Amount + " USD");
                $('#currAmount').text(result.pA_Amount + " USD");
                $(result.preAccTransactions).each(function (index, transaction) {
                    transaction.paT_CreationDate = formatDate(transaction.paT_CreationDate);

                });
                drawtable(result.preAccTransactions);
            }
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

function resetAmountPopup() {
    $('#newAmount').val('');
}
function validateAccountPopup() {

}
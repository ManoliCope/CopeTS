var projectname = checkurlserver();
var travelinfo = {}
var addbenefits = []
var selectedfieldlist = [];
var editedglobalrow = null
$(document).ready(function () {


    $('.togglebenpopup').click(function () {
        $(".btn-beneficiary").attr("thisid", 0)
        triggerbenbtn()
        showresponsemodalbyid('beneficiary-popup', -1)
    })


    var polIdValue = $(".editscreen").attr("pol-id");
    triggerasdatatable("#beneficiary-table")

    var isadmin = $(".prodadm").attr("prodadm")

    $(".isselect2").select2({
        tokenSeparators: [',', ' ']
    })
    var stat = sessionStorage.getItem('status');
    sessionStorage.removeItem('status');
    if (stat != null && stat > 0) {

    }


    getselectedfields()
    triggercalculationfields()


    $('#destination_id').select2({
        closeOnSelect: false
    });



    populatezones();
    populateproducts();
    populatedestinations()
    //populatebeneficiary()
    settofrom()
    searchbeneficiary()

    $('#printButton').click(function () {
        generatePdf();

    });

    $('.trgrthis').focusout(function () {
        getQuotation()
    });

    $('.thisbeneficiary :input[required]').focusout(function () {

        var selectedtype = document.querySelector('input[name="type"]:checked');
        var typeId = selectedtype ? selectedtype.id : '';

        if (typeId === 'is_individual') {
            getQuotation()
        }
    });


    $('.trgrthis.isselect2').on('select2:close', function () {
        getQuotation();
    });


    $('#date_of_birth').on('focusout', updateAge);

});


function generatePdf() {
    //var xhr = new XMLHttpRequest();
    //xhr.open("POST", "@Url.Action("GeneratePdf", "Pdf")", true);
    //xhr.responseType = "blob";

    //xhr.onload = function () {
    //    if (xhr.status === 200) {
    //        var blob = xhr.response;
    //        var link = document.createElement("a");
    //        link.href = window.URL.createObjectURL(blob);
    //        link.download = "output.pdf";
    //        link.click();
    //    }
    //};

    //xhr.send();


    const htmlContent = "<html><body><h1>Hello, PDF!</h1></body></html>";

    fetch('/Pdf/Get', {
        method: 'Get',
        headers: {
            'Content-Type': 'application/json'
        },
        //body: JSON.stringify(htmlContent)
    })
        .then(response => response.blob())
        .then(blob => {
            const url = URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = 'converted.pdf';
            a.click();
        });


}

function Printpolicy2() {
    const htmlContent = "<html><body><h1>Hello, PDF!</h1></body></html>";

    fetch('/Production/ConvertHtmlToPdf', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(htmlContent)
    })
        .then(response => response.blob())
        .then(blob => {
            const url = URL.createObjectURL(blob);
            const a = document.createElement('a');
            a.href = url;
            a.download = 'converted.pdf';
            a.click();
        });
}






function updateAge() {
    const dateOfBirthInput = $('#date_of_birth');
    const ageText = $('.age');

    if (dateOfBirthInput.val()) {
        const age = calculateAge(dateOfBirthInput.val());
        if (age == null)
            ageText.text('');
        else
            ageText.text("(" + age + ")");
    }
    else {
        ageText.text('');
    }

}


function searchbeneficiary() {

    $('#searchbeneficiary').keyup(function () {
        var query = $(this).val();

        var query = $(this).val();
        if (query.length >= 3) {
            searchben(query);
        } else {
            $('#searchDropdownContent').empty().hide();
        }

    });
    function searchben(query) {
        $.ajax({
            url: projectname + '/Beneficiary/SearchBeneficiaryPref',
            method: 'GET',
            data: { prefix: query },
            success: function (data) {

                //console.log(data.beneficiary)
                var dropdownContent = $('#searchDropdownContent');
                dropdownContent.empty();
                if (data.beneficiary.length > 0) {
                    for (var i = 0; i < data.beneficiary.length; i++) {
                        var bentext = data.beneficiary[i].bE_FirstName + ' ' + data.beneficiary[i].bE_MiddleName + ' ' + data.beneficiary[i].bE_LastName
                        var item = $(`<a thisid=${data.beneficiary[i].bE_Id}>`).text(bentext).attr('href', '#');
                        dropdownContent.append(item);
                    }
                    dropdownContent.show();
                    $('#searchDropdownContent a').off('click').on('click', function () {
                        $('#searchbeneficiary').val("");

                        var beneficiaryId = $(this).attr('thisid');
                        var beneficiary = data.beneficiary.find(function (b) {
                            return b.bE_Id == beneficiaryId;
                        });

                        addtotable(beneficiary, true)

                        $('#searchDropdownContent a').off('click');
                        updateAge()

                        return
                        //console.log(beneficiary)

                        $('#searchDropdownContent a').off('click');
                    });

                } else {
                    var item = $(`<span>`).text("No Benificiary Found!");
                    dropdownContent.append(item);
                    dropdownContent.show();
                    $(document).on('click', '#searchDropdownContent span', function () {
                        dropdownContent.hide();
                    });
                }
            },
            error: function () {
                $('#searchDropdownContent').empty().hide();
            }
        });
    }

    $(document).on('click', '#searchDropdownContent a', function () {
        var selectedValue = $(this).text();
        $('#search').val(selectedValue);
        $('#searchDropdownContent').empty().hide();
    });

    $(document).on('click', function (event) {
        if (!$(event.target).closest('.search-dropdown').length) {
            $('#searchDropdownContent').empty().hide();
        }
    });
}

function populatebeneficiarydatatable(tablename, data) {
    var table = $(tablename).DataTable({
        "data": data,
        "ordering": false,
        "filter": false,
        "paging": false,
        "info": false,
        "destroy": true,
        "columns": [
            {
                "title": "id",
                "data": "bE_Id",
                "className": "dt-center"
            },
            {
                "title": "Gender",
                "data": "bE_Sex",
                "className": "dt-center"
            },

            {
                "title": "First Name",
                "data": "bE_FirstName",
                "className": "dt-center"
            },
            {
                "title": "middle name",
                "data": "bE_MiddleName",
                "className": "dt-center"
            },
            {
                "title": "Last Name",
                "data": "bE_LastName",
                "className": "dt-center"
            },
            {
                "title": "Gender",
                "data": "bE_Sex",
                "className": "dt-center",
                "render": function (data, type, full) {
                    if (type === 'display' || type === 'filter') {
                        return data === 1 ? 'Male' : 'Female';
                    } else {
                        return data;
                    }
                }
            },
            {
                "title": "DOB",
                "data": "bE_DOB",
                "className": "dt-center",
                "render": function (data, type, full) {
                    if (type === 'display' || type === 'filter') {
                        // Format the date as "dd-mm-yyyy"
                        var date = new Date(data);
                        var day = date.getDate().toString().padStart(2, '0');
                        var month = (date.getMonth() + 1).toString().padStart(2, '0'); // Month is zero-based
                        var year = date.getFullYear();

                        return `${day}-${month}-${year}`;
                    }
                }
            },
            {
                "title": "passport number",
                "data": "bE_PassportNumber",
                "className": "dt-center"
            },
            {
                "title": "maiden name",
                "data": "bE_MaidenName",
                "className": "dt-center"
            },

            {
                "title": "nationality id",
                "data": "bE_Nationalityid",
                "className": "dt-center"
            },
            {
                "title": "country residence id",
                "data": "bE_CountryResidenceid",
                "className": "dt-center"
            },
            {
                "title": "Actions",
                "data": null,
                "className": "dt-center",
                "render": function (data, type, full, meta) {
                    var editButton = '<button type="button" thisid="' + full.bE_Id + '" class="btn btn-sm" onclick="editrow(this)"><i class="fas fa-edit" style="color: gray"></i></button>';
                    var deleteButton = '<button type="button" class="btn btn-sm" onclick="removerow(this)"><i class="fas fa-trash" style="color: red"></i></button>';
                    return editButton + ' ' + deleteButton;
                }
            },
        ],
        "columnDefs": [
            {
                "targets": [0, 1, 3, 7, 8, 9, 10],
                "visible": false,
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

}

function triggerasdatatable(tablename) {
    var polid = $(".editscreen").attr("pol-id");
    if (polid > 0) {
        $.ajax({
            url: projectname + '/Production/GetPolicyBeneficiaries',
            method: 'Get',
            data: { id: polid },
            success: function (response) {

                //console.log(response,'table data')
                populatebeneficiarydatatable(tablename, response)
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }
    else {
        populatebeneficiarydatatable(tablename, null)
    }
}

function addtotable(thisrow, search) {

    console.log('on change of product.. dont hide table')
    if (thisrow == undefined) {
        var thissex = 0
        if ($('#male').prop('checked')) {
            thissex = 1;
        } else if ($('#female').prop('checked')) {
            thissex = 2;
        }


        var thisrow =
        {
            "bE_Id": 0,
            "bE_Sex": 1,
            "bE_SexName": "Male",
            "bE_FirstName": $("#beneficiary-popup #first_name").val(),
            "bE_MaidenName": '',
            "bE_MiddleName": $("#beneficiary-popup #middle_name").val(),
            "bE_LastName": $("#beneficiary-popup #last_name").val(),
            "bE_DOB": $("#beneficiary-popup #date_of_birth").val(),
            "bE_PassportNumber": $("#beneficiary-popup #passport_no").val(),
            "bE_Nationalityid": $("#beneficiary-popup #nationality").val(),
            "bE_CountryResidenceid": $("#beneficiary-popup #countryofresidence").val()
        }
        var thistable = $('#beneficiary-table').DataTable();
        thistable.row.add(thisrow).draw();
        closepopup()
    }
    else {
        var thistable = $('#beneficiary-table').DataTable();
        //var allRows = thistable.rows().data()

        if (search) {
            var thistable = $('#beneficiary-table').DataTable();
            thistable.row.add(thisrow).draw();
        }
        else {
            var editedrow =
            {
                "bE_Id": thisrow.id,
                "bE_Sex": thisrow.sex,
                "bE_SexName": thisrow.sexname,
                "bE_FirstName": thisrow.firstName,
                "bE_MaidenName": '',
                "bE_MiddleName": thisrow.middleName,
                "bE_LastName": thisrow.lastName,
                "bE_DOB": thisrow.dateOfBirth,
                "bE_PassportNumber": thisrow.passportNumber,
                "bE_Nationalityid": thisrow.Nationalityid,
                "bE_CountryResidenceid": thisrow.CountryResidenceid
            }
            console.log(editedrow)
            var row = thistable.row("#" + editedrow.bE_Id);
            thistable.row($(editedglobalrow).closest("tr")).data(editedrow).draw();
        }
        editedglobalrow = null;
        closepopup()
    }
    var allRows = thistable.rows().data()
    console.log(allRows)
}
function triggerbenbtn() {
    $('.btn-beneficiary').off('click').on('click', function () {

        //if (validateForm("#beneficiary-popup .container-fluid")) {
        //    return;
        //}

        if ($(this).closest(".modal").attr("actid") && $(this).closest(".modal").attr("actid") > 0) {
            editbeneficiary(this)
        }
        else if ($(this).closest(".modal").attr("actid") && $(this).closest(".modal").attr("actid") == 0) {
            editnewbeneficiary()

        } else {
            addtotable()
        }
        return

        getQuotation()
    });


}

function setDateOfBirthField(originalDate) {
    var dateParts = originalDate.split('T')[0].split(','); // Split the date parts
    var formattedDate = dateParts[0] + '-' + dateParts[2] + '-' + dateParts[1];
    $('#beneficiary-popup #date_of_birth').val(formattedDate);
}
function editrow(me) {
    var benid = $(me).attr("thisid")

    var thistable = $('#beneficiary-table').DataTable();
    var thistr = $(me).closest('tr')
    var rowData = thistable.row(thistr).data();

    $('#beneficiary-popup #first_name').val(rowData.bE_FirstName);
    $('#beneficiary-popup #middle_name').val(rowData.bE_MiddleName);
    $('#beneficiary-popup #last_name').val(rowData.bE_LastName);
    $('#beneficiary-popup #passport_no').val(rowData.bE_PassportNumber);
    if (rowData.bE_DOB)
        $('#beneficiary-popup #date_of_birth').val(rowData.bE_DOB.split('T')[0]);

    $('#beneficiary-popup #nationality').val(rowData.bE_Nationalityid);
    $('#beneficiary-popup #countryofresidence').val(rowData.bE_CountryResidenceid);
    if (rowData.bE_Sex == 1) {
        $('#male').prop('checked', true);
    } else if (rowData.bE_Sex == 2) {
        $('#female').prop('checked', true);
    }

    $(".btn-beneficiary").attr("thisid", benid)
    triggerbenbtn()
    editedglobalrow = me;
    showresponsemodalbyid('beneficiary-popup', benid)

}
function editnewbeneficiary() {

    var thissex = 0
    var thissexname = "male"
    if ($('#male').prop('checked')) {
        thissex = 1;
        thissexname = "Male"
    } else if ($('#female').prop('checked')) {
        thissex = 2;
        thissexname = "Female"
    } else {
    }

    var thisrow =
    {
        "bE_Id": 0,
        "bE_Sex": thissex,
        "bE_SexName": thissexname,
        "bE_FirstName": $("#beneficiary-popup #first_name").val(),
        "bE_MaidenName": '',
        "bE_MiddleName": $("#beneficiary-popup #middle_name").val(),
        "bE_LastName": $("#beneficiary-popup #last_name").val(),
        "bE_DOB": $("#beneficiary-popup #date_of_birth").val(),
        "bE_PassportNumber": $("#beneficiary-popup #passport_no").val(),
        "bE_Nationalityid": $("#beneficiary-popup #nationality").val(),
        "bE_CountryResidenceid": $("#beneficiary-popup #countryofresidence").val()
    }
    var thistable = $('#beneficiary-table').DataTable();
    thistable.row($(editedglobalrow).closest("tr")).data(thisrow).draw();
    var allRows = thistable.rows().data()

    closepopup()

}
function editbeneficiary(me) {
    togglebtnloader(me)

    var thissexname = "male"
    var thissex = 0
    if ($('#male').prop('checked')) {
        thissexname = "Male"
        thissex = 1;
    } else if ($('#female').prop('checked')) {
        thissexname = "Female"
        thissex = 2;
    } else {
    }

    var benid = $(me).closest(".modal").attr("actid")
    var beneficiaryReq = {
        "id": benid,
        "firstName": $("#beneficiary-popup #first_name").val(),
        "middleName": $("#beneficiary-popup #middle_name").val(),
        "lastName": $("#beneficiary-popup #last_name").val(),
        "sex": thissex,
        "sexname": thissexname,
        "passportNumber": $("#beneficiary-popup #passport_no").val(),
        "dateOfBirth": $("#beneficiary-popup #date_of_birth").val(),
        "CountryResidenceid": $("#beneficiary-popup #countryofresidence").val(),
        "Nationalityid": $("#beneficiary-popup #nationality").val(),
    };

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/Beneficiary/EditBeneficiary",
        data: { req: beneficiaryReq },
        success: function (result) {
            addtotable(beneficiaryReq)
            removebtnloader(me)
            removeloader();
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function removerow(me) {
    var table = $("#beneficiary-table").DataTable()

    var row = table.row($(me).closest("tr"));
    row.remove().draw();

    getQuotation()
}
function settofrom() {
    $('#to, #from, #duration').change(function () {
        var toDate = new Date($('#to').val());
        var fromDate = new Date($('#from').val());
        var duration = parseInt($('#duration').val());

        if (this.id === 'from' || this.id === 'to') {
            if (!isNaN(duration) && this.id === 'from') {
                var toDate = new Date(fromDate.getTime() + (duration * 24 * 60 * 60 * 1000));
                var formattedToDate = toDate.toISOString().split('T')[0];
                $('#to').val(formattedToDate);
            }
            else {
                if (fromDate && toDate && fromDate <= toDate) {
                    duration = Math.floor((toDate - fromDate) / (1000 * 60 * 60 * 24));
                    $('#duration').val(duration);
                } else {
                    $('#duration').val('');
                }
            }
        } else if (this.id === 'duration') {
            if (fromDate && !isNaN(duration)) {
                var toDate = new Date(fromDate.getTime() + (duration * 24 * 60 * 60 * 1000));
                if (!isNaN(toDate)) {
                    var formattedToDate = toDate.toISOString().split('T')[0];
                    $('#to').val(formattedToDate);
                } else {
                    $('#to').val('');
                }
            } else {
                $('#to').val('');
            }
        }
    });
}
function populateproducts() {
    $('.typeradio .we-checkbox input[type="radio"]').on('change', function () {
        var addBeneficiaryButton = $('.btn-beneficiary');
        var beneficiaryTable = $('.beneficiary-table');

        var type = 1
        var selectedType = $(this).attr('id');
        if (selectedType == 'is_individual')
            type = 1
        else if (selectedType == 'is_family')
            type = 2
        else
            type = 3

        const table = $('#destinationtbl').DataTable();
        table.clear().draw();

        $('#product_id').empty().append('<option value="">Select Product</option>').val('').trigger('change');
        $('#zone_id').empty().append('<option value="">Select Zone</option>').val('').trigger('change');
        $('#product_id,#zone_id').prop('disabled', true);



        $.ajax({
            url: projectname + '/Production/GetProdutctsByType',
            method: 'POST',
            data: { id: type },
            success: function (response) {
                $.each(response, function (index, product) {
                    $('#product_id').append('<option value="' + product.pR_Id + '">' + product.pR_Title + '</option>');
                });
                $('#product_id,#zone_id').prop('disabled', false);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
}



function populatezones() {
    $('#product_id').on('change', function () {
        var selectedPr = $("#product_id").val();
        $('#zone_id').empty().append('<option value="">Select Zone</option>').val('').trigger('change');
        if (selectedPr == 0)
            return

        $('#zone_id').prop('disabled', true);

        $.ajax({
            url: projectname + '/Production/GetZonesByProduct',
            method: 'POST',
            data: { id: selectedPr },
            success: function (response) {
                $.each(response, function (index, zone) {
                    $('#zone_id').append('<option value="' + zone.z_Id + '">' + zone.z_Title + '</option>');
                });

                $('#zone_id').prop('disabled', false);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
}

function populatedestinations() {
    $('#zone_id').on('change', function () {
        $('#destination_id').empty();
        $('#destination_id').prop('disabled', true);
        $('#destination_id').empty().val('').trigger('change');

        if ($('#zone_id').val() == "")
            return

        $.ajax({
            url: projectname + '/Production/GetDestinationByZone',
            method: 'POST',
            data: { ZoneId: $('#zone_id').val() },
            success: function (response) {
                $.each(response, function (index, dest) {
                    $('#destination_id').append('<option value="' + dest.d_Id + '">' + dest.d_Destination + '</option>');
                });

                $('#destination_id').prop('disabled', false);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    });
}




var sendButton = document.getElementById('sendButton');
if (sendButton) {
    sendButton.addEventListener('click', sendDataIssuance);
}


function createGeneralInformationData() {
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    var generalInfoData = {
        is_family: typeId == 'is_family',
        Is_Individual: typeId == 'is_individual',
        Is_Group: typeId == 'is_group',
        productId: document.getElementById('product_id').value,
        zoneId: document.getElementById('zone_id').value,
    };

    return generalInfoData;
}

function createBeneficiaryData() {
    var beneficiaryList = [];

    var thistable = $('#beneficiary-table').DataTable();
    var thisage = [];

    thistable.rows().every(function (index) {
        var rowData = this.data();

        var beneficiary = {
            Insured: index + 1,
            insuredId: rowData.bE_Id,
            firstName: rowData.bE_FirstName,
            middleName: rowData.bE_MiddleName,
            lastName: rowData.bE_LastName,
            dateOfBirth: rowData.bE_DOB,
            age: calculateAge(rowData.bE_DOB),
            passportNo: rowData.bE_PassportNumber,
            gender: rowData.bE_Sex,
            nationalityid: rowData.bE_Nationalityid,
            countryResidenceid: rowData.bE_CountryResidenceid


        };
        beneficiaryList.push(beneficiary);

    });
    return beneficiaryList;


}


function convertToJSON(data) {
    return JSON.stringify(data);
}

function validatequatation() {
    var inputValues = [];
    var requiredFields = $('.trgrthis');
    requiredFields.each(function () {

        var field = $(this).val();
        inputValues.push({ val: field }); // to return flag valid

        var id = $(this).attr("id");

    });

    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    if (typeId === 'is_family' || typeId === 'is_group') {
        var beneficiaryTable = $('.beneficiary-table');
        if (beneficiaryTable.find('tbody tr').length == 0) {
            inputValues.push({ val: "" });
        }

    } else {
        var requiredenFields = $('.thisbeneficiary :input[required]');
        requiredenFields.each(function () {
            var field = $(this).val();
            inputValues.push({ val: field }); // to return flag valid
        });
    }
    const valid = inputValues.find(v => v.val == "");
    return valid;
}
function getQuotation() {

    if (validatequatation()) {
        $('.quotecontainer').html("<span class='validatemsg'>Please Check Mandatory Fields !</span>");
        return;
    }

    $(".result").addClass("load")

    getQuotationData()

}




function gathertravelinfo() {
    var selectedOptions = $('#destination_id option:selected');
    var selectedDestinations = selectedOptions.map(function () {
        return $(this).text();
    }).get();
    selectedDestinations = selectedDestinations.join(', ');

    var selectedDestinationIds = selectedOptions.map(function () {
        return $(this).val();
    }).get();

    var fromDate = $('#from').val();
    var toDate = $('#to').val();
    var duration = $('#duration').val();


    //// Validate fields
    //if (selectedDestinations.length === 0 || fromDate.trim() === '' ||
    //    toDate.trim() === '' || duration.trim() === '') {
    //    return false; 
    //}

    return {
        "from": fromDate,
        "to": toDate,
        "duration": duration,
        "selectedDestinations": selectedDestinations,
        "selectedDestinationIds": selectedDestinationIds,
    };
}


function calculateAge(dateOfBirth) {
    var today = new Date();
    var birthDate = new Date(dateOfBirth);
    var age = today.getFullYear() - birthDate.getFullYear();
    var monthDiff = today.getMonth() - birthDate.getMonth();
    if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}

function triggercalculationfields() {
    $('.quoatetable').on('change', function () {
        recalculateTotalPrice($(this));
    });

    $('.plans').on('change', function () {
        var selectedOption = $(this).find(':selected');
        var thistable = $(this).closest(".quoatetable")
        //console.log(addbenefits, 'addbenefits')

        var deductible = selectedOption.data('deductible');
        var sportsActivities = selectedOption.data('sportactivitiesfee');
        var price = selectedOption.data('price');

        thistable.find('span[data-dedprice]').text(deductible);
        thistable.find('[data-dedprice]').attr('data-dedprice', deductible);

        thistable.find('span[data-sportsprice]').text(sportsActivities);
        thistable.find('[data-sportsprice]').attr('data-sportsprice', sportsActivities);

        thistable.find('[data-bprice]').text(price);

        var selectedTariffId = $(this).find(':selected').data('tariffid');
        populateBenefits(thistable, selectedTariffId);

        recalculateTotalPrice(thistable)
    });
}

function populateBenefits(thistable, tariffId) {
    thistable.find('.benplus').empty().val('').trigger('change');
    var filteredBenefits = addbenefits.filter(function (item) {
        return item.tariffId === tariffId;
    });

    //console.log(filteredBenefits)
    $.each(filteredBenefits, function (index, benefit) {
        var option = $('<option>').attr('data-benprice', benefit.b_Benfees).attr('value', benefit.b_Id).text(benefit.b_Title + ' ' + benefit.b_Benfees);
        thistable.find('.benplus').append(option);
    });
}

function recalculateTotalPrice(table) {

    var selectedBenefits = table.find('.benplus option:selected');
    var totalAdditionalPrice = 0;

    var className = table.attr("class");


    selectedBenefits.each(function () {
        totalAdditionalPrice += parseFloat($(this).attr('data-benprice'));
    });

    var deductiblePrice = table.find('input[data-dedprice]:checked').length > 0 ? parseFloat(table.find('input[data-dedprice]:checked').attr('data-dedprice')) : 0;
    var sportsPrice = table.find('input[data-sportsprice]:checked').length > 0 ? parseFloat(table.find('input[data-sportsprice]:checked').attr('data-sportsprice')) : 0;

    var basePrice = parseFloat(table.find('.plans').find(':selected').attr('data-price'));
    //var basePrice = parseFloat(table.find('span[data-bprice]').attr('data-bprice'));
    var discount = parseFloat(table.find('#discount').val());

    var finalPrice = (isNaN(basePrice) ? 0 : basePrice) +
        (isNaN(totalAdditionalPrice) ? 0 : totalAdditionalPrice) +
        (isNaN(deductiblePrice) ? 0 : deductiblePrice) + (isNaN(sportsPrice) ? 0 : sportsPrice);


    table.find('span[data-bprice]').text(finalPrice.toFixed(2));

    var finalPricewithdiscount = finalPrice - (isNaN(discount) ? 0 : discount)

    table.find('#finalprice').text(finalPricewithdiscount.toFixed(2));


    var insuredstotal = $('.finalprice');
    var totalinsuredprem = 0;


    insuredstotal.each(function () {
        totalinsuredprem += parseFloat($(this).text());
    });

    $('#initpremtotal').text(totalinsuredprem.toFixed(2) + "$");

    var initialPremium = parseFloat($('#initpremtotal').text());
    var additionalValue = parseFloat($('#additiononprem').text());
    var taxVATValue = parseFloat($('#taxvat').text());
    var stampsValue = parseFloat($('#stamps').text());

    if (isNaN(initialPremium)) initialPremium = 0;
    if (isNaN(additionalValue)) additionalValue = 0;
    if (isNaN(taxVATValue)) taxVATValue = 0;
    if (isNaN(stampsValue)) stampsValue = 0;

    var grandTotal = initialPremium + additionalValue + taxVATValue + stampsValue;
    $('#grandtotal').text(grandTotal.toFixed(2) + "$");
}

function getbeneficiarydetails() {
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';
    if (typeId === 'is_family' || typeId === 'is_group') {
        var beneficiaryRows = $('.beneficiary-table tbody tr');
        beneficiaryRows.each(function (index, row) {
            var cells = $(row).find('td');
            var thisage = $(cells[3]).text(); // Assuming birthdate is in the fourth column

            quotationData.push({
                Insured: index + 1,
                Ages: thisage,
                Product: selectedProduct,
                Zone: selectedZone,
                Durations: selectedDuration,
            })
        });
    }
    else {
        var dateOfBirthInput = document.getElementById('date_of_birth').value;
        var thisage = calculateAge(dateOfBirthInput);
        quotationData.push({
            Insured: 1,
            Ages: thisage,
            Product: selectedProduct,
            Zone: selectedZone,
            Durations: selectedDuration,
        })
    }

}



function getFullNameFromIndex(index) {
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    if (typeId === 'is_family' || typeId === 'is_group') {
        const table = document.querySelector('.beneficiary-table');
        const rows = table.querySelectorAll('tbody tr');

        if (index >= 0 && index < rows.length) {
            const row = rows[index];
            const firstName = row.querySelector('td:first-child').textContent.trim();
            const lastName = row.querySelector('td:nth-child(2)').textContent.trim();
            return `${firstName} ${lastName}`;
        } else {
            return 'Invalid index';
        }
    }
    else
        return `${$('.first_name').val()} ${$('.last_name').val()}`;
}






function getQuotationData() {
    var quotationData = []

    travelinfo = gathertravelinfo();

    var selectedProduct = document.getElementById('product_id').value;
    var selectedDuration = $("#duration").val()
    var selectedZone = document.getElementById('zone_id').value;



    var thistable = $('#beneficiary-table').DataTable();
    var thisage = [];


    console.log(thistable.rows().data(),'quotation')

    thistable.rows().every(function (index) {
        var rowData = this.data();
        var dateOfBirth = calculateAge(rowData.bE_DOB); // Assuming dateOfBirth is a property of your row data
        quotationData.push({
            Insured: index + 1,
            Insuredid: rowData.bE_Id,
            Ages: dateOfBirth,
            Product: selectedProduct,
            Zone: selectedZone,
            Durations: selectedDuration,
        })

        thisage.push(dateOfBirth);
    });


    $.ajax({
        url: projectname + '/Production/GetQuotation',
        method: 'POST',
        data: { quotereq: quotationData },
        success: function (response) {
            //console.log(selectedfieldlist, 'selectedfieldlist')
            //console.log(response, 'quotationData')


            if (response.quotationResp.length > 0) {
                for (var i = 0; i < response.quotationResp.length; i++) {
                    //console.log(response.quotationResp[i], 'quotation result')
                    response.quotationResp[i].fullname = getFullNameFromIndex(response.quotationResp[i].insured - 1);
                }
                addbenefits = response.additionalBenefits
                loadQuotePartialView(response)
            }
            else {
                $('.quotecontainer').html("<span class='validatemsg'>No Result Found !</span>");
                $(".result").removeClass("load")
            }

        },
        error: function (xhr, status, error) {

            alert('big error')
            $(".result").removeClass("load")
            console.log(error);
        }
    });

    return quotationData;
}

function convertDateFormat(inputDate) {
    var parts = inputDate.split('-');
    if (parts.length !== 3) {
        throw new Error('Invalid date format');
    }

    var year = parts[0];
    var month = parts[1];
    var day = parts[2];

    return `${day}-${month}-${year}`;
}
function loadQuotePartialView(response) {
    //console.log(response)

    $.ajax({
        url: projectname + '/Production/GetPartialViewQuotation',
        type: 'POST',
        data: { quotereq: response },
        success: function (data) {
            $('.quotecontainer').html(data);
            $('.quotecontainer .incdate').html(convertDateFormat(travelinfo.from));
            $('.quotecontainer .expdate').html(convertDateFormat(travelinfo.to));
            $('.quotecontainer .duration').html(travelinfo.duration + ' Days');
            $('.quotecontainer .dest').html(travelinfo.selectedDestinations);

            var sendButton = document.getElementById('sendButton');
            if (sendButton) {
                sendButton.addEventListener('click', sendDataIssuance);
            }
            $(".isselect2").select2({
                //tags: true,
                //tokenSeparators: [',', ' '],
            })

            triggercalculationfields()
            setselectedfields()
            $(".result").removeClass("load")

        },
        error: function (error) {
            $(".result").removeClass("load")
            console.error('Error loading partial view:', error);
        }
    });
}


function sendDataIssuance() {
    var BeneficiaryDetails = [];
    var listadditionalbenefits = []
    $(".quoatetable").each(function (index) {
        var insuredSection = $(this);

        var dataForInsured = {
            insured: index + 1,
            //fullName: insuredSection.find(".card-header td:eq(1)").text(),
            tariff: insuredSection.find(".plans option:selected").attr("data-tariffid"),
            plan: insuredSection.find(".plans option:selected").val(),
            deductible: insuredSection.find("input[name='name'][data-dedprice]").prop("checked"),
            deductibleprice: parseFloat(insuredSection.find("input[name='name'][data-dedprice]").data("dedprice")),
            sportsActivities: insuredSection.find("input[name='name'][data-sportsprice]").prop("checked"),
            sportsActivitiesprice: parseFloat(insuredSection.find("input[name='name'][data-sportsprice]").data("sportsprice")),
            discount: isNaN(parseFloat(insuredSection.find(".discount").val())) ? 0 : parseFloat(insuredSection.find(".discount").val()),
            planPrice: parseFloat(insuredSection.find(".planprice").text()),
            finalPrice: parseFloat(insuredSection.find(".finalprice").text())

        };

        // Additional Benefits for the insured
        insuredSection.find(".benplus option:selected").each(function (index) {
            listadditionalbenefits.push({
                insuredid: insuredSection.attr("ins"),
                value: $(this).val(),
                price: $(this).data("benprice")
            });
        });

        BeneficiaryDetails.push(dataForInsured);
    });

    var beneficiaryData = createBeneficiaryData()
    var travelData = gathertravelinfo()
    var GeneralData = createGeneralInformationData()

    var dataToSend = {
        policyId: $(".editscreen").attr("pol-id"),

        beneficiaryDetails: BeneficiaryDetails,
        beneficiaryData: beneficiaryData,
        additionalBenefits: listadditionalbenefits,

        "from": travelData.from,
        "to": travelData.to,
        "duration": travelData.duration,
        "selectedDestinations": travelData.selectedDestinations,
        "selectedDestinationIds": travelData.selectedDestinationIds,

        "is_family": GeneralData.is_family,
        "Is_Individual": GeneralData.Is_Individual,
        "Is_Group": GeneralData.Is_Group,
        "productId": GeneralData.productId,
        "zoneId": GeneralData.zoneId,

        "initialPremium": parseFloat($('#initpremtotal').text()),
        "additionalValue": parseFloat($('#additiononprem').text()),
        "taxVATValue": parseFloat($('#taxvat').text()),
        "stampsValue": parseFloat($('#stamps').text()),
        "grandTotal": parseFloat($('#grandtotal').text()),
    };

    console.log($(".editscreen").attr("pol-id"))

    showscreenloader("load")
    $.ajax({
        url: projectname + '/Production/IssuePolicy',
        data: { IssuanceReq: dataToSend },
        method: 'POST',
        success: function (result) {
            removescreenloader();

            if (result.statusCode.code == 1) {
                if ($(".editscreen").attr("pol-id") == undefined)
                    $("#responsemodal button").click(function () {
                        gotopage("production", "Edit", result.policyID);
                    });
                showresponsemodal("1", result.statusCode.message)
            }
            else {
                showresponsemodal("0", result.statusCode.message)
            }
        },
        error: function (error) {
            removescreenloader()
            showresponsemodal("0", "Error")
            console.error("Error sending data:", error);
        }
    });
}




function showscreenloader() {
    $("#partialscreen .container-fluid").parent().prepend(` <div class="myloader" >
                                <div class="loader" style="background:none !important"></div>
                            </div>`);

    $("#partialscreen .container-fluid").css("opacity", "0.2");
    $('body').css('overflow-y', 'hidden');
}

function removescreenloader() {
    $(".myloader").remove();
    $('body').css('overflow-y', 'auto');
    $("#partialscreen .container-fluid").css("opacity", "unset")
    //$(".modal-backdrop").remove();
}

function getselectedfields() {
    selectedfieldlist = [];
    const mainDiv = $('.quotecontainer');
    mainDiv.find('table.quoatetable').each(function (index) {
        const table = $(this);

        const variableFields = {};
        variableFields['insuredId'] = table.attr('ins');
        variableFields['planId'] = table.find('.form-control.plans option:selected').data('plan');
        variableFields['additionalBenefitsIds'] = table.find('.form-control.isselect2.benplus option[selected]').map(function () {
            return $(this).val();
        }).get();

        variableFields['deductible'] = table.find('input[type="checkbox"][data-dedprice]').prop('checked');
        variableFields['sportsActivities'] = table.find('input[type="checkbox"][data-sportsprice]').prop('checked');

        variableFields['discount'] = parseFloat(table.find('#discount').val());
        selectedfieldlist.push(variableFields);
    });
}


function setselectedfields() {
    selectedfieldlist.forEach((item, index) => {
        const table = $(`.quoatetable[ins="${item.insuredId}"]`);
        table.find('.form-control.plans').val(item.planId);
        const additionalBenefitsSelect = table.find('.form-control.isselect2.benplus');
        additionalBenefitsSelect.val(item.additionalBenefitsIds).trigger('change');
        table.find('input[data-dedprice]').prop('checked', item.deductible);
        table.find('input[data-sportsprice]').prop('checked', item.sportsActivities);
        table.find('#discount').val(item.discount);
        recalculateTotalPrice(table);
    });
}
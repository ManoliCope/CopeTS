var projectname = checkurlserver();

$(document).ready(function () {
    $("#search").click(function () {

        drawtable()
        //Search();
    });
    drawtable();

    $(".isselect2").select2({
        tags: true,
        tokenSeparators: [',', ' ']
    })

    $('#destination_id').select2({
        closeOnSelect: false
    });

    //$("#destinationtbl").DataTable()


    populatezones();
    populateproducts();
    populatedestinations()
    populatebeneficiary()
    settofrom()
    searchbeneficiary()


    $('.add-travel').click(function () {
        var selectedOptions = $('#destination_id option:selected');
        var selectedDestinations = selectedOptions.map(function () {
            return $(this).text();
        }).get();
        var selectedDestinationIds = selectedOptions.map(function () {
            return $(this).val();
        }).get();


        var fromDate = $('#from').val();
        var toDate = $('#to').val();
        var duration = $('#duration').val();


        // Validate fields
        if (selectedDestinations.length === 0 || fromDate.trim() === '' ||
            toDate.trim() === '' || duration.trim() === '') {
            return false; // Prevent adding the row if any field is empty
        }

        if ($.fn.DataTable.isDataTable('#destinationtbl')) {
            $('#destinationtbl').DataTable().destroy();
        }

        var table = $('#destinationtbl').DataTable({
            searching: false,
            paging: false,
            info: false
        });
        table.on('draw', function () {
            table.column(0).nodes().each(function (cell, index) {
                var destinations = $(cell).text().split(',').map(function (destination) {
                    return destination.trim();
                }).join('<br>');

                $(cell).html(destinations);
            });
        });


        table.row.add([
            selectedDestinationIds,
            selectedDestinations.join(','), // Display destination text
            fromDate,
            toDate,
            duration,
            `<i class="fa fa-trash text-danger delete-travel" aria-hidden="true"></i>`
        ]).draw();

        $('#destination_id').val('').trigger('change');
        $('#from').val('');
        $('#to').val('');
        $('#duration').val('');

        $('#destinationtbl').on('click', '.delete-travel', function () {
            table.row($(this).closest('tr')).remove().draw();
        });

        var columnIndex = 0; // Adjust the index if the column position changes
        table.column(columnIndex).visible(false);

        var allRows = $('#destinationtbl').DataTable().rows().data().toArray();

    });

    $('input[name="sgender"]').change(function () {
        var selectedGender = $(this).val();
        var maidenNameField = $('.maiden-field');

        if (selectedGender === 'F') {
            maidenNameField.show(); // Show maiden name field for Female
        } else {
            maidenNameField.hide(); // Hide maiden name field for Male
        }
    });

});

function searchbeneficiary() {
    $('#searchbeneficiary').keyup(function () {
        var query = $(this).val();

        var query = $(this).val();
        if (query.length >= 3) {
            searchben(query);
        } else {
            $('#searchDropdownContent').empty().hide();
        }
        //if (query !== '') {
        //    search(query);
        //} else {
        //    $('#searchResults').empty();
        //}
    });
    function searchben(query) {
        $.ajax({
            url: projectname + '/Beneficiary/SearchBeneficiaryPref',
            method: 'GET',
            data: { prefix: query },
            success: function (data) {
                console.log(data.beneficiary)
                var dropdownContent = $('#searchDropdownContent');
                dropdownContent.empty();
                if (data.beneficiary.length > 0) {
                    for (var i = 0; i < data.beneficiary.length; i++) {
                        var bentext = data.beneficiary[i].bE_FirstName + ' ' + data.beneficiary[i].bE_MiddleName + ' ' + data.beneficiary[i].bE_LastName
                        var item = $(`<a thisid=${data.beneficiary[i].bE_Id}>`).text(bentext).attr('href', '#');
                        dropdownContent.append(item);
                    }
                    dropdownContent.show();
                    $(document).on('click', '#searchDropdownContent a', function () {
                        $('#searchbeneficiary').val("");

                        var beneficiaryId = $(this).attr('thisid');
                        var beneficiary = data.beneficiary.find(function (b) {
                            return b.bE_Id == beneficiaryId;
                        });

                        //console.log(beneficiary)
                        if (beneficiary) {
                            $('.thisbeneficiary .first_name').attr("thisid", beneficiary.bE_Id);
                            $('.thisbeneficiary .first_name').val(beneficiary.bE_FirstName);
                            $('.thisbeneficiary .middle_name').val(beneficiary.bE_MiddleName);
                            $('.thisbeneficiary .maiden_name').val('');
                            $('.thisbeneficiary .last_name').val(beneficiary.bE_LastName);
                            $('.thisbeneficiary .passport_no').val(beneficiary.bE_PassportNumber);
                            //$('.thisbeneficiary .dob').val(beneficiary.bE_DOB);
                            var formattedDate = '';
                            if (beneficiary.bE_DOB) {
                                var dateObj = new Date(beneficiary.bE_DOB);
                                formattedDate = dateObj.toISOString().split('T')[0];
                                $('.thisbeneficiary .dob').val(formattedDate);
                            }
                            if (beneficiary.bE_Sex === 1) {
                                $('#male').prop('checked', true);
                            } else if (beneficiary.bE_Sex === 2) {
                                $('#female').prop('checked', true);
                            }


                        }
                        $('#searchDropdownContent a').off('click');
                    });

                } else {
                    dropdownContent.hide();
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
function populatebeneficiary() {
    $('.btn-beneficiary').click(function () {
        var firstName = $('.first_name').val();
        var lastName = $('.last_name').val();
        var dateOfBirth = $('.dob').val();
        var passportNo = $('.passport_no').val();
        var selectedSexOption = document.querySelector('input[name="sgender"]:checked').value;
        var sexValue;
        if (selectedSexOption === 'M') {
            sexValue = 1;
        } else if (selectedSexOption === 'F') {
            sexValue = 2;
        }


        if (firstName === '' || lastName === '' || dateOfBirth === '' || sexValue === '') {
            return;
        }

        var beneficiaryList = $('.beneficiary-list');
        var row = '<tr>' +
            '<td>' + firstName + '</td>' +
            '<td>' + lastName + '</td>' +
            '<td>' + dateOfBirth + '</td>' +
            '<td>' + passportNo + '</td>' +
            '<td>' + selectedSexOption + '</td>' +
            '<td><button type="button" class="btn btn-sm delete-beneficiary"><i class="fas fa-trash" style="color:red"></i></button></td>' +
            '</tr>';

        beneficiaryList.append(row);

        $('.first_name').val('');
        $('.middle_name').val('');
        $('.last_name').val('');
        $('.dob').val('');
        $('.passport_no').val('');

        var beneficiaryTable = $('#beneficiaryTable').DataTable();
        beneficiaryTable.destroy(); // Destroy the existing DataTable if needed
        beneficiaryTable = $('#beneficiaryTable').DataTable(); // Initialize the DataTable
    });
}
function settofrom() {
    $('#to, #from').change(function () {
        var toDate = new Date($('#to').val());
        var fromDate = new Date($('#from').val());

        if (fromDate && toDate && fromDate <= toDate) {
            var duration = Math.floor((toDate - fromDate) / (1000 * 60 * 60 * 24));
            $('#duration').val(duration);
        } else {
            $('#duration').val('');
        }
    });

    $('#duration').change(function () {
        var fromDate = new Date($('#from').val());
        var duration = parseInt($(this).val());

        if (fromDate && duration) {
            var toDate = new Date(fromDate.getTime() + (duration * 24 * 60 * 60 * 1000));
            var formattedToDate = toDate.toISOString().split('T')[0];
            $('#to').val(formattedToDate);
        } else {
            $('#to').val('');
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

        if (selectedType === 'is_family' || selectedType === 'is_group') {
            addBeneficiaryButton.show(); // Show Add Beneficiary button for Family or Group
            beneficiaryTable.show(); // Show beneficiary table for Family or Group
        } else {
            addBeneficiaryButton.hide(); // Hide Add Beneficiary button for Single
            beneficiaryTable.hide(); // Hide beneficiary table for Single
        }

        //$("#destinationtbl tbody tr").empty();
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

function drawtable(data) {
    //console.log(data)

    var table = $('#productiontable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "Name", "className": "text-center filter", "orderable": true, "data": "name" },
            { "title": "Type", "className": "text-center filter", "orderable": true, "data": "allProfileTypes" },
            { "title": "Phone Number", "className": "text-center filter", "orderable": true, "data": "phoneNumber" },
            {
                'data': 'idProfile',
                className: "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a   title="Edit" profid="` + full.idProfile + `"  class="text-black-50" onclick="gotoprofile(this)"><i class="fas fa-book"/></a>`;
                    //return `<a  href="#" title="Register" class="text-black-50" onclick="gotopage('RegisterCall', 'Index', '` + data + `')"><i class="fas fa-book"/></a>`;
                }
            }
        ],
        orderCellsTop: true,
        fixedHeader: true
    });

    triggerfiltertable(table, "profile")
}


function Search() {
    if (validateForm("#searchform")) {
        return;
    }
    showloader("load")

    var filter = {
        profileName: $("#prname").val().trim(),
        idProfileType: $("#sprtype").val(),
    }

    $.ajax({
        type: 'POST',
        url: projectname + "/Profile/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.profiles);
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

var sendButton = document.getElementById('sendButton');
if (sendButton) {
    sendButton.addEventListener('click', sendData);
}

// Step 1: Add event listener to the button or trigger


// Step 2: Create JavaScript objects representing the data
function createGeneralInformationData() {
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    var generalInfoData = {
        is_family: typeId == 'is_family',
        Is_Individual: typeId == 'is_individual',
        Is_Group: typeId == 'is_group',
        productId: document.getElementById('product_id').value,
        zoneId: document.getElementById('zone_id').value,
        notes: document.getElementById('notes').value
    };

    return generalInfoData;
}

//function createBeneficiaryData() {
//    // Retrieve data from the Beneficiary container
//    var beneficiaryData = {
//        beneficiary: document.getElementById('searchbeneficiary').value,
//        firstName: document.querySelector('.first_name').value,
//        middleName: document.querySelector('.middle_name').value,
//        lastName: document.querySelector('.last_name').value,
//        gender: document.querySelector('input[name="sgender"]:checked').value,
//        passportNo: document.querySelector('.passport_no').value,
//        dateOfBirth: document.getElementById('date_of_birth').value
//        // Include additional fields as needed
//    };

//    return beneficiaryData;
//}


function createBeneficiaryData() {
    var beneficiaryList = [];
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    // Check if the selected type is family or group
    if (typeId === 'is_family' || typeId === 'is_group') {
        var beneficiaryTable = $('.beneficiary-table').DataTable();
        var beneficiaryRows = beneficiaryTable.rows().data();

        // Loop through the beneficiary rows and create beneficiary objects
        beneficiaryRows.each(function (index, row) {
            var firstName = row[0];
            var lastName = row[1];
            var dateOfBirth = row[2];
            var passportNo = row[3];

            // Retrieve the selected sex option
            var selectedSexOption = document.querySelector('input[name="sgender"]:checked').value;

            // Map the selected sex option to the corresponding value
            var sexValue;
            if (selectedSexOption === 'M') {
                sexValue = 1; // Set the appropriate value for male
            } else if (selectedSexOption === 'F') {
                sexValue = 2; // Set the appropriate value for female
            } else {
                sexValue = 0; // Set the appropriate default value
            }

            // Create a beneficiary object
            var beneficiaryData = {
                Id: 0, // Set the appropriate value for the Id property
                Sex: sexValue,
                SexName: '', // Set the appropriate value for the SexName property
                FirstName: firstName,
                MiddleName: '',
                LastName: lastName,
                PassportNumber: passportNo,
                DateOfBirth: dateOfBirth ? new Date(dateOfBirth) : null
            };

            // Add the beneficiary object to the beneficiary list
            beneficiaryList.push(beneficiaryData);

        });
    }
    else {
        // Retrieve the beneficiary data from individual input fields
        var firstNameInput = document.querySelector('.first_name');
        var middleNameInput = document.querySelector('.middle_name');
        var lastNameInput = document.querySelector('.last_name');
        var passportNoInput = document.querySelector('.passport_no');
        var dateOfBirthInput = document.getElementById('date_of_birth');
        var selectedSexOption = document.querySelector('input[name="sgender"]:checked').value;

        var sexValue;
        if (selectedSexOption === 'M') {
            sexValue = 1;
        } else if (selectedSexOption === 'F') {
            sexValue = 2;
        } else {
            sexValue = 0;
        }
        // Create a beneficiary object
        var beneficiaryData = {
            Id: 0, // Set the appropriate value for the Id property
            Sex: sexValue, // Set the appropriate value for the Sex property
            SexName: '', // Set the appropriate value for the SexName property
            FirstName: firstNameInput.value,
            MiddleName: middleNameInput.value,
            LastName: lastNameInput.value,
            PassportNumber: passportNoInput.value,
            DateOfBirth: dateOfBirthInput.value ? new Date(dateOfBirthInput.value) : null
        };

        // Add the beneficiary object to the beneficiary list
        beneficiaryList.push(beneficiaryData);
    }


    console.log(beneficiaryList)
    // Return the beneficiary list
    return beneficiaryList;
}


function createTravelData() {
    var travelList = [];

    var travelTable = document.getElementById('destinationtbl');

    var tbody = travelTable.getElementsByTagName('tbody')[0];
    if (tbody) {
        var travelRows = tbody.getElementsByTagName('tr');

        for (var i = 0; i < travelRows.length; i++) {

            var row = travelRows[i];
            var destination = row.cells[0].textContent;
            var from = row.cells[1].textContent;
            var to = row.cells[2].textContent;
            var duration = row.cells[3].textContent;

            // Create a travel object
            var travelData = {
                Destination: destination,
                From: from,
                To: to,
                Duration: duration
            };

            travelList.push(travelData);
        }
    }
    return travelList;
}

// Step 3: Convert objects to JSON strings
function convertToJSON(data) {
    return JSON.stringify(data);
}

// Step 4: Send the JSON strings to the server using AJAX call
function sendData() {

    $(".result").addClass("load")

    setTimeout(function () {
        $(".result").removeClass("load")
    }, 2000);

    getQuotationData()
    //console.log(getQuotationData())

    return
    var generalInfoData = createGeneralInformationData();

    var beneficiaryData = createBeneficiaryData();
    var travelData = createTravelData();
    console.log(travelData)

    return false;
    var generalInfoJSON = convertToJSON(generalInfoData);
    var beneficiaryJSON = convertToJSON(beneficiaryData);
    var travelJSON = convertToJSON(travelData);

    return;

    var url = 'your-endpoint-url';
    var method = 'POST';

    var xhr = new XMLHttpRequest();
    xhr.open(method, url, true);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onreadystatechange = function () {
        if (xhr.readyState === XMLHttpRequest.DONE && xhr.status === 200) {
            // AJAX call successful
            var response = JSON.parse(xhr.responseText);
            // Handle the response as needed
        }
    };
    xhr.send(JSON.stringify({ generalInfo: generalInfoJSON, beneficiary: beneficiaryJSON, travel: travelJSON }));
}







function getQuotationData() {
    // Retrieve ages of beneficiaries
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

    var ages = [];
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    if (typeId === 'is_family' || typeId === 'is_group') {
        var beneficiaryTable = $('.beneficiary-table').DataTable();
        var beneficiaryRows = beneficiaryTable.rows().data();

        beneficiaryRows.each(function (index, row) {
            var dateOfBirth = row[2];
            ages.push(calculateAge(dateOfBirth));
        });
    }
    else {
        var dateOfBirthInput = document.getElementById('date_of_birth').value;
        ages.push(calculateAge(dateOfBirthInput));
    }
    console.log(ages)

    // Retrieve selected product
    var selectedProduct = document.getElementById('product_id').value;

    // Retrieve selected zone
    var selectedZone = document.getElementById('zone_id').value;

    // Retrieve durations in the travel section
    var travelList = createTravelData();
    var durations = travelList.map(function (travel) {
        return travel.Duration;
    });

    // Construct the quotation data object
    //var quotationData = {
    //    Ages: [10, 28],
    //    Product: 682,
    //    //Product: selectedProduct,
    //    Zone: 270,
    //    //Zone: selectedZone,
    //    Durations: [25],
    //};


    var quotationData =
        [
            {
                Insured: 1,
                Ages: 10,
                Product: 682,
                //Product: selectedProduct,
                Zone: 270,
                //Zone: selectedZone,
                Durations: [25]

            },
            {
                Insured: 2,
                Ages: 28,
                Product: 682,
                //Product: selectedProduct,
                Zone: 270,
                //Zone: selectedZone,
                Durations: [25]

            },
            {
                Insured: 3,
                Ages: 28,
                Product: 682,
                //Product: selectedProduct,
                Zone: 270,
                //Zone: selectedZone,
                Durations: [25]

            },
        ]
    //{
    //    Ages: [10, 28],
    //        Product: 682,
    //            //Product: selectedProduct,
    //            Zone: 270,
    //                //Zone: selectedZone,
    //                Durations: [25],
    //};


    $.ajax({
        url: projectname + '/Production/GetQuotation',
        method: 'POST',
        data: { quotereq: quotationData },
        success: function (response) {
            console.log(response,'before partial')
            loadQuotePartialView(response)


        },
        error: function (xhr, status, error) {
            alert('big error')
            console.log(error);
        }
    });

    return quotationData;
}


function loadQuotePartialView(response) {
    $.ajax({
        url: projectname + '/Production/GetPartialViewQuotation',
        type: 'POST',
        data: { quotereq: response },
        success: function (data) {
            console.log(data,'respong partial')
            $('.quotecontainer').html(data);
        },
        error: function (error) {
            console.error('Error loading partial view:', error);
        }
    });
}

var thival = [
    {
        "t_Id": 26,
        "p_Id": 20,
        "p_Name": null,
        "t_Start_Age": 1,
        "t_End_Age": 80,
        "t_Number_Of_Days": 50,
        "t_Price_Amount": 25,
        "t_Net_Premium_Amount": 25,
        "t_PA_Amount": 25,
        "t_Tariff_Starting_Date": "2023-01-31T00:00:00",
        "t_Override_Amount": 25,
        "pL_Id": 301,
        "pL_Name": null,
        "age": 10,
        "duration": 25,
        "statusCode": {
            "code": 0,
            "message": null,
            "idLanguage": "1"
        }
    },
    {
        "t_Id": 26,
        "p_Id": 20,
        "p_Name": null,
        "t_Start_Age": 1,
        "t_End_Age": 80,
        "t_Number_Of_Days": 50,
        "t_Price_Amount": 25,
        "t_Net_Premium_Amount": 25,
        "t_PA_Amount": 25,
        "t_Tariff_Starting_Date": "2023-01-31T00:00:00",
        "t_Override_Amount": 25,
        "pL_Id": 301,
        "pL_Name": null,
        "age": 28,
        "duration": 25,
        "statusCode": {
            "code": 0,
            "message": null,
            "idLanguage": "1"
        }
    },
    {
        "t_Id": 24,
        "p_Id": 21,
        "p_Name": null,
        "t_Start_Age": 22,
        "t_End_Age": 33,
        "t_Number_Of_Days": 44,
        "t_Price_Amount": 55,
        "t_Net_Premium_Amount": 45,
        "t_PA_Amount": 66,
        "t_Tariff_Starting_Date": "2020-12-31T00:00:00",
        "t_Override_Amount": 777777,
        "pL_Id": 302,
        "pL_Name": null,
        "age": 28,
        "duration": 25,
        "statusCode": {
            "code": 0,
            "message": null,
            "idLanguage": "1"
        }
    }
]
function populateQuotationCards() {
    const quotecontainer = document.querySelector('.quotecontainer');

    // Loop through the Response array and generate quotation cards
    for (const quotation of thival) {
        const card = document.createElement('div');
        card.classList.add('card', 'mt-2');
        card.innerHTML = `
                    <div class="card-body" style="font-size: 13px;">
                        <div>
                            <b>Destination(s)</b>
                            <span class="float-end">
                                Aruba
                            </span>
                        </div>
                        <div>
                            <b>Inception Date</b>
                            <span class="float-end">${quotation.t_Tariff_Starting_Date}</span>
                        </div>
                        <div>
                            <b>Expiry Date</b>
                            <span class="float-end">08-07-2023</span>
                        </div>
                        <div>
                            <b>Duration</b>
                            <span class="float-end">${quotation.duration} Days</span>
                        </div>
                        <div>
                            <span class="float-end total_price" data-currency="USD"></span>
                        </div>
                        <table class="table table-sm table-bordered mt-3">
                            <thead>
                                <tr class="thead-light text-center">
                                    <th>Full Name</th>
                                    <th>Cons. / Cumul. Days</th>
                                    <th>Base Price</th>
                                    <th>Discount</th>
                                    <th>Final Price</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr class="text-center">
                                    <td>${calculateFullName(quotation.age)}</td>
                                    <td>
                                        <select class="form-control1 cum_con_days" name="plans_price[1][]">
                                            <option value="${quotation.t_Id}" data-bprice="${quotation.t_Price_Amount}" 
                                            data-fprice="${quotation.t_Net_Premium_Amount}" data-price0="${quotation.t_PA_Amount}" 
                                            data-deductible="false">${quotation.p_Name}</option>
                                        </select>
                                    </td>
                                    <td class="base_price" data-price="${quotation.t_Price_Amount}">
                                        ${quotation.t_Price_Amount} USD
                                    </td>
                                    <td class="discount" data-discount="0">
                                        0 %
                                        <input type="hidden" class="discount" name="plans_discount[0][]" value="0">
                                    </td>
                                    <td>
                                        <span class="final_price" data-price="${quotation.t_Net_Premium_Amount}">
                                            ${quotation.t_Net_Premium_Amount} USD
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                `;

        quotecontainer.appendChild(card);
    }
}
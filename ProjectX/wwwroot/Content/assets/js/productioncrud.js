var projectname = checkurlserver();
var travelinfo = {}
var addbenefits = []
$(document).ready(function () {
    var polIdValue = $(".editscreen").attr("pol-id");
    var isadmin = $(".prodadm").attr("prodadm")

    $(".isselect2").select2({
        tokenSeparators: [',', ' ']
    })
    var stat = sessionStorage.getItem('status');
    sessionStorage.removeItem('status');
    if (stat != null && stat > 0) {

    }
    //$('#product_id').on('select2:selecting', function (e) {
    //    // Get the entered text in the search box
    //    const enteredText = $('.select2-search__field').val();

    //    // Check if there's no option containing the entered text
    //    if ($('#product_id').find("option:contains('" + enteredText + "')").length === 0) {
    //        e.preventDefault(); // Prevent the selection
    //        alert('Option not found!'); // Show an alert or any other indication
    //    }
    //});

    triggercalculationfields()


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

    $('#printButton').click(function () {

        Printpolicy();


    });

    //$('.add-travel').click(function () {
    //    var selectedOptions = $('#destination_id option:selected');
    //    var selectedDestinations = selectedOptions.map(function () {
    //        return $(this).text();
    //    }).get();
    //    var selectedDestinationIds = selectedOptions.map(function () {
    //        return $(this).val();
    //    }).get();


    //    var fromDate = $('#from').val();
    //    var toDate = $('#to').val();
    //    var duration = $('#duration').val();
    //    alert(fromDate)

    //    // Validate fields
    //    if (selectedDestinations.length === 0 || fromDate.trim() === '' ||
    //        toDate.trim() === '' || duration.trim() === '') {
    //        return false; // Prevent adding the row if any field is empty
    //    }

    //    if ($.fn.DataTable.isDataTable('#destinationtbl')) {
    //        $('#destinationtbl').DataTable().destroy();
    //    }

    //    var table = $('#destinationtbl').DataTable({
    //        searching: false,
    //        paging: false,
    //        info: false
    //    });
    //    table.on('draw', function () {
    //        table.column(0).nodes().each(function (cell, index) {
    //            var destinations = $(cell).text().split(',').map(function (destination) {
    //                return destination.trim();
    //            }).join('<br>');

    //            $(cell).html(destinations);
    //        });
    //    });


    //    table.row.add([
    //        selectedDestinationIds,
    //        selectedDestinations.join(','), // Display destination text
    //        fromDate,
    //        toDate,
    //        duration,
    //        `<i class="fa fa-trash text-danger delete-travel" aria-hidden="true"></i>`
    //    ]).draw();

    //    $('#destination_id').val('').trigger('change');
    //    $('#from').val('');
    //    $('#to').val('');
    //    $('#duration').val('');

    //    $('#destinationtbl').on('click', '.delete-travel', function () {
    //        table.row($(this).closest('tr')).remove().draw();
    //    });

    //    var columnIndex = 0; // Adjust the index if the column position changes
    //    table.column(columnIndex).visible(false);

    //    var allRows = $('#destinationtbl').DataTable().rows().data().toArray();

    //});

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

    //$('.thisbeneficiary :input[required]').change(function () {
    //    showresponsemodal("0", "Modifying beneficiary information will result in creation of a new beneficiary!!")
    //});

    $('.trgrthis.isselect2').on('select2:close', function () {
        getQuotation();
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

    $('#date_of_birth').on('focusout', updateAge);



});


function Printpolicy() {
    const requestData = {
        PolicyID: 43
    };

    fetch('/Production/GeneratePdf', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
        //body: JSON.stringify(requestData),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.blob();
        })
        .then(blob => {
            const url = URL.createObjectURL(blob);

            const link = document.createElement('a');
            link.href = url;
            link.download = 'RazorPdf.pdf';
            link.click();

            URL.revokeObjectURL(url);
        })
        .catch(error => {
            console.error('Error:', error);
        });
}






// Function to update the age text on date change
function updateAge() {
    //function calculateAge(date) {
    //    const birthDate = new Date(date);
    //    const now = new Date();
    //    let age = now.getFullYear() - birthDate.getFullYear();
    //    const monthDiff = now.getMonth() - birthDate.getMonth();
    //    if (monthDiff < 0 || (monthDiff === 0 && now.getDate() < birthDate.getDate())) {
    //        age--;
    //    }
    //    if (age < 0) {
    //        return 0;
    //    }
    //    return age;
    //}
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
                            updateAge()

                        }
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
function populatebeneficiary() {
    $('.btn-beneficiary').click(function () {
        if (validateForm(".thisbeneficiary")) {
            return;
        }

        var firstName = $('.first_name').val();
        var lastName = $('.last_name').val();
        var dateOfBirth = formatDate_DdMmYyyy($('.dob').val());

        var passportNo = $('.passport_no').val();
        var selectedSexOption = document.querySelector('input[name="sgender"]:checked').value;
        var sexValue;
        if (selectedSexOption === 'M') {
            sexValue = 1;
        } else if (selectedSexOption === 'F') {
            sexValue = 2;
        }


        if (firstName === '' || lastName === '' || dateOfBirth === '' || sexValue === '' || passportNo === '') {
            return;
        }
        var age = $('.age').text().replace(/\(|\)/g, '');

        var beneficiaryList = $('.beneficiary-list');
        var row = '<tr>' +
            '<td insuredId=' + $('.first_name').attr("thisid") + ' >' + firstName + '</td>' +
            '<td>' + lastName + '</td>' +
            '<td>' + dateOfBirth + '</td>' +
            '<td>' + age + '</td>' +
            '<td>' + passportNo + '</td>' +
            '<td>' + selectedSexOption + '</td>' +
            '<td><button type="button" class="btn btn-sm" onclick="removerow(this)"><i class="fas fa-trash" style="color:red"></i></button></td>' +
            '</tr>';

        beneficiaryList.append(row);

        $('.first_name').removeAttr("thisid").val('');

        $('.middle_name').val('');
        $('.last_name').val('');
        $('.dob').val('');
        $('.passport_no').val('');
        $('.age').text('');
        //var beneficiaryTable = $('#beneficiaryTable').DataTable();
        //beneficiaryTable.destroy(); // Destroy the existing DataTable if needed
        //beneficiaryTable = $('#beneficiaryTable').DataTable(); // Initialize the DataTable

        getQuotation()

        //createBeneficiaryData()
    });
}

function removerow(me) {
    $(me).closest("tr").remove();
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

    //$('#duration').change(function () {
    //    var fromDate = new Date($('#from').val());
    //    var duration = parseInt($(this).val());

    //    if (fromDate && duration) {
    //        var toDate = new Date(fromDate.getTime() + (duration * 24 * 60 * 60 * 1000));
    //        var formattedToDate = toDate.toISOString().split('T')[0];
    //        $('#to').val(formattedToDate);
    //    } else {
    //        $('#to').val('');
    //    }
    //});
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




var sendButton = document.getElementById('sendButton');
if (sendButton) {
    sendButton.addEventListener('click', sendDataIssuance);
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
    };

    return generalInfoData;
}

function createBeneficiaryData() {
    var beneficiaryList = [];
    var selectedtype = document.querySelector('input[name="type"]:checked');
    var typeId = selectedtype ? selectedtype.id : '';

    if (typeId === 'is_family' || typeId === 'is_group') {
        $('.beneficiary-list tr').each(function (index, row) {
            var thisinsuredId = $(row).find('td:eq(0)').attr('insuredid');
            thisinsuredId = thisinsuredId !== undefined ? thisinsuredId : 0;

            var beneficiary = {
                Insured: index + 1,
                insuredId: thisinsuredId,
                firstName: $(row).find('td:eq(0)').text(),
                lastName: $(row).find('td:eq(1)').text(),
                dateOfBirth: $(row).find('td:eq(2)').text(),
                age: parseInt($(row).find('td:eq(3)').text()),
                passportNo: $(row).find('td:eq(4)').text(),
                gender: $(row).find('td:eq(5)').text()
            };
            beneficiaryList.push(beneficiary);
        });
    }
    else {
        var thisinsuredId = $('#first_name').attr('thisid');
        thisinsuredId = thisinsuredId != "undefined" ? thisinsuredId : 0;

        var beneficiary = {
            Insured: 1,
            insuredId: thisinsuredId,
            firstName: $('#first_name').val(),
            middleName: $('.middle_name').val(),
            lastName: $('#last_name').val(),
            dateOfBirth: $('#date_of_birth').val(),
            age: calculateAge($('#date_of_birth').val()),
            passportNo: $('#passport_no').val(),
            gender: $('input[name="sgender"]:checked').val(),
        };
        beneficiaryList.push(beneficiary);
    }

    return beneficiaryList;
}


// Step 3: Convert objects to JSON strings
function convertToJSON(data) {
    return JSON.stringify(data);
}

// Step 4: Send the JSON strings to the server using AJAX call
function validatequatation() {
    var inputValues = [];
    var requiredFields = $('.trgrthis');
    requiredFields.each(function () {

        var field = $(this).val();
        inputValues.push({ val: field }); // to return flag valid

        var id = $(this).attr("id");

        //if (field == undefined || field == '') {
        //    $(this).css('border-color', 'red');
        //    $(this).parent().find(".select2-container").addClass("select2-borderred");
        //} else {
        //    $(this).css('border-color', '#e2e7f1');
        //    $(this).parent().find(".select2-container").removeClass("select2-borderred");
        //}
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
    //console.log(getQuotationData())


    ///  saving policy if quotation accepted..
    var generalInfoData = createGeneralInformationData();
    var beneficiaryData = createBeneficiaryData();
    var travelData = gathertravelinfo();


    //console.log(generalInfoData)
    //console.log(beneficiaryData)
    //console.log(travelData)


    return false;
    var generalInfoJSON = convertToJSON(generalInfoData);
    var beneficiaryJSON = convertToJSON(beneficiaryData);
    var travelJSON = convertToJSON(travelData);



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


    // Validate fields
    if (selectedDestinations.length === 0 || fromDate.trim() === '' ||
        toDate.trim() === '' || duration.trim() === '') {
        return false; // Prevent adding the row if any field is empty
    }

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
    //$('.benplus').on('change', recalculateTotalPrice);
    //$('input[data-dedprice]').on('change', recalculateTotalPrice);
    //$('input[data-sportsprice]').on('change', recalculateTotalPrice);
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
    console.log(grandTotal)
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
    //console.log('this', travelinfo)
    // Retrieve ages of beneficiaries

    // Retrieve durations in the travel section
    //var travelList = createTravelData();
    //var durations = travelList.map(function (travel) {
    //    return travel.Duration;
    //});



    var selectedProduct = document.getElementById('product_id').value;
    var selectedDuration = $("#duration").val()
    var selectedZone = document.getElementById('zone_id').value;




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


    //var quotationData =
    //    [
    //        {
    //            Insured: 1,
    //            Ages: 10,
    //            Product: 682,
    //            Zone: 270,
    //            Durations: [25]
    //        },
    //        {
    //            Insured: 2,
    //            Ages: 28,
    //            Product: 682,
    //            Zone: 270,
    //            Durations: [5]
    //        },
    //        {
    //            Insured: 3,
    //            Ages: 10,
    //            Product: 682,
    //            Zone: 270,
    //            Durations: [5]
    //        },
    //    ]



    $.ajax({
        url: projectname + '/Production/GetQuotation',
        method: 'POST',
        data: { quotereq: quotationData },
        success: function (response) {
            console.log(response, 'quotationData')


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


function loadQuotePartialView(response) {
    //console.log(response)

    $.ajax({
        url: projectname + '/Production/GetPartialViewQuotation',
        type: 'POST',
        data: { quotereq: response },
        success: function (data) {

            $('.quotecontainer').html(data);
            $('.quotecontainer .incdate').html(travelinfo.from);
            $('.quotecontainer .expdate').html(travelinfo.to);
            $('.quotecontainer .duration').html(travelinfo.duration + ' days');
            $('.quotecontainer .dest').html(travelinfo.selectedDestinations);

            var sendButton = document.getElementById('sendButton');
            if (sendButton) {
                sendButton.addEventListener('click', sendDataIssuance);
            }
            $(".isselect2").select2({
                //tags: true,
                tokenSeparators: [',', ' '],
            })

            triggercalculationfields()
            $(".result").removeClass("load")
            setTimeout(function () {
            }, 2000);

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


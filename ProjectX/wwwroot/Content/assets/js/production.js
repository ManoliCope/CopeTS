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


    populateproducts();
    populatedestinations()
    populatebeneficiary()
    settofrom()


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

function populatebeneficiary() {
    $('.btn-beneficiary').click(function () {
        var firstName = $('.first_name').val();
        var lastName = $('.last_name').val();
        var dateOfBirth = $('.dob').val();
        var passportNo = $('.passport_no').val();

        if (firstName === '' || lastName === '' || dateOfBirth === '' ) {
            return; 
        }

        var beneficiaryList = $('.beneficiary-list');
        var row = '<tr>' +
            '<td>' + firstName + '</td>' +
            '<td>' + lastName + '</td>' +
            '<td>' + dateOfBirth + '</td>' +
            '<td>' + passportNo + '</td>' +
            '<td><button type="button" class="btn btn-sm delete-beneficiary"><i class="fas fa-trash"></i></button></td>' +
            '</tr>';

        beneficiaryList.append(row);

        // Clear the input fields
        $('.first_name').val('');
        $('.middle_name').val('');
        $('.last_name').val('');
        $('.dob').val('');
        $('.passport_no').val('');

        // Initialize or update the DataTable
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

        $('#product_id').empty().append('<option value="">Select Product</option>').val('').trigger('change');
        $('#product_id').prop('disabled', true);
        $.ajax({
            url: projectname + '/Production/GetProdutctsByType',
            method: 'POST',
            data: { id: type },
            success: function (response) {
                $.each(response, function (index, product) {
                    $('#product_id').append('<option value="' + product.pR_Id + '">' + product.pR_Title + '</option>');
                });

                $('#product_id').prop('disabled', false);
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

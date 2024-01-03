var projectname = checkurlserver();

$(document).ready(function () {
    drawtable();

    if ($("#search").length > 0)
        Search()

    $("#search").click(function () {
        Search();
    });
    $(".resetdiv").click(function () {
        var divname = $(this).closest(".card-body").attr("id")
        resetAllValues(divname);
        resetdatatable("#zonetable");
    });
    $("#create").click(function () {
        addnew();
    });
    $("#edit").click(function () {
        edit();
    });
    $("#btndelete").click(function () {
        showresponsemodalbyid('confirm-email-approval', $("#divinfo").attr("mid"))
    });
    $("#confirmdeletebtn").click(function () {
        deletezne(this);
    });
    triggerdesitnationlist()
});


function triggerme() {

}


function triggerdesitnationlist() {
    // Add click event handler to expandable arrows
    $('ul.tree-list > li.expandable > span.arrow').click(function () {
        $(this).parent('li').toggleClass('collapsed');
        $(this).siblings('ul').slideToggle();
    });

    // Prevent click event propagation for deeper levels
    $('ul.tree-list > li > ul > li').click(function (e) {
        e.stopPropagation();
    });

    // Handle parent checkbox click event
    $('input.parent-checkbox').click(function (e) {
        var isChecked = $(this).prop('checked');
        $(this).closest('li').find('input[type="checkbox"]').prop('checked', isChecked);
    });


    var continentCheckboxes = document.querySelectorAll('.parent-checkbox');
    for (var i = 0; i < continentCheckboxes.length; i++) {
        var continentCheckbox = continentCheckboxes[i];
        var destinationCheckboxes = continentCheckbox.parentNode.nextElementSibling.querySelectorAll('input[type="checkbox"]');
        var allChecked = true;
        for (var j = 0; j < destinationCheckboxes.length; j++) {
            if (!destinationCheckboxes[j].checked) {
                allChecked = false;
                break;
            }
        }
        continentCheckbox.checked = allChecked;
    }

}



function drawtable(data) {
    console.log(data)
    var table = $('#zonetable').DataTable({
        "data": data,
        "paging": true,
        "ordering": true,
        "filter": true,
        "destroy": true,
        "columns": [
            { "title": "ID", "className": "text-center filter", "orderable": true, "data": "z_Id" },
            { "title": "Title", "className": "text-center filter", "orderable": true, "data": "z_Title" },
            //{
            //    "title": "Destination ID",
            //    "className": "text-center filter",
            //    "orderable": true,
            //    "data": "z_Destination_Id",
            //    "render": function (data, type, row) {
            //        console.log(data)
            //        return data.join(", ");
            //    }
            //},
            //{
            //    "title": "Destination",
            //    "className": "text-center filter",
            //    "orderable": true,
            //    "data": "Destination",
            //    "render": function (data) {
            //        return data.join(", ");
            //    }
            //},
            {
                "data": 'id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full) {
                    return `<a  title="Edit" zneid="` + full.z_Id + `" class="text-black-50" onclick="gotozne(this)"><i class="fas fa-edit pr-1"></i></a>`;
                }
            },
            {
                "data": 'id',
                "className": "dt-center editor-edit",
                "render": function (data, type, full, meta) {
                    return `<a title="Delete" zneid="` + full.z_Id + `" class="text-black-50" onclick="showresponsemodalbyid('confirm-email-approval',${full.z_Id},${meta.row})"><i class="fas fa-times red"></i></a>`;
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
        "id": $("#id").val(),
        "title": $("#title").val(),
        "destinationId": [],
        "destination": []
    };

    //$("#destinationId option:selected").each(function () {
    //    filter.destinationId.push($(this).val());
    //});

    //$("#destination option:selected").each(function () {
    //    filter.destination.push($(this).val());
    //});




    $.ajax({
        type: 'POST',
        url: projectname + "/zone/Search",
        data: { req: filter },
        success: function (result) {
            removeloader();

            if (result.statusCode.code != 1)
                showresponsemodal("error", result.statusCode.message)
            else {
                drawtable(result.zone);
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

function addnew() {
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")

    var zneReq = {
        "id": $("#id").val(),
        "title": $("#Z_Title").val(),
        "destinationId": []
    };
    var selectedCheckboxes = $.map($('.tree-list input[type="checkbox"]:checked'), function (checkbox) {
        if (checkbox.value !== '0') {
            return checkbox.value;
        }
    });

    zneReq.destinationId = selectedCheckboxes;
    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/zone/Createzone",
        data: { req: zneReq },
        success: function (result) {
            removeloader();

            showresponsemodal(result.statusCode.code, result.statusCode.message)
            $("#responsemodal button").click(function () {
                if (result.statusCode.code == 1 && result.id != "0")
                    gotopage("zone", "Edit", result.id);
            });
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}


function checkContinent(checkbox, continentId) {
    console.log(continentId)
    var continentCheckbox = document.getElementById('checkbox_' + continentId);
    var destinationCheckboxes = checkbox.parentNode.parentNode.parentNode.querySelectorAll('ul input[type="checkbox"]');


    //var destinationCheckboxes = document.querySelectorAll('#checkbox_' + continentId + ' ~ ul input[type="checkbox"]');
    var allChecked = true;
    console.log(destinationCheckboxes.length)
    for (var i = 0; i < destinationCheckboxes.length; i++) {

        if (!destinationCheckboxes[i].checked) {
            allChecked = false;
            break;
        }
    }
    continentCheckbox.checked = allChecked;
}
function edit() {
    if (validateForm(".container-fluid")) {
        return;
    }

    showloader("load")

    var zneReq = {
        "id": $("#divinfo").attr("mid"),
        "title": $("#Z_Title").val(),
        "destinationId": []
    };

    var selectedValues = $.map($('.tree-list input[type="checkbox"]:checked'), function (checkbox) {
        if (checkbox.value !== '0') {
            return checkbox.value;
        }
    });



    zneReq.destinationId = selectedValues;



    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/zone/Editzone",
        data: { req: zneReq },
        success: function (result) {

            removeloader();
            //if (result.statusCode.code == 1 && profile.IdProfile == "0")
            //    gotopage("Profile", "Index");
            showresponsemodal(1, result.statusCode.message)
        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
        }
    });
}

function deletezne(me) {
    if (validateForm(".container-fluid")) {
        return;
    }
    togglebtnloader(me)
    var thisid = $(me).closest("#confirm-email-approval").attr("actid")
    var rowindex = $(me).closest("#confirm-email-approval").attr("trindex")

    $.ajax({
        type: 'post',
        dataType: 'json',
        url: projectname + "/zone/Deletezone",
        data: { id: thisid },
        success: function (result) {

            if (result.statusCode.code == 1) {
                if ($('#zonetable').length > 0) {
                    var myTable = $('#zonetable').DataTable();
                    myTable.row(rowindex).remove().draw();
                    removebtnloader(me);
                    showresponsemodal(result.statusCode.code, result.statusCode.message)
                }
                else {
                    showresponsemodal(result.statusCode.code, result.statusCode.message, "zone")
                    removebtnloader(me);
                }

            }
            else {
                showresponsemodal(result.statusCode.code, result.statusCode.message)
                removebtnloader(me);
            }

        },
        failure: function (data, success, failure) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        },
        error: function (data) {
            showresponsemodal("Error", "Bad Request")
            removebtnloader(me);
        }
    });
}
function gotozne(me) {
    showloader("load")
    window.location.href = "/zone/edit/" + $(me).attr("zneid");
    removeloader();
    return
}

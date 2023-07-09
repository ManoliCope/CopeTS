var projectname = checkurlserver();

$(document).ready(function () {
    $("#search").click(function () {
        var data 
            
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
});

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

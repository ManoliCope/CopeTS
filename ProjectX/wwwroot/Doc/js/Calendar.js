var allevents = [];

function savevisit(patientid, patientname, start, end, allDay, Reason, firstvisit, surgery) {
    showloader();

    var NewVisit = {
        PatienID: patientid,
        PatientName: patientname,
        From: start,
        To: end,
        Reason: Reason,
        FirstVisit: firstvisit,
        Surgery: surgery
    }

    //console.log(NewVisit, 'visit')

    $.ajax({
        type: 'Post',
        url: '/Profile/AddVisit',
        data: {
            NewVisit
        },

        success: function (result) {
            //alert('success')

            setTimeout(function () {
                hideloader()
                var classvisit;
                if (NewVisit.Surgery == "s")
                    classvisit = "success";
                else
                    classvisit = "info";

                var Patientid = result;
                $('#calendar').fullCalendar('renderEvent',
                    {
                        title: patientname,
                        start: start,
                        end: end,
                        allDay: false,
                        //className: 'success',
                        className: classvisit,
                        id: Patientid
                    },
                    true
                );
                $('#calendar').fullCalendar('unselect');
                $(".closeme").click();

            }, 1000);
        },
        error: function (jqXHR, exception) {
            //alert('error')
            var msg = '';
            if (jqXHR.status === 0) {
                msg = 'Not connect.\n Verify Network.';
            } else if (jqXHR.status == 404) {
                msg = 'Requested page not found. [404]';
            } else if (jqXHR.status == 500) {
                msg = 'Internal Server Error [500].';
            } else if (exception === 'parsererror') {
                msg = 'Requested JSON parse failed.';
            } else if (exception === 'timeout') {
                msg = 'Time out error.';
            } else if (exception === 'abort') {
                msg = 'Ajax request aborted.';
            } else {
                msg = 'Uncaught Error.\n' + jqXHR.responseText;
            }
            alert(msg)
        }
    });
}

function triggerdropupdate(event, dayDelta, minuteDelta) {
    var eventid = event.id;
    $.ajax({
        type: 'Post',
        url: '/Profile/UpdateVisit',
        data: {
            eventid, dayDelta, minuteDelta
        },
        success: function (result) {
        }
    });

}

function getallvisits(date, d, m, y) {
    var testallevents = [
        {
            title: 'All Day Event',
            start: new Date(y, m, 1)
        },
        {
            id: 999,
            title: 'Repeating Event new',
            start: new Date(y, m, d - 3, 16, 0),
            allDay: false,
            className: 'info'
        },
        {
            id: 999,
            title: 'Repeating Event',
            start: new Date(y, m, d + 4, 16, 0),
            allDay: false,
            className: 'info'
        },

        {
            title: 'Lunch',
            start: new Date(y, m, d, 12, 0),
            end: new Date(y, m, d, 14, 0),
            allDay: false,
            className: 'important'
        },
        {
            title: 'Birthday Party',
            start: new Date(y, m, d + 1, 19, 0),
            end: new Date(y, m, d + 1, 22, 30),
            allDay: false,
        },
        {
            title: 'Click for Google',
            start: new Date(y, m, 28),
            end: new Date(y, m, 29),
            url: 'http://google.com/',
            className: 'success'
        },
        {
            title: 'new date',
            start: '2021-05-05 00:00:00.000',
            end: '2017-08-28 00:00:00.000',
            //url: 'http://google.com/',
            className: 'success'
        },
        {
            title: 'IM HERE',
            start: 'Tue May 02 2021 00:00:00 GMT+0300 (Eastern European Summer Time)',
            allDay: false,
            //end: 'Tue May 12 2021 00:00:00 GMT+0300 (Eastern European Summer Time)',
            //url: 'http://google.com/',
            className: 'info'
        },
        {
            title: 'Meeting herexxx',
            start: new Date('Sun May 02 2021 10:30:00 GMT+0300 (Eastern European Summer Time)'),
            allDay: false,
            className: 'important'
        },

    ]


    $.ajax({
        type: 'Get',
        url: '/DProfile/getallvisits',
        success: function (result) {
            result.forEach(function (element) {
                if (element.surgery == "s")
                    element.className = "success";
                else if (element.surgery == "a")
                    element.className = "info";
                else
                    element.className = "important";

                element.url = '/Profile/GetPatientsID?id=' + element.PatientID;
                element.start = new Date(element.start);
                element.end = new Date(element.end);
                element.allDay = false;

            });

            drawcalendar(result);
            // drawcalendar(testallevents);
        }
    });

}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    return [year, month, day].join('-');
}

function formatDateandtime(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2)
        month = '0' + month;
    if (day.length < 2)
        day = '0' + day;

    var hr = date.getHours();
    if (hr < 10) {
        hr = "0" + hr;
    }
    var min = date.getMinutes();
    if (min < 10) {
        min = "0" + min;
    }

    var time = hr + ":" + min

    //gettimefromdate();
    //alert('add time to date beforea dding to sql')
    var mydate = [year, month, day].join('-');
    var finaldate = [mydate, time].join(' ')

    return finaldate;


}

function gettimefromdate(date) {

    var hr = date.getHours();
    if (hr < 10) {
        hr = "0" + hr;
    }
    var min = date.getMinutes();
    if (min < 10) {
        min = "0" + min;
    }

    var time = hr + ":" + min
    return time;
}

function drawcalendar(allevents) {
    var calendartype;
    if (window.innerWidth < 500)
        calendartype = 'agendaDay';
    else
        calendartype = 'month';

    var calendar = $('#calendar').fullCalendar({
        header: {
            left: 'title',
            center: 'agendaDay,agendaWeek,month',
            right: 'prev,next today'
        },
        editable: true,
        firstDay: 1, //  1(Monday) this can be changed to 0(Sunday) for the USA system
        selectable: true,
        defaultView: calendartype,// month //agendaWeek //agendaDay 

        axisFormat: 'h:mm',
        columnFormat: {
            month: 'ddd',    // Mon
            week: 'ddd d', // Mon 7
            day: 'dddd M/d',  // Monday 9/7
            agendaDay: 'dddd d'
        },
        titleFormat: {
            month: 'MMMM yyyy', // September 2009
            week: "MMMM yyyy", // September 2009
            day: 'MMMM yyyy'                  // Tuesday, Sep 8, 2009
        },
        allDaySlot: false,
        selectHelper: true,
        select: function (start, end, allDay) {
            $("#triggerpopup").click(function () {
                $("#fromdate").val(start);
                $("#todate").val(end);
                $("#visitdate").val(formatDate(start))

                //alert('here')

                if (start.getHours() == 0)
                    $("#visittime").val("09:00")
                else
                    $("#visittime").val(gettimefromdate(start))

            });

            $('#triggerpopup').click();

            $(".closeme").click(function () {
                $('#calendar').fullCalendar('unselect')
            });

            $("#Saveevent").unbind("click");
            $("#Saveevent").bind("click", function () {
                //var patient = $("#patientname").val();
                var patientid, patientname;

                var firstvisit = $("#newpatient").prop('checked');
                var surgery = $("#surgery").prop('checked');
                if (surgery)
                    surgery = 's'
                else
                    surgery = 'a'

                //alert(surgery)

                var Reason = $("#Reason").val();

                if (firstvisit) {
                    patientname = $("#newpatientname").val();
                }
                else {
                    patientname = $("#patientname option:selected").text();
                    patientid = $("#patientname").val();
                }

                var mydate = $("#visitdate").val() + " " + $("#visittime").val()

                //alert($("#visitdate").val())
                //alert('send date and time to sql and fix there without js conversion')

                var startdate = new Date(mydate);
                startdate = formatDateandtime(startdate)

                // set visit enddate 15mins ahead of visit date
                var enddate = new Date(startdate);
                enddate.setMinutes(enddate.getMinutes() + 30);
                enddate = formatDateandtime(enddate)

                //alert(patientname)

                if (patientname) {
                    savevisit(patientid, patientname, startdate, enddate, allDay, Reason, firstvisit, surgery);
                }
            });

        },
        droppable: true, // this allows things to be dropped onto the calendar !!!
        drop: function (date, allDay) { // this function is called when something is dropped

            // retrieve the dropped element's stored Event Object
            var originalEventObject = $(this).data('eventObject');

            // we need to copy it, so that multiple events don't have a reference to the same object
            var copiedEventObject = $.extend({}, originalEventObject);

            // assign it the date that was reported
            copiedEventObject.start = date;
            copiedEventObject.allDay = allDay;

            // render the event on the calendar
            // the last `true` argument determines if the event "sticks" (http://arshaw.com/fullcalendar/docs/event_rendering/renderEvent/)
            $('#calendar').fullCalendar('renderEvent', copiedEventObject, true);

            // is the "remove after drop" checkbox checked?
            if ($('#drop-remove').is(':checked')) {
                // if so, remove the element from the "Draggable Events" list
                $(this).remove();
            }

        },

        events: allevents,
    });
}

//alert('check calendar day week month with original event types')

$('#newpatient').change(function () {
    togglepatient();
});

function togglepatient() {
    //alert($("#newpatient").checked)

    if ($("#newpatient").prop('checked')) {
        $(".oldpatientdiv").hide();
        $(".newpatientdiv").show();
    }
    else {
        $(".oldpatientdiv").show();
        $(".newpatientdiv").hide();
    }
}

$(document).ready(function () {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    togglepatient();
    getallvisits(date, d, m, y)

    /*  className colors

   className: default(transparent), important(red), chill(pink), success(green), info(blue)

   */


    /* initialize the external events
    -----------------------------------------------------------------*/

    $('#external-events div.external-event').each(function () {
        // create an Event Object (http://arshaw.com/fullcalendar/docs/event_data/Event_Object/)
        // it doesn't need to have a start or end
        var eventObject = {
            title: $.trim($(this).text()) // use the element's text as the event title
        };

        // store the Event Object in the DOM element so we can get to it later
        $(this).data('eventObject', eventObject);

        // make the event draggable using jQuery UI
        $(this).draggable({
            zIndex: 999,
            revert: true,      // will cause the event to go back to its
            revertDuration: 0  //  original position after the drag
        });

    });


    /* initialize the calendar
    -----------------------------------------------------------------*/




});

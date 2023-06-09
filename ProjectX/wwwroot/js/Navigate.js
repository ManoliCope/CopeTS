
function gotowebform(PageName, parameter) {
    showloader();
    $.ajax({
        type: 'GET',
        url: "/Navigate/index?pagename=" + PageName + "&parameter=" + parameter,
        success: function (result) {
            togglemenucolor(PageName + parameter)
            $('#partialscreen .content-page').html("").html(result).scrollTop(0)

            //setTimeout(function () {
            //removeloader();
            //}, 6000);
        },
        error: function (request, status, error) {
            removeloader();
            window.location.href = "/login";

            //showresponsemodal("error", "Bad Request!")
        }
    });
    //window.location.href = "Navigate?pagename=" + PageName;
}


function togglemenucolor() {
    var path = window.location.pathname;
    var abc = path.split("/");
    var controller = abc[2];
    var action = abc[3] || "";

    var PageName = controller + action

    $(".metismenu").find('li').removeClass("mm-active");
    $(".metismenu").find('a').removeAttr("aria-expanded").removeClass("selectedpage").css('color', '#4b4b5a');
    //$(".metismenu").find(`[pg='${PageName}']`).css('color', 'red')
    $(".metismenu").find(`[pg='${PageName}']`).addClass("selectedpage")
    $(".metismenu").find(`[pg='${PageName}']`).closest("ul").closest("li").find("a").first().css('color', '#5369f8')
}

function gotopage(PageName, action, parameter) {
    showloader();
    if (!action)
        action = '';
    //alert("/" + PageName + "/" + action)

    var link;
    if(parameter)
        link = PageName + "/" + action + "?id=" + parameter;
    else
        link = PageName + "/" + action;


    removeloader();
    $('#partialscreen .content-page').css("display", "block")

    window.location.href = "/" + link;


    //$.ajax({
    //    type: 'GET',
    //    url: "/" + link,
    //    success: function (result) {
          
    //        togglemenucolor(PageName + action)

    //        $('#partialscreen .content-page').html("").html(result)

    //        //setTimeout(function () {
    //        removeloader();
    //        $('#partialscreen .content-page').css("display", "block")
    //        $(".scrollpartialscreen").scrollTop(0)

    //        //}, 500);
    //    },
    //    error: function (request, status, error) {
    //        removeloader();

    //        window.location.href = "/login";

    //        //showresponsemodal("error", "Bad Request!")
    //    }
    //});
    //window.location.href = "Navigate?pagename=" + PageName;
}


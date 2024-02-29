var mainContent;
var titleContent;

$(function () {
    mainContent = $("#MainContent"); /// render partial views.
    titleContent = $("title"); // render titles.
});

var routingApp = $.sammy("#MainContent", function () {
    this.get("#/Home/Index", function (context) {
        titleContent.html("Student Page");
        $.get("/Home/Index", function (data) {
            context.$element().html(data);
        });
    });


    this.get("#/Home/About", function (context) {
        titleContent.html("About");
        $.get("/Home/About", function (data) {
            context.$element().html(data);
        });
    });

    this.get("#/Home/Contact", function (context) {
        titleContent.html("Contact");
        $.get("/Home/Contact", function (data) {
            context.$element().html(data);
        });
    });

    this.get("#/Account/Login", function (context) {
        titleContent.html("Contact");
        $.get("/Account/Login", function (data) {
            context.$element().html(data);
        });
    });

});

$(function () {
    routingApp.run("#/Home/Index"); // default routing page.
});

function IfLinkNotExist(type, path) {
    if (!(type != null && path != null))
        return false;

    var isExist = true;

    if (type.toLowerCase() == "get") {
        if (routingApp.routes.get != undefined) {
            $.map(routingApp.routes.get, function (item) {
                if (item.path.toString().replace("/#", "#").replace(/\\/g, '').replace("$/", "").indexOf(path) >= 0) {
                    isExist = false;
                }
            });
        }
    } else if (type.toLowerCase() == "post") {
        if (routingApp.routes.post != undefined) {
            $.map(routingApp.routes.post, function (item) {
                if (item.path.toString().replace("/#", "#").replace(/\\/g, '').replace("$/", "").indexOf(path) >= 0) {
                    isExist = false;
                }
            });
        }
    }
    return isExist;
}
//var app = $.sammy('#main', function () {
//    this.get('#/Home', function (context) {
//        context.$element().load('/Home/Index');
//    });

//    this.get('#/About', function (context) {
//        context.$element().load('/Home/About');
//    });

//    // Add more routes as needed
//});

//$(function () {
//    app.run('#/Home');
//});
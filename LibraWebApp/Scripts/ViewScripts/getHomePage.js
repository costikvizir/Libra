//function goToHomePage() {
//    debugger;
//    $.ajax({
//        url: "/Home/Index",
//        data: {
//        },
//        xhrFields: {
//            withCredentials: true
//        },
//        method: "GET",
//        success: function (response) {
//            $("#mainDiv").html(null);
//            $("#mainDiv").html(response);
//        },
//    });
//}

//function goToHomePage() {
//    $.ajax({
//        url: "/Home/Index",
//        method: "GET",
//        success: function (response) {
//            $("#mainDiv").html(response);
//            // Push state to the history
//            history.pushState({ page: "HomePage" }, "Home", "Index");
//        },
//    });
//}
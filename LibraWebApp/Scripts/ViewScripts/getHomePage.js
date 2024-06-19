function goToHomePage() {
    debugger;
    $.ajax({
        url: "/Home/Dashboard",
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            // $("#mainDiv").html(null);
            $("#mainDiv").html(response);
            history.pushState({ page: "Dashboard" }, "Home Page", "/Home/Dashboard");
        },
    });
}
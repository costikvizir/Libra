function goToAddPos() {
    $.ajax({
        url: "/Pos/AddPos",
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            $("#mainDiv").html(null);
            $("#mainDiv").html(response);
            history.pushState({ page: "AddPos" }, "Add Pos", "/Pos/AddPos");
        },
    });
}
$('#content').on('click', '#clearFormBtn', function () {
    $('#AddPosForm')[0].reset(); // Reset form fields
});

$(document).on('submit', '#AddPosForm', function (event) {
    debugger;
    event.preventDefault();

    $.ajax({
        url: $(this).attr('action'),
        type: $(this).attr('method'),
        data: $(this).serialize(),
        success: function (response) {
            $('#mainDiv').html(response);
            console.log("success add pos");
            console.log(response.success);
            // Call handleUserAddSuccess only if the response indicates success

            if (response.success) {
                handlePosAddSuccess();
            } else {
                // console.error('Error in response: ', response);
                console.log("Inside error add pos");
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
            console.log("Outside error add pos");
        }
    });
});

function handlePosAddSuccess() {
    debugger;
    alert('Pos added successfully');
    goToAllPos();
    initializePosList();
}

function resetForm() {
    var form = document.getElementById("AddPosForm");
    form.reset();
}
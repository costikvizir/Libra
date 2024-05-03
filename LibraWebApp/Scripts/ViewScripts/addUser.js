


function goToAddUser() {
	debugger;
	$.ajax({
		url: "/User/AddUser",
		data: {
		},
		xhrFields: {
			withCredentials: true
		},
		method: "GET",
		success: function (response) {
			$("#mainContainer").html(null);
			$("#mainContainer").html(response);
		},
	});
}

$(document).ready(function () {
    $('#AddUserForm').submit(function (event) {
        event.preventDefault(); // Prevent the form from submitting normally

        // Serialize form data
        var formData = $(this).serialize();

        // AJAX request to add user
        $.ajax({
            url: "/User/AddUser",
            type: "POST",
            data: formData,
            success: function (response) {
                // If the user is successfully added, insert the response (partial view) into the desired container
                $("#mainContainer").html(null);
                $('#mainContainer').html(response);
                initializeUserList(); // Initialize user list after loading the partial view
            },
            error: function (xhr, status, error) {
                // Handle errors if needed
                console.error(error);
            }
        });
    });
});

//function goToAllUsersAndInitializeList() {
//    debugger;
//    $.ajax({
//        url: "/User/GetAllUsers",
//        data: {},
//        xhrFields: {
//            withCredentials: true
//        },
//        method: "GET",
//        success: function (response) {
//            $("#mainContainer").html(null);
//            $("#mainContainer").html(response);
//            initializeUserList(); // Initialize user list after loading the "All Users" view
//        },
//    });
//}


//function goToAllUsers() {
//	debugger;
//	$.ajax({
//		url: "/User/GetAllUsers",
//		data: {
//		},
//		xhrFields: {
//			withCredentials: true
//		},
//		method: "GET",
//		success: function (response) {
//			$("#mainContainer").html(null);
//			$("#mainContainer").html(response);
//			initializeUserList();
//		},
//	});
//}

window.onload = function () {
	var inputs = document.querySelectorAll('input,select');
	for (var i = 0; i < inputs.length; i++) {
		inputs[i].addEventListener('invalid', function () {
			this.form.classList.add('was-validated');
		});
	}
}
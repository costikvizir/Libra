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
            $("#mainDiv").html(null);
            $("#mainDiv").html(response);
        },
    });
}

//$(document).on('submit', '#addUserForm', function (event) {
//	event.preventDefault();
//	debugger;
//	goToAllUsers();

//	// everything else you want to do on submit
//});
$(document).on('submit', '#AddUserForm', function (event) {
    event.preventDefault();
    $.ajax({
        url: $(this).attr('action'),
        type: $(this).attr('method'),
        data: $(this).serialize(),
        success: function (response) {
            $('#mainDiv').html(response);
        },
        error: function (xhr, status, error) {
            console.error(error);
        }
    });
});

$(document).ready(function () {
    $('#inputUserName').on('blur', function () {
        var userName = $(this).val();
        $.ajax({
            url: '"/User/IsUserNameAvailable",',
            type: 'POST',
            data: { userName: userName },
            success: function (response) {
                if (!response) {
                    $('#userNameError').text('Username already exists').show();
                } else {
                    $('#userNameError').text('').hide();
                }
            }
        });
    });
});
//$(document).ready(function () {
//	console.log('call goToAllUsersAdd');
//	debugger;

//	$('#submitButton').click(function (event) {
//		event.preventDefault(); // Prevent the default form submission
//		goToAllUsersAdd();
//	});
//});

//$(document).ready(function () {
//	console.log('submit form 1 method');
//	debugger;
//	$('#addUserForm').submit(function (e) {
//		e.preventDefault(); // Prevent the default form submission
//		console.log("Form submitted via AJAX");
//		$.ajax({
//			url: $(this).attr('action'), // Form action URL
//			type: $(this).attr('method'), // Form method (POST)
//			data: $(this).serialize(), // Form data
//			success: function (response) {
//				console.log("Server response:", response);
//				// Render the response within the main layout
//				$('#mainDiv').html(response);

//			},
//			error: function (xhr, status, error) {
//				console.error(error);
//			}
//		});
//	});
//});

//$(function () {
//	console.log('call goToAllUsers');
//	debugger;
//	$('#AddUserForm').submit(function (e) {
//		e.preventDefault(); // Prevent the default form submission
//		$.post("/User/AddUser",
//			$(this).serialize()); // AJAX POST request
//		goToAllUsers();
//	});
//});

function handleUserAddSuccess() {
    debugger;
    alert('User added successfully');
    goToAllUsers();
    //$('#usersList').DataTable().ajax.reload();
}
window.onload = function () {
    var inputs = document.querySelectorAll('input,select');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].addEventListener('invalid', function () {
            this.form.classList.add('was-validated');
        });
    }
}

function goToAllUsersAdd() {
    debugger;
    $.ajax({
        url: "/User/GetAllUsers",
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            $("#mainDiv").html(null);
            $("#mainDiv").html(response);
        },
    });
}

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

//$(document).on('submit', '#addUserForm', function (event) {

//	event.preventDefault();
//	debugger;
//	goToAllUsers();

//	// everything else you want to do on submit
//});

//$(document).ready(function () {
//	$('#submitButton').click(function (event) {
//		event.preventDefault(); // Prevent the default form submission
//		goToAllUsersAdd();
//	});
//});

//$(document).ready(function () {
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
//				$('#mainContainer').html(response);
			
//			},
//			error: function (xhr, status, error) {
//				console.error(error);
//			}
//		});
//	});
//});

//$(function () {
//	debugger;
//	$('#AddUserForm').submit(function (e) {
//		e.preventDefault(); // Prevent the default form submission
//		$.post("/User/AddUser",
//			$(this).serialize()); // AJAX POST request
//		goToAllUsers();
//	});
//});

function handleUserAddSuccess() {
    alert('User added successfully');
}
window.onload = function () {
	var inputs = document.querySelectorAll('input,select');
	for (var i = 0; i < inputs.length; i++) {
		inputs[i].addEventListener('invalid', function () {
			this.form.classList.add('was-validated');
		});
	}
}

//function goToAllUsersAdd() {
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
//		},
//	});
//}
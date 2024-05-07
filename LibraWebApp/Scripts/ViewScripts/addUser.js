
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

//$(document).ready(function () {
//	$('#submitButton').click(function (event) {
//		event.preventDefault(); // Prevent the default form submission
//		goToAllUsersAdd();
//	});
//});

$(function () {
	debugger;
	$('#AddUserForm').submit(function (e) {
		e.preventDefault(); // Prevent the default form submission
		$.post("/User/AddUser",
			$(this).serialize()); // AJAX POST request
		goToAllUsers();
	});
});


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
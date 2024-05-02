
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

window.onload = function () {
	var inputs = document.querySelectorAll('input,select');
	for (var i = 0; i < inputs.length; i++) {
		inputs[i].addEventListener('invalid', function () {
			this.form.classList.add('was-validated');
		});
	}
}
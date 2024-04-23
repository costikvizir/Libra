
function goToAddUser() {

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
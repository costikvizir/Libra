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
			$("#mainContainer").html(null);
			$("#mainContainer").html(response);
		},
	});
}
$('#content').on('click', '#clearFormBtn', function () {
	$('#AddPosForm')[0].reset(); // Reset form fields
});

function handlePosAddSuccess() {
	alert('Pos added successfully');
}

function resetForm() {
	var form = document.getElementById("AddPosForm");
	form.reset();
}
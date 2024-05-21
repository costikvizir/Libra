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
		},
	});
}
$('#content').on('click', '#clearFormBtn', function () {
	$('#AddPosForm')[0].reset(); // Reset form fields
});

function handlePosAddSuccess() {
	debugger;
	alert('Pos added successfully');
	goToAllPos();
}

function resetForm() {
	var form = document.getElementById("AddPosForm");
	form.reset();
}

function initializeIssuesList() {
	var table = $('#issueList').DataTable({
		select: 'single',
		processing: true,
		select: true,
		serverSide: true, 
		ajax: {
			url: "/Issue/GetAllIssuesJson",
			type: "POST",
			dataType: "json",
			ataSrc: "data",
		},
		columns: [
			{ title: "Id", data: "Id", name: "id", visible: false },
			{ title: "PosId", data: "PosId", name: "posid", autoWidth: true, searchable: true },
			{ title: "PosName", data: "PosName", name: "posname", autoWidth: true, searchable: true },
			{ title: "CreatedBy", data: "UserCreated", name: "usercreated", autoWidth: true, searchable: true },
			{ title: "Date", data: "CreationDate", name: "creationdate", autoWidth: true, searchable: true },
			{ title: "IssueType", data: "Type", name: "type", autoWidth: true, searchable: true },
			{ title: "Status", data: "Status", name: "status", autoWidth: true, searchable: true },
			{ title: "AssignedTo", data: "AssignedTo", name: "assignedto", autoWidth: true, searchable: true },
			{ title: "Memo", data: "Memo", name: "memo", autoWidth: true, searchable: true }
		]
	});

	$('#issueList tbody').on('click', 'tr', function () {
		if ($(this).hasClass('selected')) {
			$(this).removeClass('selected');
			$('#detailsButton, #deleteButton').prop('disabled', true);  // Disable button
		}
		else {
			table.$('tr.selected').removeClass('selected');
			$(this).addClass('selected');
			$('#detailsButton, #deleteButton').prop('disabled', false);  // Enable button
		}
	});

	// Initially disable the buttons
	$('#detailsButton, #deleteButton').prop('disabled', true);

	$('#deleteButton').on('click', function () {
		var data = table.row('.selected').data();
		if (data) {
			$('#deleteItemName').text(data.Name);
			$('#deleteButton').data('issueId', data.Id);
		}
	});

	$('.btn-outline-danger').on('click', function () {
		var issueId = $('#deleteButton').data('issueId');
		$.ajax({
			url: '/Issue/DeleteIssue/' + issueId,
			type: 'POST',
			data: { id: issueId },
			success: function (data) {
				table.row('.selected').remove().draw(false);
				$('#deleteModal').modal('hide');
				$('.modal-backdrop').remove();
				console.log('Issue deleted successfully');

			}
			//error: function (jqXHR, textStatus, errorThrown) {
			//	alert('Error: ' + errorThrown);
			//}
		});
	});

	$('#detailsButton').click(function () {
		debugger; 

		var data = table.row('.selected').data();  // Get the data of the selected row
        var issueId = data.Id;
		$.ajax({
			url: "/Issue/GetIssueById?id=" + issueId,
			data: {
				Id: data.Id  
			},
			method: "GET",
			success: function (response) {
				console.log('AJAX request successful, response:', response);
				$('#main-modal-container').html(response);
				$('#main-modal').modal('show');
			}
		})
	});
}



//$(document).ready(function () {
//	initializeIssuesList();
//});
function goToAllIssues() {
	debugger;
	$.ajax({
		url: "/Issue/GetAllIssues",
		data: {
		},
		xhrFields: {
			withCredentials: true
		},
		method: "GET",
		success: function (response) {
			$("#mainDiv").html(null);
			$("#mainDiv").html(response);
			initializeIssuesList();
		},
	});
}

document.getElementById('buttonWrapper').addEventListener('click', function () {
	var button = document.getElementById('deleteButton');
	if (button.disabled) {
		alert('Please select a row');
	}
});
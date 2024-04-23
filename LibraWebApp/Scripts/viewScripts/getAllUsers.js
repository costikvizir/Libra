//$(document).ready(function () {
//	console.log("Get All Users Script");
//	var table = $('#usersList').DataTable({
//		select: true,
//		ajax: {
//			url: "/User/GetAllUsersJson",
//			type: "GET",
//			dataType: "json",
//			dataSrc: ''
//		},
//		columns: [
//			{ data: "Id", visible: false },
//			{ data: "Name" },
//			{ data: "Login" },
//			{ data: "Email" },
//			{ data: "Role" },
//			{ data: "Telephone" }
//		]
//	});

//	$('#usersList tbody').on('click', 'tr', function () {
//		if ($(this).hasClass('selected')) {
//			$(this).removeClass('selected');
//			$('#editButton, #deleteButton').prop('disabled', true);  // Disable buttons
//		}
//		else {
//			table.$('tr.selected').removeClass('selected');
//			$(this).addClass('selected');
//			$('#editButton, #deleteButton').prop('disabled', false);  // Enable buttons
//		}
//	});

//	// Initially disable the buttons
//	$('#editButton, #deleteButton').prop('disabled', true);

//	$('#editButton').click(function () {

//		var data = table.row('.selected').data();  // Get the data of the selected row

//		$.ajax({
//			url: "/User/UpdateUser",
//			data: {
//				Id: data.Id
//			},
//			method: "GET",
//			success: function (response) {
//				console.log('AJAX request successful, response:', response);
//				$('#main-modal-container').html(response);
//				$('#main-modal').modal('show');
//			}
//		})
//	});

//	$('#deleteButton').on('click', function () {
//		var data = table.row('.selected').data();
//		if (data) {
//			$('#deleteItemName').text(data.Name);
//			$('#deleteButton').data('userid', data.Id);
//		}
//	});

//	$('.btn-outline-danger').on('click', function () {
//		var userId = $('#deleteButton').data('userid');
//		$.ajax({
//			url: '/User/DeleteUser/' + userId,
//			type: 'POST',
//			data: { id: userId },
//			success: function (data) {
//				table.row('.selected').remove().draw(false);
//				$('#deleteModal').modal('hide');
//				$('.modal-backdrop').remove();
//				console.log('User deleted successfully');

//			}
//			//error: function (jqXHR, textStatus, errorThrown) {
//			//	alert('Error: ' + errorThrown);
//			//}
//		});
//	});

//});
function initializeUserList() {
    console.log("Get All Users Script");
    var table = $('#usersList').DataTable({
        select: true,
        ajax: {
            url: "/User/GetAllUsersJson",
            type: "GET",
            dataType: "json",
            dataSrc: ''
        },
        columns: [
            { data: "Id", visible: false },
            { data: "Name" },
            { data: "Login" },
            { data: "Email" },
            { data: "Role" },
            { data: "Telephone" }
        ]
    });

    $('#usersList tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
            $('#editButton, #deleteButton').prop('disabled', true);  // Disable buttons
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            $('#editButton, #deleteButton').prop('disabled', false);  // Enable buttons
        }
    });

    // Initially disable the buttons
    $('#editButton, #deleteButton').prop('disabled', true);

    $('#editButton').click(function () {

        var data = table.row('.selected').data();  // Get the data of the selected row

        $.ajax({
            url: "/User/UpdateUser",
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

    $('#deleteButton').on('click', function () {
        var data = table.row('.selected').data();
        if (data) {
            $('#deleteItemName').text(data.Name);
            $('#deleteButton').data('userid', data.Id);
        }
    });

    $('.btn-outline-danger').on('click', function () {
        var userId = $('#deleteButton').data('userid');
        $.ajax({
            url: '/User/DeleteUser/' + userId,
            type: 'POST',
            data: { id: userId },
            success: function (data) {
                table.row('.selected').remove().draw(false);
                $('#deleteModal').modal('hide');
                $('.modal-backdrop').remove();
                console.log('User deleted successfully');
            }
        });
    });
}

//$(document).ready(function () {
//    initializeUserList();
//});

//$(document).ready(function () {
//    $('#grdPrUpPrj').DataTable()
//});

$(document).ready(function () {
    initializeUserList();
});


//@* $(document).ready(function () {
//	LoadAddUserForm();
//});

//function LoadAddUserForm() {
//	$.ajax({
//		url: "@Url.Action("GetAllUsersJson", "User")",
//		type: "GET",
//		success: function (response) {
//			$("#allUsersForm").html(response);
//		},
//		error: function (response) {
//			console.log(response);
//		}
//	});
//}* @
 function goToAllUsers() {
		
	$.ajax({
		url: "/User/GetAllUsers", 
		data: {
		},
		xhrFields: {
			withCredentials: true
		},
		method: "GET",
		success: function (response) {
			$("#mainContainer").html(null);  
			$("#mainContainer").html(response);
            initializeUserList();
		},
	});
 }

	document.getElementById('buttonWrapper').addEventListener('click', function () {
		var button = document.getElementById('deleteButton');
		if (button.disabled) {
			alert('Please select a row');
		}
	});
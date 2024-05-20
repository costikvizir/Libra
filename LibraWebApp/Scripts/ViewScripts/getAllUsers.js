
function initializeUserList() {
    debugger;
    console.log("Initialize User List Datatable");
    var columns = [
        { data: "Id", name: "id", visible: false },
        { title: "Name", data: "Name", name: "name", autoWidth: true, searchable: true },
        { title: "Login", data: "Login", name: "login", autoWidth: true, searchable: true },
        { title: "Email", data: "Email", name: "email", autoWidth: true, searchable: true },
        { title: "Role", data: "Role", name: "role", autoWidth: true, searchable: true },
        { title: "Telephone", data: "Telephone", name: "telephone", autoWidth: true, searchable: true }
    ];

    var table = $('#usersList').DataTable({
        select: 'single',
        processing: true,
        select: true,
        serverSide: true,
        pageLength: 10,
        //scrollX: true,
        ajax: {
            url: "/User/GetAllUsersJson",
            type: "POST",
            dataType: "json",
            dataSrc: "data",
            //pageLength: 10,
            //processing: true,
            //serverSide: true,

        },
        columns: columns,
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


function goToAllUsers() {
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
            initializeUserList();
        },
    });
}

//document.getElementById('buttonWrapper').addEventListener('click', function () {
//    var button = document.getElementById('deleteButton');
//    if (button.disabled) {
//        alert('Please select a row');
//    }
//});
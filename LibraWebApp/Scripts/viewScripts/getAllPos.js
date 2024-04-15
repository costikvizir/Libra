//$(document).ready(function () {
//    var table = $('#posList').DataTable({
//        select: true,
//        ajax: {
//            url: "/Pos/GetAllPosJson",
//            type: "GET",
//            dataType: "json",
//            dataSrc: ''
//        },
//        columns: [
//            { title: "Name", data: "Name" },
//            { title: "Telephone", data: "Telephone" },
//            { title: "Cellphone", data: "Cellphone" },
//            { title: "Brand", data: "Brand" },
//            { title: "Full Address", data: "FullAddress" }
//        ]
//    });

// Filter the table based on the input fields

//function initializeDataTables() {
export const initializeDataTables = () => {
    var table = $('#posList').DataTable({
        select: true,
        ajax: {
            url: "/Pos/GetAllPosJson",
            type: "GET",
            dataType: "json",
            dataSrc: ''
        },
        columns: [
            { title: "Name", data: "Name" },
            { title: "Telephone", data: "Telephone" },
            { title: "Cellphone", data: "Cellphone" },
            { title: "Brand", data: "Brand" },
            { title: "Full Address", data: "FullAddress" }
        ]
    });
    $('#inputPosId, #inputPosName, #inputPosBrand, #inputFullAddress').on('input', function () {
        // Apply the filter and redraw the table
        table
            .column(0).search($('#inputPosId').val())
            .column(1).search($('#inputPosName').val())
            .column(4).search($('#inputPosBrand').val())
            .column(5).search($('#inputFullAddress').val())
            .draw();
    });


    // Disable and enable buttons based on row selection
    $('#posList tbody').on('click', 'tr', function () {
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

    $('#deleteButton').on('click', function () {
        var data = table.row('.selected').data();
        if (data) {
            $('#deleteItemName').text(data.Name);
            $('#deleteButton').data('posid', data.PosId);
        }
    });

    // retrieve the PosId from the button data attribute and send it to the server
    // sets the id in the url and sends it to the server
    $('.btn-outline-danger').on('click', function () {
        var posId = $('#deleteButton').data('posid');
        $.ajax({
            url: '/Pos/DeletePos/' + posId,
            type: 'POST',
            data: { id: posId },
            success: function (data) {
                table.row('.selected').remove().draw(false);
                $('#deleteModal').modal('hide');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('Error: ' + errorThrown);
            }
        });
    });

    // Add button click event redirect to AddPos page
    $('#addButton').click(function () {
        window.location.href = '/Pos/AddPos';
    });

    // Edit button click event redirect to UpdatePos page
    $('#editButton').click(function () {
        var data = table.row('.selected').data();
        if (data) {
            var url = '/Pos/UpdatePos?' + $.param({ id: data.id });
            window.location.href = url;
        } else {
            alert('Please select a row');
        }
    });

    //$('#editButton').click(function () {
    //	var data = table.row('.selected').data();
    //	if (data) {
    //		$.ajax({
    //			url: '/Pos/UpdatePos',
    //			type: 'GET',
    //			data: { id: data.id },
    //			success: function (response) {
    //				// Handle success
    //				// If you want to replace the current page content with the response
    //				$('body').html(response);
    //			},
    //			error: function (jqXHR, textStatus, errorThrown) {
    //				// Handle error
    //				alert('Error: ' + textStatus);
    //			}
    //		});
    //	} else {
    //		alert('Please select a row');
    //	}
    //});

    // Edit button click event redirect to UpdatePos page
    //$('#editButton').click(function () {
    //	var data = table.row('.selected').data();
    //	$.ajax({
    //		url = '/Pos/UpdatePos?' + $.param({ id: data.id }),
    //		type: 'GET',
    //		contentType: 'application/json',
    //		data: { id: data.PosId },
    //		success: function (data) {
    //			 Handle success
    //		},
    //		error: function (jqXHR, textStatus, errorThrown) {
    //			 Handle error
    //		}
    //	});
    //});

    //});

    // Warning message if no row is selected
    document.getElementById('buttonWrapper').addEventListener('click', function () {
        var button = document.getElementById('deleteButton');
        if (button.disabled) {
            alert('Please select a row');
        }
    });
}
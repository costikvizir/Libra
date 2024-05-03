
function initializePosList() {
    console.log("Initialize Pos List Datatable");
    debugger;
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
            { title: "Status", data: "Status" },
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
            $('#editButton, #deleteButton, #detailsButton').prop('disabled', true);  // Disable buttons
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
            $('#editButton, #deleteButton, #detailsButton').prop('disabled', false);  // Enable buttons
        }

    });

    // Initially disable the buttons
    $('#editButton, #deleteButton, #detailsButton').prop('disabled', true);

    $('#deleteButton').on('click', function () {
        var data = table.row('.selected').data();
        if (data) {
            $('#deleteItemName').text(data.Name);
            $('#deleteButton').data('posid', data.PosId);
        }
    });

    $('#detailsButton').on('click', function () {
        var data = table.row('.selected').data();
        if (data) {
            $('#detailsItemName').text(data.Name);
            $('#deleteButton').data('posid', data.PosId);
        }
    });

    $('#editButton').on('click', function () {
        var data = table.row('.selected').data();
        if (data) {
            $('#editItemName').text(data.Name);
            $('#editButton').data('posid', data.PosId);
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
                //$('#deleteModal').modal('hide');
                alert('Error: ' + errorThrown);
            }
        });
    });


    // Details button click event redirect to DetailsPos page

    $('#detailsButton').on('click', function () {
        var data = table.row('.selected').data();
        if (data) {
            var posId = data.PosId; 
            goToPosDetails(posId);
        } else {
            alert('Please select a row');
        }
    });

    // Edit button click event redirect to UpdatePos page
    $('#editButton').click(function () {
        debugger;
        var data = table.row('.selected').data();
        if (data) {
            var posId = data.PosId;
            //var url = '/Pos/UpdatePos?=' + posId;
            //window.location.href = url;
            goToPosEdit(posId);
        } else {
            alert('Please select a row');
        }
    });


    // Warning message if no row is selected
    document.getElementById('buttonWrapper').addEventListener('click', function () {
        var button = document.getElementById('deleteButton');
        if (button.disabled) {
            alert('Please select a row');
        }
    });
}

//$(document).ready(function () {
//    $('#posList').DataTable();
//});

function initializePosIssuesList(posId) {
    var table = $('#issueList').DataTable({
        select: false,
        searching: false,
        paging: false,
        ajax: {
            url: "/Issue/GetIssuesJsonByPosId?id=" + posId,
            type: "GET",
            dataType: "json",
            dataSrc: ''
        },
        columns: [
            { title: "Id", data: "Id", visible: false },
            { title: "PosId", data: "PosId", visible: false },
            { title: "PosName", data: "PosName" },
            { title: "CreatedBy", data: "UserCreated" },
            { title: "Date", data: "CreationDate" },
            { title: "IssueType", data: "Type" },
            { title: "Status", data: "Status" },
            { title: "AssignedTo", data: "AssignedTo" },
            { title: "Memo", data: "Memo" }
        ]
    });
}
function goToAllPos() {
    debugger;
    $.ajax({
        url: "/Pos/GetAllPos",
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            $("#mainContainer").html(null);
            $("#mainContainer").html(response);
            initializePosList();
        },
    });
}

function goToPosDetails(posId) {
    debugger;
    $.ajax({
        url: "/Pos/GetPosById?id=" + posId,
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            $("#mainContainer").html(null);
            $("#mainContainer").html(response);
            initializePosIssuesList(posId);
        },
    });
}

function goToPosEdit(posId) {
    debugger;
    $.ajax({
        url: "/Pos/UpdatePos?id=" + posId,
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

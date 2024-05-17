var table;
function initializePosListForIssue() {
    debugger;
    table = $('#posList').DataTable({
        select: 'single',
        processing: true,
        select: true,
        serverSide: true,
        ajax: {
            url: "/Pos/GetAllPosJson",
            type: "POST",
            dataType: "json",
            dataSrc: "data",
        },
        columns: [
            { title: "Name", data: "Name", name: "name", autoWidth: true, searchable: true },
            { title: "Telephone", data: "Telephone", name: "telephone", autoWidth: true, searchable: true },
            { title: "Cellphone", data: "Cellphone", name: "cellphone", autoWidth: true, searchable: true },
            { title: "Brand", data: "Brand", name: "brand", autoWidth: true, searchable: true },
            { title: "Status", data: "Status", name: "status", autoWidth: true, searchable: true },
            { title: "Full Address", data: "FullAddress", name: "fulladdress", autoWidth: true, searchable: true }
        ]
    });

    $('#inputPosName, #inputPosBrand, #inputFullAddress').on('input', function () {
        // Apply the filter and redraw the table
        console.log("Filtering ... ")
        //Filtering based on second, fifth and sixth columns of the table
        table
            .column(1).search($('#inputPosName').val())
            .column(4).search($('#inputPosBrand').val())
            .column(5).search($('#inputFullAddress').val())
            .draw();
    });

    $('#posList tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        }
        else {
            table.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    $('#openIssue').on('click', function () {
        debugger;
        var data = table.row('.selected').data();
        if (data) {
            // $('#editItemName').text(data.Name);
            $('#openIssue').data('posId', data.PosId);
        }
    });

    //$('#button').click(function () {
    //    table.row('.selected').remove().draw(false);
    //});
}

//$(document).ready(function () {
//    initializePosListForIssue();
//    // Other code to bind events or manipulate the DOM
//});

function goToAddIssue() {
    debugger;
    $.ajax({
        url: "/Issue/AddIssue",
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            $("#mainContainer").html(null);
            $("#mainContainer").html(response);
            initializePosListForIssue();
        },
    });
}

//initializePosList();
function goToOpenIssue(posId) {
    debugger;
    console.log("Go to open issue, pos id= " + posId);
    $.ajax({
        url: "/Issue/OpenIssue?id=" + posId,
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

$(document).on('click', '#openIssue', function () {
    debugger;
    // initializePosListForIssue();
    var data = table.row('.selected').data();
    if (data) {
        var posId = data.PosId; 
        goToOpenIssue(posId);
    } else {
        alert('Please select a row');
    }
});

function handleIssueAddSuccess() {
    debugger;
    initializePosListForIssue();
    alert('Issue added successfully');
}
document.getElementById('buttonWrapper').addEventListener('click', function () {
    var button = document.getElementById('deleteButton');
    if (button.disabled) {
        alert('Please select a row');
    }
});
var table;
function initializePosListForIssue() {
    debugger;
    table = $('#posList').DataTable({
        select: 'single',
        processing: true,
        select: true,
        serverSide: true,
        ajax: {
            url: "/Pos/GetAllPosCustomSearchJson",
            //url: "/Pos/GetAllPosJson",
            type: "POST",
            dataType: "json",
            data: function (d) {
                //d.Name = $('#inputPosName').val();
                //d.Brand = $('#inputPosBrand').val();
                //d.FullAddress = $('#inputFullAddress').val();

                d.name = $('#inputPosName').val();
                d.brand = $('#inputPosBrand').val();
                d.fullAddress = $('#inputFullAddress').val();
                console.log("Data sent to server:", d); // Debug lo

                //d.search = {
                //    value: `${d.name} ${d.brand} ${d.fullAddress}`.trim(),
                //    regex: false
                //};

                return d;
            },
            dataSrc: "data",
        },
        columns: [
            { title: "Name", data: "Name", name: "name", autoWidth: true, searchable: true },
            { title: "Telephone", data: "Telephone", name: "telephone", autoWidth: true, searchable: true },
            { title: "Cellphone", data: "Cellphone", name: "cellphone", autoWidth: true, searchable: true },
            { title: "Brand", data: "Brand", name: "brand", autoWidth: true, searchable: true },
            {
                title: "Status",
                data: "Status",
                name: "status",
                autoWidth: true,
                searchable: true,
                createdCell: function (td, cellData, rowData, row, col) {
                    if (cellData !== 'No active issues') {
                        $(td).css('color', 'red');
                        $(td).prepend('<i class="fas fa-exclamation-triangle"></i> ');
                        //$(td).css('font-weight', 'bold');
                    }
                }
            },
            { title: "Full Address", data: "FullAddress", name: "fulladdress", autoWidth: true, searchable: true }
        ],
        dom: '<"top"i>rt<"bottom"lp><"clear">'
    });

    $('#inputPosName, #inputPosBrand, #inputFullAddress').on('input', function () {
        console.log("Filtering ... ");
        table.ajax.reload(); // Trigger a new AJAX request to the server with updated parameters
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
            //$("#mainDiv").html(null);
            $("#mainDiv").html(response);
            initializePosListForIssue();
            history.pushState({ page: "AddIssue" }, "Add Issue", "/Issue/AddIssue");
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
            // $("#mainDiv").html(null);
            $("#mainDiv").html(response);
            history.pushState({ page: "OpenIssue", posId: posId }, "Pos Details", "/Issue/OpenIssue?id=" + posId);
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

//$(document).ready(function () {
//    initializePosListForIssue();
//});

function handleIssueAddSuccess() {
    alert('Issue added successfully');
}
document.getElementById('buttonWrapper').addEventListener('click', function () {
    var button = document.getElementById('deleteButton');
    if (button.disabled) {
        alert('Please select a row');
    }
});
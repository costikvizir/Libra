
function initializePosList() {
    var table = $('#posList').DataTable({
        select: 'single',
        ajax: {
            url: "/Pos/GetAllPosJson",
            type: "GET",
            dataType: "json",
            dataSrc: ''
        },
        columns: [
            { title: "Pos Id", data: "PosId", visible: false },
            { title: "Name", data: "Name" },
            { title: "Telephone", data: "Telephone" },
            { title: "Cellphone", data: "Cellphone" },
            { title: "Brand", data: "Brand" },
            { title: "Full Address", data: "FullAddress" }
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

    $('#button').click(function () {
        table.row('.selected').remove().draw(false);
    });

    $('#openIssue').click(function () {
        console.log('Button clicked');  // Debugging statement
        var data = table.row('.selected').data();
        console.log('Data:', data);  // Debugging statement
        if (data) {
            var url = '/Issue/OpenIssue?' + $.param(data);
            console.log('URL:', url);  // Debugging statement
            window.location.href = url;
        } else {
            alert('Please select a row');
        }
    });
}

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
            initializePosList();
        },
    });
}
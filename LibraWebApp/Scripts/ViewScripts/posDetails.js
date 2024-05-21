function initializeIssuesList() {

    //var data = @Html.Raw(Json.Encode(Model.PosGet));


    var table = $('#issueList').DataTable({
        // select: true,
        // scrollX: true,
        ajax: {
            url: "/Issue/GetIssuesJsonByPosId?" + $.param({ id: data.PosId }),
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
        ],
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        var data = row.data();
                        return 'Details for ' + data.PosName;
                    }
                }),
                renderer: $.fn.dataTable.Responsive.renderer.tableAll()
            }
        }
    });
}

function goToPosDetails() {
    $.ajax({
        url: "/Pos/GetPosById",
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
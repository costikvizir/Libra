﻿function initializeIssuesList() {

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
        ]
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
            $("#mainContainer").html(null);
            $("#mainContainer").html(response);
            initializeIssuesList();
        },
    });
}
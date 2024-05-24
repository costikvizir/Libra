function handleIssueOpenSuccess() {
    alert('Issue Added Successfully!');
    goToAllIssues();
}

$(document).ready(function () {
    console.log('ready');
    // Initially disable inputSubclass and selectProblem
    $('#inputSubclass').prop('disabled', true);
    $('#selectProblem').prop('disabled', true);

    // Enable/Disable inputSubclass based on inputType selection
    $('#inputType').change(function () {
        if ($(this).val() === "select") {
            $('#inputSubclass').prop('disabled', true);
            $('#selectProblem').prop('disabled', true);
        } else {
            $('#inputSubclass').prop('disabled', false);
        }
    });

    // Enable/Disable selectProblem based on inputSubclass selection
    $('#inputSubclass').change(function () {
        if ($(this).val() === "select") {
            $('#selectProblem').prop('disabled', true);
            $('#inputType').prop('disabled', false);
        } else {
            $('#selectProblem').prop('disabled', false);
            $('#inputType').prop('disabled', true);
        }
    });

    $('#inputType').change(function () {
        var selectedTypeId = $(this).val();
        $.ajax({
            url: '@Url.Action("GetIssueSubtypes", "Issue")', // Replace 'YourControllerName' with the actual name
            type: 'GET',
            data: { issueTypeId: selectedTypeId },
            success: function (data) {
                var subTypeSelect = $('#inputSubclass');
                subTypeSelect.empty(); // Clear existing options

                $.each(data, function (index, subtype) {
                    subTypeSelect.append($('<option>', {
                        value: subtype.Id,
                        text: subtype.SubTypeName // Adjust based on your JSON structure
                    }));
                });
            }
        });
    });
});

//$(document).ready(function () {
//    $('#inputType').change(function () {
//        var selectedTypeId = $(this).val();
//        $.ajax({
//            url: '@Url.Action("GetIssueSubtypes", "Issue")', // Replace 'YourControllerName' with the actual name
//            type: 'GET',
//            data: { issueTypeId: selectedTypeId },
//            success: function (data) {
//                var subTypeSelect = $('#inputSubclass');
//                subTypeSelect.empty(); // Clear existing options

//                $.each(data, function (index, subtype) {
//                    subTypeSelect.append($('<option>', {
//                        value: subtype.Id,
//                        text: subtype.SubTypeName // Adjust based on your JSON structure
//                    }));
//                });
//            }
//        });
//    });
//});
function handleIssueOpenSuccess() {
    alert('Issue Added Successfully!');
    goToAllIssues();
}

$(document).ready(function () {
    // Initially disable inputSubclass and selectProblem
    $('#inputSubclass').prop('disabled', true);
    $('#selectProblem').prop('disabled', true);

    // Enable/Disable inputSubclass based on inputType selection and fetch subtypes
    $('#inputType').change(function () {
        var selectedTypeId = $(this).val();

        if (selectedTypeId === "select") {
            $('#inputSubclass').prop('disabled', true);
            $('#selectProblem').prop('disabled', true);
            $('#inputSubclass').empty(); // Clear existing options
            $('#inputSubclass').append($('<option>', {
                value: 'select',
                text: 'Select Subtype'
            }));
            $('#selectProblem').empty(); // Clear existing options
            $('#selectProblem').append($('<option>', {
                value: 'select',
                text: 'Select Problem'
            }));
        } else {
            $('#inputSubclass').prop('disabled', false);

            // AJAX call to fetch subtypes
            $.ajax({
                url: '@Url.Action("GetIssueSubtypes", "Issue")', // Replace 'Issue' with the actual controller name if different
                type: 'GET',
                data: { issueTypeId: selectedTypeId },
                success: function (data) {
                    var subTypeSelect = $('#inputSubclass');
                    subTypeSelect.empty(); // Clear existing options

                    subTypeSelect.append($('<option>', {
                        value: 'select',
                        text: 'Select Subtype'
                    }));

                    $.each(data, function (index, subtype) {
                        subTypeSelect.append($('<option>', {
                            value: subtype.Id,
                            text: subtype.SubTypeName
                        }));
                    });

                    // Disable selectProblem until a subtype is selected
                    $('#selectProblem').prop('disabled', true);
                },
                error: function (xhr, status, error) {
                    console.error("An error occurred while fetching subtypes: " + error);
                }
            });
        }
    });

    // Enable/Disable selectProblem based on inputSubclass selection and fetch problems
    $('#inputSubclass').change(function () {
        var selectedSubtypeId = $(this).val();

        if (selectedSubtypeId === "select") {
            $('#selectProblem').prop('disabled', true);
            $('#inputType').prop('disabled', false);
            $('#selectProblem').empty(); // Clear existing options
            $('#selectProblem').append($('<option>', {
                value: 'select',
                text: 'Select Problem'
            }));
        } else {
            $('#selectProblem').prop('disabled', false);

            // AJAX call to fetch problems
            $.ajax({
                url: '@Url.Action("GetIssueSubtypes", "Issue")', // Replace 'Issue' with the actual controller name if different
                type: 'GET',
                data: { subtypeId: selectedSubtypeId },
                success: function (data) {
                    var problemSelect = $('#selectProblem');
                    problemSelect.empty(); // Clear existing options

                    problemSelect.append($('<option>', {
                        value: 'select',
                        text: 'Select Problem'
                    }));

                    $.each(data, function (index, problem) {
                        problemSelect.append($('<option>', {
                            value: problem.Id,
                            text: problem.ProblemName
                        }));
                    });

                    $('#inputType').prop('disabled', true);
                },
                error: function (xhr, status, error) {
                    console.error("An error occurred while fetching problems: " + error);
                }
            });
        }
    });
});
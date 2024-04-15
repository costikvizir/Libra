$(document).ready(function () {
    // Function to load POS form content via AJAX
    function loadPOSForm() {
        $.ajax({
            url: '@Url.Action("AddPos", "Pos")',
            method: 'GET',
            success: function (response) {
                $('#content').html(response); // Update content with received HTML
            },
            error: function (xhr, status, error) {
                console.error('Error loading POS form:', error);
            }
        });
    }

    // Initial content loading
    loadPOSForm();

    // Click event handler for Save POS button
    //@* $('#content').on('click', '#savePOSBtn', function () {
    //    // Assuming the form is submitted via AJAX
    //    var formData = $('#AddPosForm').serialize(); // Serialize form data
    //    $.ajax({
    //        url: '@Url.Action("AddPos", "Pos")',

    //        data: formData,
    //        xhrFields: {
    //            withCredentials: true
    //        },
    //        method: "GET",
    //        success: function (response) {
    //            $("#AddUserForm").html(null);  //<div id = "AddUserForm"></div>
    //            $("#AddUserForm").html(response)
    //            error: function(xhr, status, error) {
    //                console.error('Error saving POS:', error);
    //            }
    //        });
    //});* @

        // Click event handler for Clear Form button
        $('#content').on('click', '#clearFormBtn', function () {
            $('#AddPosForm')[0].reset(); // Reset form fields
        });
});

function resetForm() {
    var form = document.getElementById("AddPosForm");
    form.reset();
}
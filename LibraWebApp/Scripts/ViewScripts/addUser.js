
function validateTelephoneInput(input) {
    let value = input.value;
    // Allow only digits and optionally a '+' at the beginning
    if (value.charAt(0) === '+') {
        input.value = '+' + value.slice(1).replace(/\D/g, '');
    } else {
        input.value = value.replace(/\D/g, '');
    }
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode == 43 && evt.target.selectionStart == 0) {
        return true; // Allow '+' only at the beginning
    }
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false; // Allow only digits
    }
    return true;
}

function handlePaste(event) {
    var paste = (event.clipboardData || window.clipboardData).getData('text');
    if (!/^\+?[0-9]*$/.test(paste)) {
        event.preventDefault();
    }
}

function goToAddUser() {
    debugger;
    $.ajax({
        url: "/User/AddUser",
        data: {
        },
        xhrFields: {
            withCredentials: true
        },
        method: "GET",
        success: function (response) {
            $("#mainDiv").html(null);
            $("#mainDiv").html(response);
        },
    });
}

$(document).on('submit', '#AddUserForm', function (event) {
    event.preventDefault();
    $.ajax({
        url: $(this).attr('action'),
        type: $(this).attr('method'),
        data: $(this).serialize(),
        success: function (response) {
            $('#mainDiv').html(response);

            // Call handleUserAddSuccess only if the response indicates success
            if (response.success) {
                handleUserAddSuccess();
            } else {
               // console.error('Error in response: ', response);
                console.log("error add user");
            }
        },
        error: function (xhr, status, error) {
            //console.error(error);
            console.log("error add user");
        }
    });
});

///, OnSuccess = "handleUserAddSuccess"

//$('#AddUserForm').on('submit', function (event) {
//    debugger;
//    console.log('Init form submission!');
//    event.preventDefault();
//    $.ajax({
//        url: $(this).attr('action'),
//        type: $(this).attr('method'),
//        data: $(this).serialize(),
//        success: function (response) {
//            $('#mainDiv').html(response);

//            // Call handleUserAddSuccess only if the response indicates success
//            if (response.success) {
//                handleUserAddSuccess();
//            } else {
//                // console.error('Error in response: ', response);
//                console.log("error add user");
//            }
//        },
//        error: function (xhr, status, error) {
//            //console.error(error);
//            console.log("error add user");
//        }
//    });
//});



//("form").on("submit", function (e) {
//    var dataString = $(this).serialize();

//    // alert(dataString); return false;
//    $.ajax({
//        type: "POST",
//        url: "/User/GetAllUsers",
//        data: dataString,
//        success: function () {
//            $("#mainDiv").html(null);
//            $("#mainDiv").html(response);
//            initializeUserList();
//        }
//    });
//    e.preventDefault();
//});


//function handleUserAddSuccess() {
//    debugger;


//    //$('#usersList').DataTable().ajax.reload();
//    //alert('User added successfully');
//    //goToAllUsers();
//    var form = document.getElementById('AddUserForm');
//    if (form.checkValidity()) {
//        alert('User added successfully');
//        goToAllUsers();
//    } else {
//        // If the form is not valid, show the validation messages
//        form.classList.add('was-validated');
//    }
//}


function handleUserAddSuccess(response) {
    debugger;
    alert('User added successfully');
    goToAllUsers();

    //if (response.success) {
    //    alert('User added successfully');
    //    goToAllUsers();
    //} else {
    //    // If there are validation errors, display them
    //    var form = document.getElementById('AddUserForm');

    //    // Remove existing validation messages
    //    var validationMessages = form.querySelectorAll('.text-danger');
    //    validationMessages.forEach(function (message) {
    //        message.innerHTML = '';
    //    });

    //    // Display new validation messages from the server response
    //    for (var key in response.errors) {
    //        if (response.errors.hasOwnProperty(key)) {
    //            var errorMessageElement = document.querySelector(`[data-valmsg-for='${key}']`);
    //            if (errorMessageElement) {
    //                errorMessageElement.innerHTML = response.errors[key];
    //            }
    //        }
    //    }

    //    // Add was-validated class to the form
    //    form.classList.add('was-validated');
    
}
window.onload = function () {
    var inputs = document.querySelectorAll('input,select');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].addEventListener('invalid', function () {
            this.form.classList.add('was-validated');
        });
    }
}

document.addEventListener('DOMContentLoaded', (event) => {
    const form = document.getElementById('AddUserForm');
    const telephoneInput = document.getElementById('inputTelephone');
    telephoneInput.addEventListener('input', () => validateTelephoneInput(telephoneInput));
    telephoneInput.addEventListener('keypress', isNumberKey);
    telephoneInput.addEventListener('paste', handlePaste);

    form.addEventListener('submit', (event) => {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
            form.classList.add('was-validated');
        }
    });
});

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
    debugger;
    event.preventDefault();

    $.ajax({
        url: $(this).attr('action'),
        type: $(this).attr('method'),
        data: $(this).serialize(),
        success: function (response) {
            $('#mainDiv').html(response);
            console.log("success add user");
            console.log(response.success);
            // Call handleUserAddSuccess only if the response indicates success
            
            if (response.success) {
                handleUserAddSuccess();
            } else {
               // console.error('Error in response: ', response);
                console.log("Inside error add user");
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
            console.log("Outside error add user");
        }
    });
});


function handleUserAddSuccess() {
    debugger;
    alert('User added successfully');
    goToAllUsers();
    initializeUserList(); // Initialize DataTables after loading the view
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
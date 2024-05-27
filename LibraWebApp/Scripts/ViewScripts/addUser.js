//function validateTelephoneInput(input) {
//    let value = input.value;
//    // Allow only digits and optionally a '+' at the beginning
//    input.value = value.replace(/(?!^)\D/g, '');
//    // Ensure the first character is either a digit or '+'
//    if (input.value.charAt(0) !== '+' && isNaN(parseInt(input.value.charAt(0)))) {
//        input.value = '';
//    }
//}

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

//$(document).on('submit', '#addUserForm', function (event) {
//	event.preventDefault();
//	debugger;
//	goToAllUsers();

//	// everything else you want to do on submit
//});
//$(document).on('submit', '#AddUserForm', function (event) {
//    event.preventDefault();
//    $.ajax({
//        url: $(this).attr('action'),
//        type: $(this).attr('method'),
//        data: $(this).serialize(),
//        success: function (response) {
//            $('#mainDiv').html(response);
//        },
//        error: function (xhr, status, error) {
//            console.error(error);
//            console.log("error add user");
//        }
//    });
//});

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
                console.error('Error in response: ', response);
                console.log("error add user");
            }
        },
        error: function (xhr, status, error) {
            console.error(error);
            console.log("error add user");
        }
    });
});

//$(document).ready(function () {
//    $('#inputUserName').on('blur', function () {
//        var userName = $(this).val();
//        $.ajax({
//            url: '"/User/IsUserNameAvailable",',
//            type: 'POST',
//            data: { userName: userName },
//            success: function (response) {
//                if (!response) {
//                    $('#userNameError').text('Username already exists').show();
//                } else {
//                    $('#userNameError').text('').hide();
//                }
//            }
//        });
//    });
//});
//$(document).ready(function () {
//	console.log('call goToAllUsersAdd');
//	debugger;

//	$('#submitButton').click(function (event) {
//		event.preventDefault(); // Prevent the default form submission
//		goToAllUsersAdd();
//	});
//});



//$(function () {
//	console.log('call goToAllUsers');
//	debugger;
//	$('#AddUserForm').submit(function (e) {
//		e.preventDefault(); // Prevent the default form submission
//		$.post("/User/AddUser",
//			$(this).serialize()); // AJAX POST request
//		goToAllUsers();
//	});
//});

function handleUserAddSuccess() {
    debugger;
    alert('User added successfully');
    goToAllUsers();
    //$('#usersList').DataTable().ajax.reload();
}
window.onload = function () {
    var inputs = document.querySelectorAll('input,select');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].addEventListener('invalid', function () {
            this.form.classList.add('was-validated');
        });
    }
}

function goToAllUsersAdd() {
    debugger;
    $.ajax({
        url: "/User/GetAllUsers",
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

document.addEventListener('DOMContentLoaded', (event) => {
    const telephoneInput = document.getElementById('inputTelephone');
    telephoneInput.addEventListener('input', () => validateTelephoneInput(telephoneInput));
    telephoneInput.addEventListener('keypress', isNumberKey);
    telephoneInput.addEventListener('paste', handlePaste);
});
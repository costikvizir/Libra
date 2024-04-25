$('#inputMorningOpening').datetimepicker({
    format: 'hh:mm',
    icons: {
        up: 'fa fa-chevron-up',
        down: 'fa fa-chevron-down'
    }
});

$('#inputMorningOpening').on("dp.change", function (e) {
    $('#inputMorningClosing').data("DateTimePicker").minDate(e.date);
});

$('#inputMorningClosing').datetimepicker({
    format: 'HH:mm',
    icons: {
        up: 'fa fa-chevron-up',
        down: 'fa fa-chevron-down'
    }
});

$('#inputMorningClosing').on("dp.change", function (e) {
    $('#inputAfternoonOpening').data("DateTimePicker").minDate(e.date);
});

$('#inputAfternoonOpening').datetimepicker({
    format: 'HH:mm',
    icons: {
        up: 'fa fa-chevron-up',
        down: 'fa fa-chevron-down'
    }
});

$('#inputAfternoonOpening').on("dp.change", function (e) {
    $('#inputAfternoonClosing').data("DateTimePicker").minDate(e.date);
});

$('#inputAfternoonClosing').datetimepicker({
    format: 'HH:mm',
    icons: {
        up: 'fa fa-chevron-up',
        down: 'fa fa-chevron-down'
    }
});

function resetForm() {
    var form = document.getElementById("AddPosForm");
    form.reset();
}

//$('#inputMorningOpening, #inputMorningClosing, #inputAfternoonOpening, #inputAfternoonClosing').on('keydown', function (e) {
//    // Check if the key pressed is the up arrow (increase value)
//    if (e.keyCode === 38) {
//        // Trigger the animation for the up button
//        $(this).closest('.input-group').find('.fa-chevron-up').addClass('animate');
//        setTimeout(function () {
//            $('.animate').removeClass('animate');
//        }, 300);
//    }
//    // Check if the key pressed is the down arrow (decrease value)
//    else if (e.keyCode === 40) {
//        // Trigger the animation for the down button
//        $(this).closest('.input-group').find('.fa-chevron-down').addClass('animate');
//        setTimeout(function () {
//            $('.animate').removeClass('animate');
//        }, 300);
//    }
//});
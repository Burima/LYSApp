$("#txtBookingFromDate").datepicker({
    minDate: 0,
    maxDate: "+1M +0D",
    numberOfMonths: 2,
    onSelect: function () {
        var toDate = $(this).datepicker('getDate');
        toDate.setDate(toDate.getMonth() + 1);
        alert(toDate);
        $("#txtBookingToDate").datepicker({
            minDate: 0,
            maxDate: "+1M +0D",
            numberOfMonths: 2,
        });
    }
});

//$('#txtBookingFromDate').click(function () {
//    fnFillBookingToDate();

//});
//function fnFillBookingToDate() {
//    if ($('#txtBookingFromDate').val() != '') {
//        $('#divSelectFromDateWarning').hide();
//    } else {
//        $('#divSelectFromDateWarning').show();
//    }
//}


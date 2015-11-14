$("#txtBookingFromDate").datepicker({
    minDate: 0,
    maxDate: "+1M +0D",
    numberOfMonths: 2,
    dateFormat: "dd/mm/yy",
    onSelect: function () {
        var toDate = $(this).datepicker('getDate');
        //alert(toDate);
        toDate.setMonth(toDate.getMonth() + 1);
        //alert(toDate);
        $("#txtBookingToDate").datepicker({
            minDate: toDate,
            maxDate: "+1Y +0M +0D",
            numberOfMonths: 2,
            dateFormat: "dd/mm/yy"
        });
    }
});

//click on datepicker icon calender opens
$('.glyphicon-calendar').click(function () {
    $(this).parent().parent().find('.lys-date-picker').focus();   
});



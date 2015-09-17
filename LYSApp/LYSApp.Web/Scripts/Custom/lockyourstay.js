
$(document).ready(function () {
    //required field validator for all elements in the application
    jQuery.extend(jQuery.validator.messages, {
        required: "Required"
    });

    $('.required').each(function (i) {
        $(this).on('keydown keyup keypress', function () {
            var id = $(this).attr("id");
            var value = $("#" + id).val();
            //alert($(this).html());
            if (value.trim() == "") {
                console.log("no value detected" + id + "----------" + value);
                $("#" + id).css("border", "1px solid red");
                flag = true;
            }
            else {
                $("#" + id).css("border", "1px solid #e5e6e7");
                flag = false;
            }

        });
    });
});




/* END :  Custom select box */

//START: Number and character validation parts
//Backspace, Tab, Enter, Delete, Left, top, right, bottom
var Operable_keys = [8, 9, 13, 46, 37, 38, 39, 40];

function isCharField(event) {
    var obj = (event.srcElement) ? event.srcElement : event.target;
    $(obj).val($(obj).val().replace(/[^a-zA-Z ]/g, '').replace(/([~!@#$%^&*()_+=`{}\[\]\|\\:;'<>,.\/\\\?])+/g, '-').replace(/^(-)+|(-)+$/g, ''));
    var charCode = (event.keyCode) ? event.keyCode : event.which;
    if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105))
        return false;
    return true;
}

function isNumberKey(evt) {
    var obj = (evt.srcElement) ? evt.srcElement : evt.target;
    $(obj).val($(obj).val().replace(/[^0-9]/g, '').replace(/([~!@#$%^&*()_+=`{}\[\]\|\\:;'<>,.\/? ])+/g, '-').replace(/^(-)+|(-)+$/g, ''));
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if ((charCode >= 48 && charCode <= 57) || (charCode >= 96 && charCode <= 105) || ($.inArray(charCode, Operable_keys) != -1))
        return true;
    return false;
}

function addHyphen(obj, evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (!(charCode >= 37 && charCode <= 40)) {
        var z = $(obj).val().replace("-", "");
        if (z.length > 5)
            z = z.substring(0, 5) + "-" + z.substring(5);
        $(obj).val(z);
    }
}

//show the limit of char left
function charlimit() {
    $("form :input").each(function () {
        var input = $(this); // This is the jquery object of the input, do what you will
        if (input.parent().find("span.span-char-left").length) {
            input.parent().find("span.span-char-left").text((input.attr("maxlength")) - (input.val().trim().length));
        }
    });
};

//keyup event for all inputs
function inputkeyup() {
    $("form :input").each(function () {
        var input = $(this);
        input.keyup(function () {
            if (input.parent().find("span.span-char-left").length) {
                input.parent().find("span.span-char-left").text((input.attr("maxlength")) - (input.val().trim().length));
            }
        });
    })
};
//END: Number and character validation parts


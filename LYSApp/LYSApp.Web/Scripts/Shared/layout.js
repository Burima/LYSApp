$(document).ready(function () {

    ////if any error in log in
    //if (loginError != '') {
    //    $('#signin span.errormessage').html(loginError);
    //    $('#signin div.errorblock').removeClass('hidden');
    //    $('#signin').modal('show');
    //}

    //if (registerError != '') {
    //    $('#signup span.errormessage').html(registerError);
    //    $('#signup div.errorblock').removeClass('hidden');
    //    $('#signup').modal('show');
    //}
    //set external login test as Sign UP/Sign In based on the modal header text
    $('.modal-body .action').each(function (index, element) {
        //alert($(this).closest('div.modal-content').find('h4').html());
        $(this).html($(this).closest('div.modal-content').find('h4').html());
    });
    //char alone
    //$(".charAlone").on(" keydown ", function (event) { return isCharField(event); });

    //$("#form-SignUp").validate({
    //    rules: {
    //        registerEmail: {
    //            email: true
    //        }
    //    }
    //});

    //message timeout
    // window.setTimeout(function () { $("#modal-message").alert('close'); }, 5000);

    //forgot password modal
    $('.modal-fp').click(function () {
        $('#signin').modal('hide');
        //get email id from sign in modal email field (if any)
        $('#txtForgotPasswordEmail').val($('#LoginViewModel_Email').val());
        $('#forgotpassword').modal('show');
    });

    $('#btnForgotPassword').click(function () {
        if ($('#form-ForgotPassword').valid()) {
            var jmodel = { email: $("#txtForgotPasswordEmail").val() };
            //alert(jmodel);
            $.ajax({
                url: ForgotPasswordURL,
                type: 'POST',
                data: jmodel,
                dataType: 'html',
                success: function (data, textStatus, XMLHttpRequest) {
                    $('#forgotpassword').modal('hide');
                    showModalMessage(data);

                },
                error: function (xhr, status) {
                    //alert('e')
                }
            })//ajax end
        } else {
            return false;
        }

    });
    
    //subscribe email function
    $('.btnSubscribe').click(function () {
        if ($('#form-Subscribe').valid()) {
            var model = { Email: $("#txtEmailForSubscribe").val() };
            $('.btnSubscribe').prop('disabled', true);
            $.ajax({
                url: EmailSubscribeURL,
                type: 'POST',
                data: JSON.stringify(model),
                contentType: "application/json",
                dataType: 'html',
                success: function (data, textStatus, XMLHttpRequest) {
                    //alert(data);
                    $('.btnSubscribe').prop('disabled', false);;//enable for subscription again
                    $("#txtEmailForSubscribe").val("");//make email field empty
                    $('#subscribe-info').html(data).show();//show message for 5sec                    
                    setTimeout(function () {
                        $("#subscribe-info").hide('blind', {}, 500)
                    }, 5000);
                },
                error: function (xhr, status) {
                    //alert(xhr.status);
                }
            })//ajax end
        } else {
            return false;
        }
    });


    /*----------------------------- List your property functionality ---------------------------------------*/

    $(".numbersAlone").on("keyup keydown keypress", function (event) { return isNumberKey(event); });
    $(".email").on("keyup keydown keypress", function () {
        var id = $(this).attr("id");
        var value = $("#" + id).val();
        if (value.trim() == "" || validateEmail(value) == false) {
            //console.log("no value detected " + id + "----------" + value);
            $("#" + id).css("border", "1px solid red");
        } else {
            $("#" + id).css("border", "1px solid #e5e6e7");
        }
    });

    function validateForm() {
        var flag = false;
        $('.required').each(function () {
            var id = $(this).attr("id");
            var value = $("#" + id).val();
            if (value.trim() == "") {
                //console.log("no value detected" + id + "----------" + value);
                $("#" + id).css("border", "1px solid red");
                flag = false;
            }
            else {
                $("#" + id).css("border", "1px solid #e5e6e7");
                flag = true;
            }

        });

        $('.email').each(function () {
            var id = $(this).attr("id");
            var value = $("#" + id).val();
            if (validateEmail(value) == false) {
                $("#" + id).css("border", "1px solid red");
                flag = false;
            }
            else {
                $("#" + id).css("border", "1px solid #e5e6e7");
                flag = true;
            }
        });

        $('.mobile').each(function () {
            var id = $(this).attr("id");
            var value = $("#" + id).val();
            if (value.length != 10) {
                $("#" + id).css("border", "1px solid red");
                flag = false;
            }
            else {
                $("#" + id).css("border", "1px solid #e5e6e7");
                flag = true;
            }
        });

        return flag;
    }
    $('.btn-list-your-property').click(function () {
        if (validateForm()) {
            var model = { FirstName: $("#txtFirstName").val(), LastName: $("#txtLastName").val(), Email: $("#txtEmail").val(), Mobile: $("#txtMobile").val(), Address: $("#txtAddress").val() };

            //alert(model);
            $('#modal-list-your-property').modal('hide');
            $('.btn-list-your-property').prop('disabled', true);
            showProgress(false, "Listing your record. Please wait...");
            $.ajax({
                url: ListYourPropertyURL,
                type: 'POST',
                data: JSON.stringify(model),
                dataType: 'html',
                contentType: "application/json",
                success: function (data, textStatus, XMLHttpRequest) {                    
                    hideProgress();
                    $('.btn-list-your-property').prop('disabled', false);
                    $('#form-list-your-property').find("input, textarea").val("");
                    showModalMessage(data);
                },
                error: function (xhr, status) {                    
                    hideProgress();
                    $('.btn-list-your-property').prop('disabled', false);
                    showModalMessage("Something went wrong! Please contact support@lockyourstay.com.");
                }


            });
        }

    });
});
/*----------------------------- End List your property functionality ---------------------------------------*/
//this method will show the modal message. content will be passes in the parameter data.
function showModalMessage(data) {
    $('#spnMessage').html(data);
    $('#modal-message').modal('show');
    //hide message modal after 5sec
    window.setTimeout(function () { $('#modal-message').modal('hide'); }, 5000);
};

function loginSuccess(response) {
    if (response.Success) {
        window.location.href="/"
    } else {
        $('#signin span.errormessage').html(response.Error);
            $('#signin div.errorblock').removeClass('hidden');            
    }
}
function loginFailed(response) {
    alert('f ' + response);
}

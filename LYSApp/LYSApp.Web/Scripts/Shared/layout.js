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

    

    //$('#btnForgotPassword').click(function () {
    //    if ($('#form-ForgotPassword').valid()) {
    //        var jmodel = { email: $("#txtForgotPasswordEmail").val() };
    //        //alert(jmodel);
    //        $.ajax({
    //            url: ForgotPasswordURL,
    //            type: 'POST',
    //            data: jmodel,
    //            dataType: 'html',
    //            success: function (data, textStatus, XMLHttpRequest) {
    //                $('#forgotpassword').modal('hide');
    //                showModalMessage(data);

    //            },
    //            error: function (xhr, status) {
    //                //alert('e')
    //            }
    //        })//ajax end
    //    } else {
    //        return false;
    //    }

    //});

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

/*--------------------------------- log in with ajax -----------------------------------------------*/
function loginBegin() {
    $('#spinner_modal_empty .modal-body').css('top', '182px').css('left', '92px');
    showProgress(false, "");
    //alert($(this).find('input[type="submit"]'));
}
function loginSuccess(response) {
    hideProgress();
    if (response.Success) {
        window.location.href = "/"
    } else {
        if (response.Error != null && response.Error != undefined && response.Error != '') {//Eror not null
            if (response.EmailConfirmed != null && response.EmailConfirmed != undefined && !response.EmailConfirmed) {//send user for email to resend modal
                $('#signin span.errormessage').html(response.Error).delay(500);//show error
                $('#modalEmailVerification #Email').val($('#form-SignIn #Email').val());//set resend email with recently signed up email
                $('#form-SignIn').each(function () {
                    this.reset();//reset form values
                });
                $('#signin').modal('hide');//hide sign in modal
                $('#modalEmailVerification').modal('show');//show email resend modal
            } else {//email confirmed but error occured
                $('#signin span.errormessage').html(response.Error);
                $('#signin div.errorblock').removeClass('hidden');
            }
        }
        else {
            $('#signin span.errormessage').html("Something went wrong! Please try again.");
            $('#signin div.errorblock').removeClass('hidden');
        }

    }
}
function loginFailed(response) {
    hideProgress();
    if (response.Error != undefined && response.Error != '') {
        $('#signin span.errormessage').html(response.Error);
    }
    else {
        $('#signin span.errormessage').html("Something went wrong! Please try again.");
    }
    $('#signin div.errorblock').removeClass('hidden');

}

/*--------------------------------- Sign Up with ajax -----------------------------------------------*/
function signupBegin() {
    $('#spinner_modal_empty .modal-body').css('top', '236px').css('left', '80px');
    showProgress(false, "");
}
function signupSuccess(response) {
    hideProgress();
    //alert(response.Error);
    if (response.Success) {
        $('#modalEmailVerification #Email').val($('#form-SignUp #Email').val());//set resend email with recently signed up email
        $('#form-SignUp').each(function () {
            this.reset();//reset form values
        });
        $('#signup').modal('hide');//hide sign up modal
        $('#modalEmailVerification').modal('show');//show email resend modal
    } else {
        if (response.Error != undefined && response.Error != '') {
            $('#signup span.errormessage').html(response.Error);
        } else {
            $('#signup span.errormessage').html("Something went wrong! Please try again.");
        }
        $('#signup div.errorblock').removeClass('hidden');
    }
}
function signupFailed(response) {
    hideProgress();
    //alert('f' + response);
    if (response.Error != undefined && response.Error != '') {
        $('#signup span.errormessage').html(response.Error);
    } else {
        $('#signup span.errormessage').html("Something went wrong! Please try again.");
    }

    $('#signup div.errorblock').removeClass('hidden');
}

/* ----------------------------- Resend Email Verification ----------------------------*/
function resendemailverificationBegin() {
    $('#spinner_modal_empty .modal-body').css('top', '58px').css('left','90px');
    showProgress(false, "");
}

function resendemailverificationSuccess(response) {
    hideProgress();
    if (response.Success) {
        $('#modalEmailVerification span.errormessage').html("Email send successfully.");
    } else {
        if (response.Error != undefined && response.Error != '') {
            $('#modalEmailVerification span.errormessage').html(response.Error);
        } else {
            $('#modalEmailVerification span.errormessage').html("Something went wrong! Please try again.");
        }
    }
    $('#modalEmailVerification div.errorblock').removeClass('hidden');
}
function resendemailverificationFailed(response) {
    hideProgress();
    if (response.Error != undefined && response.Error != '') {
        $('#modalEmailVerification span.errormessage').html(response.Error);
    } else {
        $('#modalEmailVerification span.errormessage').html("Something went wrong! Please try again.");
    }

    $('#modalEmailVerification div.errorblock').removeClass('hidden');
}
/*---------------------------- forgot password -----------------------------------*/
//forgot password modal
$('.modal-fp').click(function () {
    //get email id from sign in modal email field (if any)
    $('#form-ForgotPassword #Email').val($('#form-SignIn #Email').val());
    $('#form-SignIn').each(function () {
        this.reset();//reset form values
    });
    $('#signin').modal('hide');
    $('#forgotpassword').modal('show');
});

function forgotpasswordBegin() {
    $('#spinner_modal_empty .modal-body').css('top', '38px').css('left', '80px');
    showProgress(false, "");
}

function forgotpasswordSuccess(response) {
    hideProgress();
    $('#forgotpassword').modal('hide');
    showModalMessage(response);
}

function forgotpasswordFailed(response) {
    hideProgress();
    $('#forgotpassword').modal('hide');
    showModalMessage("Something went wrong! Please contact support@lockyourstay.com.");
}


/*----------------------------------- reset password ----------------------------*/
function resetpasswordBegin() {
    $('#spinner_modal_empty .modal-body').css('top', '105px').css('left', '80px');
    showProgress(false, "");
}

function resetpasswordSuccess(response) {
    hideProgress();
    if (response.Success) {
        $('#resetpassword span.errormessage').html(response.Error);
        $('#resetpassword div.errorblock').removeClass('hidden');
        window.setTimeout(function () {
            //$(".alert-success").alert('close');
        }, 5000);
        $('#signin').modal('show');
        //window.location.href = "/";
    } else {
        if (response.Error != undefined && response.Error != '') {
            $('#resetpassword span.errormessage').html(response.Error);
        } else {
            $('#resetpassword span.errormessage').html("Something went wrong! Please try again.");
        }
        $('#resetpassword div.errorblock').removeClass('hidden');
    }
}
function resetpasswordFailed(response) {
    hideProgress();
    if (response.Error != undefined && response.Error != '') {
        $('#resetpassword span.errormessage').html(response.Error);
    } else {
        $('#resetpassword span.errormessage').html("Something went wrong! Please try again.");
    }

    $('#resetpassword div.errorblock').removeClass('hidden');
}


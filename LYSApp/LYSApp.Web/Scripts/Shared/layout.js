$(document).ready(function () {

    //if any error in log in
    if (loginError != '') {       
        $('#signin span.errormessage').html(loginError);
        $('#signin div.errorblock').removeClass('hidden');
        $('#signin').modal('show');
    }

    if (registerError != '') {
        $('#signup span.errormessage').html(registerError);
        $('#signup div.errorblock').removeClass('hidden');
        $('#signup').modal('show');
    }
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
                    $('#spnMessage').html(data);
                    $('#modal-message').modal('show');
                    //hide message modal after 5sec
                    window.setTimeout(function () { $('#modal-message').modal('hide'); }, 5000);


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
            var jmodel = { email: $("#txtEmailForSubscribe").val() };
            $.ajax({
                url: EmailSubscribeURL,
                type: 'POST',
                data: jmodel,
                dataType: 'html',
                success: function (data, textStatus, XMLHttpRequest) {
                    if (data.toUpperCase() == "SUCCESS") {
                        $('#subscribe-info').html('Thank you for you subscription!');
                    } else {
                        $('#subscribe-info').html("Whoops! Please try again later.");
                    }
                    setTimeout(function () {
                        $("#subscribe-info").hide('blind', {}, 500)
                    }, 5000);
                },
                error: function (xhr, status) {
                    alert('e')
                }
            })//ajax end
        } else {
            return false;
        }
    });
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
$(document).ready(function () {
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
            if (value.length!=10) {
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
            showProgress(false, "Listing your record. Please wait...");
            $.ajax({
                url: "/list-your-property",
                type: 'POST',
                data:  JSON.stringify(model),
                dataType: 'json',
                contentType: "application/json",
                success: function (data, textStatus, XMLHttpRequest) {
                    hideProgress();
                    if (data.toUpperCase() == "SUCCESS") {
                                
                        //show success modal
                                
                    }
                    else {
                        //show failure modal
                    }


                },
                error: function (xhr, status) {
                    hideProgress();
                    //show failure modal
                }


            });
        }

    });
});
/*----------------------------- End List your property functionality ---------------------------------------*/

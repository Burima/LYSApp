$(document).ready(function () {


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
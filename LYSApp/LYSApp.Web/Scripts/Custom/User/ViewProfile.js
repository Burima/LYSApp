
$(document).ready(function () {
    $(".numbersAlone").on("keyup keydown keypress", function (event) { return isNumberKey(event); });
    $(".charAlone").on(" keydown ", function (event) { return isCharField(event); });
    //if message show in  modal
    if (message != "") {
        $('#modalShowMessage').modal('show');               
    }
    $(".jcrop-holder").find("div").eq(0).addClass("formatDiv");
           
    $("#fileProfile").change(function (e) {
               
        var file = $(this).val();
        var ext = file.split('.').pop();
        if (ext.toLowerCase() == "gif" || ext.toLowerCase() == "jpeg" || ext.toLowerCase() == "jpg" || ext.toLowerCase() == "png") {
            readURL(this);
            $('#spnRemove').show();
        }
        else {
            fnDismissModal();
        }
    });
           
    $("#changePassword").click(function () {
        $("#modalChangePassword").modal({ backdrop: 'static', keyboard: false });
        //show the modal
        $('#modalChangePassword').modal('show');

    });

});
      
$('#btnChangePassword').click(function () {
    fnSaveLocation();
});

function fnSaveLocation() {
    var jmodel = { Password: $("#txtNewPassword").val()};

    showProgress(false, "Updating Password. Please wait...");
    $.ajax({
        url: UpdateLocationUrl,
        type: 'POST',
        data: jmodel,
        dataType: 'JSON',
        success: function (response, textStatus, XMLHttpRequest) {
            if (response.toUpperCase() == "SUCCESS") {
                $('#modalSelectCityAndArea').modal('hide');
                location.href = ApartmentsUrl;
            } else if (response.toUpperCase() == "FAILED") {
                alert('something went wrong. Please try again!');
                //Adding of new Apartment failed
            }
            hideProgress();
        },
        error: function (xhr, status) {
            alert('error');
            hideProgress();
        }
    });
}

function fnRemove() {
    $('#fileProfile').val("");
    $('#spnRemove').hide();
    $("#photoFilePath").html("No File Chosen");//
}

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {

            $("#divCropResizeImage").modal('show');
            $("#hdnImageSource").val(e.target.result);
            $('.jcrop-holder').replaceWith('');
            $("#divCropResizeImage .modal-body").empty();
            $("#divCropResizeImage .modal-body").append("<div class=\"row\" style=\"margin:5%;\">  <div class=\"col-lg-2\"></div><div class=\"col-lg-4\"><img id=\"demo3\" alt=\"Jcrop Example\" style=\"width: 100%\" /></div>");
            $("#divCropResizeImage .modal-body").append("</div></div></div></div>");
            $('#demo3').replaceWith('<img id="demo3" src="' + e.target.result + '"/>');
            FormImageCrop.init();

            $("#imgPreview").attr("src", e.target.result);
            $("div.jcrop-holder img").attr("src", e.target.result);

        }
        reader.readAsDataURL(input.files[0]);
    }
}

function fnLoadImage() {
    $("#hdnIsImageCropped").val("1");
    var imageCropWidth = $("#hdnImageCropWidth").val();
    var imageCropHeight = $("#hdnImageCropHeight").val();
    var cropPointX = $("#hdnCropPointX").val();
    var cropPointY = $("#hdnCropPointY").val();

    if (imageCropWidth == 0 && imageCropHeight == 0) {
        alert("Please select crop area.");
        return;
    }
    
    $.ajax({
        url: CropImageUrl,
        type: 'POST',
        data: {
            imagePath: $("#demo3").attr("src"),
            cropPointX: cropPointX,
            cropPointY: cropPointY,
            imageCropWidth: imageCropWidth,
            imageCropHeight: imageCropHeight,
            fileName: $("#hdnFileName").val()
        },
        success: function (data) {
            $("#divCropResizeImage").modal('hide');
            $("#fileProfile").files = data.PhotoPath;
            $("#imgProfile").attr("src", data.PhotoPath);
            $('#hdnFileName').val(data.filename);
            $('#profilePic').val(data.PhotoPath);
            document.getElementById("profile").src = data.PhotoPath;
        },
        error: function (data) { }
    });
}



function fnDismissModal() {
        $('#divCropResizeImage').modal('hide');
        $("#hdnIsImageCropped").val("0");
        $("#imgProfile").attr("src", $("#hdnImageSource").val());
        $("#divCropResizeImage .modal-body").empty();
        $("#divCropResizeImage .modal-body").append("<div class=\"row\"><div class=\"col-md-6\"><img id=\"demo3\" max-width=\"500px\" max-height=\"500px\" alt=\"Jcrop Example\" style=\"width: 100%\" /></div>");
        $("#divCropResizeImage .modal-body").append("<div class=\"col-md-6\"><div id=\"preview-pane\"><div class=\"preview-container\"><img id=\"imgPreview\" max-width=\"500px\" max-height=\"500px\" class=\"jcrop-preview\" alt=\"Preview\" />");
        $("#divCropResizeImage .modal-body").append("</div></div></div></div>");
}

$('#btnCroplose').click(function () {
    fnLoadImage();
});

$('#btnSkipCropping').click(function () {
    fnLoadImage();
});

$('#btnCrop').click(function () {
    fnLoadImage();
});
var FormImageCrop = function () {
    var demo3 = function () {
        // Create variables (in this scope) to hold the API and image size
        var jcrop_api,
            boundx,
            boundy,
            // Grab some information about the preview pane
            $preview = $('#preview-pane'),
            $pcnt = $('#preview-pane .preview-container'),
            $pimg = $('#preview-pane .preview-container img'),

            xsize = $pcnt.width(),
            ysize = $pcnt.height();

        //console.log('init', [xsize, ysize]);

        $('#demo3').Jcrop({
            onChange: updatePreview,
            onSelect: updatePreview,
            boxWidth: 500,   //Maximum width you want for your bigger images
            boxHeight: 300,  //Maximum Height for your bigger images         
            setSelect: [0, 0, 200, 100],
            aspectRatio: 1,
            allowSelect: true
        }, function () {
            // Use the API to get the real image size
            var bounds = this.getBounds();
            boundx = bounds[0];
            boundy = bounds[1];
            // Store the API in the jcrop_api variable
            jcrop_api = this;
            // Move the preview into the jcrop container for css positioning
            //$preview.appendTo(jcrop_api.ui.holder);
        });

        function updatePreview(c) {
            if (parseInt(c.w) > 0) {
                var rx = xsize / c.w;
                var ry = ysize / c.h;
                $("#hdnImageCropWidth").val(c.w);
                $("#hdnImageCropHeight").val(c.h);
                $("#hdnCropPointX").val(c.x);
                $("#hdnCropPointY").val(c.y);
                $pimg.css({
                    width: Math.round(rx * boundx) + 'px',
                    height: Math.round(ry * boundy) + 'px',
                    marginLeft: '-' + Math.round(rx * c.x) + 'px',
                    marginTop: '-' + Math.round(ry * c.y) + 'px'
                });
            }
        };
    }


    return {
        //main function to initiate the module
        init: function () {

            if (!jQuery().Jcrop) {;
                return;
            }
            demo3();
        }

    };

}();
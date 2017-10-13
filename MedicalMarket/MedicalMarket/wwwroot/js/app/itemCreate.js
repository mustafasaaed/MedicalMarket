$(document).ready(function () {
    $('input[name=images]').on('change', function () {
        var names = [];
        for (var i = 0; i < $(this).get(0).files.length; ++i) {
            names.push($(this).get(0).files[i].name);
        }

        for (var i = 0; i < names.length; i++) {
            var fileName = $(this).get(0).files[i].name;
            var idxDot = fileName.lastIndexOf(".") + 1;
            var extFile = fileName.substr(idxDot, fileName.length).toLowerCase();

            if ($.inArray(extFile, ['jpg', 'jpeg']) == -1) {
                alert('من فضلك اختار صور ب انتهاء "jpg او jpeg"');
                $('input[name=images]').val("");
            }
        }
    });
});
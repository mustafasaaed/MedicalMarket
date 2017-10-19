$(document).ready(function () {
    var delid;
    var doneid;

    $('a.donebtn').on('click', function () {
        $('#doneModal').modal('show');
        doneid = $(this).attr('data-id');
    });

    $('a.deletebtn').on('click', function () {
        $('#deleteModal').modal('show');
        delid = $(this).attr('data-id');
    });

    $('#del').on('click', function () {
        var token = $('input[name="__RequestVerificationToken"]').val();

        var data = {
            __RequestVerificationToken: token
        };

        $.post('/Orders/Delete/' + delid, data)
            .done(function (response, status, jqxhr) {
                $('#deleteModal').modal('hide');
                $('#row-' + response).fadeOut('slow');
            })
            .fail(function (jqxhr, status, error) {
                alert('something went wrong! please try again later !')
            });
    });


    $('#done-btn').on('click', function () {
        $.post('/Orders/done/' + doneid)
            .done(function (data) {
                $('#doneModal').modal('hide');
                $('#row-' + data).fadeOut('slow');
            })
            .fail(function (jqxhr, status, error) {
                alert('something went wrong! please try again later !')
            });
    });
});

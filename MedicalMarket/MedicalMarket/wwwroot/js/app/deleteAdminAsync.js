$(document).ready(function () {

    var adminEmail;
    var id;
    $('a.deletebtn').on('click', function () {
        $('#deleteModal').modal('show');
        adminEmail = $(this).attr('data-email');
        id = $(this).attr('data-id');
    });

    $('#del').on('click', function () {
        var token = $('input[name="__RequestVerificationToken"]').val();

        var data = {
            __RequestVerificationToken: token,
            email: adminEmail
        };

        console.log('email: ' + adminEmail);
        $.post('Admin/RemoveAdmin/' , data)
            .done(function (response, status, jqxhr) {
                $('#deleteModal').modal('hide');
                $('#row-' + id).fadeOut('slow');
            })
            .fail(function (jqxhr, status, error) {
                console.log(jqxhr);
                console.log(status);
                console.log(error);
                alert('something went wrong! please try again later !');
            });
    });
});

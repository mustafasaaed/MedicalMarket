$(document).ready(function () {

    var id;
    $('a.deletebtn').on('click', function () {
        $('#deleteModal').modal('show');
        id = $(this).attr('data-id');
    });

    $('#del').on('click', function () {
        var token = $('input[name="__RequestVerificationToken"]').val();

        var data = {
            __RequestVerificationToken: token
        };

        $.post('Items/Delete/' + id, data)  
            .done(function (response, status, jqxhr) {
                $('#deleteModal').modal('hide');
                location.reload(true);
            })
            .fail(function (jqxhr, status, error) {
                alert('something went wrong! please try again later !')
            });
    });
});

$(document).ready(function() {
    $('#deletebtn').on('click', function() {
        $('#deleteModal').modal('show');
    });

    $('#del').on('click', function() {
        var itemId = $('#deletebtn').attr('data-id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        var data = {
            __RequestVerificationToken : token
        };

        $.post('Categories/Delete/' + itemId, data)
            .done(function(response, status, jqxhr) {
                $('#deleteModal').modal('hide');
                location.reload(true);
            })
            .fail(function(jqxhr, status, error) {
                alert('something went wrong! please try again later !')
            });
    });
});

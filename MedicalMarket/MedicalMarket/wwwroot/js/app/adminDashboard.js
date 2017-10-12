$(document).ready(function () {
    $('#donebtn').on('click', function () {
        $('#doneModal').modal('show');
    });

    $('#deletebtn').on('click', function () {
        $('#deleteModal').modal('show');
    });

    $('#del').on('click', function () {
        var itemId = $('#deletebtn').attr('data-id');
        var token = $('input[name="__RequestVerificationToken"]').val();

        var data = {
            __RequestVerificationToken: token
        };

        $.post('/Orders/Delete/' + itemId, data)
            .done(function (response, status, jqxhr) {
                $('#deleteModal').modal('hide');
                $('#row-' + response).fadeOut('slow');
            })
            .fail(function (jqxhr, status, error) {
                alert('something went wrong! please try again later !')
            });
    });


    $('#done-btn').on('click', function () {
        var orderId = $('#donebtn').attr('data-id');
        $.post('/Orders/done/' + orderId)
            .done(function (data) {
                $('#doneModal').modal('hide');
                $('#row-' + data).fadeOut('slow');
            })
            .fail(function (jqxhr, status, error) {
                alert('something went wrong! please try again later !')
            });
    });
});

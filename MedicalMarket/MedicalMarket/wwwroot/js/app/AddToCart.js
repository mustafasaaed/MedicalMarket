﻿$(document).ready(function () {

    $('#addToCartBtn').on('click', function () {
        var itemId = $('#addToCartBtn').attr('data-id');
        var count = $('#units').val();
        var data = {
            Id: itemId,
            Count: count
        };
        $.post('/ShoppingCart/AddToCart/' + itemId,data)
            .done(function (res, status, jqxhr) {
                $('#addtocartModal').modal('show');
            })
            .fail(function () {
                $('#modalContent').html('عفوا, شئ ما قد حدث!')
            });
    });
});
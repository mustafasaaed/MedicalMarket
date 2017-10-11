$(document).ready(function () {


    var count = $('#itemsTable').children('tbody').children('tr').length;
    console.log(count);
    if (count <= 2) {
        $('#checkoutBtn').click(function (e) { e.preventDefault(); });
        $('#checkoutBtn').toggleClass("disabled");
    }   


    // Document.ready -> link up remove event handler
    $(".RemoveLink").click(function () {
        // Get the id from the link
        var recordToDelete = $(this).attr("data-id");

        if (recordToDelete != '') {

            // Perform the ajax post
            $.post("/ShoppingCart/RemoveFromCart", { "id": recordToDelete },
                function (data) {
                    // Successful requests get here
                    // Update the page elements
                    console.log(data);
                    if (data.itemCount == 0) {
                        $('#row-' + data.deleteId).fadeOut('slow');
                    } else {
                        $('#item-count-' + data.deleteId).text(data.itemCount);
                    }

                    $('#cart-total').text(data.cartTotal);
                    $('#update-message').text('تم حذف العنصر');
                    $('#cart-status').text('Cart (' + data.cartCount + ')');

                    var count = $('#itemsTable').length;
                    console.log(count);
                    if (count == 1) {
                        $('#checkoutBtn').click(function (e) { e.preventDefault(); });
                        $('#checkoutBtn').toggleClass("disabled");
                    }


                });
        }
    });



    function handleUpdate() {
        // Load and deserialize the returned JSON data
        var json = context.get_data();
        var data = Sys.Serialization.JavaScriptSerializer.deserialize(json);

        // Update the page elements
        if (data.ItemCount == 0) {
            $('#row-' + data.deleteId).fadeOut('slow');
        } else {
            $('#item-count-' + data.deleteId).text(data.itemCount);
        }

        $('#cart-total').text(data.cartTotal);
        $('#update-message').text(data.message);
        $('#cart-status').text('Cart (' + data.cartCount + ')');
    }
});
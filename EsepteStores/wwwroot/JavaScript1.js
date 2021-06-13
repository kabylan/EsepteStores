
function showCartItems() {

    var listsHtml = "";


    var items = JSON.parse(sessionStorage.getItem("cart"));


    items.map(i => listsHtml += "<tr><td class='text-center'>" + i.productName
        + "</td><td class='text-center'>" + i.productPrice + "</td></tr>");

    console.log(listsHtml);
    $('.productsList').html(listsHtml);
}

function addCartItemsToForm() {

    var listsHtml = "";

    var items = JSON.parse(sessionStorage.getItem("cart"));

    for (var i = 0; i < items.length; i++) {
        //listsHtml += "<input type='hidden' name='productId[" + i + "]' value='" + items[i].productId + "' />";
        listsHtml += "<input type='hidden' name='productIds[" + i + "]' value='" + items[i].productId + "' />";
    }
    console.log(listsHtml);
    $('.productsIds').html(listsHtml);
}


$(document).ready(function () {
    addCartItemsToForm();
    showCartItems();

    $('#sendFormButton').click(function () {
        clearItems();
        location.reload();
    });
});
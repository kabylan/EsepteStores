
function showCart() {
    var items = JSON.parse(sessionStorage.getItem("cart"));
    console.log(items);
}

// добавление
function addToCart(productId, productName, productPrice) {

    var items = JSON.parse(sessionStorage.getItem("cart"));

    items.push({ productId: productId, productName: productName, productPrice: productPrice, addedDateTime: new Date().toString() });

    sessionStorage.setItem("cart", JSON.stringify(items));

    countItems();

    addItemToListInNav();
}

// удаление
function removeFromCart(addedDateTime) {

    var items = JSON.parse(sessionStorage.getItem("cart"));

    // фильтр элемента которую удаляем
    items = items.filter(function (el) { return el.addedDateTime != addedDateTime; });

    sessionStorage.setItem("cart", JSON.stringify(items));

    countItems();

    addItemToListInNav();
}

function countItems() {

    if (!sessionStorage.getItem("cart")) {
        sessionStorage.setItem("cart", JSON.stringify([]));
    }

    var items = JSON.parse(sessionStorage.getItem("cart"));

    var itemsCount = items.length;

    $('#itemsCount').text(itemsCount);
}


function addItemToListInNav() {

    var listsHtml = "";
    var items = JSON.parse(sessionStorage.getItem("cart"));
    items.map(i => listsHtml += "<li><a href='/Details/" + i.productId + "'>" + i.productName + "</a></li>");

    $('.ulCartProducts').html(listsHtml);
}

function clearItems() {

    sessionStorage.setItem("cart", JSON.stringify([]));

    countItems();

    addItemToListInNav();
}

countItems();

addItemToListInNav();

$('.owl-carousel').owlCarousel({
    loop: false,
    margin: 10,
    responsive: {
        0: {
            items: 1
        },
        600: {
            items: 3
        },
        1000: {
            items: 5
        }
    }
});

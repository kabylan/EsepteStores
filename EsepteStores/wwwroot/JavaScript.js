
    //Работа с cookie для добавления товара на корзину
    // создать значение cookie для корзины
    if (!getCookie("cart")) document.cookie = "cart=[]";
     
    // добавление
        function addToCart(productId, productName, productPrice) {
            var items = JSON.parse(getCookie("cart"));
            items.push({ productId: productId, productName: productName, productPrice: productPrice, addedDateTime: new Date().toString() });
            document.cookie = "cart=" + JSON.stringify(items);
            countItems();
            addItemToListInNav();
        }
        // удаление
        function removeFromCart(addedDateTime) {
            var items = JSON.parse(getCookie("cart"));
            // фильтр элемента которую удаляем
            items = items.filter(function (el) { return el.addedDateTime != addedDateTime; });
            document.cookie = "cart=" + JSON.stringify(items);
            countItems();
        }
        function countItems() {
            var items = JSON.parse(getCookie("cart"));
            var itemsCount = items.length;
            $('#itemsCount').text(itemsCount);
        }
        // получаем cookie для корзины cart
        function getCookie(name) {
            var value = `; ${document.cookie}`;
            var parts = value.split(`; ${name}=`);
            if (parts.length === 2) return parts.pop().split(';').shift();
        }
        function addItemToListInNav() {

            var listsHtml = "";
            var items = JSON.parse(getCookie("cart"));
            items.map(i => listsHtml += "<li><a href='/Details/" + i.productId + "'>" + i.productName + "</a></li>");

            $('.ulCartProducts').html(listsHtml);
        }
        function clearItems() {
            document.cookie = "cart=[]";
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
        })

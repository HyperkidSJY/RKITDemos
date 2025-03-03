$(function () {

    const products = [
        { "ID": 1, "name": "Electronics", "categoryId": 0 , isExpanded: true, },
        { "ID": 2, "name": "Mobile Phones", "categoryId": 1 },
        { "ID": 3, "name": "Apple iPhone 13", "categoryId": 2, "price": 999 },
        { "ID": 4, "name": "Samsung Galaxy S21", "categoryId": 2, "price": 799 },
        { "ID": 5, "name": "Google Pixel 6", "categoryId": 2, "price": 599 },
        { "ID": 6, "name": "Laptops", "categoryId": 1 },
        { "ID": 7, "name": "Apple MacBook Pro", "categoryId": 6, "price": 1299 },
        { "ID": 8, "name": "Dell XPS 13", "categoryId": 6, "price": 999 },
        { "ID": 9, "name": "HP Spectre x360", "categoryId": 6, "price": 1099 },
        { "ID": 10, "name": "Clothing", "categoryId": 0 },
        { "ID": 11, "name": "Men's Clothing", "categoryId": 10 },
        { "ID": 12, "name": "T-Shirts", "categoryId": 11, "price": 20 },
        { "ID": 13, "name": "Jeans", "categoryId": 11, "price": 40 },
        { "ID": 14, "name": "Jackets", "categoryId": 11, "price": 60 },
        { "ID": 15, "name": "Women's Clothing", "categoryId": 10 },
        { "ID": 16, "name": "Dresses", "categoryId": 15, "price": 50 },
        { "ID": 17, "name": "Tops & Blouses", "categoryId": 15, "price": 30 },
        { "ID": 18, "name": "Skirts", "categoryId": 15, "price": 25 },
        { "ID": 19, "name": "Home Appliances", "categoryId": 0 },
        { "ID": 20, "name": "Refrigerators", "categoryId": 19, "price": 500 },
        { "ID": 21, "name": "LG Double Door", "categoryId": 20, "price": 700 },
        { "ID": 22, "name": "Samsung Side-by-Side", "categoryId": 20, "price": 750 },
        { "ID": 23, "name": "Whirlpool Top Freezer", "categoryId": 20, "price": 600 },
        { "ID": 24, "name": "Washing Machines", "categoryId": 19, "price": 350 },
        { "ID": 25, "name": "Bosch Front Load", "categoryId": 24, "price": 450 },
        { "ID": 26, "name": "Samsung Top Load", "categoryId": 24, "price": 400 },
        { "ID": 27, "name": "LG Semi-Automatic", "categoryId": 24, "price": 300 }
    ];

    $("#treeView").dxTreeView({
        dataSource: products,
        dataStructure: "plain",
        keyExpr: "ID",
        displayExpr: "name",
        parentIdExpr: "categoryId",
        itemTemplate: function (item) {
            if (item.price) {
                return `<div> ${item.name} ($${item.price}) </div>`;
            } else {
                return `<div> ${item.name} </div>`;
            }
        },
        searchEnabled: true,
        searchMode: "startswith",
        selectionMode: "single",
        selectByClick: true,
        onItemSelectionChanged: function(e) {
            const selectedProduct = e.itemData;
            if(selectedProduct.price) {
                $("#product-details").removeClass("hidden");
                $("#product-details > .price").text("$" + selectedProduct.price);
                $("#product-details > .name").text(selectedProduct.name);
            } else {
                $("#product-details").addClass("hidden");
            }
        },
        searchEditorOptions: {
            placeholder: "Type search value here...",
            width: 300
        },
        expandedExpr: 'isExpanded',

    });


});
$(function () {
    const categories = [
        {
            text: "Electronics",
            items: [
                {
                    text: "Mobile Phones",
                    items: [
                        { text: "Apple iPhone 13" },
                        { text: "Samsung Galaxy S21" },
                        { text: "Google Pixel 6" }
                    ]
                },
                {
                    text: "Laptops",
                    items: [
                        { text: "Apple MacBook Pro" },
                        { text: "Dell XPS 13" },
                        { text: "HP Spectre x360" }
                    ]
                },
                {
                    text: "Televisions",
                    items: [
                        { text: "Samsung QLED 4K" },
                        { text: "LG OLED TV" },
                        { text: "Sony Bravia 55-inch" }
                    ]
                }
            ]
        },
        {
            text: "Fashion",
            items: [
                {
                    text: "Men's Clothing",
                    items: [
                        { text: "T-Shirts" },
                        { text: "Jeans" },
                        { text: "Jackets" }
                    ]
                },
                {
                    text: "Women's Clothing",
                    items: [
                        { text: "Dresses" },
                        { text: "Tops & Blouses" },
                        { text: "Skirts" }
                    ]
                },
                {
                    text: "Accessories",
                    items: [
                        { text: "Watches" },
                        { text: "Sunglasses" },
                        { text: "Bags" }
                    ]
                }
            ]
        },
        {
            text: "Home Appliances",
            items: [
                {
                    text: "Refrigerators",
                    items: [
                        { text: "LG Double Door" },
                        { text: "Samsung Side-by-Side" },
                        { text: "Whirlpool Top Freezer" }
                    ]
                },
                {
                    text: "Washing Machines",
                    items: [
                        { text: "Bosch Front Load" },
                        { text: "Samsung Top Load" },
                        { text: "LG Semi-Automatic" }
                    ]
                },
                {
                    text: "Air Conditioners",
                    items: [
                        { text: "Carrier Split AC" },
                        { text: "Daikin Inverter AC" },
                        { text: "Voltas Window AC" }
                    ]
                }
            ]
        },
        {
            text: "Books",
            // disabled : true, 
            items: [
                {
                    text: "Fiction",
                    items: [
                        { text: "The Great Gatsby" },
                        { text: "To Kill a Mockingbird" },
                        { text: "1984" }
                    ]
                },
                {
                    text: "Non-Fiction",
                    items: [
                        { text: "Sapiens" },
                        { text: "Educated" },
                        { text: "Becoming" }
                    ]
                },
                {
                    text: "Children's Books",
                    items: [
                        { text: "Harry Potter" },
                        { text: "The Gruffalo" },
                        { text: "Matilda" }
                    ]
                }
            ]
        }
    ];

    const menu = $("#menu").dxMenu({
        items: categories,
        // orientation: "vertical",
        hideSubmenuOnMouseLeave: true,
        onItemClick: function (e) {
            if (e.itemData.text) {
                console.log(e.itemData.text + " has been clicked!");
            }
        },
        animation: {
            show: {
                type: 'fade',  // Fade-in animation when the menu is shown
                from: 0,       // Initial opacity
                to: 1,         // Final opacity
                duration: 300  // Duration of the animation in milliseconds
            },
            hide: {
                type: 'fade',  // Fade-out animation when the menu is hidden
                from: 1,       // Initial opacity
                to: 0,         // Final opacity
                duration: 300  // Duration of the animation in milliseconds
            }
        },
        // itemTemplate: function (itemData, itemIndex, itemElement) {
        //     itemElement.append("<i>" + itemData.text + "</i>");
        // },
        width: 500,
        showFirstSubmenuMode: "onHover",
        submenuDirection :  "leftOrTop" //'auto' | 'leftOrTop' | 'rightOrBottom' 
    }).dxMenu("instance");

    // Initialize a checkbox to toggle menu visibility
    $("#checkbox").dxCheckBox({
        text: 'Enable adaptivity',
        onValueChanged: function (e) {
            menu.option('adaptivityEnabled', e.value)
        }
    });
});
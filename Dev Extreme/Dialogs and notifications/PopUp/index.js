$(function () {
    const products = [
        {
            ID: 1,
            Name: "Apple iPhone 14 Pro",
            ManufactureDate: "2022-09-16",
            Price: "$999.99",
            Quantity: 200,
            Location: "Apple Store, San Francisco",
            Picture: "https://images.unsplash.com/photo-1537589376225-5405c60a5bd8?q=80&w=1964&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        },
        {
            ID: 2,
            Name: "Samsung Galaxy S22 Ultra",
            ManufactureDate: "2022-02-25",
            Price: "$1199.99",
            Quantity: 150,
            Location: "Samsung Store, New York",
            Picture: "https://images.unsplash.com/photo-1610945265064-0e34e5519bbf?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        },
        {
            ID: 3,
            Name: "Sony PlayStation 5",
            ManufactureDate: "2020-11-12",
            Price: "$499.99",
            Quantity: 100,
            Location: "Best Buy, Los Angeles",
            Picture: "https://images.unsplash.com/photo-1622297845775-5ff3fef71d13?q=80&w=2014&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        },
        {
            ID: 4,
            Name: "Microsoft Surface Laptop 4",
            ManufactureDate: "2021-04-15",
            Price: "$999.99",
            Quantity: 80,
            Location: "Microsoft Store, Chicago",
            Picture: "https://plus.unsplash.com/premium_photo-1670274609267-202ec99f8620?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        },
        {
            ID: 5,
            Name: "Apple MacBook Pro 16-inch (2021)",
            ManufactureDate: "2021-10-18",
            Price: "$2399.99",
            Quantity: 60,
            Location: "Apple Store, Los Angeles",
            Picture: "https://images.unsplash.com/photo-1611186871348-b1ce696e52c9?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
        }
    ];
    
    let product = null;

    const popupContentTemplate = function () {
        return $('<div>').append(
            $(`<p>Name: <span>${product.Name}</span></p>`),
            $(`<p>Manufacture Date: <span>${product.ManufactureDate}</span></p>`),
            $(`<p>Price: <span>${product.Price}</span></p>`),
            $(`<p>Stock Quantity: <span>${product.Quantity}</span></p>`),
            $(`<p>Location: <span>${product.Location}</span></p>`)
        );
    };

    const popup = $('#popup').dxPopup({
        contentTemplate: popupContentTemplate,
        width: 300,
        height: 280,
        container: '.dx-viewport',
        showTitle: true,
        title: 'Information',
        visible: false,
        dragEnabled: false,
        hideOnOutsideClick: true,
        showCloseButton: false,
        position: {
            at: 'bottom',
            my: 'center',
            collision: 'fit',
        },
        
        toolbarItems: [{
            locateInMenu: 'always',
            widget: 'dxButton',
            toolbar: 'top',
            options: {
                text: 'More info',
                onClick() {
                    const message = `More info about ${product.Name}`;
                    DevExpress.ui.notify({
                        message,
                        position: {
                            my: 'center top',
                            at: 'center top',
                        },
                    }, 'success', 3000);
                },
            },
        }, {
            widget: 'dxButton',
            toolbar: 'bottom',
            location: 'before',
            options: {
                icon: 'cart',
                stylingMode: 'contained',
                text: 'Order',
                onClick() {
                    const message = `Order has been palced for ${product.Name}`;
                    DevExpress.ui.notify({
                        message,
                        position: {
                            my: 'center top',
                            at: 'center top',
                        },
                    }, 'success', 3000);
                },
            },
        }, {
            widget: 'dxButton',
            toolbar: 'bottom',
            location: 'after',
            options: {
                text: 'Close',
                stylingMode: 'outlined',
                type: 'normal',
                onClick() {
                    popup.hide();
                },
            },
        }],
    }).dxPopup('instance');

    // Loop through products and create list
    products.forEach((currentProduct) => {
        $('<li>').append(
            $('<img>')
                .attr('alt', `${currentProduct.Name}`)
                .attr('src', currentProduct.Picture)
                .attr('id', `image${currentProduct.ID}`)
                .css('width', '100px'), // optional styling for images
            $('<br>'),
            $('<span>').html(`<i>${currentProduct.Name}</i>`),
            $('<br>'),
            $('<div>')
                .addClass('button-info')
                .dxButton({
                    text: 'Details',
                    onClick() {
                        product = currentProduct;
                        popup.option({
                            contentTemplate: () => popupContentTemplate(),
                            'position.of': `#image${product.ID}`,
                        });
                        popup.show();
                    },
                }),
        ).appendTo($('#products'));
    });
});
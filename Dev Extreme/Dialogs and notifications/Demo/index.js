$(function () {
    const serviceUrl = "https://67ac29aa5853dfff53d983ca.mockapi.io/api/products/products/";

    // Initialize LoadPanel for loading indication
    const loadPanel = $("#loader").dxLoadPanel({
        shading: true,
        position: { my: 'center', at: 'center' },
        visible: false,
        showIndicator: true,
        message: "Loading..."
    }).dxLoadPanel("instance");

    // Initialize CustomStore for CRUD operations
    const customStore = new DevExpress.data.CustomStore({
        key: "id",
        load: loadProducts,
        insert: insertProduct,
        update: updateProduct,
        remove: removeProduct,
    });

    // Create a Menu component
    const menu = $("#menu").dxMenu({
        displayExpr: "name",
        itemTemplate: function (itemData, itemIndex, itemElement) {
            let item = null;
            if (itemData.name === "Add Product") {
                item = $('<div>').text(itemData.name).attr("id", `addProduct`);
            } else {
                item = $('<div>').text(itemData.name).attr("id", `menu-${itemData.name}`);
            }
            itemElement.append(item);
        }
    }).dxMenu("instance");

    // Initialize empty menu categories
    const menuItems = [
        { name: "Electronics", id: "Electronics", items: [] },
        { name: "Wearables", id: "Wearables", items: [] },
        { name: "Home Appliances", id: "Home Appliances", items: [] },
        { name: "Audio", id: "Audio", items: [] },
    ];

    // Load the products and update the menu
    updateMenuWithProducts();

    menuItems.push({
        name: "Add Product",
        onClick: function () {
            openAddProductPopup();
        },
    });

    menu.option("items", menuItems);

    // Function to load products and populate the menu
    function loadProducts() {
        loadPanel.show();
        return $.get(serviceUrl)
            .then((data) => {
                loadPanel.hide();
                return data;
            })
            .catch((error) => {
                loadPanel.hide();
                console.log("Error loading data: ", error);
                throw "Data loading error";
            });
    }

    // Insert new product
    function insertProduct(values) {
        loadPanel.show();
        return $.post(serviceUrl, values)
            .then((data) => {
                loadPanel.hide();
                return data;
            })
            .catch((error) => {
                loadPanel.hide();
                console.log("Error inserting data: ", error);
                throw "Data insert error";
            });
    }

    // Update existing product
    function updateProduct(key, values) {
        loadPanel.show();
        return $.ajax({
            url: serviceUrl + key,
            method: "PUT",
            data: values
        })
            .then((data) => {
                loadPanel.hide();
                return data;
            })
            .catch((error) => {
                loadPanel.hide();
                console.log("Error updating data: ", error);
                throw "Data update error";
            });
    }

    // Remove product by key
    function removeProduct(key) {
        loadPanel.show();
        return $.ajax({
            url: serviceUrl + key,
            method: "DELETE"
        })
            .then(() => {
                loadPanel.hide();
                return { success: true };
            })
            .catch((error) => {
                loadPanel.hide();
                console.log("Error removing data: ", error);
                throw "Data remove error";
            });
    }

    // Update menu with products
    function updateMenuWithProducts() {
        return new Promise((resolve, reject) => {
            let promises = menuItems.map((category) => {
                return new Promise((resolveCategory, rejectCategory) => {
                    customStore.load().then((products) => {
                        const filteredProducts = products.filter(product => product.category === category.id);
                        category.items = filteredProducts.map(product => ({
                            name: product.name,
                            category: product.category,
                            price: product.price,
                            stockQuantity: product.stockQuantity,
                            supplier: product.supplier,
                            rating: product.rating,
                            reviews: product.reviews,
                            salesLastMonth: product.salesLastMonth,
                            releaseDate: product.releaseDate,
                            discount: product.discount,
                            shippingWeight: product.shippingWeight,
                            color: product.color,
                            location: product.location,
                            id: product.id,
                            onClick: function () {
                                mProduct = product;
                                popup.option({ contentTemplate: () => popupContentTemplate() });
                                popup.show();
                            }
                        }));

                        resolveCategory();
                    }).catch((error) => {
                        console.error("Error loading products:", error);
                        rejectCategory(error);
                    });
                });
            });

            Promise.all(promises).then(() => {
                menu.option("items", menuItems);
                resolve(menuItems);
            }).catch((error) => {
                reject(error);
            });
        });
    }

    // Popup Content Template 
    const popupContentTemplate = function () {
        return $('<div>')
            .append(
                $(`<p><strong>Name:</strong> ${mProduct.name}</p>`),
                $(`<p><strong>Category:</strong> ${mProduct.category}</p>`),
                $(`<p><strong>Price:</strong> $${mProduct.price}</p>`),
                $(`<p><strong>Stock Quantity:</strong> ${mProduct.stockQuantity}</p>`),
                $(`<p><strong>Supplier:</strong> ${mProduct.supplier}</p>`),
                $(`<p><strong>Rating:</strong> ${mProduct.rating}</p>`),
                $(`<p><strong>Reviews:</strong> ${mProduct.reviews}</p>`),
                $(`<p><strong>Sales Last Month:</strong> ${mProduct.salesLastMonth}</p>`),
                $(`<p><strong>Release Date:</strong> ${mProduct.releaseDate}</p>`),
                $(`<p><strong>Discount:</strong> ${mProduct.discount}%</p>`),
                $(`<p><strong>Shipping Weight:</strong> ${mProduct.shippingWeight}kg</p>`),
                $(`<p><strong>Color:</strong> ${mProduct.color}</p>`),
                $(`<p><strong>Location:</strong> ${mProduct.location}</p>`)
            );
    };

    // Popup for product details
    const popup = $("#popup").dxPopup({
        contentTemplate: popupContentTemplate,
        showTitle: true,
        title: 'More details',
        height: 600,
        dragEnabled: false,
        visible: false,
        toolbarItems: [{
            widget: 'dxButton',
            toolbar: 'bottom',
            location: 'before',
            options: {
                icon: 'trash',
                text: 'Delete',
                onClick() {
                    customStore.remove(mProduct.id).then(() => {
                        DevExpress.ui.notify({
                            message: `Product ${mProduct.name} deleted successfully`,
                        }, 'success', 3000);
                        popup.hide();
                        updateMenuWithProducts();
                    }).catch((error) => {
                        DevExpress.ui.notify({
                            message: `Error deleting product: ${error}`,
                        }, 'error', 3000);
                    });
                },
            },
        }],
    }).dxPopup('instance');

    // Open Add Product Popup
    function openAddProductPopup() {
        const addProductFormContent = function () {
            const $scrollView = $('<div/>');

            // Create the form and add DevExtreme dxForm inside the scroll view
            const $form = $('<div/>').dxForm({
                items: [{
                    dataField: 'productName',
                    label: { text: 'Product Name' },
                    editorType: 'dxTextBox',
                    editorOptions: {
                        placeholder: 'Enter product name',
                        showClearButton: true
                    },
                    validationRules: [{ type: 'required', message: 'Product Name is required' }]
                }, {
                    dataField: 'category',
                    label: { text: 'Category' },
                    editorType: 'dxSelectBox',
                    editorOptions: {
                        items: ['Electronics', 'Home Appliances', 'Wearables', 'Audio'],
                        value: 'Electronics'
                    },
                    validationRules: [{ type: 'required', message: 'Category is required' }]
                }, {
                    dataField: 'price',
                    label: { text: 'Price' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        value: 0
                    },
                    validationRules: [{ type: 'required', message: 'Price is required' }]
                }, {
                    dataField: 'stockQuantity',
                    label: { text: 'Stock Quantity' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        value: 0
                    },
                    validationRules: [{ type: 'required', message: 'Stock Quantity is required' }]
                }, {
                    dataField: 'supplier',
                    label: { text: 'Supplier' },
                    editorType: 'dxTextBox',
                    editorOptions: {
                        placeholder: 'Enter supplier name',
                        showClearButton: true
                    }
                }, {
                    dataField: 'rating',
                    label: { text: 'Rating' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        max: 5,
                        value: 0
                    }
                }, {
                    dataField: 'reviews',
                    label: { text: 'Reviews' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        value: 0
                    }
                }, {
                    dataField: 'salesLastMonth',
                    label: { text: 'Sales Last Month' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        value: 0
                    }
                }, {
                    dataField: 'releaseDate',
                    label: { text: 'Release Date' },
                    editorType: 'dxDateBox',
                    editorOptions: {
                        placeholder: 'Enter release date'
                    }
                }, {
                    dataField: 'discount',
                    label: { text: 'Discount' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        value: 0
                    }
                }, {
                    dataField: 'shippingWeight',
                    label: { text: 'Shipping Weight' },
                    editorType: 'dxNumberBox',
                    editorOptions: {
                        min: 0,
                        value: 0
                    }
                }, {
                    dataField: 'color',
                    label: { text: 'Color' },
                    editorType: 'dxTextBox',
                    editorOptions: {
                        placeholder: 'Enter color'
                    }
                }, {
                    dataField: 'location',
                    label: { text: 'Location' },
                    editorType: 'dxTextBox',
                    editorOptions: {
                        placeholder: 'Enter location'
                    }
                }]
            });

            // Wrap the form in a dxScrollView for scrolling
            $scrollView.append($form).dxScrollView({
                width: '100%',
                height: '100%',
                showScrollbar: 'always'
            });

            return $scrollView;
        };

        const addProductPopup = $("#addProductPopup").dxPopup({
            contentTemplate: addProductFormContent,
            showTitle: true,
            title: "Add Product",
            visible: true,
            width: 500, // Set width for the popup
            height: 600, // Set height for the popup
            dragEnabled: false,
            toolbarItems: [{
                locate: 'before',
                widget: 'dxButton',
                toolbar: 'bottom',
                options: {
                    text: 'Save',
                    onClick: function () {
                        const form = addProductPopup.content().find('.dx-form').dxForm('instance');
                        const formData = form.option('formData');

                        // Validate the form before submission
                        if (form.validate().isValid) {
                            const newProduct = {
                                name: formData.productName,
                                category: formData.category, 
                                price: formData.price,
                                stockQuantity: formData.stockQuantity,
                                supplier: formData.supplier,
                                rating: formData.rating,
                                reviews: formData.reviews,
                                salesLastMonth: formData.salesLastMonth,
                                releaseDate: formData.releaseDate,
                                discount: formData.discount,
                                shippingWeight: formData.shippingWeight,
                                color: formData.color,
                                location: formData.location
                            };

                            customStore.insert(newProduct).then(function (product) {
                                DevExpress.ui.notify({
                                    message: `Product ${product.name} added successfully`,
                                }, 'success', 3000);
                                addProductPopup.hide();
                                updateMenuWithProducts();
                            }).catch(function (error) {
                                DevExpress.ui.notify({
                                    message: `Error adding product: ${error}`,
                                }, 'error', 3000);
                            });
                        }
                    }
                },
            }]
        }).dxPopup('instance');
    }
});

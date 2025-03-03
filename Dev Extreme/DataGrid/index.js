
$(function () {
    const customStore = new DevExpress.data.CustomStore({
        key: "id",
        load: function () {
            return $.get("https://67ac29aa5853dfff53d983ca.mockapi.io/api/products/products")
                .then(function (data) {
                    return data;
                })
                .catch(function (error) {
                    console.log("Error loading data: ", error);
                    throw "Data loading error";
                });
        },

        // Insert method
        insert: function (values) {
            return $.post("https://67ac29aa5853dfff53d983ca.mockapi.io/api/products/products", values)
                .then(function (data) {
                    return data;
                })
                .catch(function (error) {
                    console.log("Error inserting data: ", error);
                    throw "Data insert error";
                });
        },

        // Update method - Using ID to identify the record to update
        update: function (key, values) {
            return $.ajax({
                url: "https://67ac29aa5853dfff53d983ca.mockapi.io/api/products/products/" + key, // Use the key (ID) to identify the record
                method: "PUT",
                data: values
            })
                .then(function (data) {
                    return data;
                })
                .catch(function (error) {
                    console.log("Error updating data: ", error);
                    throw "Data update error";
                });
        },

        // Delete method - Using ID to identify which record to delete
        remove: function (key) {
            console
            return $.ajax({
                url: "https://67ac29aa5853dfff53d983ca.mockapi.io/api/products/products/" + key, // Use the key (ID) to identify the record
                method: "DELETE"
            })
                .then(function () {
                    return { success: true };
                })
                .catch(function (error) {
                    console.log("Error removing data: ", error);
                    throw "Data remove error";
                });
        },

        // byKey: function (key) {
        //     return $.get("https://67ac29aa5853dfff53d983ca.mockapi.io/api/products/products" + key)
        //         .then(function (data) {
        //             return data;
        //         })
        //         .catch(function (error) {
        //             console.log("Error loading data: ", error);
        //             throw "Data loading error";
        //         });
        // }
    });

    const dataGrid = $("#gridContainer").dxDataGrid({
        dataSource: customStore,
        keyExpr: "id",
        columnAutoWidth: true,
        allowColumnResizing: true,
        allowColumnReordering: true,
        columnFixing: {
            enabled: true
        },
        columns: [
            { dataField: "id", validationRules: [{ type: "required" }] },
            {
                dataField: "name",
                validationRules: [{
                    type: "stringLength",
                    min: 3,
                    message: "Login should be at least 3 symbols long"
                }]
            },
            {
                dataField: "category",
                validationRules: [{ type: "required" }],
                lookup: {
                    dataSource: [
                        'Electronics',
                        'Home Appliances',
                        'Audio',
                        'Wearables',
                    ]
                },
                // filterOperations: ["contains", "="],
                // selectedFilterOperation: "contains",
                // filterValue: "Electronics" // predined filtering
            }, //groupIndex: 0
            {
                dataField: "price", validationRules: [{ type: "required" }], //sortOrder: "desc" //sortIndex : 0 , 1 , 2 , 3
                headerCellTemplate: $('<b><i>Price</i></b>'),
                customizeText: function (cellInfo) {
                    return "$" + cellInfo.value;
                }
            }, //column heaader template
            { dataField: "stockQuantity", validationRules: [{ type: "required" }] },
            { dataField: "supplier", validationRules: [{ type: "required" }] },
            {
                caption: "Rate",
                columns: ["rating", "reviews"], //band columns
            },
            { dataField: "salesLastMonth", validationRules: [{ type: "required" }] },
            { dataField: "releaseDate", dataType: "date", width: 100, validationRules: [{ type: "required" }] },
            { dataField: "discount", visible: false, validationRules: [{ type: "required" }], hidingPriority: 0 }, // first priority to hide we can hide many,
            { dataField: "shippingWeight", validationRules: [{ type: "required" }] },
            {
                dataField: "color",
                validationRules: [{ type: "required" }],
                cellTemplate: function (element, info) {
                    element.append("<div>" + info.text + "</div>")
                        .css("color", info.value);
                }
            },
            { dataField: "location", validationRules: [{ type: "required" }] },  //groupIndex : 1
            {
                type: "buttons",
                buttons: [
                    { name: "edit", cssClass: "my-edit-class" }, // Custom edit button class
                    { name: "delete", cssClass: "my-delete-class" } // Custom delete button class
                ] // you can add extra buttons also.
            },
            {
                caption: "Discounted Price",
                calculateCellValue: function (rowData) {
                    return rowData.price - (rowData.price * rowData.discount / 100);
                },
                customizeText: function (cellInfo) {
                    return "$" + cellInfo.value;
                }
            },
        ], // you need to specify all fields otherwise you will get only specified columns
        columnChooser: { enabled: true },
        sorting: { mode: "single" }, // single or multiple
        filterRow: { visible: true },
        searchPanel: { visible: true },
        groupPanel: { visible: true }, //you can skip group index if you add this.  
        editing: {
            mode: "Popup", //Batch
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            // selectTextOnEditStart: true,
            // startEditAction: 'click',
        },
        selection: { mode: "multiple" }, // multiple
        onSelectionChanged: function (e) {
            // e.component.byKey(e.currentSelectedRowKeys[0]).done(product => {
            //     if (product) {
            //         $("#selected-product").text(`Selected product: ${product.name}`);
            //     }
            // });
            e.component.refresh(true);
        },
        summary: {
            totalItems: [{ // groupItems :
                summaryType: "count", //sum", "avg", and "count",
                column: "id",
            },
            {
                name: 'SelectedRowsSummary',
                showInColumn: 'price', // Use the correct column name for the price field
                displayFormat: 'Sum: {0}',
                valueFormat: 'currency',
                summaryType: 'custom', // Custom summary type
            }],
            groupItems: [{
                name: "ProductsByCategory",
                column: "Category",
                summaryType: "count"
            }],
            calculateCustomSummary(options) {
                if (options.name === 'SelectedRowsSummary') {
                    if (options.summaryProcess === 'start') {
                        options.totalValue = 0;  // Initialize sum to 0
                    }
                    if (options.summaryProcess === 'calculate') {
                        // Add to total value if the row is selected
                        if (options.component.isRowSelected(options.value.id)) {
                            options.totalValue += options.value.price; // Use the correct field for price
                        }
                    }
                }
            },
        },
        sortByGroupSummaryInfo: [{
            summaryItem: "ProductsByCategory",  // or "count" | 0 | "OrderNumber"
            sortOrder: "desc"            // or "asc"
        }],
        masterDetail: {
            enabled: true,
            template: function (container, options) {
                const product = options.data;
                const name = $("<h3>").text(product.name);
                const price = $("<p>").text(product.price);
                container.append(name, price);
            }
        },
        export: {
            enabled: true
        },
        onExporting: function (e) {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet('Products');

            DevExpress.excelExporter.exportDataGrid({
                component: e.component,
                worksheet: worksheet
            }).then(function () {
                workbook.xlsx.writeBuffer().then(function (buffer) {
                    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Products.xlsx');
                });
            });
            e.cancel = true;
        },
        // onRowValidating: function (e) {
        //     if (e.isValid && e.newData.Login === "Administrator") {
        //         e.isValid = false;
        //         e.errorText = "You cannot log in as Administrator";
        //     }
        // }
        headerFilter: {
            visible: true,
            allowSearch: true
        },
        filterPanel: { visible: false },
        filterSyncEnabled: true,
        filterBuilder: {
            customOperations: [{
                name: "isGreaterThan",
                caption: "Greater Than",
                dataTypes: ["number"],
                hasValue: true,
                calculateFilterExpression: function (filterValue, field) {
                    return [field.dataField, ">", filterValue];
                }
            }]
        },
        filterBuilderPopup: {
            width: 400,
            title: "Synchronized Filter"
        },
        paging: {
            enabled: false,
            pageSize: 5,
            pageIndex: 0,   // Shows the second page
            showPageSizeSelector: true,
            // allowedPageSizes: [10, 20, 50],
            showNavigationButtons: true
        },
        scrolling: {
            mode: "standard", // or "virtual" | "infinite"
            scrollByContent: true,
            scrollByThumb: true,
            showScrollbar: "onHover" // or "onScroll" | "always" | "never"
        },
        grouping: {
            contextMenuEnabled: true
        },
        loadPanel: {
            enabled: true,
            indicatorSrc: "https://js.devexpress.com/Content/data/loadingIcons/rolling.svg"
        },
        stateStoring: {
            enabled: true,
            type: 'localStorage',
            storageKey: 'products',
        },
    }).dxDataGrid("instance");

    $("#deleteRowButton").dxButton({
        text: "Delete Row",
        onClick: function () {
            dataGrid.deleteRow(1);
        }
    });
    // dataGrid.clearSorting();

    $("#filter-button").dxButton({
        text: "Show Filter Builder",
        onClick: function () {
            dataGrid.option("filterBuilderPopup", { visible: true });
        }
    });

    $('#state-reset-link').on('click', () => {
        dataGrid.state(null);
    });

});

// Events - editing
// onRowInserting
// onRowInserted
// onRowUpdating
// onRowUpdated
// onRowRemoving
// onRowRemoved 

// events in group
// onRowExpanding: function (e) {
//     // Handler of the "rowExpanding" event
// },
// onRowExpanded: function (e) {
//     // Handler of the "rowExpanded" event
// },
// onRowCollapsing: function (e) {
//     // Handler of the "rowCollapsing" event
// },
// onRowCollapsed: function (e) {
//     // Handler of the "rowCollapsed" event
// }
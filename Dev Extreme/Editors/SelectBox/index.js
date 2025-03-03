$(function () {
    const data = [{
        ID: 1,
        Name: 'Banana',
        Category: 'Fruits',
        imgSrc: "https://images.unsplash.com/photo-1528825871115-3581a5387919?q=80&w=1015&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    }, {
        ID: 2,
        Name: 'Cucumber',
        Category: 'Vegetables',
        imgSrc: "https://images.unsplash.com/photo-1587411768638-ec71f8e33b78?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Y3VjdW1iZXJ8ZW58MHx8MHx8fDA%3D"
    }, {
        ID: 3,
        Name: 'Apple',
        Category: 'Fruits',
        imgSrc: "https://images.unsplash.com/photo-1576179635662-9d1983e97e1e?q=80&w=987&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    }, {
        ID: 4,
        Name: 'Tomato',
        Category: 'Vegetables',
        imgSrc: "https://images.unsplash.com/photo-1607305387299-a3d9611cd469?q=80&w=1170&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
    }, {
        ID: 5,
        Name: 'Apricot',
        Category: 'Fruits',
        imgSrc: "https://images.unsplash.com/photo-1602813812581-0713dae489da?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8N3x8YXByaWNvdHxlbnwwfHwwfHx8MA%3D%3D"
    }];

    const dataSource = new DevExpress.data.DataSource({
        store: data,
        type: "array",
        key: "ID",
        group: "Category"
    });

    $("#selectBox").dxSelectBox({
        dataSource: dataSource,
        valueExpr: "ID",
        displayExpr: "Name",
        searchEnabled: true,
        onValueChanged: function (e) {
            console.log(e.value);
            console.log(e.previousValue);
        },
        itemTemplate: function (itemData, itemIndex, itemElement) {
            return $("<div />").append(
                $("<img />").attr("src", itemData.imgSrc).css("width", "50px").css("margin-right", "10px"),
                $("<p />").text(itemData.Name).css("display", "inline-block")
            );
        },
        grouped: true,
        searchExpr: ['Name', 'ID'], //multiple
        searchMode: 'startswith',
        searchTimeout: 100, //milliseconds
        acceptCustomValue: true,
        onCustomItemCreating: function (e) {
            let nextId = data.length + 1;
            const customItem = {
                ID: nextId,
                Name: e.text,
                Category: 'Fruits',
                imgSrc: "https://via.placeholder.com/150?text=" + e.text
            };
            e.customItem = customItem; 
            e.component.getDataSource().store().insert(customItem)
                .done(function () {
                    // Reload the data source to reflect the new custom item
                    dataSource.reload();
                    // Set the newly added custom item as the selected value
                    e.component.option("value", customItem.ID);
                })
                .fail(function () {
                    console.log("Error adding custom item");
                });
        },
        showClearButton : true,
        spellcheck : true,
        showBeforeSearch : true,
        minSearchLength: 3,
        paginate: true,
        pageSize: 10,
    });
});
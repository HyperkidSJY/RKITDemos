$(function() {
    // 1. Create and Configure a Widget (DataGrid)
    let dataGridInstance = $("#dataGridContainer").dxDataGrid({
        dataSource: [
            { ID: 1, Name: "John Doe", Age: 29 },
            { ID: 2, Name: "Jane Smith", Age: 34 },
            { ID: 3, Name: "Sam Brown", Age: 41 }
        ],
        editing: {
            mode: "cell",
            allowUpdating: true
        },
        columns: ["ID", "Name", "Age"],
        onRowUpdating: function(e) {
            console.log("Row updating:", e);
        }
    }).dxDataGrid("instance");

    // 2. Get a Widget Instance
    $("#get-instance").click(function() {
        // Get the instance of the DataGrid widget
        let instance = $("#dataGridContainer").dxDataGrid("instance");
        alert("Widget Instance: " + instance);
    });

    // 3. Get and Set Options
    $("#get-set-options").click(function() {
        // Get options
        let dataSource = dataGridInstance.option("dataSource");
        let editMode = dataGridInstance.option("editing.mode");
        alert("DataSource: " + JSON.stringify(dataSource) + "\nEditing Mode: " + editMode);

        // Set new options
        dataGridInstance.option({
            dataSource: [
                { ID: 4, Name: "Alice Walker", Age: 25 },
                { ID: 5, Name: "Bob White", Age: 38 }
            ],
            editing: {
                mode: "batch",
                allowDeleting: true
            }
        });
        alert("Updated DataGrid's options!");
    });

    // 4. Call Methods
    $("#call-methods").click(function() {
        // Example: Calling `getDataSource().items()` to get all rows
        let allRows = dataGridInstance.getDataSource().items();
        alert("All Rows in DataGrid: " + JSON.stringify(allRows));
    });

    // 5. Handle Events (Example: Handling row update)
    $("#handle-events").click(function() {
        // Attach an event handler to the "rowUpdated" event
        dataGridInstance.on("rowUpdated", function(e) {
            alert("Row updated: " + JSON.stringify(e.data));
        });

        // Trigger an update to see the event in action
        dataGridInstance.option("dataSource")[0].Name = "Updated Name";
        dataGridInstance.refresh();  // Refresh the grid to trigger the event
    });

    // 6. Destroy a Widget
    $("#destroy-widget").click(function() {
        // Destroy the DataGrid widget
        $("#dataGridContainer").dxDataGrid("dispose"); //free up the allocated resources
        $("#dataGridContainer").remove(); //remove the UI component's associated DOM node
        alert("DataGrid widget has been destroyed!");
    });
});

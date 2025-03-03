$(function () {
    // ArrayStore with options
    var store = new DevExpress.data.ArrayStore({
        data: [
            { id: 1, name: "Shivam Yadav", age: 22 },
            { id: 2, name: "Rohit Rathod", age: 25 }
        ],
        key: "id",  // Specifies the key for the items

        // Event handler when data is loaded
        onLoaded: function (result) {
            console.log("Data Loaded: ", result);
        },

        // Event handler when a new item is added
        onInserted: function (values, key) {
            console.log("New User Added: ", values, "with key:", key);
        },

        // Event handler when an item is being inserted
        onInserting: function (values) {
            console.log("Inserting New User: ", values);
        },

        // Event handler when an item is updated
        onUpdated: function (key, values) {
            console.log("User Updated: Key:", key, "New Values:", values);
        },

        // Event handler when a user is being updated
        onUpdating: function (key, values) {
            console.log("Updating User: Key:", key, "New Values:", values);
        },

        // Event handler when an item is removed
        onRemoved: function (key) {
            console.log("User Removed: Key:", key);
        },

        // Event handler when an item is being removed
        onRemoving: function (key) {
            console.log("Removing User: Key:", key);
        },

        // Error handler for the store
        errorHandler: function (error) {
            console.log("Error: ", error.message);
            alert("An error occurred: " + error.message);
        },

        // Handler for changes before they are pushed to the store
        onPush: function (changes) {
            console.log("Changes about to be pushed: ", changes);
        }
    });

    // DevExtreme DataGrid to show and interact with the store data
    var grid = $("#grid").dxDataGrid({
        dataSource: store,
        columns: [
            { dataField: "id", caption: "ID" },
            { dataField: "name", caption: "Name" },
            { dataField: "age", caption: "Age" }
        ],
        editing: {
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            store.push([{ type: "insert", data: e.data }]);
            refreshGrid();
        },
        onRowUpdated: function (e) {
            store.push([{ type: "update", key: e.key, data: e.data }]);
            refreshGrid();
        },
        onRowRemoved: function (e) {
            store.push([{ type: "remove", key: e.key }]);
            refreshGrid();
        }
    }).dxDataGrid("instance");

    function refreshGrid() {
        grid.refresh(); // Refresh the grid to show updated data
    }

    // Add new user to the store
    $("#addUserBtn").on('click', function () {
        var newUser = { id: 3, name: "Sam Wilson", age: 28 };
        store.push([{ type: "insert", data: newUser }]);
        console.log("New User Added: ", newUser);
        refreshGrid();
    });

    // Clear all data in the store
    $("#clearBtn").on('click', function () {
        store.clear();
        console.log("All data has been cleared.");
        refreshGrid();
    });

    // Load data into the store (simulating async load)
    $("#loadBtn").on('click', function () {
        store.load().done(function () {
            console.log("Data successfully loaded.");
            refreshGrid();
        }).fail(function (error) {
            console.error("Error loading data:", error);
            alert("Failed to load data. Error: " + error.message);
        });
    });

    // Examples of using other methods

    // Insert new data item using insert method
    store.insert({ id: 4, name: "Peter Parker", age: 22 });

    // Fetch a specific item by key
    var user = store.byKey(1);
    console.log("User with key 1:", user);

    // Get the key of a specific data item
    var key = store.keyOf(user);
    console.log("Key of user:", key);

    // Create a query for filtering data
    // var query = store.createQuery();
    // query.filter("age", ">", 23).load().done(function (result) {
    //     console.log("Query result:", result);
    // }).fail(function (error) {
    //     console.error("Query failed:", error);
    // });

    // Update an item with a specific key
    store.update(1, { name: "Shivam Updated", age: 23 });

    // Remove an item by key
    store.remove(2);
    console.log("User with key 2 removed.");

    // Get the total count of items (using load with options)
    store.totalCount().done(function (count) {
        console.log("Total count of items:", count);
    }).fail(function (error) {
        console.error("Failed to get the total count:", error);
    });

    // Attach and detach event handlers
    store.on("push", function (changes) {
        console.log("Push event triggered with changes:", changes);
    });
    
    // Detach the event handler after 5 seconds
    setTimeout(function () {
        store.off("push");
        console.log("Event handler for 'push' detached.");
    }, 5000);
});
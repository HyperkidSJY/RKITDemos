$(function () {
    var customStore = new DevExpress.data.CustomStore({
        key: "id",
        cacheRawData : true,
        loadMode : "raw" ,  //processed
        take : 10 ,
        load: function () {
            return $.get("https://67a70408510789ef0dfcbb1f.mockapi.io/api/users")
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
            // Sanitize and ensure only the necessary fields are included
            const sanitizedValues = {
                name: values.name,
                email: values.email,
                id: values.id  // Only include these fields
            };

            return $.post("https://67a70408510789ef0dfcbb1f.mockapi.io/api/users", sanitizedValues)
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
            // Ensure the id field is included and properly mapped
            const updatedValues = {
                name: values.name,
                email: values.email,
                id: values.id // Ensure that the ID is included
            };

            return $.ajax({
                url: "https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/" + key, // Use the key (ID) to identify the record
                method: "PUT",
                data: updatedValues
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
                url: "https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/" + key, // Use the key (ID) to identify the record
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

        byKey: function (key) {
            return $.get("https://67a70408510789ef0dfcbb1f.mockapi.io/api/users/" + key)
                .then(function (data) {
                    return data;
                })
                .catch(function (error) {
                    console.log("Error loading data: ", error);
                    throw "Data loading error";
                });
        }
    });

    // Initialize DevExtreme DataGrid using Custom Store
    $("#grid").dxDataGrid({
        dataSource: customStore,
        columns: [
            { dataField: "id", caption: "ID" },
            { dataField: "name", caption: "Name" },
            { dataField: "email", caption: "Email" }
        ],
        editing: {
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true
        },
        onRowInserted: function (e) {
            customStore.push([{ type: "insert", data: e.data }]);
        },
        onRowUpdated: function (e) {
            customStore.push([{ type: "update", key: e.key, data: e.data }]);
        },
        onRowRemoved: function (e) {
            customStore.push([{ type: "remove", key: e.key }]);
        },
        onErrorOccurred: function (e) {
            alert("Error occurred: " + e.error.message);
        },
        onRowClick: function (e) {
            var rowData = e.data;
            customStore.byKey(rowData.id)
                .then(function (data) {
                    console.log("Fetched data for row ID:", rowData.id, data);
                })
                .catch(function (error) {
                    console.log("Error fetching data by key:", error);
                });
        },
    });
});

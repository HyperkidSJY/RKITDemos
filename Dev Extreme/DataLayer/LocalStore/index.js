$(function () {
    // Unique data for employees (local store)
    const employees = [
        { id: 1, firstName: "Sophia", lastName: "Leroy", role: "Developer" },
        { id: 2, firstName: "Aidan", lastName: "Chang", role: "Designer" },
        { id: 3, firstName: "Olivia", lastName: "Morris", role: "Manager" },
        { id: 4, firstName: "Ethan", lastName: "Brown", role: "QA" },
        { id: 5, firstName: "Isabella", lastName: "Johnson", role: "Developer" },
        { id: 6, firstName: "Mason", lastName: "Williams", role: "Designer" },
        { id: 7, firstName: "Mia", lastName: "Davis", role: "Manager" },
        { id: 8, firstName: "Jackson", lastName: "Smith", role: "Developer" },
        { id: 9, firstName: "Liam", lastName: "Garcia", role: "Developer" },
        { id: 10, firstName: "Amelia", lastName: "Rodriguez", role: "QA" },
        { id: 11, firstName: "Harper", lastName: "Martinez", role: "Manager" },
        { id: 12, firstName: "Lucas", lastName: "Lopez", role: "Designer" },
        { id: 13, firstName: "Maya", lastName: "Gonzalez", role: "Developer" },
        { id: 14, firstName: "James", lastName: "Hernandez", role: "QA" },
        { id: 15, firstName: "Ella", lastName: "Wilson", role: "Manager" },
    ];


    const localStore = new DevExpress.data.LocalStore({
        key: "id",  // The unique identifier for each record
        data: employees,
        name: "employees",
        errorHandler: function (error) {
            console.log(error.message);
        },
        immediate: false,
        flushInterval: 3000, // the data at which the store will be flushed in locastorage.
        onInserted: function (values, key) {
            console.log(values);
        },
        onLoaded: function (result) {
            console.log("data loaded");
        }
    });

    // Create the LocalStore with employees data
    const store = new DevExpress.data.DataSource({
        store: localStore,
        group: "role",
    });

    // Initialize dxSelectBox with the LocalStore as the data source
    const selectBox = $("#select-box").dxSelectBox({
        dataSource: store,
        displayExpr: "firstName",
        valueExpr: "id",  // Use the 'id' property as the value
        grouped: true,  // Enable grouping
        onValueChanged: function (e) {
            console.log("Selected Employee ID: ", e.value);  // Log the selected employee's ID
        }
    }).dxSelectBox('instance');

    $("#addEmployee").on("click", function () {
        const firstName = $("#firstName").val().trim();
        const lastName = $("#lastName").val().trim();
        const role = $("#role").val().trim();

        if (firstName && lastName && role) {
            const newEmployee = {
                id: localStore.totalCount() + 1,  // Assign a new unique ID based on the current count
                firstName: firstName,
                lastName: lastName,
                role: role,
            };

            // Add the new employee to the LocalStore
            localStore.insert(newEmployee);

            // Refresh the SelectBox by updating the DataSource
            store.load();

            // Clear input fields
            $("#firstName").val('');
            $("#lastName").val('');
            $("#role").val('');

            // Refresh the selectBox to show new data
            selectBox.refresh();

            alert("Employee added successfully!");
        } else {
            alert("Please fill all fields!");
        }
    });
});
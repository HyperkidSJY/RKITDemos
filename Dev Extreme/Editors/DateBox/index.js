$(function () {
    const currentDate = new Date();

    const disabledDates = [
        new Date("07/1/2024"),
        new Date("07/2/2024"),
        new Date("07/3/2024")
    ]

    // Initialize dxDateBox with key options
    const dateBox = $("#datebox").dxDateBox({
        value: currentDate,                          // Initial value set to current date
        type: "datetime",                            // Set format as DateTime
        displayFormat: "MM/dd/yyyy HH:mm",           // Display format for the DateTime
        min: new Date(2020, 0, 1),                   // Minimum selectable date (Jan 1, 2020)
        max: new Date(2025, 11, 31),                 // Maximum selectable date (Dec 31, 2025)
        validationMessageMode: 'always',             // Always show validation messages
        validationStatus: 'invalid',                 // Set validation status to invalid initially
        onValueChanged: function (e) {
            // Show the selected value
            $('#status').text('Selected Value: ' + e.value);
        },
        onFocusIn: function () {
            $('#status').text('Focused on DateBox');
        },
        onInitialized: function () {
            console.log("DateBox is initialized.");
        },
        onClosed: function () {
            console.log("DateBox dropdown is closed.");
        },
        placeholder: "Select a date",                 // Placeholder text
        showClearButton: true,                       // Show the Clear button
        showDropDownButton: true,                    // Show the drop-down button
        isValid: function () {                        // Custom validation logic
            const selectedDate = dateBox.option("value");
            return selectedDate && selectedDate >= new Date(2020, 0, 1) && selectedDate <= new Date(2025, 11, 31);
        },
        disabledDates: function (args) {
            const dayOfWeek = args.date.getDay();
            const month = args.date.getMonth();
            const isWeekend = args.view === "month" && (dayOfWeek === 0 || dayOfWeek === 6);
            const isMarch = (args.view === "year" || args.view === "month") && month === 2;

            return isWeekend || isMarch;
        },
    }).dxDateBox('instance');

    // Validate the DateBox
    $('#validate-datebox').on('click', function () {
        if (!dateBox.option('isValid')) {
            $('#status').text('DateBox value is invalid!');
        } else {
            $('#status').text('DateBox value is valid!');
        }
    });

    // Adjust the size of the DateBox component
    dateBox.option('width', 300);  // Set width to 300px

    // Additional events you can experiment with
    dateBox.on('onInput', function (e) {
        console.log('Input value changed: ', e.value);
    });

    // Change visibility dynamically
    // setTimeout(() => {
    //     dateBox.option('visible', false);  // Hide DateBox after 5 seconds
    //     console.log("DateBox hidden.");
    // }, 5000);

    // Initialize dxDateBox with key options
    const myDateBox = $("#myDateBox").dxDateBox({
        value: new Date(),                 // Initial value set to current date
        type: "datetime",                  // Set format as DateTime
        displayFormat: "MM/dd/yyyy HH:mm", // Display format for the DateTime
        placeholder: "Select a date",      // Placeholder text
        showClearButton: true,             // Show the Clear button
        showDropDownButton: true,          // Show the drop-down button
        onValueChanged: function (e) {
            $('#status').text('Selected Value: ' + e.value);
        }
    }).dxDateBox('instance');

    // Focus My DateBox
    $('#focus-myDateBox').on('click', function () {
        myDateBox.focus(); // Sets focus to the My DateBox input element
    });

    // Blur My DateBox
    $('#blur-myDateBox').on('click', function () {
        myDateBox.blur(); // Removes focus from the My DateBox input element
    });

    // Open Drop-down
    $('#open-myDateBox').on('click', function () {
        myDateBox.open(); // Opens the drop-down editor
    });

    // Close Drop-down
    $('#close-myDateBox').on('click', function () {
        myDateBox.close(); // Closes the drop-down editor
    });

    // Repaint My DateBox
    $('#repaint-myDateBox').on('click', function () {
        myDateBox.repaint(); // Repaints the UI component without reloading data
    });

    // Reset My DateBox
    $('#reset-myDateBox').on('click', function () {
        myDateBox.reset(); // Resets the value to the default value (empty)
    });

    // Dispose My DateBox
    $('#dispose-myDateBox').on('click', function () {
        myDateBox.dispose(); // Disposes of the resources allocated to the My DateBox instance
        $('#status').text("My DateBox has been disposed.");
    });

    // Begin Update
    $('#begin-update-myDateBox').on('click', function () {
        myDateBox.beginUpdate(); // Prevents the UI component from refreshing
        $('#status').text("My DateBox is in update mode.");
    });

    // End Update
    $('#end-update-myDateBox').on('click', function () {
        myDateBox.endUpdate(); // Refreshes the UI component after the beginUpdate() call
        $('#status').text("My DateBox update mode ended.");
    });



    // events in datebox
    $("#eventDateBox").dxDateBox({
        value: new Date(),
        type: "datetime",
        displayFormat: "yyyy-MM-dd HH:mm:ss",
        onChange: function(e) {
            $('#output').text("onChange: Value changed to " + e.value);
        },
        onClosed: function() {
            $('#output').text("onClosed: EventDateBox is closed.");
        },
        onContentReady: function() {
            $('#output').text("onContentReady: EventDateBox content is ready.");
        },
        onCopy: function() {
            $('#output').text("onCopy: Content copied.");
        },
        onCut: function() {
            $('#output').text("onCut: Content cut.");
        },
        onDisposing: function() {
            $('#output').text("onDisposing: EventDateBox is being disposed.");
        },
        onEnterKey: function() {
            $('#output').text("onEnterKey: Enter key pressed.");
        },
        onFocusIn: function() {
            $('#output').text("onFocusIn: EventDateBox got focus.");
        },
        onFocusOut: function() {
            $('#output').text("onFocusOut: EventDateBox lost focus.");
        },
        onInitialized: function() {
            $('#output').text("onInitialized: EventDateBox initialized.");
        },
        onInput: function(e) {
            $('#output').text("onInput: Value changed to " + e.value);
        },
        onKeyDown: function(e) {
            $('#output').text("onKeyDown: Key pressed: " + e.keyName);
        },
        onKeyUp: function(e) {
            $('#output').text("onKeyUp: Key released: " + e.keyName);
        },
        onOpened: function() {
            $('#output').text("onOpened: EventDateBox is opened.");
        },
        onOptionChanged: function(e) {
            $('#output').text("onOptionChanged: " + e.name + " changed.");
        },
        onPaste: function() {
            $('#output').text("onPaste: Content pasted.");
        },
        onValueChanged: function(e) {
            $('#output').text("onValueChanged: Value changed to " + e.value);
        }
    });
});
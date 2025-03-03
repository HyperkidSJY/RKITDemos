$(function() {
    // Initialize NumberBox with default settings
    $("#numberBoxContainer").dxNumberBox({
        value: 20,
        min: 16,
        max: 100,
        showSpinButtons: true,
        step: 10
    });

    // Create a NumberBox instance with custom settings
    const numberBox = $("#numericInput").dxNumberBox({
        id: "ageInput",
        name: "age",
        value: 25,
        step: 1,
        showSpinButtons: true,
        useLargeSpinButtons: true,
        placeholder: "Enter your age",
        disabled: false,
        readOnly: false,
        activeStateEnabled: true,
        focusStateEnabled: true,
        hoverStateEnabled: true,
        stylingMode: "outlined",
        width: "100%",
        height: "40px",
        hint: "Age must be between 18 and 100",
        validationMessageMode: "always",  // Always show validation message
        showClearButton: true,  // Allow clearing the value
        validationRules: [
            {
                type: "required",
                message: "Age is required"
            },
            {
                type: "range",
                message: "Age must be between 18 and 100",
                min: 18,
                max: 100
            },
            {
                type: "custom",
                message: "Invalid value. Please enter a valid number.",
                validation: function(value) {
                    console.log("Custom validation triggered: Checking if value is a number:", value);
                    return !isNaN(value) && value >= 18 && value <= 100;  // Ensure value is a number and within the valid range
                }
            }
        ],
        onValueChanged: function(e) {
            const value = e.value;
            console.log("Value changed:", value);
            $("#numericOutput").text("Your age: " + value);  // Display updated age value
            validateNumberBox(numberBox);  // Trigger validation on value change
        }
    }).dxNumberBox("instance");

    // Function to handle validation
    function validateNumberBox(numberBox) {
        console.log("Validating NumberBox...");

        const isValid = numberBox.option("isValid");
        const validationMessage = $("#validationMessage");

        // If the input is invalid, show the validation error message
        if (!isValid) {
            const errors = numberBox.option("validationErrors");
            console.log("Validation failed. Errors:", errors);
            if (errors.length > 0) {
                // Set validation status to "invalid" and show the message
                numberBox.option({
                    validationStatus: "invalid",
                    validationErrors: [{ message: errors[0].message }]  // Show the first error message
                });
                validationMessage.text(errors[0].message);  // Display the error message manually
            }
        } else {
            console.log("Validation passed.");
            // If valid, set status to "valid" and clear the validation message
            numberBox.option({
                validationStatus: "valid",
                validationErrors: []  // Clear any previous validation errors
            });
            validationMessage.text("");  // Clear the validation message
        }
    }

    // Trigger validation on input (each time the value changes)
    numberBox.option("onInput", function(e) {
        console.log("Input event triggered.");
        validateNumberBox(numberBox);  // Trigger validation on each input change
    });

    // Trigger initial validation on load
    validateNumberBox(numberBox);


    var priceBox = $("#priceBox").dxNumberBox({
        value: 100, // Default value
        min: 1,     // Minimum price
        max: 10000, // Maximum price
        showSpinButtons: true, // Allow spin buttons
        step: 1,     // Step size for incrementing/decrementing the value
        showClearButton: true, // Show a button to clear the value
        format: "currency", // Format the number as currency
        onValueChanged: function(e) {
            validatePrice(e.value);
        },
        onEnterKey: function(e) {
            logEvent("enterKey", "Enter key pressed!");
        },
        onInput: function(e) {
            logEvent("input", "Input changed: " + e.value);
        },
        onKeyDown: function(e) {
            logEvent("keyDown", "Key down: " + e.event.key);
        },
        onKeyUp: function(e) {
            logEvent("keyUp", "Key up: " + e.event.key);
        },
        onCopy: function(e) {
            logEvent("copy", "Text copied: " + e.value);
        },
        onCut: function(e) {
            logEvent("cut", "Text cut: " + e.value);
        },
        onPaste: function(e) {
            logEvent("paste", "Text pasted: " + e.value);
        },
        onOptionChanged: function(e) {
            logEvent("optionChanged", "Option changed: " + e.fullName);
        }
    }).dxNumberBox("instance");

    // Validation function to check if the price is within the valid range
    function validatePrice(value) {
        let validationMessage = $("#validationMessageM");
        if (value < 1) {
            validationMessage.text("Price must be at least $1.");
        } else if (value > 10000) {
            validationMessage.text("Price cannot exceed $10,000.");
        } else {
            validationMessage.text("");
        }
    }

    // Method to handle focus and blur
    $("#priceBox").on("focusin", function() {
        console.log("PriceBox focused!");
    }).on("focusout", function() {
        console.log("PriceBox lost focus!");
    });

    // Reset the value using the reset button
    $("#resetButton").on("click", function() {
        priceBox.reset();
        console.log("Price reset to null.");
        $("#validationMessageM").text(""); // Clear the validation message
    });

    // Demonstrate beginUpdate() and endUpdate()
    $("#beginUpdateButton").on("click", function() {
        priceBox.beginUpdate();
        priceBox.option("value", 2000); // Changes will not trigger immediate UI update
        console.log("Value updated but UI is not refreshed yet.");
    });

    $("#endUpdateButton").on("click", function() {
        priceBox.endUpdate(); // Refresh the UI after the value update
        console.log("UI refreshed after endUpdate().");
    });

    // Example of clearing the input value
    // setTimeout(function() {
    //     priceBox.resetOption("value");
    //     console.log("Price value reset to default using resetOption('value').");
    // }, 5000); // Reset after 5 seconds for demonstration

    function logEvent(eventType, data) {
        const logMessage = `${eventType}: ${data}`;
        const logElement = $("#eventsLog");
        logElement.append("<p>" + logMessage + "</p>");
        console.log(logMessage);  // Optionally log to console as well
    }

    setTimeout(function() {
        priceBox.option("min", 50); // Change minimum price limit
    }, 5000);
});

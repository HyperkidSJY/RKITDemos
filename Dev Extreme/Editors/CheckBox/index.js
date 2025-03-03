$(function () {
    $("#check-box").dxCheckBox({
        value: null, //null , false, true
        enableThreeStateBehavior: true, //true, false
        text: 'Approve',
        hint: 'Approve',
        iconSize: 25,
        onValueChanged: function (e) {
            if (e.value) {
                DevExpress.ui.notify("The CheckBox is checked", "success", 500);
            }
        },
        accessKey: "c", //shortcut key to access
        focusStateEnabled: true, //specify whether the ui component can be focused using key.
        activeStateEnabled: true, //it changes its visual state as a result of user interaction.
        elementAttr: {
            class: "custom_class"
        }, //it will add class name to it.
        hoverStateEnabled: true,
        name: "custom_name"
    });

    $("#disabled-box").dxCheckBox({
        value: null,
        enableThreeStateBehavior: true,
        disabled: true,
        text: "Disabled",
        height: 50,
        width: 50
    });


    //for checking dirty
    const dirtyCheckBox = $("#dirty-box").dxCheckBox({
        value: false, // Initially unchecked
    }).dxCheckBox('instance');

    $('#checkDirty').on('click', function () {
        const isDirty = dirtyCheckBox.option('isDirty');
        $('#dirty-status').text('Is Dirty: ' + isDirty);
    });
    setTimeout(function () {
        dirtyCheckBox.option('value', true); // Change the checkbox to checked
    }, 2000);


    //for validation
    const validateBox = $("#validate-box").dxCheckBox({
        value: false,
        validationMessageMode: 'always', // Validation message always visible
        validationMessagePosition: 'bottom', // Message appears below the checkbox
        validationStatus: 'invalid', // Initially invalid
        onValueChanged: function (e) {
            // Reset the error message when the value changes
            $('#validation-message').text('');
        }
    }).dxCheckBox('instance');
    // Set up the validator for the checkbox with a required rule
    const validator = validateBox.$element().dxValidator({
        validationRules: [{
            type: 'required',
            message: 'You must check this box to proceed.'
        }]
    }).dxValidator('instance');
    $('#validate').on('click', function () {
        // Manually trigger validation
        $('#validation-message').text('');
        const validationResult = validator.validate();
        if (validationResult.isValid === false) {
            // Show the validation error message
            $('#validation-message').text(validator.option('validationErrors')[0].message);
        } else {
            // If valid, show success message
            $('#validation-message').text('The checkbox is valid!');
        }
    });

    $("#read-only").dxCheckBox({
        value: true,
        text: "Read only",
        readOnly: true
    });


    const checkBox = $("#myCheckbox").dxCheckBox({
        value: false,
        text: "Accept Terms and Conditions",
        onValueChanged: function (e) {
            $('#status').text('CheckBox value changed to: ' + e.value);
        }
    }).dxCheckBox('instance');

    // beginUpdate() - Starts batching changes to the CheckBox
    //Prevents the UI component from refreshing until the endUpdate() method is called.
    $('#begin-update').on('click', function () {
        checkBox.beginUpdate();
        $('#status').text('CheckBox updates are batched.');
    });

    // endUpdate() - Applies the batched updates to the CheckBox
    $('#end-update').on('click', function () {
        checkBox.endUpdate();
        $('#status').text('CheckBox updates are applied.');
    });

    // dispose() - Disposes of the CheckBox
    $('#dispose-checkbox').on('click', function () {
        checkBox.dispose();
        $('#status').text('CheckBox disposed!');
    });

    //   focus() - Focuses on the CheckBox
    $('#focus-checkbox').on('click', function () {
        checkBox.focus();
        $('#status').text('CheckBox is focused!');
    });

    // reset() - Resets the value of the CheckBox to its initial state
    $('#reset-checkbox').on('click', function () {
        checkBox.reset();
        $('#status').text('CheckBox value reset to: ' + checkBox.option('value'));
    });

    //option() - Get the current value of an option (e.g., value, text)
    console.log('Current value option: ', checkBox.option('value')); // Logs the value option

    //Set new option using option(optionName, optionValue)
    checkBox.option('text', 'I accept the Terms');

    //Get an instance using getInstance(element)
    const checkboxInstance = $("#myCheckbox").dxCheckBox('instance');
    console.log('Instance using dxCheckBox("instance"):', checkboxInstance);


    //   registerKeyHandler() - Registers a custom key handler for the CheckBox
    checkBox.registerKeyHandler('enter', function () {
        alert('Enter key pressed!');
    });

    //   resetOption(optionName) - Resets an option to its default value
    checkBox.resetOption('value'); // Resets the 'value' option back to its default state
    $('#status').text('CheckBox value reset to: ' + checkBox.option('value'));

    //   on() - Attaches an event handler (in this case, onValueChanged)
    checkBox.on('valueChanged', function (e) {
        console.log('CheckBox value changed (on): ' + e.value);
    });

    //   off() - Removes the event handler (for example, off the 'valueChanged' handler)
    // checkBox.off('valueChanged');
    $('#status').text('Default text for all CheckBoxes set.');
});


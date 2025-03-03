$(function () {
    $("#textBoxContainer").dxTextBox({
        placeholder: "password",
        mode: "password", // 'email' | 'password' | 'search' | 'tel' | 'text' | 'url' 
        maxLength: 10,
        showClearButton: true,
        // readOnly : true,
        stylingMode: "underlined",
        // hint: 'Enter password',
    });

    $('#mask').dxTextBox({
        mask: '+91 (X00) 000-0000',
        maskRules: {
            X: /[7-9]/  // Indian mobile numbers start with 7, 8, or 9
        },
        inputAttr: { 'aria-label': 'Mask' },
        onValueChanged: function (e) {
            // Remove non-numeric characters to get only the raw phone number
            const rawValue = e.value.replace(/[^\d]/g, '');  // Strip out non-numeric characters
            console.log(rawValue);
            // Validate the cleaned-up value (it should be a 10-digit number starting with 7, 8, or 9)
            if (rawValue.length === 10 && /^[789]\d{9}$/.test(rawValue)) {
                console.log('Phone number value changed to:', e.value);  // Valid number, log it
            } else {
                alert("Invalid phone number! Please enter a valid number starting with 7, 8, or 9.");
                console.log('Invalid phone number!');  // Invalid number, show alert and log
            }
        },
        validationMessageMode: 'auto',
        validationError: 'Please enter a valid phone number starting with 7, 8, or 9.',
    });
    

    $('#full-name').dxTextBox({
        value: 'Smith',
        showClearButton: true,
        placeholder: 'Enter full name',
        inputAttr: { 'aria-label': 'Full Name' },
        valueChangeEvent: 'keyup',
        onValueChanged(data) {
            emailEditor.option('value', `${data.value.replace(/\s/g, '').toLowerCase()}@corp.com`);
        },
    });

    const emailEditor = $('#email').dxTextBox({
        value: 'smith@corp.com',
        readOnly: true,
        inputAttr: { 'aria-label': 'Email' },
        hoverStateEnabled: false,
    }).dxTextBox('instance');

});
$(function () {
    const sendRequest = function (value) {
        const existingProductNames = ['Product A', 'Product B', 'Product C'];
        const d = $.Deferred();
        setTimeout(() => {
            d.resolve(!existingProductNames.includes(value));
        }, 1000);
        return d.promise();
    };

    const maxDate = new Date();

    // Validation Summary
    $('#validationSummary').dxValidationSummary({});


    // Product Code Validation
    $('#product-code-validation').dxTextBox({
        placeholder: "Product Code (ABC-1234)",
        hint: "Enter Product Code",
        inputAttr: { 'aria-label': 'Product Code' },
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Product Code is required',
        }, {
            type: 'pattern',
            pattern: /^[A-Z]{3}-\d{4}$/,
            message: 'Product Code must match the pattern ABC-1234 (e.g., ABC-1234)',
        }],
    });

    // Product Name Validation
    $('#product-name-validation').dxTextBox({
        placeholder: "Product name",
        hint: "Product name",
        inputAttr: { 'aria-label': 'Product Name' },
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Product Name is required',
        }, {
            type: 'stringLength',
            min: 3,
            message: 'Product Name must have at least 3 characters',
        }, {
            type: 'async',
            message: 'Product Name already exists',
            validationCallback(params) {
                return sendRequest(params.value);
            },
        }, {
            type: 'custom',
            message: 'Product Name cannot contain the word "Test"',
            validationCallback(params) {
                const productName = params.value;
                return !productName.includes('Test');
            },
        }],
    });

    // Product Category Validation
    $('#category-validation').dxSelectBox({
        dataSource: ['Electronics', 'Furniture', 'Clothing', 'Food'],
        inputAttr: { 'aria-label': 'Product Category' },
        placeholder: "Catergory",
        hint: "Category",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Product Category is required',
        }],
    });

    // Product Price Validation
    $('#price-validation').dxNumberBox({
        inputAttr: { 'aria-label': 'Product Price' },
        min: 0.01,
        placeholder: "Price",
        hint: "Price",
        showSpinButtons: true,
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Product Price is required',
        }, {
            type: 'range',
            min: 0.01,
            message: 'Product Price must be a positive number',
        }, {
            type: 'numeric',
            message: 'Price must be a valid number',
        }],
    });

    // Manufacture Date Validation
    $('#manufacture-date-validation').dxDateBox({
        invalidDateMessage: 'The date must be in the format MM/dd/yyyy',
        inputAttr: { 'aria-label': 'Manufacture Date' },
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Manufacture Date is required',
        }, {
            type: 'range',
            max: maxDate,
            message: 'Manufacture Date cannot be in the future',
        }],
    });

    // Product Description Validation
    $('#description-validation').dxTextArea({
        inputAttr: { 'aria-label': 'Product Description' },
        height: 100,
    }).dxValidator({
        validationRules: [{
            type: 'stringLength',
            max: 250,
            message: 'Product Description cannot exceed 250 characters',
        }],
    });

    $('#confirm-password-validation').dxTextBox({
        placeholder: "Confirm Password",
        hint: "Confirm Password",
        inputAttr: { 'aria-label': 'Confirm Password' },
        mode: 'password'
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Confirm Password is required',
        }, {
            type: 'compare',
            comparisonTarget: function () {
                return $('#product-name-validation').dxTextBox('instance').option('value'); // Compare to product name, change to relevant field for passwords
            },
            message: "Confirm Password does not match Product Name"
        }]
    });

    // Email Rule Validation
    $('#email-validation').dxTextBox({
        placeholder: "Email",
        hint: "Email",
        inputAttr: { 'aria-label': 'Product Email' },
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Email is required',
        }, {
            type: 'email',
            message: 'Please enter a valid email address',
        }],
    });


    $('#form').on('submit', (e) => {
        DevExpress.ui.notify({
            message: 'You have submitted the form',
            position: {
                my: 'center top',
                at: 'center top',
            },
        }, 'success', 3000);

        e.preventDefault();
    });

    // Submit Button
    $('#submit-btn').dxButton({
        text: 'Register Product',
        type: 'default',
        width: '100%',
        useSubmitBehavior: true
    });
});
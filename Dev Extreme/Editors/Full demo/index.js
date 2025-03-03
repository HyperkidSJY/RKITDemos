$(function () {

    const subscriptions = ["Basic", "Standard", "Premium", "Custom"];
    const countries = [
        { id: 1, name: "India", continent: "Asia", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/in.svg" },
        { id: 2, name: "China", continent: "Asia", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/cn.svg" },
        { id: 3, name: "Japan", continent: "Asia", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/jp.svg" },

        { id: 4, name: "USA", continent: "North America", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/us.svg" },
        { id: 5, name: "Canada", continent: "North America", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/ca.svg" },
        { id: 6, name: "Mexico", continent: "North America", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/mx.svg" },

        { id: 7, name: "UK", continent: "Europe", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/gb.svg" },
        { id: 8, name: "Germany", continent: "Europe", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/de.svg" },
        { id: 9, name: "France", continent: "Europe", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/fr.svg" },

        { id: 10, name: "Brazil", continent: "South America", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/br.svg" },
        { id: 11, name: "Argentina", continent: "South America", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/ar.svg" },

        { id: 12, name: "Australia", continent: "Oceania", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/au.svg" },
        { id: 13, name: "New Zealand", continent: "Oceania", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/nz.svg" },

        { id: 14, name: "South Africa", continent: "Africa", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/za.svg" },
        { id: 15, name: "Nigeria", continent: "Africa", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/ng.svg" },
        { id: 16, name: "Kenya", continent: "Africa", flag: "https://cdnjs.cloudflare.com/ajax/libs/flag-icon-css/3.5.0/flags/4x3/ke.svg" }
    ];


    const dataSource = new DevExpress.data.DataSource({
        store: countries,
        type: "array",
        key: "ID",
        group: "continent"
    });

    const changePasswordMode = function (name) {
        const editor = $(name).dxTextBox('instance');
        editor.option('mode', editor.option('mode') === 'text' ? 'password' : 'text');
    };

    const gender = ["Male", "Female", "Other"];


    $("#full-name").dxTextBox({
        placeholder: "Full Name",
        hint: "Full Name",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Name is required',
        }, {
            type: 'pattern',
            pattern: /^[^0-9]+$/,
            message: 'Do not use digits in the Name.',
        }, {
            type: 'stringLength',
            min: 2,
            message: 'Name must have at least 2 symbols',
        }],
    });


    $("#email").dxTextBox({
        placeholder: "Email",
        hint: "Email",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Email is required',
        }, {
            type: 'email',
            message: 'Please enter a valid email address',
        }],
    });


    $("#password").dxTextBox({
        mode: 'password',
        inputAttr: { 'aria-label': 'Password' },
        mode: "password",
        onValueChanged() {
            const editor = $('#confirm-password').dxTextBox('instance');
            if (editor.option('value')) {
                $('#confirm-password').dxValidator('validate');
            }
        },
        buttons: [{
            name: 'password',
            location: 'after',
            options: {
                icon: 'eyeopen',
                stylingMode: 'text',
                onClick: () => changePasswordMode('#password'),
            },
        }],
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Password is required',
        },
        ]
    });


    $("#confirm-password").dxTextBox({
        mode: 'password',
        inputAttr: { 'aria-label': 'Password' },
        buttons: [{
            name: 'password',
            location: 'after',
            options: {
                icon: 'eyeopen',
                stylingMode: 'text',
                onClick: () => changePasswordMode('#confirm-password'),
            },
        }],
    }).dxValidator({
        validationRules: [{
            type: 'compare',
            comparisonTarget() {
                const password = $('#password').dxTextBox('instance');
                if (password) {
                    return password.option('value');
                }
                return null;
            },
            message: "'Password' and 'Confirm Password' do not match.",
        },
        {
            type: 'required',
            message: 'Confirm Password is required',
        }],
    });


    $("#address").dxTextArea({
        placeholder: "Address",
        hint: "Address",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Address is required',
        }, {
            type: 'stringLength',
            max: 250,
            message: 'Address cannot exceed 250 characters',
        }
        ]
    });


    $("#gender").dxRadioGroup({
        placeholder: "Gender",
        hint: "Gender",
        dataSource: gender,
        layout: "horizontal",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Gender is required',
        }
        ]
    });


    $("#birth-date").dxDateBox({
        placeholder: "Birthdate",
        hint: "Birthdate",
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Date of birth is required',
        }, {
            type: 'custom',
            message: 'You must be at least 18 years old.',
            validationCallback: function (params) {
                const birthDate = params.value;
                if (!birthDate) return false;  // If no date is provided, it's an invalid value

                const today = new Date();
                const age = today.getFullYear() - birthDate.getFullYear();
                const month = today.getMonth() - birthDate.getMonth();
                const day = today.getDate() - birthDate.getDate();

                // Adjust age if the birthday hasn't occurred yet this year
                if (month < 0 || (month === 0 && day < 0)) {
                    return age >= 18;
                }

                return age >= 18;
            }
        }
        ]
    });


    $("#profile-upload").dxFileUploader({
        placeholder: "profile picture",
        hint: "profile picture",
        accept: "image/*",// Only allow image files
        name: "files",
        uploadMode: "useButtons", // "useButtons", "instantly",
        // uploadUrl : "https://api.escuelajs.co/api/v1/files/upload"
    }).dxValidator({
        validationRules: [
            {
                type: 'required',
                message: 'Profile picture is required',
            },
            {
                type: 'custom',
                message: 'Please upload a valid image file',
                validationCallback: function(params) {
                    const file = params.value;
                    if (file && file[0]) {
                        const fileType = file[0].type;
                        // Check if the file is an image (optional check for file type)
                        return fileType.startsWith("image/");
                    }
                    return false;
                }
            }
        ]
    });


    $("#country").dxSelectBox({
        placeholder: "Country",
        hint: "Country",
        dataSource: dataSource,
        valueExpr: "id",
        displayExpr: "name",
        searchEnabled: true,
        grouped: true,
        searchExpr: ['name', 'id'], //multiple
        searchMode: 'startswith',
        searchTimeout: 100, //milliseconds
        itemTemplate: function (itemData, itemIndex, itemElement) {
            return $("<div />").append(
                $("<img />").attr("src", itemData.flag).css("width", "20px").css("margin-right", "10px"),
                $("<p />").text(itemData.name).css("display", "inline-block").css("height", "15px")
            );
        },
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Country is required',
        },
        ]
    });


    $("#subscription").dxDropDownBox({
        value: [],
        dataSource: subscriptions,
        placeholder: "Subscription",
        hint: "Subscription",
        labelMode: "floating",
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: subscriptions,
                selectionMode: "multiple",
                showSelectionControls: true,
                onSelectionChanged: function (arg) {
                    let selectedItems = e.component.option("value");
                    const addedItems = arg.addedItems;
                    const removedItems = arg.removedItems;
                    selectedItems = [...selectedItems, ...addedItems];
                    selectedItems = selectedItems.filter(item => !removedItems.includes(item));
                    e.component.option("value", selectedItems);
                },
            });
            list = $list.dxList('instance');
            return $list;
        },
        openOnFieldClick: true,
    }).dxValidator({
        validationRules: [{
            type: 'required',
            message: 'Please choose subscription',
        },
        ]
    });


    $("#newsletters").dxCheckBox({
        hint: "news letters",
    });


    $("#terms-conditions").dxCheckBox({
        hint: "terms and conditions",
        value: false

    }).dxValidator({
        validationRules: [{
            type: 'compare',
            comparisonTarget() { return true; },
            message: 'You must agree to the Terms and Conditions',
        }],
    });

     const saveToSession = () => {
        const formData = {
            fullName: $("#full-name").dxTextBox("instance").option("value"),
            email: $("#email").dxTextBox("instance").option("value"),
            password: $("#password").dxTextBox("instance").option("value"),
            confirmPassword: $("#confirm-password").dxTextBox("instance").option("value"),
            address: $("#address").dxTextArea("instance").option("value"),
            gender: $("#gender").dxRadioGroup("instance").option("value"),
            birthDate: $("#birth-date").dxDateBox("instance").option("value"),
            profilePicture: $("#profile-upload").dxFileUploader("instance").option("value"),
            country: $("#country").dxSelectBox("instance").option("value"),
            subscription: $("#subscription").dxDropDownBox("instance").option("value"),
            newsletters: $("#newsletters").dxCheckBox("instance").option("value"),
            termsConditions: $("#terms-conditions").dxCheckBox("instance").option("value")
        };

        // Store the form data in session storage
        sessionStorage.setItem("formData", JSON.stringify(formData));
    };

    const resetForm = () => {
        $("#full-name").dxTextBox("instance").reset();
        $("#email").dxTextBox("instance").reset();
        $("#password").dxTextBox("instance").reset();
        $("#confirm-password").dxTextBox("instance").reset();
        $("#address").dxTextArea("instance").reset();
        $("#gender").dxRadioGroup("instance").reset();
        $("#birth-date").dxDateBox("instance").reset();
        $("#profile-upload").dxFileUploader("instance").reset();
        $("#country").dxSelectBox("instance").reset();
        $("#subscription").dxDropDownBox("instance").reset();
        $("#newsletters").dxCheckBox("instance").reset();
        $("#terms-conditions").dxCheckBox("instance").reset();
    };

    $('#form').on('submit', (e) => {
        saveToSession();
        DevExpress.ui.notify({
            message: 'You have submitted the form',
            position: {
                my: 'center top',
                at: 'center top',
            },
        }, 'success', 3000);
        resetForm();
        e.preventDefault();
    });


    // $("#reset").dxButton({
    //     hint: "Reset",
    //     text: 'Reset Form',
    //     type: 'danger',
    //     width: '100%',
    //     onClick: () => {
    //         resetForm();
    //     }
    // });


    $("#submit").dxButton({
        hint: "submit",
        text: 'Register',
        type: 'default',
        width: '100%',
        useSubmitBehavior: true
    });

    // Validation Summary
    $('#validationSummary').dxValidationSummary({});

});
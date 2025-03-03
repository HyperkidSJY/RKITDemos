const fruits = ["Apples", "Oranges", "Lemons", "Pears", "Pineapples"];
const dataSource = fruits;
let list;


$(function () {
    $("#drop-down-box").dxDropDownBox({
        value : [],
        dataSource,
        label: "Fruits",
        labelMode: "floating",
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource,
                allowItemDeleting: true,
                onItemDeleting: function (e) {
                    if (dataSource.length === 1) {
                        e.cancel = true;
                    }
                },
                selectionMode: "multiple",
                showSelectionControls : true,
                onSelectionChanged: function (arg) {
                    let selectedItems = e.component.option("value");
                    const addedItems = arg.addedItems;
                    const removedItems = arg.removedItems;

                    // Add selected items to the value array
                    selectedItems = [...selectedItems, ...addedItems];
                    
                    // Remove unselected items from the value array
                    selectedItems = selectedItems.filter(item => !removedItems.includes(item));

                    e.component.option("value", selectedItems);
                    // e.component.close();
                },
            });
            list = $list.dxList('instance');
            return $list;
        },
        acceptCustomValue: true,
        openOnFieldClick: false,
        onEnterKey: function (e) {
            dataSource.push(e.component.option("value"));
            e.component.option("value", "");
            list.reload();
        }
    });

    //synchronise it with other elements.
    // Initialize a simple drop-down box with a list of fruits
    $("#fruitDropDown").dxDropDownBox({
        value: "Oranges", // Set default value to one of the items in the dataSource
        dataSource: ["Apples", "Oranges", "Bananas", "Pears", "Grapes"],
        label: "Select Fruit",
        contentTemplate: function (e) {
            // Display the list of fruits inside the DropDownBox
            const $list = $("<div>").dxList({
                dataSource: e.component.option("dataSource"),
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    // When a fruit is selected, update the value of DropDownBox and TextBox
                    if (arg.addedItems.length > 0) {
                        e.component.option("value", arg.addedItems[0]);
                        e.component.close();
                    }
                }
            });
            return $list;
        },
        onValueChanged: function (e) {
            // When the value changes in the DropDownBox, update the TextBox value
            $("#fruitTextBox").dxTextBox("instance").option("value", e.value);
        }
    });

    // Initialize a TextBox where changes will reflect in the DropDownBox
    $("#fruitTextBox").dxTextBox({
        value: "Oranges", // Set the default value of the text box as well
        onValueChanged: function (e) {
            // When the TextBox value changes, update the DropDownBox value
            $("#fruitDropDown").dxDropDownBox("instance").option("value", e.value);
        }
    });



    // Define data for countries
    const countries = [
        { id: 1, name: "United States", capital: "Washington, D.C.", population: "331 million" },
        { id: 2, name: "Canada", capital: "Ottawa", population: "38 million" },
        { id: 3, name: "Germany", capital: "Berlin", population: "83 million" },
        { id: 4, name: "India", capital: "New Delhi", population: "1.38 billion" },
        { id: 5, name: "Australia", capital: "Canberra", population: "25 million" }
    ];

    // Initialize the DropDownBox with contentTemplate and validation
    const dropDownBox = $("#countrySelector").dxDropDownBox({
        value: null,  // No value initially
        dataSource: countries,  // Data for countries
        displayExpr: "name",  // Display the country name
        valueExpr: "id",  // Use the country id as the value
        placeholder: "Select a country",  // Placeholder text
        showClearButton: true,  // Show Clear button
        showDropDownButton: true,  // Show Drop-down button
        acceptCustomValue: false,  // Do not allow custom input
        focusStateEnabled: true,  // Enable focus state (for keyboard navigation)
        hoverStateEnabled: true,  // Enable hover effect
        width: 350,  // Set width of the DropDownBox
        height: 40,  // Set height of the DropDownBox
        validationMessageMode: "auto",  // Automatically show validation messages
        disabled: false,  // Ensure the DropDownBox is enabled

        // Validation Rules
        validationRules: [
            {
                type: "required",  // Validate that a country is selected
                message: "Please select a country."
            }
        ],

        onValueChanged: function (e) {
            const selectedCountry = countries.find(country => country.id === e.value);

            // Clear validation message when the user selects a country
            $("#validationMessage").text("");

            if (selectedCountry) {
                // Show country details below the DropDownBox
                $("#countryCapital").html(`<b>Capital:</b> ${selectedCountry.capital}`);
                $("#countryPopulation").html(`<b>Population:</b> ${selectedCountry.population}`);
            } else {
                // Clear the country details when no value is selected
                $("#countryCapital").html("");
                $("#countryPopulation").html("");
            }
        },

        // Use contentTemplate to customize the drop-down list
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: countries, // List items
                allowItemDeleting: true, // Allow item deletion
                selectionMode: "single", // Allow single item selection
                onItemDeleting: function (itemDeletingEvent) {
                    // Prevent deletion if only one item is left
                    if (countries.length === 1) {
                        itemDeletingEvent.cancel = true;
                    }
                },
                onSelectionChanged: function (arg) {
                    if (arg.addedItems.length > 0) {
                        const selectedCountry = arg.addedItems[0];
                        e.component.option("value", selectedCountry.id); // Set the value in DropDownBox
                        e.component.close(); // Close the drop-down
                    }
                },
                itemTemplate: function (data) {
                    const $item = $("<div>")
                        .addClass("custom-list-item")
                        .text(data.name)
                        .on("click", function () {
                            e.component.option("value", data.id); // Set selected country in DropDownBox
                            e.component.close(); // Close the drop-down on selection
                        });
                    return $item;
                }
            });

            return $list;
        }
    }).dxDropDownBox("instance"); // Get the instance of the DropDownBox

    // Add a submit button and check validation on form submission
    // Add a submit button and check validation on form submission
    $("#submitBtn").on("click", function () {
        // Trigger validation manually by checking if a value is selected
        const selectedCountry = dropDownBox.option("value");

        // Check if a valid country is selected
        if (!selectedCountry) {
            // If no country is selected, show the validation message
            const errorMessage = "Please select a country.";
            $("#validationMessage").text(errorMessage);  // Show validation error message
            dropDownBox.option({
                validationStatus: "invalid",
                validationErrors: [{ message: errorMessage }]  // Set error message
            });
        } else {
            // If validation passes, clear validation errors
            dropDownBox.option({
                validationStatus: "valid",
                validationErrors: []  // Clear any validation errors
            });
            $("#validationMessage").text("");  // Clear validation message
            alert("Form submitted successfully!");  // Proceed with submission logic
        }
    });

    const regions = [
        { id: 1, name: "North America" },
        { id: 2, name: "Europe" },
        { id: 3, name: "Asia" },
        { id: 4, name: "Africa" },
        { id: 5, name: "Australia" }
    ];
    const colors = ["Red", "Green", "Blue", "Yellow", "Pink"];

    const fruitSelector = $("#fruitSelector").dxDropDownBox({
        dataSource: fruits,
        placeholder: "Select a fruit",
        showClearButton: true,
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: fruits,
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    if (arg.addedItems.length > 0) {
                        e.component.option("value", arg.addedItems[0]);
                        e.component.close();
                    }
                },
                itemTemplate: function (data) {
                    const $item = $("<div>").addClass("custom-list-item").text(data);
                    return $item;
                }
            });
            return $list;
        },
        onValueChanged: function (e) {
            if (!e.value) {
                $("#fruitErrorMessage").text("Please select a fruit.");
            } else {
                $("#fruitErrorMessage").text("");
            }
        }
    }).dxDropDownBox("instance");

    // Region DropDownBox (Previously countrySelector)
    const regionSelector = $("#regionSelector").dxDropDownBox({
        dataSource: regions,
        displayExpr: "name",
        valueExpr: "id",
        placeholder: "Select a region",
        showClearButton: true,
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: regions,
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    if (arg.addedItems.length > 0) {
                        e.component.option("value", arg.addedItems[0].id);
                        e.component.close();
                    }
                },
                itemTemplate: function (data) {
                    const $item = $("<div>").addClass("custom-list-item").text(data.name);
                    return $item;
                }
            });
            return $list;
        },
        onValueChanged: function (e) {
            if (!e.value) {
                $("#regionErrorMessage").text("Please select a region.");
            } else {
                $("#regionErrorMessage").text("");
            }
        }
    }).dxDropDownBox("instance");

    // Color DropDownBox
    const colorSelector = $("#colorSelector").dxDropDownBox({
        dataSource: colors,
        placeholder: "Select a color",
        showClearButton: true,
        contentTemplate: function (e) {
            const $list = $("<div>").dxList({
                dataSource: colors,
                selectionMode: "single",
                onSelectionChanged: function (arg) {
                    if (arg.addedItems.length > 0) {
                        e.component.option("value", arg.addedItems[0]);
                        e.component.close();
                    }
                },
                itemTemplate: function (data) {
                    const $item = $("<div>").addClass("custom-list-item").text(data);
                    return $item;
                }
            });
            return $list;
        },
        onValueChanged: function (e) {
            if (!e.value) {
                $("#colorErrorMessage").text("Please select a color.");
            } else {
                $("#colorErrorMessage").text("");
            }
        }
    }).dxDropDownBox("instance");

    // Submit button logic
    $("#submitBtnM").on("click", function () {
        // Check if each DropDownBox has a selected value
        const fruitValue = fruitSelector.option("value");
        const regionValue = regionSelector.option("value");
        const colorValue = colorSelector.option("value");

        if (!fruitValue) {
            $("#fruitErrorMessage").text("Please select a fruit.");
        } else {
            $("#fruitErrorMessage").text("");
        }

        if (!regionValue) {
            $("#regionErrorMessage").text("Please select a region.");
        } else {
            $("#regionErrorMessage").text("");
        }

        if (!colorValue) {
            $("#colorErrorMessage").text("Please select a color.");
        } else {
            $("#colorErrorMessage").text("");
        }

        // If all selections are made, submit successfully
        if (fruitValue && regionValue && colorValue) {
            alert("Form submitted successfully!");
        }
    });

    // Example usage of methods
    setTimeout(function () {
        // Focus on the region selector after 2 seconds
        regionSelector.focus();
    }, 2000);

    setTimeout(function () {
        // Open fruit selector after 4 seconds
        fruitSelector.open();
    }, 4000);

    setTimeout(function () {
        // Close color selector after 6 seconds
        colorSelector.close();
    }, 6000);

    setTimeout(function () {
        // Reset all values after 8 seconds
        fruitSelector.reset();
        regionSelector.reset();
        colorSelector.reset();
    }, 8000);

    setTimeout(function () {
        // Use beginUpdate and endUpdate to prevent UI refresh
        fruitSelector.beginUpdate();
        regionSelector.beginUpdate();
        colorSelector.beginUpdate();

        setTimeout(function () {
            // Simulate a data update after 10 seconds
            fruitSelector.endUpdate();
            regionSelector.endUpdate();
            colorSelector.endUpdate();
        }, 2000);
    }, 10000);

});

$(function () {

    const data = [
        { name: "Banana", category: "Fruits", color: "yellow" }, // disabled: true 
        { name: "Apple", category: "Fruits", color: "red" },
        { name: "Cucumber", category: "Vegetable", color: "green" },  //visible: false
        { name: "Tomato", category: "Vegetable", color: "red" },
    ];


    $("#radioGroupContainer").dxRadioGroup({
        // dataSource: ["banana", "apple", "cherry", "grapes"],
        dataSource: data,
        value: "apple",
        displayExpr: "name",
        value: data[2],
        layout: "horizontal", //vertical
        itemTemplate: function (itemData, itemIndex, itemElement) {
            itemElement.append(
                $("<div />").attr("style", "color:" + itemData.color)
                    .text(itemData.name)
            );
        },
        onContentReady: function () {
            console.log("RadioGroup content is ready.");
        },
        onDisposing: function () {
            console.log("RadioGroup is being disposed of.");
        },
        onInitialized: function () {
            console.log("RadioGroup is initialized.");
        },
        onOptionChanged: function (e) {
            console.log("Option changed:", e);
        },
        onValueChanged: function (e) {
            console.log("Value changed:", e);
        }
    });

    const programmingLanguages = [
        { id: 1, name: "JavaScript" },
        { id: 2, name: "Python" },
        { id: 3, name: "Java" },
        { id: 4, name: "C#" },
        { id: 5, name: "Ruby" }
    ];

    const radioGroup = $("#programing-language").dxRadioGroup({
        dataSource: programmingLanguages, // Bind data
        displayExpr: "name",              // Field to display
        valueExpr: "id",                  // Unique identifier field
        value: 1,                         // Default value (JavaScript)
        layout: "horizontal",             // Horizontal layout
        hint: "Select your preferred programming language", // Hint text
        width: 400,                       // Set width
        height: 60,                       // Set height
        name: "programmingLanguage",      // Name attribute for underlying HTML element
        onContentReady: function () {
            console.log("Programming language RadioGroup content is ready.");
        },
        onDisposing: function () {
            console.log("Programming language RadioGroup is being disposed of.");
        },
        onInitialized: function () {
            console.log("Programming language RadioGroup is initialized.");
        },
        onOptionChanged: function (e) {
            console.log("Programming language option changed:", e);
        },
        onValueChanged: function (e) {
            console.log("Programming language value changed:", e);
        }    
    }).dxRadioGroup("instance");

    // Button Event: Update RadioGroup value programmatically
    $('#updateButton').on('click', function () {
        radioGroup.option("value", 3); // Set value to 'Java'
        alert("Updated value to Java");
    });

    // Button Event: Reset the value to the default
    $('#resetButton').on('click', function () {
        radioGroup.reset(); // Resets the value to the default value
        alert("Value reset to the default (JavaScript)");
    });

    // Button Event: Focus on RadioGroup programmatically
    $('#focusButton').on('click', function () {
        radioGroup.focus(); // Focuses on the RadioGroup
        alert("Focused on the RadioGroup");
    });

    // Button Event: Repaint the RadioGroup
    // $('#repaintButton').on('click', function () {
    //     radioGroup.repaint(); // Repaints the RadioGroup without reloading data
    //     alert("Repainted the RadioGroup");
    // });

    // You can also use beginUpdate and endUpdate with jQuery
    $('#beginUpdateButton').on('click', function () {
        radioGroup.beginUpdate(); // Prevents the UI component from refreshing
        radioGroup.option("value", 2); // Update value to 'Python'
        radioGroup.option("width", 500); // Update width
        radioGroup.endUpdate(); // Refreshes the component after changes
    });

});
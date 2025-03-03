$(function () {
    $("#textAreaContainer").dxTextArea({
        placeholder: "Enter at least 5 characters...",
        spellcheck: true,
        // value: "The text that should not be edited",
        // readOnly: true,
        height: 200,
        width: 300,
        // autoResizeEnabled: true,
        // minHeight: 100,
        // maxHeight: 200
        maxLength: 100,
    });

    $("#textArea1").dxTextArea({
        value: "",
        placeholder: "Type here to synchronize with the second Text Area",
        height: 150,
        autoResizeEnabled: true, // Auto resize text area based on content
        onValueChanged: function(e) {
            // Synchronize text between the two text areas
            const value = e.value;
            $("#textArea2").dxTextArea("instance").option("value", value);
        },
        onKeyUp: function(e) {
            // Synchronize text between the two text areas on keyup event
            const value = e.event.target.value;
            console.log("Text changed in TextArea 1 (onKeyUp):", value);
            $("#textArea2").dxTextArea("instance").option("value", value);
        }
    });

    // Second Text Area
    $("#textArea2").dxTextArea({
        value: "",
        placeholder: "This text area will sync with the first one",
        height: 150,
        readOnly : true,
        autoResizeEnabled: true, // Auto resize text area based on content
        onValueChanged: function(e) {
            // Synchronize text between the two text areas
            const value = e.value;
            $("#textArea1").dxTextArea("instance").option("value", value);
        },
        onKeyUp: function(e) {
            // Synchronize text between the two text areas on keyup event
            const value = e.event.target.value;
            console.log("Text changed in TextArea 2 (onKeyUp):", value);
            $("#textArea1").dxTextArea("instance").option("value", value);
        }
    });
});
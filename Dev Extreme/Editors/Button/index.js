$(function () {
    // Create a button and log interactions
    const button = $("#button").dxButton({
        text: "Click me!",
        onClick: function () {
            DevExpress.ui.notify("The button was clicked");
        },
        type: "success", // "normal""success" "default" "danger"
        stylingMode: "outlined", //contained , text
        icon: "comment",
        // visible : false,
        width: "200px"
    }).dxButton("instance");

    // Log the button instance to the console
    console.log(button.instance);

    // Add event handler for when the button is clicked
    button.on('click', function () {
        $("#log").text('Button was clicked at ' + new Date().toLocaleTimeString());
    });



    // Use beginUpdate and endUpdate to batch updates
    button.beginUpdate();
    button.option('text', "New Text");
    button.option('icon', "check");
    button.endUpdate();

    // Option: Dynamically change the button's text using option()
    setTimeout(function () {
        button.option('text', "Updated Text");
    }, 3000);

    // Update the visibility of the button
    setTimeout(function () {
        button.option('visible', false);
    }, 5000);

    // Repaint the button manually
    setTimeout(function () {
        button.beginUpdate();
        button.option('visible', true);
        button.repaint();
        button.endUpdate();
    }, 7000);



    $("#login").dxTextBox({
        name: "Login",
        placeholder: "username",
        hint: "username"
    }).dxValidator({
        validationRules: [
            { type: "required", message: "username is required" }
        ]
    });

    $("#password").dxTextBox({
        name: "Password",
        mode: "password",
        hint: "password",
        placeholder: "password"
    }).dxValidator({
        validationRules: [
            { type: "required", message: "password is required" }
        ]
    });

    $("#validateAndSubmit").dxButton({
        text: "Submit",
        type: "success",
        useSubmitBehavior: true,
        onClick: function () {
            let loginValid = $("#login").dxValidator("instance").validate().isValid;
            let passwordValid = $("#password").dxValidator("instance").validate().isValid;
            if (loginValid && passwordValid) {
                DevExpress.ui.notify("Form submitted successfully!", "success", 2000);
            } else {
                DevExpress.ui.notify("Please fill out the required fields.", "error", 2000);
            }
        }
    });
});
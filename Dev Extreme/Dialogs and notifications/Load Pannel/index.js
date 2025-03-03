$(function () {
    const loadPanel = $('#loadPanelContainer').dxLoadPanel({
        visible: false, // Initially hidden
        message: 'Please wait...',
        showIndicator: true,
        // closeOnOutsideClick: true,
        // position: { my: "left", at: "left", of: "#targetElement" },
        shading: true,
        shadingColor: 'rgba(0,0,0,0.4)'
    }).dxLoadPanel('instance');

    // Start operation on button click
    $('#startOperationButton').click(function () {
        loadPanel.show();

        // Simulate a background operation
        setTimeout(function () {
            loadPanel.hide();
            alert('Operation completed!');
        }, 3000); // Simulate 3 seconds operation
    });
});
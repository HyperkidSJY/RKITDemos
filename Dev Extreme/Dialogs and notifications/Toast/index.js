$(function () {
    const types = ['error', 'info', 'success', 'warning'];
 
    $("#show-message").dxButton({
        text: "Show message",
        onClick: function() {
            DevExpress.ui.notify(
                {
                    width: 230,
                    height: 50,
                    position: {
                        my: "bottom",
                        at: "bottom",
                        of: "#container"
                    },
                    contentTemplate: (element) => {
                        element.append('<p>You have a new message</p> &nbsp;');
                        element.append('<i class="dx-icon-email icon-style"></i>');
                    }
                }, 
                types[Math.floor(Math.random() * 4)], 
                500
            );
        },
    });
});
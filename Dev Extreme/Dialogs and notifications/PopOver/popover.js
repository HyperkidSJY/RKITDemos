console.log('connected');

$(function () {
    $("#popoverGojo").dxPopover({
        target: "#imgGojo",
        showEvent: 'dxhoverstart',
        hideEvent: 'dxhoverend',
        title: "Satoru Gojo",
        showTitle: true,
        width: 300,
    });
    $("#popoverSukuna").dxPopover({
        target: "#imgSukuna",
        showEvent: 'dxhoverstart',
        hideEvent: 'dxhoverend',
        title: "Ryomen Sukuna",
        showTitle: true,
        width: 300,
        // position: "right",
    });
    $("#popoverZoro").dxPopover({
        target: "#imgZoro",
        showEvent: 'dxhoverstart',
        hideEvent: 'dxhoverend',
        title: "Roronoa Zoro",
        showTitle: true,
        width: 300,
    });
    $("#popoverNagi").dxPopover({
        target: "#imgNagi",
        showEvent: 'dxhoverstart',
        hideEvent: 'dxhoverend',
        title: "Seishiro Nagi",
        showTitle: true,
        width: 300,
    });
    $("#popoverLuffy").dxPopover({
        target: "#imgLuffy",
        showEvent: 'dxhoverstart',
        hideEvent: 'dxhoverend',
        title: "Monkey D. Luffy",
        showTitle: true,
        width: 300,
        position: "top",
    });

});
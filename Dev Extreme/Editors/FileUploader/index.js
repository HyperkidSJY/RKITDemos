$(function() {
    $("#fileUploaderContainer").dxFileUploader({
        name: "files",
        uploadMode: "useButtons", // "useButtons", "instantly",
        uploadUrl : "https://api.escuelajs.co/api/v1/files/upload",
        // chunkSize: 400000 // 400 KB
    });

    // for using the uploaded file.
    // let file = $("#fileUploaderContainer").dxFileUploader("option", "value");

    // console.log(file);




    //For Upadting Parameter of the fileUploader we can use it for instantly or useButton

    let student = {id : 1 , name : "Shivam" , department : "CE" , semester :  8};

    $("#fileUploadWithparameters").dxFileUploader({
        name : "file",
        uploadMode :  "instantly",
        uploadUrl : "http://example.com/fileUpload",
        allowedFileExtensions: [".jpg", ".png"],
        minFileSize: 1024, // 1 KB
        maxFileSize: 1024 * 1024, // 1 MB
        onValueChanged : function(e){
            let url = e.component.option("uploadUrl");
            url = updateParameter(url,"id",student.id);
            e.component.option("uploadUrl",url);
            console.log($("#fileUploaderContainer").dxFileUploader("instance").option("uploadUrl"));
        }
    });

    $("#numberBoxContainer").dxNumberBox({
        value: student.semester,
        onValueChanged: function (e) {
            if ( e.value !== e.previousValue ) {
                let fileUploader = $("#fileUploaderContainer").dxFileUploader("instance");
                let url = fileUploader.option("uploadUrl");
                url = updateParameter(url, "office", e.value);            
                fileUploader.option("uploadUrl", url);
                console.log($("#fileUploaderContainer").dxFileUploader("instance").option("uploadUrl"));
            }
        }
    });
    function updateParameter (uri, key, value) {
        let re = new RegExp("([?&])" + key + "=.*?(&|$)", "i");
        let separator = uri.indexOf('?') !== -1 ? "&" : "?";
        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + "=" + value + '$2');
        }
        else {
            return uri + separator + key + "=" + value;
        }
    }

    let fileUploaderInstance = $('#simpleFileUploader').dxFileUploader({
        name: "file",
        multiple: true,
        uploadMode: "useButtons", // or "instantly"
        uploadUrl: "https://api.escuelajs.co/api/v1/files/upload",
        allowCanceling: true,
        allowedFileExtensions: [".png", ".jpeg", ".jpg"],
        invalidFileExtensionMessage: "enter valid Extentions",
        invalidMaxFileSizeMessage: "Too big file",
        invalidMinFileSizeMessage: "Too small file",
        labelText: "drop your file here",
        dialogTrigger: "#open-dialog",
        dropZone: "#drop-zone",
        maxFileSize: 102400,
        readyToUploadMessage: "ready to upload",
        selectButtonText: "Choose File",
        uploadButtonText: "send",
        // uploadCustomData
        uploadedMessage: "Upload complete",
        uploadFailedMessage: "upload incompleted",
        uploadFile: function (file, progressCallback) {
            console.log("uploadfile");
            console.log(file);
        },
        uploadHeaders: {
            "Content-Type": "multipart/form-data",
        },
        uploadMethod: "POST",
        onBeforeSend: function () {
            console.log("onBefore Send")
        },
        onContentReady: function () {
            console.log("onContentReady")
        },
        onDropZoneEnter: function () {
            console.log("onDropZoneEnter")
        },
        onDropZoneLeave: function () {
            console.log("onDropZoneLeave");
        },
        onInitialized: function () {
            console.log("onInitialized")
        },
        onOptionChanged: function () {
            console.log("onOptionChanged")
        },
        onProgress: function (e) {
            console.log(e);
            console.log("onDropZoneLeave")
        },
        onFilesUploaded: function (e) {
            console.log(e);
            console.log("onFilesUploaded")
        },
        onUploadAborted: function (e) {
            console.log(e);
            console.log("onUploadAborted")
        },
        onUploaded: function (e) {
            console.log(e);
            console.log("onUploaded")
        },
        onUploadError: function (e) {
            console.log(e);
            console.log("onUploadError")
        },
        onUploadStarted: function (e) {
            console.log(e);
            console.log("onUploadStarted")
        },
 
    }).dxFileUploader("instance");
    setTimeout(() => {
        var files = fileUploaderInstance.option("value");
        if (files.length > 0) {
            fileUploaderInstance.abortUpload(files[0]);
            console.log("Upload aborted for:", files[0].name);
        }
    }, 1000);
    $('#button').dxButton({
        Text: "show files",
        onClick: function () {
            let files = fileUploaderInstance.option("showFileList");
            console.log(files);
        }
    });
});    
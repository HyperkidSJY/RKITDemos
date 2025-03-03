$(function () {

    const names = [{ id: 1, fname: "Shivam" , lname : "Yadav" , role : "developer" },
    { id: 2, fname: "Rohit" , lname : "Rathod" , role : "QA" },
    { id: 3, fname: "Sam" , lname : "Wilson" , role : "developer" },
    { id: 4, fname: "John" , lname : "Doe" , role : "QA" },];

    const store = new DevExpress.data.DataSource({
        store: names,
        key: "id",
        // filter : [["name", "endswith", "n"], "or", ["name", "startswith", "R"]],
        group: { selector: "role", desc: true }, // true and false.
        map : function(dataItem){
           return  {
            fname: dataItem.fname + " " + dataItem.lname,
            id: dataItem.id,
           };
        },
        onLoadError: function (error) {
            console.log(error.message);
        },
        paging : true,
        pageSize : 1,
        reshapeOnPush : true,
        searchExpr: ["fname" ,"id"],
        sort: [
            "fname",
            { selector: "fname", desc: true }
        ],
    });

    $("#select-box").dxSelectBox({
        dataSource: store,
        displayExpr: function(data) {
            return data.firstName + " " + data.lastName;  // Display full name
        },
        valueExpr: "id",
        grouped : true,
        searchEnabled : true,
        onValueChanged : function(e){
            console.log(e);
        }
    });
    
});
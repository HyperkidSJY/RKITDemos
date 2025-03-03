$(function () {

    const employees = [
        { id: 1, name: 'John', position: 'Manager', salary: 5000, experience: 5 },
        { id: 2, name: 'Anna', position: 'Developer', salary: 4000, experience: 3 },
        { id: 3, name: 'Mike', position: 'Developer', salary: 4500, experience: 4 },
        { id: 4, name: 'Sarah', position: 'Designer', salary: 3500, experience: 2 },
        { id: 5, name: 'David', position: 'Manager', salary: 6000, experience: 6 }
    ];


    let step = function (total, itemData) {
        // "total" is the accumulator, initialized to an object
        // "itemData" is the current employee data
        total.salarySum += itemData.salary * itemData.experience;  // Weighted salary
        total.experienceSum += itemData.experience;  // Total experience
        return total;
    };

    // Define the finalize function
    let finalize = function (total) {
        // Calculate the weighted average salary by dividing the total weighted salary by total experience
        return total.experienceSum > 0 ? total.salarySum / total.experienceSum : 0;
    };

    // Perform the aggregation operation
    DevExpress.data.query(employees)
        .aggregate(
            { salarySum: 0, experienceSum: 0 },  // Initial accumulator
            step,  // Step function
            finalize // Finalize function
        )
        .done(function (result) {
            console.log('Weighted Average Salary:', result);  // Outputs weighted average salary
        });


    DevExpress.data.query(employees)
        // .select("salary")
        .avg("salary")
        .done(function (result) {
            console.log('Average Salary:', result);
        });

    DevExpress.data.query(employees)
        .count()
        .done(function (result) {
            console.log('Average Salary:', result);
        });

    DevExpress.data.query(employees)
        .enumerate()
        .done(function (result) {
            console.log('Average Salary:', result);
        });

    let filteredData = DevExpress.data.query(employees)
        .filter(["salary", ">", 4000])
        .toArray();

    console.log(filteredData);
    
    let groupedData = DevExpress.data.query(employees)
    .groupBy("position")
    .toArray();

    console.log(groupedData);

    DevExpress.data.query(employees)
    .max("salary") // for min also // same we can use select
    .done(function (result) {
       console.log('Max Salary:', result);
    });

    
    // slice(skip, take)
    let subset = DevExpress.data.query(employees)
    .slice(2, 3)
    .toArray();
    console.log(subset);

    let sortedData = DevExpress.data.query(employees)
    .sortBy("experience",true)
    .thenBy("salary",ture) // desc : true
    .toArray();
    console.log(sortedData);


    DevExpress.data.query(employees)
    .sum("experience")
    .done(function (result) {
       console.log('Sum of Experience:', result);
    });

    
});
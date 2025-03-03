using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Demonstrates LINQ operations on DataTables such as filtering, grouping, joining, sorting, and aggregation.
    /// </summary>
    public class LinqInDataTable
    {
        /// <summary>
        /// Main method that executes various LINQ operations on a DataTable.
        /// </summary>
        static void Main(string[] args)
        {
            // Create a DataTable and add columns
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));

            // Add rows to the DataTable
            table.Rows.Add(1, "Alice", 30);
            table.Rows.Add(2, "Bob", 25);
            table.Rows.Add(3, "Charlie", 35);
            table.Rows.Add(4, "David", 40);

            // LINQ query to select all rows where Age > 30
            //var result = from row in table.AsEnumerable()
            //             where row.Field<int>("Age") > 30
            //             select row;

            //// Display the results
            //foreach (var row in result)
            //{
            //    Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}");
            //}

            // Grouping by Age and counting the number of people in each group
            /// <summary>
            /// Groups rows by Age and counts the number of people in each group.
            /// </summary>
            var groupedResult = from row in table.AsEnumerable()
                                group row by row.Field<int>("Age") into ageGroup
                                select new
                                {
                                    Age = ageGroup.Key,
                                    Count = ageGroup.Count(),
                                    People = ageGroup.Select(r => r.Field<string>("Name"))
                                };

            /// <summary>
            /// Displays the grouped results.
            /// </summary>
            foreach (var group in groupedResult)
            {
                Console.WriteLine($"Age: {group.Age}, Count: {group.Count}");
                foreach (var name in group.People)
                {
                    Console.WriteLine($"  {name}");
                }
            }

            // JOIN two DataTables based on the "Id" column
            /// <summary>
            /// Joins two DataTables based on the "Id" column and selects specific columns.
            /// </summary>
            DataTable table2 = new DataTable();
            table2.Columns.Add("Id", typeof(int));
            table2.Columns.Add("Department", typeof(string));

            table2.Rows.Add(1, "HR");
            table2.Rows.Add(2, "IT");
            table2.Rows.Add(3, "Finance");
            table2.Rows.Add(4, "Marketing");

            var joinedResult = from person in table.AsEnumerable()
                               join dept in table2.AsEnumerable()
                               on person.Field<int>("Id") equals dept.Field<int>("Id")
                               select new
                               {
                                   Name = person.Field<string>("Name"),
                                   Age = person.Field<int>("Age"),
                                   Department = dept.Field<string>("Department")
                               };

            /// <summary>
            /// Displays the joined result of two DataTables.
            /// </summary>
            foreach (var item in joinedResult)
            {
                Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Department: {item.Department}");
            }

            // Sorting the DataTable rows by Age in descending order
            /// <summary>
            /// Sorts rows by Age in descending order.
            /// </summary>
            var sortedResult = from row in table.AsEnumerable()
                               orderby row.Field<int>("Age") descending
                               select row;

            /// <summary>
            /// Displays the sorted results.
            /// </summary>
            foreach (var row in sortedResult)
            {
                Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}");
            }

            // Aggregating to find the average age
            /// <summary>
            /// Calculates the average age of people in the DataTable.
            /// </summary>
            var averageAge = table.AsEnumerable()
                                  .Average(row => row.Field<int>("Age"));

            /// <summary>
            /// Displays the average age of people.
            /// </summary>
            Console.WriteLine($"Average Age: {averageAge}");
        }
    }
}

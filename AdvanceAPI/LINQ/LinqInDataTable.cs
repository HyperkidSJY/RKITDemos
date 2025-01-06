using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    public class LinqInDataTable
    {
        static void Main(string[] args)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));

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

            //Grouping
            //var groupedResult = from row in table.AsEnumerable()
            //                    group row by row.Field<int>("Age") into ageGroup
            //                    select new
            //                    {
            //                        Age = ageGroup.Key,
            //                        Count = ageGroup.Count(),
            //                        People = ageGroup.Select(r => r.Field<string>("Name"))
            //                    };

            //foreach (var group in groupedResult)
            //{
            //    Console.WriteLine($"Age: {group.Age}, Count: {group.Count}");
            //    foreach (var name in group.People)
            //    {
            //        Console.WriteLine($"  {name}");
            //    }
            //}

            //JOIN
            //DataTable table2 = new DataTable();
            //table2.Columns.Add("Id", typeof(int));
            //table2.Columns.Add("Department", typeof(string));

            //table2.Rows.Add(1, "HR");
            //table2.Rows.Add(2, "IT");
            //table2.Rows.Add(3, "Finance");
            //table2.Rows.Add(4, "Marketing");

            //// LINQ query to join two DataTables based on the "Id" column
            //var joinedResult = from person in table.AsEnumerable()
            //                   join dept in table2.AsEnumerable()
            //                   on person.Field<int>("Id") equals dept.Field<int>("Id")
            //                   select new
            //                   {
            //                       Name = person.Field<string>("Name"),
            //                       Age = person.Field<int>("Age"),
            //                       Department = dept.Field<string>("Department")
            //                   };

            //foreach (var item in joinedResult)
            //{
            //    Console.WriteLine($"Name: {item.Name}, Age: {item.Age}, Department: {item.Department}");
            //}

            //Sorting
            var sortedResult = from row in table.AsEnumerable()
                               orderby row.Field<int>("Age") descending
                               select row;

            foreach (var row in sortedResult)
            {
                Console.WriteLine($"Id: {row["Id"]}, Name: {row["Name"]}, Age: {row["Age"]}");
            }

            //Agregrate
            var averageAge = table.AsEnumerable()
                      .Average(row => row.Field<int>("Age"));

            Console.WriteLine($"Average Age: {averageAge}");

        }
    }
}

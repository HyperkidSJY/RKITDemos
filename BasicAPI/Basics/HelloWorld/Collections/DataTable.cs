using System;
using System.Data;
using System.Linq;
using System.Transactions;

namespace Collections
{
    /// <summary>
    /// The DataTable1 class demonstrates various operations on a DataTable, such as adding columns and rows,
    /// updating and deleting rows, filtering data, querying using LINQ, and sorting.
    /// </summary>
    class DataTable1
    {
        static void Main()
        {
            #region Create DataTable and Add Columns
            // Step 1: Create a DataTable
            DataTable table = new DataTable("Employee");

            // Step 2: Add Columns
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Age", typeof(int));
            table.Columns.Add("Department", typeof(string));
            table.Columns.Add("Salary", typeof(decimal));

            // Step 3: Add Primary Key
            table.PrimaryKey = new DataColumn[] { table.Columns["Id"] };
            #endregion

            #region Add Rows
            // Step 4: Add Rows
            table.Rows.Add(1, "Alice", 28, "HR", 50000);
            table.Rows.Add(2, "Bob", 35, "IT", 70000);
            table.Rows.Add(3, "Charlie", 25, "Finance", 60000);
            table.Rows.Add(4, "Diana", 30, "IT", 80000);
            table.Rows.Add(5, "Eve", 40, "HR", 55000);

            Console.WriteLine("Original DataTable:");
            PrintDataTable(table);
            #endregion

            #region Update a Row
            // Step 5: Update a Row
            DataRow rowToUpdate = table.Rows.Find(2); // Find row with Id = 2
            if (rowToUpdate != null)
            {
                rowToUpdate["Name"] = "Bobby";
                rowToUpdate["Salary"] = 75000;
            }

            Console.WriteLine("\nAfter Updating Bob's Salary and Name:");
            PrintDataTable(table);
            #endregion

            #region Delete a Row
            // Step 6: Delete a Row
            DataRow rowToDelete = table.Rows.Find(5); // Find row with Id = 5
            if (rowToDelete != null)
            {
                table.Rows.Remove(rowToDelete);
            }

            Console.WriteLine("\nAfter Deleting Eve:");
            PrintDataTable(table);
            #endregion

            #region Filter Rows
            // Step 7: Filter Rows
            Console.WriteLine("\nEmployees in IT Department:");
            DataRow[] filteredRows = table.Select("Department = 'IT'");
            foreach (DataRow row in filteredRows)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["Age"]}, {row["Department"]}, {row["Salary"]}");
            }
            #endregion

            #region LINQ Query
            // Step 8: Using LINQ to Query DataTable
            Console.WriteLine("\nEmployees with Salary > 60000 (Using LINQ):");
            var highSalaryEmployees = from row in table.AsEnumerable()
                                      where row.Field<decimal>("Salary") > 60000
                                      select row;
            foreach (var row in highSalaryEmployees)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["Age"]}, {row["Department"]}, {row["Salary"]}");
            }
            #endregion

            #region Sort Rows
            // Step 9: Sort Rows
            Console.WriteLine("\nSorted DataTable by Name:");
            DataView sortedView = table.DefaultView;
            sortedView.Sort = "Name ASC";
            DataTable sortedTable = sortedView.ToTable();
            PrintDataTable(sortedTable);
            #endregion
        }

        #region Helper Methods
        /// <summary>
        /// Prints the contents of a DataTable to the console.
        /// </summary>
        /// <param name="table">The DataTable to print.</param>
        static void PrintDataTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.Write(item + "\t");
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}

//LINQ: Queries in-memory collections or data through .NET frameworks.
//SQL: Queries relational databases directly.

//Performance:
//LINQ: Best for small, in-memory datasets.Relies on translation when querying databases(e.g., via Entity Framework).
//SQL: Optimized for large - scale database queries.
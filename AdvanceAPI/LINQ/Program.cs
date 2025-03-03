using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Main class to demonstrate LINQ operations such as joining, selecting, and aggregating data from two lists.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main method that demonstrates LINQ queries and operations like Join, Average, Max, and Min.
        /// </summary>
        static void Main(string[] args)
        {
            // List of employees and departments are retrieved from the data source
            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();

            // LINQ Query to join the employee list with department list based on DepartmentId
            /// <summary>
            /// This LINQ query joins employees and departments based on their DepartmentId and selects 
            /// relevant fields such as FirstName, LastName, AnnualSalary, Manager status, and Department name.
            /// </summary>
            var resultList = from emp in employees
                             join dept in departments
                             on emp.DepartmentId equals dept.Id
                             select new
                             {
                                 FirstName = emp.FirstName,
                                 LastName = emp.LastName,
                                 AnnualSalary = emp.AnnualSalary,
                                 Manager = emp.IsManager,
                                 Department = dept.LongName
                             };

            // Display the result of the LINQ query (joined employee data)
            /// <summary>
            /// Iterates over the result set to display each employee's details.
            /// </summary>
            foreach (var emp in resultList)
            {
                Console.WriteLine(emp.FirstName);
                Console.WriteLine(emp.LastName);
                Console.WriteLine(emp.AnnualSalary);
                Console.WriteLine(emp.Manager);
                Console.WriteLine(emp.Department);
            }

            // Display aggregated information: Average, Max, and Min Annual Salary
            /// <summary>
            /// Displays the average, maximum, and minimum annual salary of the employees from the query result.
            /// </summary>
            Console.WriteLine($"Average Annual Salary: {resultList.Average(a => a.AnnualSalary)}");
            Console.WriteLine($"Max Annual Salary: {resultList.Max(a => a.AnnualSalary)}");
            Console.WriteLine($"Min Annual Salary: {resultList.Min(a => a.AnnualSalary)}");

            // Wait for user input to end the program
            Console.ReadKey();
        }
    }
}

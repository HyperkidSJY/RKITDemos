using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    public class LinqExamples3
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            // Equality Operator: SequenceEqual
            // This checks if two sequences are equal (i.e., contain the same elements in the same order).
            // var integerList1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            // var integerList2 = new List<int> { 1, 2, 3, 4, 5, 6 };
            // var boolSequenceEqual = integerList1.SequenceEqual(integerList2);
            // Console.WriteLine(boolSequenceEqual); // Will return true if both lists are equal

            // Concatenation Operator: Concat
            // Concatenates two sequences (list1 + list2)
            // List<int> integerList1 = new List<int> { 1, 2, 3, 4 };
            // List<int> integerList2 = new List<int> { 5, 6, 7, 8, 9, 10 };
            // IEnumerable<int> integerListConcat = integerList1.Concat(integerList2);
            // foreach (var item in integerListConcat)
            //     Console.WriteLine(item);

            // Concatenating two lists of Employees:
            // List<Employee> employeeList2 = new List<Employee> 
            // {
            //     new Employee { Id = 5, FirstName = "Tony", LastName = "Stark" },
            //     new Employee { Id = 6, FirstName = "Debbie", LastName = "Townsend" }
            // };

            // IEnumerable<Employee> results = employeeList.Concat(employeeList2);
            // foreach (var item in results)
            //     Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");

            // Aggregate Operators: Aggregate, Average, Count, Sum, Max
            // Aggregate Example: Total Annual Salary including bonus
            decimal totalAnnualSalary = employeeList.Aggregate<Employee, decimal>(0, (totAnnualSalary, e) =>
            {
                var bonus = (e.IsManager) ? 0.04m : 0.02m;
                totAnnualSalary = (e.AnnualSalary + (e.AnnualSalary * bonus)) + totAnnualSalary;
                return totAnnualSalary;
            });

            Console.WriteLine($"Total Annual Salary of all employees (including bonus): {totalAnnualSalary}");

            // Aggregate Example: Employee Annual Salaries including bonus
            string data = employeeList.Aggregate<Employee, string, string>("Employee Annual Salaries (including bonus): ",
                (s, e) =>
                {
                    var bonus = (e.IsManager) ? 0.04m : 0.02m;
                    s += $"{e.FirstName} {e.LastName} - {e.AnnualSalary + (e.AnnualSalary * bonus)}, ";
                    return s;
                }, s => s.Substring(0, s.Length - 2) // Remove last comma
            );

            Console.WriteLine(data);

            // Average: Average Annual Salary for a specific department
            decimal average = employeeList.Where(e => e.DepartmentId == 3).Average(e => e.AnnualSalary);
            Console.WriteLine($"Average Annual Salary (Technology Department): {average}");

            // Count: Number of Employees in a specific department
            int countEmployees = employeeList.Count(e => e.DepartmentId == 3);
            Console.WriteLine($"Number of Employees (Technology Department): {countEmployees}");

            // Sum: Total Annual Salaries of all employees
            decimal totalSalaries = employeeList.Sum(e => e.AnnualSalary);
            Console.WriteLine($"Total Annual Salaries: {totalSalaries}");

            // Max: Highest Annual Salary among employees
            var highestSalary = employeeList.Max(e => e.AnnualSalary);
            Console.WriteLine($"Highest Annual Salary: {highestSalary}");

            Console.ReadKey();
        }
    }
}

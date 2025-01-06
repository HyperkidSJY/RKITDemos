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

            ////Equality Operator
            ////SequenceEqual
            // var integerList1 = new List<int> { 1, 2, 3, 4, 5, 6 };
            //var integerList2 = new List<int> { 1, 2, 3, 4, 5, 6 };

            //var boolSequenceEqual = integerList1.SequenceEqual(integerList2);
            //Console.WriteLine(boolSequenceEqual);


            ////Concatenation Operator
            ////Concat

            //List<int> integerList1 = new List<int> { 1, 2, 3, 4 };
            //List<int> integerList2 = new List<int> { 5, 6, 7, 8, 9, 10 };

            //IEnumerable<int> integerListConcat = integerList1.Concat(integerList2);

            //foreach (var item in integerListConcat)
            //    Console.WriteLine(item);

            //List<Employee> employeeList2 = new List<Employee> { new Employee { Id = 5, FirstName = "Tony", LastName = "Stark" }, new Employee { Id = 6, FirstName = "Debbie", LastName = "Townsend" } };

            //IEnumerable<Employee> results = employeeList.Concat(employeeList2);

            //foreach (var item in results)
            //    Console.WriteLine($"{item.Id,-5} {item.FirstName,-10} {item.LastName,-10}");



            ////Aggregate Operators - Aggregate, Average, Count, Sum, Max
            ////Aggregate Operator

            //decimal totalAnnualSalary = employeeList.Aggregate<Employee, decimal>(0, (totAnnualSalary, e) =>
            //{
            //    var bonus = (e.IsManager) ? 0.04m : 0.02m;

            //    totAnnualSalary = (e.AnnualSalary + (e.AnnualSalary * bonus)) + totAnnualSalary;

            //    return totAnnualSalary;
            //});

            //Console.WriteLine($"Total Annual Salary of all employees (including bonus): {totalAnnualSalary}");

            //string data = employeeList.Aggregate<Employee, string, string>("Employee Annual Salaries (including bonus): ",
            //    (s, e) =>
            //    {
            //        var bonus = (e.IsManager) ? 0.04m : 0.02m;

            //        s += $"{e.FirstName} {e.LastName} - {e.AnnualSalary + (e.AnnualSalary * bonus)}, ";
            //        return s;
            //    }, s => s.Substring(0, s.Length - 2)
            //);

            //Console.WriteLine(data);

            ////Average
            //decimal average = employeeList.Where(e => e.DepartmentId == 3).Average(e => e.AnnualSalary);

            //Console.WriteLine($"Average Annual Salary (Technology Department): {average}");

            ////Count
            //int countEmployees = employeeList.Count(e => e.DepartmentId == 3);

            //Console.WriteLine($"Number of Employees (Technology Department): {countEmployees}");

            ////Sum
            //decimal result = employeeList.Sum(e => e.AnnualSalary);
            //Console.WriteLine($"Total Annual Salaries: {result}");

            ////Max
            //var result = employeeList.Max(e => e.AnnualSalary);
            //Console.WriteLine($"Highest Annual Salary: {result}");

        }
    }
}

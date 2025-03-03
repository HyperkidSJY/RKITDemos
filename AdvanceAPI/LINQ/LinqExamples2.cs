using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Demonstrates various LINQ operations such as sorting, grouping, and filtering.
    /// </summary>
    public class LinqExamples2
    {
        /// <summary>
        /// Main method that demonstrates LINQ sorting, grouping, and filtering.
        /// </summary>
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            // Sorting Operations OrderBy, OrderByDescending, ThenBy, ThenByDescending
            // Method Syntax
            //var results = employeeList.Join(departmentList, e => e.DepartmentId, d => d.Id,
            //    (emp, dept) => new {
            //        Id = emp.Id,
            //        FirstName = emp.FirstName,
            //        LastName = emp.LastName,
            //        AnnualSalary = emp.AnnualSalary,
            //        DepartmentId = emp.DepartmentId,
            //        DepartmentName = dept.LongName
            //    }).OrderBy(o => o.DepartmentId).ThenByDescending(o => o.AnnualSalary);

            //foreach (var item in results)
            //    Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName, -10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");

            // Query Syntax
            //var results = from emp in employeeList
            //              join dept in departmentList
            //              on emp.DepartmentId equals dept.Id
            //              orderby emp.DepartmentId, emp.AnnualSalary descending
            //              select new
            //              {
            //                  Id = emp.Id,
            //                  FirstName = emp.FirstName,
            //                  LastName = emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //                  DepartmentId = emp.DepartmentId,
            //                  DepartmentName = dept.LongName
            //              };
            //foreach (var item in results)
            //    Console.WriteLine($"First Name: {item.FirstName,-10} Last Name: {item.LastName,-10} Annual Salary: {item.AnnualSalary,10}\tDepartment Name: {item.DepartmentName}");

            // Grouping Operators
            // GroupBy - Deferred
            //var groupResult = from emp in employeeList
            //                  group emp by emp.DepartmentId;

            //foreach (var empGroup in groupResult)
            //{
            //    Console.WriteLine($"Department Id: {empGroup.Key}");

            //    foreach (Employee emp in empGroup)
            //    {
            //        Console.WriteLine($"\tEmployee Fullname: {emp.FirstName} {emp.LastName}");
            //    }
            //}

            // ToLookup Operator - Immediate
            //var groupResult = employeeList.OrderBy(o => o.DepartmentId).ToLookup(e => e.DepartmentId);
            //var groupResult = employeeList.ToLookup(e => e.DepartmentId);

            //foreach (var empGroup in groupResult)
            //{
            //    Console.WriteLine($"Department Id: {empGroup.Key}");

            //    foreach (Employee emp in empGroup)
            //    {
            //        Console.WriteLine($"\tEmployee Fullname: {emp.FirstName} {emp.LastName}");
            //    }
            //}

            // All, Any, Contains Quantifier Operators
            // All and Any Operators
            //var annualSalaryCompare = 40000;

            //bool isTrueAll = employeeList.All(e => e.AnnualSalary > annualSalaryCompare);
            //if (isTrueAll)
            //{
            //    Console.WriteLine($"All employee annual salaries are above {annualSalaryCompare}");
            //}
            //else
            //{
            //    Console.WriteLine($"Not all employee annual salaries are above {annualSalaryCompare}");
            //}

            //bool isTrueAny = employeeList.Any(e => e.AnnualSalary > annualSalaryCompare);
            //if (isTrueAny)
            //{
            //    Console.WriteLine($"At least one employee has an annual salary above {annualSalaryCompare}");
            //}
            //else
            //{
            //    Console.WriteLine($"No employees have an annual salary above {annualSalaryCompare}");
            //}

            // Contains Operator
            //var searchEmployee = new Employee
            //{
            //    Id = 3,
            //    FirstName = "Douglas",
            //    LastName = "Roberts",
            //    AnnualSalary = 40000.2m,
            //    IsManager = false,
            //    DepartmentId = 1
            //};

            //bool containsEmployee = employeeList.Contains(searchEmployee, new EmployeeComparer());

            //if (containsEmployee)
            //{
            //    Console.WriteLine($"An employee record for {searchEmployee.FirstName} {searchEmployee.LastName} was found");
            //}
            //else
            //{
            //    Console.WriteLine($"An employee record for {searchEmployee.FirstName} {searchEmployee.LastName} was not found");
            //}

            // OfType filter Operator
            //ArrayList mixedCollection = Data.GetHeterogeneousDataCollection();

            //var stringResult = from s in mixedCollection.OfType<string>()
            //                   select s;
            //foreach (var item in stringResult)
            //    Console.WriteLine(item);

            // ElementAt, ElementAtOrDefault, First, FirstOrDefault, Last, LastOrDefault, Single and SingleOrDefault Element Operators
            // ElementAt, ElementAtOrDefault Operators
            //var emp = employeeList.ElementAtOrDefault(12);

            //if (emp != null)
            //{
            //    Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");
            //}
            //else
            //{
            //    Console.WriteLine("This employee record does not exist within the collection");
            //}

            // First, FirstOrDefault, Last, LastOrDefault Operators
            //List<int> integerList = new List<int> {3,13,23,17,26,87};

            //int result = integerList.LastOrDefault(i => i % 2 == 0);

            //if (result != 0)
            //{
            //    Console.WriteLine(result);
            //}
            //else
            //{
            //    Console.WriteLine("There are no even numbers in the collection");
            //}

            // Single, SingleOrDefault Operators
            var emp = employeeList.SingleOrDefault();

            if (emp != null)
            {
                Console.WriteLine($"{emp.Id,-5} {emp.FirstName,-10} {emp.LastName,-10}");
            }
            else
            {
                Console.WriteLine("This employee does not exist within the collection");
            }

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Custom comparer to compare two Employee objects.
    /// </summary>
    public class EmployeeComparer : IEqualityComparer<Employee>
    {
        /// <summary>
        /// Determines if two Employee objects are equal.
        /// </summary>
        public bool Equals([AllowNull] Employee x, [AllowNull] Employee y)
        {
            if (x.Id == y.Id && x.FirstName.ToLower() == y.FirstName.ToLower() && x.LastName.ToLower() == y.LastName.ToLower())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for an Employee object.
        /// </summary>
        public int GetHashCode([DisallowNull] Employee obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}

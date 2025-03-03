using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Provides extension methods for working with collections of Employee objects.
    /// </summary>
    public static class EnumerableCollectionExtensionMethods
    {
        /// <summary>
        /// Filters employees with a high annual salary (greater than or equal to 50,000).
        /// </summary>
        /// <param name="employees">The collection of employees to filter.</param>
        /// <returns>An IEnumerable of employees who meet the salary condition.</returns>
        public static IEnumerable<Employee> GetHighSalariedEmployees(this IEnumerable<Employee> employees)
        {
            foreach (Employee emp in employees)
            {
                // Log employee being accessed
                Console.WriteLine($"Accessing employee: {emp.FirstName + " " + emp.LastName}");

                // Yield employee if salary is above the threshold
                if (emp.AnnualSalary >= 50000)
                    yield return emp;
            }
        }
    }

    /// <summary>
    /// Example class demonstrating LINQ usage with Employee and Department data.
    /// </summary>
    public class LinqExamples1
    {
        /// <summary>
        /// Main method that demonstrates various LINQ operations.
        /// </summary>
        static void Main(string[] args)
        {
            List<Employee> employeeList = Data.GetEmployees();
            List<Department> departmentList = Data.GetDepartments();

            // Example: Select and Where Operators - Method Syntax
            //var results = employeeList.Select(e => new
            //{
            //    FullName = e.FirstName + " " + e.LastName,
            //    AnnualSalary = e.AnnualSalary
            //}).Where(e => e.AnnualSalary >= 50000);

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            //}

            // Example: Select and Where Operators - Query Syntax
            //var results = from emp in employeeList
            //              where emp.AnnualSalary >= 50000
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary
            //              };

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            //}

            // Example: Deferred Execution - Lazy Evaluation
            //var results = from emp in employeeList.GetHighSalariedEmployees()
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary
            //              };

            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Sam",
            //    LastName = "Davis",
            //    AnnualSalary = 100000.20m,
            //    IsManager = true,
            //    DepartmentId = 2
            //});

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            //}

            // Example: Immediate Execution
            //var results = (from emp in employeeList.GetHighSalariedEmployees()
            //               select new
            //               {
            //                   FullName = emp.FirstName + " " + emp.LastName,
            //                   AnnualSalary = emp.AnnualSalary
            //               }).ToList();

            //employeeList.Add(new Employee
            //{
            //    Id = 5,
            //    FirstName = "Sam",
            //    LastName = "Davis",
            //    AnnualSalary = 100000.20m,
            //    IsManager = true,
            //    DepartmentId = 2
            //});

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}");
            //}

            // Example: Join Operation - Method Syntax
            //var results = departmentList.Join(employeeList,
            //        department => department.Id,
            //        employee => employee.DepartmentId,
            //        (department, employee) => new
            //        {
            //            FullName = employee.FirstName + " " + employee.LastName,
            //            AnnualSalary = employee.AnnualSalary,
            //            DepartmentName = department.LongName
            //        }
            //    );

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}\t{item.DepartmentName}");
            //}

            // Example: Join Operation - Query Syntax
            //var results = from dept in departmentList
            //              join emp in employeeList
            //              on dept.Id equals emp.DepartmentId
            //              select new
            //              {
            //                  FullName = emp.FirstName + " " + emp.LastName,
            //                  AnnualSalary = emp.AnnualSalary,
            //                  DepartmentName = dept.LongName
            //              };

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"{item.FullName,-20} {item.AnnualSalary,10}\t{item.DepartmentName}");
            //}

            // Example: GroupJoin Operator - Method Syntax
            //var results = departmentList.GroupJoin(employeeList,
            //        dept => dept.Id,
            //        emp => emp.DepartmentId,
            //        (dept,employeesGroup) => new {
            //            Employees = employeesGroup,
            //            DepartmentName = dept.LongName
            //        }
            //    );

            //foreach (var item in results)
            //{
            //    Console.WriteLine($"Department Name: {item.DepartmentName}");
            //    foreach (var emp in item.Employees)
            //        Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
            //}

            // Example: GroupJoin Operator - Query Syntax
            var results = from dept in departmentList
                          join emp in employeeList
                          on dept.Id equals emp.DepartmentId
                          into employeeGroup
                          select new
                          {
                              Employees = employeeGroup,
                              DepartmentName = dept.LongName
                          };

            foreach (var item in results)
            {
                Console.WriteLine($"Department Name: {item.DepartmentName}");
                foreach (var emp in item.Employees)
                    Console.WriteLine($"\t{emp.FirstName} {emp.LastName}");
            }

            Console.ReadKey();
        }
    }
}

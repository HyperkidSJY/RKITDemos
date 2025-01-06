using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI.LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            //List<Employee> employees = Data.GetEmployees();

            //List<Employee> filteredList = employees.Filter(emp => emp.IsManager == true);

            //foreach (Employee emp in filteredList)
            //{
            //    Console.WriteLine(emp.FirstName);
            //    Console.WriteLine(emp.LastName);
            //    Console.WriteLine(emp.AnnualSalary);
            //    Console.WriteLine(emp.IsManager);
            //}
            //Console.ReadKey();

            //var filteredList = employees.Where(emp => emp.IsManager == true);

            //foreach (Employee emp in filteredList)
            //{
            //    Console.WriteLine(emp.FirstName);
            //    Console.WriteLine(emp.LastName);
            //    Console.WriteLine(emp.AnnualSalary);
            //    Console.WriteLine(emp.IsManager);
            //}
            //Console.ReadKey();

            List<Employee> employees = Data.GetEmployees();
            List<Department> departments = Data.GetDepartments();

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
            foreach (var emp in resultList)
            {
                Console.WriteLine(emp.FirstName);
                Console.WriteLine(emp.LastName);
                Console.WriteLine(emp.AnnualSalary);
                Console.WriteLine(emp.Manager);
                Console.WriteLine(emp.Department);
            }

            Console.WriteLine(resultList.Average(a => a.AnnualSalary));
            Console.WriteLine(resultList.Max(a => a.AnnualSalary));
            Console.WriteLine(resultList.Min(a => a.AnnualSalary));

            Console.ReadKey();
        }
    }
}

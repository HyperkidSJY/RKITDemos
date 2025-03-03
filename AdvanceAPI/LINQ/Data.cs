using System.Collections;

namespace AdvanceAPI.LINQ
{
    /// <summary>
    /// Provides methods for retrieving sample data related to employees and departments.
    /// </summary>
    public static class Data
    {
        /// <summary>
        /// Gets a list of employee objects.
        /// </summary>
        /// <returns>A list of Employee objects.</returns>
        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            // Creating and adding employee objects to the list
            Employee employee = new Employee
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Jones",
                AnnualSalary = 60000.3m,
                IsManager = true,
                DepartmentId = 1
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 2,
                FirstName = "Sarah",
                LastName = "Jameson",
                AnnualSalary = 80000.1m,
                IsManager = true,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 3,
                FirstName = "Douglas",
                LastName = "Roberts",
                AnnualSalary = 40000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);
            employee = new Employee
            {
                Id = 4,
                FirstName = "Jane",
                LastName = "Stevens",
                AnnualSalary = 30000.2m,
                IsManager = false,
                DepartmentId = 2
            };
            employees.Add(employee);

            return employees;
        }

        /// <summary>
        /// Gets a list of department objects.
        /// </summary>
        /// <returns>A list of Department objects.</returns>
        public static List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            // Creating and adding department objects to the list
            Department department = new Department
            {
                Id = 1,
                ShortName = "HR",
                LongName = "Human Resources"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 2,
                ShortName = "FN",
                LongName = "Finance"
            };
            departments.Add(department);
            department = new Department
            {
                Id = 3,
                ShortName = "TE",
                LongName = "Technology"
            };
            departments.Add(department);

            return departments;
        }

        /// <summary>
        /// Gets a heterogeneous collection of data including integers, strings, and objects.
        /// </summary>
        /// <returns>An ArrayList containing various types of objects.</returns>
        public static ArrayList GetHeterogeneousDataCollection()
        {
            ArrayList arrayList = new ArrayList();

            // Adding different types of data to the ArrayList
            arrayList.Add(100);
            arrayList.Add("Bob Jones");
            arrayList.Add(2000);
            arrayList.Add(3000);
            arrayList.Add("Bill Henderson");
            arrayList.Add(new Employee { Id = 6, FirstName = "Jennifer", LastName = "Dale", AnnualSalary = 90000, IsManager = true, DepartmentId = 1 });
            arrayList.Add(new Employee { Id = 7, FirstName = "Dane", LastName = "Hughes", AnnualSalary = 60000, IsManager = false, DepartmentId = 2 });
            arrayList.Add(new Department { Id = 4, ShortName = "MKT", LongName = "Marketing" });
            arrayList.Add(new Department { Id = 5, ShortName = "R&D", LongName = "Research and Development" });
            arrayList.Add(new Department { Id = 6, ShortName = "PRD", LongName = "Production" });

            return arrayList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceAPI
{
    public class DynamicType
    {
        static void Main()
        {
            dynamic myVar = 10;      // Initially an integer
            Console.WriteLine(myVar); // Output: 10

            myVar = "Hello, world";   // Now it's a string
            Console.WriteLine(myVar); // Output: Hello, world

            myVar = 3.14;             // Now it's a double
            Console.WriteLine(myVar); // Output: 3.14

            dynamic myObj = new { Name = "John", Age = 30 };
            Console.WriteLine(myObj.Name);  // Output: John
            Console.WriteLine(myObj.Age);   // Output: 30

            // You can even assign new properties dynamically, although they won't exist at compile-time
            myObj.Gender = "Male";  // This will not throw a compile-time error
            Console.WriteLine(myObj.Gender);  // Output: Male
        }
    }
}

//Performance: Because dynamic bypasses compile-time checks, the operations on dynamic objects are slower due to runtime type resolution.

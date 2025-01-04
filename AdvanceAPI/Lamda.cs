namespace AdvanceAPI
{
    using System;
    using System.Linq;

    public class Lamda
    {
        // 1. Basic Syntax of Lambda Expressions
        /// <summary>
        /// Demonstrates the basic syntax of lambda expressions, which are anonymous methods.
        /// In this case, an Action delegate is used to print a message.
        /// </summary>
        public void BasicSyntaxLambda()
        {
            Action sayHello = () => Console.WriteLine("Hello, world!");
            sayHello();  // Output: Hello, world!
        }

        // 2. Lambda with Parameters
        /// <summary>
        /// Demonstrates a lambda expression that takes a parameter.
        /// A Func delegate is used to square the input integer.
        /// </summary>
        public void LambdaWithParameters()
        {
            Func<int, int> square = x => x * x;
            Console.WriteLine(square(5));  // Output: 25
        }

        // 3. Lambda with Multiple Parameters
        /// <summary>
        /// Demonstrates a lambda expression with multiple parameters.
        /// A Func delegate is used to add two integers.
        /// </summary>
        public void LambdaWithMultipleParameters()
        {
            Func<int, int, int> add = (x, y) => x + y;
            Console.WriteLine(add(5, 3));  // Output: 8
        }

        // 4. Lambda with Block Body (Multiple Statements)
        /// <summary>
        /// Demonstrates a lambda expression with a block body containing multiple statements.
        /// This method multiplies two numbers and adds 10 to the result.
        /// </summary>
        public void LambdaWithBlockBody()
        {
            Func<int, int, int> multiplyAndAdd = (x, y) =>
            {
                int result = x * y;
                return result + 10;
            };
            Console.WriteLine(multiplyAndAdd(3, 4));  // Output: 22
        }

        // 5. Using Lambda Expressions with LINQ
        /// <summary>
        /// Demonstrates the use of lambda expressions within a LINQ query.
        /// The method filters even numbers from an array and then squares them.
        /// </summary>
        public void LambdaWithLINQ()
        {
            var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var squaredEvenNumbers = numbers.Where(x => x % 2 == 0).Select(x => x * x);
            foreach (var num in squaredEvenNumbers)
            {
                Console.WriteLine(num);  // Output: 4, 16, 36, 64, 100
            }
        }

        // 6. Using Lambda Expressions with OrderBy and ThenBy
        /// <summary>
        /// Demonstrates the use of lambda expressions to sort data.
        /// This method sorts people first by age, then by name.
        /// </summary>
        public void LambdaWithOrderByThenBy()
        {
            var people = new[]
            {
            new { Name = "John", Age = 30 },
            new { Name = "Alice", Age = 25 },
            new { Name = "Bob", Age = 30 },
            new { Name = "Charlie", Age = 35 }
            };
            var sortedPeople = people.OrderBy(p => p.Age).ThenBy(p => p.Name);
            foreach (var person in sortedPeople)
            {
                Console.WriteLine($"{person.Name}, {person.Age}");
            }
        }

        // Combining All Topics into a Single Main Method
        /// <summary>
        /// The main entry point for the Lamda. This method demonstrates all the above concepts by calling each method in sequence.
        /// </summary>
        public static void Main(string[] args)
        {
            // Creating an instance of the Lamda class
            Lamda Lamda = new Lamda();

            // Calling each demo method:

            Console.WriteLine("1. Basic Syntax of Lambda Expressions");
            Lamda.BasicSyntaxLambda();

            Console.WriteLine("\n2. Lambda with Parameters");
            Lamda.LambdaWithParameters();

            Console.WriteLine("\n3. Lambda with Multiple Parameters");
            Lamda.LambdaWithMultipleParameters();

            Console.WriteLine("\n4. Lambda with Block Body (Multiple Statements)");
            Lamda.LambdaWithBlockBody();

            Console.WriteLine("\n5. Using Lambda Expressions with LINQ");
            Lamda.LambdaWithLINQ();

            Console.WriteLine("\n6. Using Lambda Expressions with OrderBy and ThenBy");
            Lamda.LambdaWithOrderByThenBy();
        }
    }

}

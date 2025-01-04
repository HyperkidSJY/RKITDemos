namespace OperatorsAndExpressionsAndStatements
{   /// <summary>
    /// Demonstrates the usage of various operators in C# for performing operations on variables and values. 
    /// Includes examples of arithmetic, relational, logical, bitwise, assignment, increment/decrement, and ternary operators.
    /// </summary>
    public class Operators
    {
        public static void Main()
        {
            //Operators --> It is used to perform various operations on variables and values.

            //1. Arithmetic Operators
            int a = 10, b = 3;
            Console.WriteLine(a + b);  // Addition: 13
            Console.WriteLine(a - b);  // Subtraction: 7
            Console.WriteLine(a * b);  // Multiplication: 30
            Console.WriteLine(a / b);  // Division: 3 (integer division)
            Console.WriteLine(a % b);  // Modulus: 1

            //2. Relational Operators
            Console.WriteLine(a > b);   // Greater than: True
            Console.WriteLine(a < b);   // Less than: False
            Console.WriteLine(a >= b);  // Greater than or equal to: True
            Console.WriteLine(a <= b);  // Less than or equal to: False
            Console.WriteLine(a == b);  // Equal to: False
            Console.WriteLine(a != b);  // Not equal to: True

            //3. Logical Operators
            bool x = true, y = false;
            Console.WriteLine(x && y);  // Logical AND: False
            Console.WriteLine(x || y);  // Logical OR: True
            Console.WriteLine(!x);      // Logical NOT: False

            //4. Bitwise Operators
            int p = 5, q = 3;  // Binary: p=0101, q=0011
            Console.WriteLine(p & q);  // AND: 1 (Binary: 0001)
            Console.WriteLine(p | q);  // OR:  7 (Binary: 0111)
            Console.WriteLine(p ^ q);  // XOR: 6 (Binary: 0110)
            Console.WriteLine(~p);     // NOT: -6 (Two's complement)

            //5. Assignment Operators
            int c = 10;
            c += 5;  // Same as c = c + 5
            c -= 2;  // Same as c = c - 2
            c *= 3;  // Same as c = c * 3
            c /= 4;  // Same as c = c / 4
            c %= 2;  // Same as c = c % 2
            Console.WriteLine(c);

            //6. Increment and Decrement Operators
            int d = 5;
            Console.WriteLine(++d);  // Pre-increment: 6
            Console.WriteLine(d--);  // Post-decrement: 6 (then d becomes 5)

            //7. Ternary Operators
            int age = 20;
            string result = (age >= 18) ? "Adult" : "Minor";
            Console.WriteLine(result);  // Output: Adult
        }
    }
}
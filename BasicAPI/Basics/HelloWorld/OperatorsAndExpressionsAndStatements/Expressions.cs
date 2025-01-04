namespace OperatorsAndExpressionsAndStatements
{
    /// <summary>
    /// Demonstrates the concept of expressions in C#, which are combinations of operators, variables, and literals that produce a value.
    /// Includes examples of arithmetic and logical expressions.
    /// </summary>
    public class Expressions
    {
        public static void Main()
        {
            //An expression is a combination of operators, variables, and literals that produce a value.
            int x = 10, y = 5;
            int result = (x + y) * 2;  // Expression: Combines operators and variables
            Console.WriteLine(result);  // Output: 30

            int age = 10;
            bool isEligible = (age >= 18 && age < 60);  // Logical Expression
            Console.WriteLine(isEligible);  // Output: True
        }
    }
}

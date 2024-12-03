using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorsAndExpressionsAndStatements
{
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

namespace OperatorsAndExpressionsAndStatements
{   /// <summary>
    /// Demonstrates the concept of statements in C#, which are complete units of execution. 
    /// Includes examples of declaration, assignment, control (conditional, looping, switch), jump statements, 
    /// and method declarations with overloading.
    /// </summary>
    public class Statements
    {
        static int PlusMethod(int x, int y)
        {
            return x + y;
        }

        static double PlusMethod(double x, double y)
        {
            return x + y;
        }
        public static void Main()
        {
            //A statement is a complete unit of execution, such as declarations, assignments, and method calls.
            
            //1. Declaration Statements
            int number;  // Declaring a variable

            //2. Assignment Statements
            number = 25;  // Assigning a value to a variable

            //3. Control Statements

            //a. Conditional Statemets
            if (number > 20)
            {
                Console.WriteLine("Number is greater than 20");
            }
            else
            {
                Console.WriteLine("Number is 20 or less");
            }

            //b. Looping Statements
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Iteration: " + i);
            }

            //c. Switch Statement
            switch (number)
            {
                case 25:
                    Console.WriteLine("Number is 25");
                    break;
                default:
                    Console.WriteLine("Number is not 25");
                    break;
            }

            //d. Jump Statements
            for (int i = 0; i < 5; i++)
            {
                if (i == 3)
                    break;  // Exit the loop when i equals 3
                Console.WriteLine("Value: " + i);
            }

            //Define & calling of Methods
            int myNum1 = PlusMethod(8, 5);
            double myNum2 = PlusMethod(4.3, 6.26);
            Console.WriteLine("Int: " + myNum1);
            Console.WriteLine("Double: " + myNum2);
        }
    }
}

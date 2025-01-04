/// <summary>
/// Demonstrates the use of namespaces in C# to organize classes and avoid naming conflicts.
/// Two classes named 'Hello' exist in different namespaces: 'First' and 'OOPConcepts'. 
/// The 'Namespaces' class in 'OOPConcepts' creates instances of both 'Hello' classes and calls their methods to display different greetings.
/// </summary>
namespace First
{
    public class Hello
    {
        public void sayHello() { Console.WriteLine("Hello First Namespace"); }
    }
}
namespace OOPConcepts
{   
    public class Hello
    {
        public void sayHello() { Console.WriteLine("Hello Second Namespace"); }
    }
    public class Namespaces
    {
        public static void Main()
        {
            First.Hello h1 = new First.Hello();
            OOPConcepts.Hello h2 = new OOPConcepts.Hello();
            h1.sayHello();
            h2.sayHello();

        }
    }
}

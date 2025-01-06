using DemoLibrary;

namespace AdvanceAPI
{
    public class BaseLib
    {
        static void Main(string[] args)
        {
            string fullName = PersonProcessor.JoinName("Shivam", "Yadav");
            Console.WriteLine(fullName);
        }

    }
}

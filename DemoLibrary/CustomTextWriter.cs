namespace DemoLibrary
{
    public class CustomTextWriter : TextWriter
    {
        // Override the Encoding property (must be implemented as it's abstract)
        public override System.Text.Encoding Encoding => System.Text.Encoding.UTF8;

        // Override the Write method
        public override void Write(char value)
        {
            // Simple custom logic: just print the character to the console with a prefix
            Console.Write($"CustomWrite: {value}");
        }
    }
}
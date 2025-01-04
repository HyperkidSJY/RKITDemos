namespace Collections
{
    /// <summary>
    /// Demonstrates String and DateTime class operations:
    /// 1. String manipulation: length, case, substring, replace, split, join, comparison, and interpolation.
    /// 2. DateTime operations: current date/time, formatting, adding/subtracting days, parsing, and comparison.
    /// </summary>

    

    //string is an alias in C# for System.String.
    //So technically, there is no difference.It's like int vs. System.Int32.

    //As far as guidelines, it's generally recommended to use string any time you're referring to an object.

    //e.g.

    //string place = "world";

    //    Likewise, I think it's generally recommended to use String if you need to refer specifically to the class.

    //e.g.

    //string greet = String.Format("Hello {0}!", place);

    public class Strings
    {
        public static void Main(string[] args)
        {
            // Working with String class
            //String vs string 
            Console.WriteLine("----- String Class -----");
            string fullName = "John Doe";
            Console.WriteLine($"Original String: {fullName}");

            // String length
            Console.WriteLine($"Length: {fullName.Length}");

            // Convert to uppercase and lowercase
            Console.WriteLine($"Uppercase: {fullName.ToUpper()}");
            Console.WriteLine($"Lowercase: {fullName.ToLower()}");

            // Substring
            Console.WriteLine($"First Name: {fullName.Substring(0, 4)}");

            // Replace a substring
            string replacedName = fullName.Replace("Doe", "Smith");
            Console.WriteLine($"Replaced String: {replacedName}");

            // Split and join
            string[] nameParts = fullName.Split(' ');
            Console.WriteLine($"Split: First Part = {nameParts[0]}, Second Part = {nameParts[1]}");
            string joinedName = string.Join("-", nameParts);
            Console.WriteLine($"Joined with '-': {joinedName}");

            // String comparison
            string anotherName = "john doe";
            Console.WriteLine($"Case-sensitive comparison with 'john doe': {fullName == anotherName}");
            Console.WriteLine($"Case-insensitive comparison: {string.Equals(fullName, anotherName, StringComparison.OrdinalIgnoreCase)}");

            // String interpolation
            Console.WriteLine($"Hello, {fullName}! Welcome!");

            // Working with DateTime class
            Console.WriteLine("\n----- DateTime Class -----");

            // Current date and time
            DateTime now = DateTime.Now;
            Console.WriteLine($"Current Date and Time: {now}");

            // Formatting DateTime
            Console.WriteLine($"Formatted Date: {now.ToString("dd-MM-yyyy")}");
            Console.WriteLine($"Formatted Time: {now.ToString("HH:mm:ss")}");

            // Adding and subtracting days
            DateTime tomorrow = now.AddDays(1);
            DateTime yesterday = now.AddDays(-1);
            Console.WriteLine($"Tomorrow: {tomorrow}");
            Console.WriteLine($"Yesterday: {yesterday}");

            //subtract
            //date only -dis
           
        

            // Parsing DateTime from a string
            string dateInput = "2024-12-25";
            if (DateTime.TryParse(dateInput, out DateTime parsedDate))
            {
                Console.WriteLine($"Parsed Date: {parsedDate:dddd, MMMM dd, yyyy}");
            }
            else
            {
                Console.WriteLine("Invalid date format.");
            }

            // Getting individual components
            Console.WriteLine($"Year: {now.Year}, Month: {now.Month}, Day: {now.Day}");
            Console.WriteLine($"Hour: {now.Hour}, Minute: {now.Minute}, Second: {now.Second}");

            // Comparing dates
            DateTime newYear = new DateTime(2025, 1, 1);
            int comparison = DateTime.Compare(now, newYear);
            if (comparison < 0)
            {
                Console.WriteLine("Current date is before New Year 2025.");
            }
            else if (comparison > 0)
            {
                Console.WriteLine("Current date is after New Year 2025.");
            }
            else
            {
                Console.WriteLine("Today is New Year 2025!");
            }
        }
    }
}

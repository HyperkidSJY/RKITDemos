//HelloWorld Program
Console.WriteLine("Hello, World!");

//Demo with Write Command.
Console.Write("This is written using write.");
Console.Write("This does not add new line");

//Data Types in C#
string name = "Shivam";
Console.WriteLine(name);

int age = 21;
Console.WriteLine(age);

age = 22;
Console.WriteLine(age);

double myDoubleNum = 5.99D;
char myLetter = 'D';
bool myBool = true;
Console.WriteLine(myLetter);
Console.WriteLine(myBool);
Console.WriteLine(myDoubleNum);

//Constants
const int myNum = 15;
//myNum = 20; // error

//Display Variables
string fname = "Shivam";
Console.WriteLine("Hello " + fname);

//Multiple variables
int x = 5, y = 6, z = 50;
Console.WriteLine(x + y + z);

//Type Conversions

//Implicit Casting (automatically) - converting a smaller type to a larger type size
int myInt = 9;
double myDouble = myInt;       // Automatic casting: int to double

Console.WriteLine(myInt);      // Outputs 9
Console.WriteLine(myDouble);   // Outputs 9

//Explicit Casting(manually) -converting a larger type to a smaller size type
myInt = 9;
myDouble = myInt;       // Automatic casting: int to double

Console.WriteLine(myInt);      // Outputs 9
Console.WriteLine(myDouble);   // Outputs 9

//Type Conversions methods
myInt = 10;
myDouble = 5.25;
myBool = true;

Console.WriteLine(Convert.ToString(myInt));    // convert int to string
Console.WriteLine(Convert.ToDouble(myInt));    // convert int to double
Console.WriteLine(Convert.ToInt32(myDouble));  // convert double to int
Console.WriteLine(Convert.ToString(myBool));   // convert bool to string
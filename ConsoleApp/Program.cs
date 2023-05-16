// See https://aka.ms/new-console-template for more information
using ConsoleApp;
using ConsoleApp.EFOperations;

//TestBasicsOfCSharp();
//Arrays.Test();
//Methods.Test();

//TestStudentClass();
//Inheritance.Test();
//MemoryManagement.Test();
//EventsAndDelegates.Test();
//ExceptionHandling.Test();
//Miscellaneous.Test();
//FirstDbOperations.Test2();
//WorkingWithEF.Test();
//LinqOperations.Test();
 WebAPICalls.Test();

static void TestStudentClass()
{
    Student student = new Student();
    
    student.PrintDetails();
    student.RollNo = 999;
    student.Name = "Sample";
    student.Grade = "10th";
    student.PrintDetails();
}

static void TestBasicsOfCSharp()
{
    Console.WriteLine("Hello, World!");

    Console.WriteLine("Press a key to terminate..");
    Console.ReadKey();
    /* To Execute this program, use the following sequences
     * F5   - Compile the code and Start the execution in Debugging mode. 
     * CTRL + F5 - Compiles the code and starts without debugging mode 
     * SHIFT+F5 - terminate the execution 
     */

    string name = "Sample";
    //Output Formatting
    Console.WriteLine("Name is " + name);
    //string.Format() -> positional arguments and formatting - using {0}, {1},,,,, 
    Console.WriteLine("Name is {0} {1}", name, name);
    //Interpolation - Templated Strings -> $"..{expression}"
    Console.WriteLine($"Name is {name}");
    int first = 10, second = 20;
    Console.WriteLine(first + " + " + second + " = " + (first + second));
    Console.WriteLine("{0} + {1} = {2}", first, second, (first + second));
    Console.WriteLine($"{first} + {second} = {first + second}");
    //Formatting output -> :C-Currency, :N-Number format, :D dateformat
    Console.WriteLine("{0:C3} + {1:C} = {2:N4}", first, second, (first + second));
    Console.WriteLine($"{first} + {second} = {first + second}");


}
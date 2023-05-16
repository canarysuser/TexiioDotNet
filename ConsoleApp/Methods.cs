using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Methods
    {
        internal static void Test()
        {
            string byVal = nameof(SwapByVal), byRef=nameof(SwapByRef);
            string after = "After", before = "Before";
            string format = "{0} calling {1}, x = {2}, y = {3}";
            int x = 10, y = 20; 
            string line = "".PadLeft(45, '=');
            Console.Clear();
            Console.WriteLine(format, before, byVal, x, y);
            SwapByVal(x, y);
            Console.WriteLine(format, after, byVal, x, y);
            Console.WriteLine(line);
            Console.WriteLine(format, before, byRef, x, y);
            SwapByRef(ref x, ref y); //Actual arguments
            Console.WriteLine(format, after, byRef, x, y);
            Console.WriteLine(line);
            //int num = 2, sq, cube, result; 
            //result = Power(num, out sq, out cube);
            int num = 2;
            int result = Power(num, out int sq, out int cube);
            Console.WriteLine($"Num:{num}, Square:{sq}, Cube:{cube}, result:{result}");
            Console.WriteLine(line);
            string strNum = "1234"; 
            if(int.TryParse(strNum, out int value)) //TryParse tries to convert the number
                //returns true if the conversion succeeds, else false. The converted number is stored in value
            {
                Console.WriteLine("Conversion succeeded");
            } else
            { Console.WriteLine("Conversion Failed."); }
            value = int.Parse(strNum);
            Console.WriteLine(  "Parse succeeded.");
            Console.WriteLine(line);
            Params("Zero arguments passed.");
            Params("1 int arguments passed.", 10);
            Params("3 int arguments passed.", 1,2,3);
            Params("Many arguments passed.", 1,2,3,4,5,6,7,8,9,0,3,4,5,6,7,4,5,6,7);
            Console.WriteLine(line);
            Optional();
            Optional(name: "Sample1");
            Optional(salary: 12345);
            Optional(salary: 98765, id: 1234, name: "Example");
            Optional(555, "Ordered", 88888);
            Console.WriteLine(line);

            int Add(int a, int b)
            {
                return a + b;
            }

            result = Add(500, 500);
            Console.WriteLine(result);
            //Outer Function is Test() 
            //Nested Function is Add() -- Local Function 

        }
        static void Optional ( int id = 9999, string name="Unassigned", double salary = -1)
        {
            
            Console.WriteLine($"ID: {id}, Name: {name}, Salary:{salary}");
        }
        static void Params(string message, params int[] args)
        {
            Console.WriteLine(message);
            Console.WriteLine("Number of arguments passed: " + args.Length);
        }
        static int Power(int num, out int square, out int cube)
        {
            square = num * num;
            cube = square * num;
            return cube * num;
        }
        static void SwapByVal(int a, int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }
        static void SwapByRef(ref int a, ref int b)
        {
            a = a + b;
            b = a - b;
            a = a - b;
        }
    }
}

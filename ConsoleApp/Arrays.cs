using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Arrays
    {
        internal static void Test()
        {
            Console.Clear();
            int[] arr = new int[100];
            Console.WriteLine("Array declared. Initializing now...");
            Random r = new Random(); //helps in generating random numbers - not unique numbers 
            for(int i=0; i<arr.Length; i++)
            {
                arr[i] = r.Next(minValue: 400, maxValue: 1000);
            }
            Console.WriteLine("Array initialized. Printing now...");
            foreach(int i in arr)
            {
                Console.Write($"{i}\t");
            }
            Console.WriteLine("\n\nSorting the array. Pls wait....");
            Array.Sort(arr);
            Console.WriteLine("Array Sorted. Printing values....");
            foreach (int i in arr)
            {
                Console.Write($"{i}\t");
            }
            Console.WriteLine("\nSearching for elements in the array.");
            Console.Write("Enter value to search: ");
            //ReadLine() returns a string. 
            //Convert string into a number -> 
            //Use Parse() -> all numeric types support the Parse function. 
            //Parse() takes a string input and returns the appropriate type 
            //Parse() supported by int, short, double, float, long, DateTime, bool. 
            //Use the Convert class -> provides ToXXXX() functions to convert between the types 
            //ToXXXX() => ToInt32(), ToInt16(), ToDouble(), ..... 
            //int a = int.Parse("1000"); 
            //int b = Convert.ToInt32("1222"); 
            int searchFor = int.Parse(Console.ReadLine()!);
            //! - null-forgiving/suppression operator
            int foundAt = Array.BinarySearch(arr, searchFor);
            Console.WriteLine("{0} is {1}",
                searchFor,
                (foundAt > -1) ? $"found at {foundAt + 1}" : "not found in the array."
            );
            //Multi=dimensional arrays 
            int[,] twoD = new int[5, 5]; 
            for(int i = 0; i<twoD.GetLength(dimension: 0); i++)
            {
                for(int j = 0; j < twoD.GetLength(dimension: 1); j++)
                {
                    twoD[i, j] = i * j;
                }
            }
            //Print the values from the twoD array. 


        }
    }
}

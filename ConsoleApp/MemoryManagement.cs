using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class MemoryManagement
    {
        private readonly int _counter = 0; //readonly variables can be assigned a value in the ctor, not thereafter
        private string[] array;
        private const int MaxSize = 1_000; //_ (underscore) digit separator - compiler feature 
        public MemoryManagement(int counter)
        {
            _counter = counter;
            array = new string[MaxSize];
            for (int i = 0; i < MaxSize; i++)
            {
                array[i] = $"This is long string created to fill in the array at {i}";
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"**** OBJECT {_counter} Created ***");
            Console.ResetColor();
        }
        ~MemoryManagement()
        {
            array = null!;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"**** OBJECT {_counter} DESTROYED ***");
            Console.ResetColor();
        }
        internal static void Test()
        {
            MemoryManagement mm; 
            for (int i = 0;i<100;i++)
            {
                mm = new MemoryManagement(i);
                if(i>35 && i < 65)
                {
                    Console.ForegroundColor= ConsoleColor.Magenta;
                    Console.WriteLine($"Generation of 'mm' is {GC.GetGeneration(mm)}");
                    Console.ResetColor();
                    GC.Collect();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"After 1st collection, Generation of 'mm' is {GC.GetGeneration(mm)}");
                    Console.ResetColor();
                    if(i>45 && i < 50)
                    {
                        GC.Collect();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"After 2nd collection, Generation of 'mm' is {GC.GetGeneration(mm)}");
                        Console.ResetColor();
                    }
                }
            }
            
            Console.ResetColor();

            Console.WriteLine("Press a key to terminate.");
            Console.ReadKey();
        }
    }
}

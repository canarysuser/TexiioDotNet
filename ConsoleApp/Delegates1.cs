using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //Step 1: Declaration of Delegates 
    public delegate int ArithmeticDelegate(int x, int y);


    internal class Delegates1
    {
        //Instance Method
        public int Add(int x, int y) => x + y;
        //Non-Instance Method
        static int Minus(int x, int y) => x - y;
        public int Multiply(int x, int y) => x * y;
        static int Divide(int x, int y) => (y > 0) ? x / y : 0;

        internal static void Test()
        {
            Delegates1 d = new Delegates1();
            //Step 2: Instantiation 
            ArithmeticDelegate ad = new ArithmeticDelegate(d.Add);
            //Step 3: INvocation 
            var result = ad(10,10); //type-inference 
            Console.WriteLine($"{nameof(Add)}(10, 10) returns {result}"); //nameof returns the name of the member
            result = ad.Invoke(50, 50);
            Console.WriteLine($"{nameof(Add)}(50, 50) returns {result}"); //nameof returns the name of the member

            //Multi-cast delegate 
            ad += new ArithmeticDelegate(Minus);
            ad += new ArithmeticDelegate(d.Multiply);
            ad += Divide;

            result = ad.Invoke(100, 100);
            Console.WriteLine($"XXXXX(100, 100) returns {result}"); //nameof returns the name of the member

            //Local Function 
            void IterateOverInvocationList(ArithmeticDelegate arithDel)
            {
                foreach(Delegate del in arithDel.GetInvocationList())
                {
                    object res = del.DynamicInvoke(500, 5)!; 
                    Console.WriteLine($"Method {del.Method.Name}(500,5) returns {res}");
                }
            }

            IterateOverInvocationList(ad);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{

    class Statics
    {
        internal int instanceField = 1234;
        internal static int staticField = 9876; 

        static Statics() { Console.WriteLine("Statics.staticCtor() called."); }
        public Statics() { Console.WriteLine("Statics.instance Ctor() called."); }

        public void InstanceMethod()
        {
            Console.WriteLine($"Static Field: {staticField}, Instance Field: {instanceField}");
        }
        public static void StaticMethod()
        {
            Console.WriteLine($"Static Field: {staticField}, Instance Field: NOT ACCESSIBLE");
        }
    }

    public static class Utilities
    {
        public static bool IsValid(this string input) => input.Contains("@");
    }

    internal class Miscellaneous
    {
        internal static void Test()
        {

            Statics.StaticMethod();
            Statics s = new Statics();
            s.InstanceMethod();



            //object obj = new object();
            //obj.ToString(); 
            ////first parameter to the function is always the object on which it was invoked.
            ////hidden parameter that is passed and referenced as "this" inside the function 

            //string s2 = "";
            //s2.IsValid();
            //string email = "someone@example.com";
            //if (email.IsValid())
            //{
            //    Console.WriteLine("Email is valid.");
            //} else { Console.WriteLine("Email is not valid."); }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class ExceptionHandling
    {
        static void ThrowException()
        {
            int a=10, b=0, c=0;
            c = a / b;
        }
        static void HandleExceptions()
        {
            try
            {
                ThrowException();
            }
            catch (DivideByZeroException de)
            {
                Console.WriteLine(de.Message);
                //throw de;
                throw;
            }
            catch (ArithmeticException ae) { Console.WriteLine(ae.Message); }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally { Console.WriteLine("Completed...."); }
        }
        internal static void Test()
        {
            try
            {
                HandleExceptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}\nStack Trace: \n{ex.StackTrace}");
            }
        }
    }
}

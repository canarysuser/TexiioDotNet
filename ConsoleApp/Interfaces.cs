using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    interface IBase
    {
       string Name { get; } //Property Declaration 
    }
    interface IChild : IBase
    {
        void Show(); //Method declaration 
    }
    class InterfaceImplementation : IBase, IChild
    {
        public string Name { get; set; }
        public void Show() { Console.WriteLine("InterfaceImplementation.Show() called."); }
    }
    internal class Interfaces
    {
        internal static void Test()
        {
            InterfaceImplementation ii = new InterfaceImplementation();
            ii.Show(); ii.Name = "Test";
            IBase ib = ii; //Late Binding 
            Console.WriteLine(ib.Name);
            //ib.Show();
            IChild ic = ii; 
            Console.WriteLine(ic.Name);
            ic.Show();
        }
    }
}

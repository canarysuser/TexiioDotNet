using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    abstract class AbstractBaseClass
    {
        public AbstractBaseClass()
        {
            Console.WriteLine("AbstractBaseClass.ctor( ) called.");
        }
        public abstract void Show();

    }
    class BaseClass : AbstractBaseClass
    {
        private int _privateField = 1234;
        protected int _protectedField = 1222;
        public int _publicField = 1111;
        internal int _internalField = 2222;
        protected internal int _protIntField = 2222;

        public BaseClass() 
        {
            Console.WriteLine("BaseClass.ctor( ) called.");
        }
        public BaseClass(int number)
        {
            _privateField = _protectedField = _publicField = _internalField = _protIntField = number;
            Console.WriteLine("BaseClass.ctor( int parameter ) called.");
        }

        public override void Show()
        {
            StringBuilder sb = new();
            sb.AppendLine("*** BASECLASS.SHOW() CALLED ***** ")
                .AppendLine($"Private Field: {_privateField}")
                .AppendLine($"Protected Field: {_protectedField}")
                .AppendLine($"Public Field: {_publicField}")
                .AppendLine($"Internal Field: {_internalField}")
                .AppendLine($"Protected Internal Field: {_protIntField}");
            Console.WriteLine(sb.ToString());
        }
    }
    class DerivedClass : BaseClass
    {
        public DerivedClass() { Console.WriteLine("DerivedClass.ctor(  ) called."); }
        public DerivedClass(int number) : base(number) 
        {
            Console.WriteLine("DerivedClass.ctor( int parameter ) called.");
        }
        public override void Show()
        {
            StringBuilder sb = new();
            sb.AppendLine("*** DERIVEDCLASS.SHOW() CALLED ***** ")
                //.AppendLine($"Private Field: {_privateField}")
                .AppendLine($"Protected Field: {_protectedField}")
                .AppendLine($"Public Field: {_publicField}")
                .AppendLine($"Internal Field: {_internalField}")
                .AppendLine($"Protected Internal Field: {_protIntField}");
            Console.WriteLine(sb.ToString());
        }
    }
    internal class Inheritance
    {
        internal static void Test()
        {
            BaseClass bc1 = new BaseClass(); 
            bc1.Show();
            BaseClass bc2 = new BaseClass(98765);
            bc2.Show();
            Console.WriteLine("".PadLeft(45, '='));
            DerivedClass dc1 = new DerivedClass();
            dc1.Show(); 
            DerivedClass dc2 = new DerivedClass(45678);
            dc2.Show();
            Console.WriteLine("".PadLeft(45, '='));

            bc1 = dc1;
            bc1.Show();

            //AbstractBaseClass abc = new AbstractBaseClass();
        }
    }
}

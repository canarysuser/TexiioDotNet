using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class GenericCollection<T> 
    {
        List<T> list = new List<T>();
        public void Add(T item) => list.Add(item);
        public void Remove(T item) => list.Remove(item);
        public void Clear() => list.Clear();
        public T GetAt(int index) => list[index];
        public T[] GetAll() => list.ToArray();
        //public static explicit T operator+(T t1, T t2) { return new T(); }
    }
    internal class GenericExample
    {
        internal static void Test()
        {
            GenericCollection<int> intColl = new GenericCollection<int>();
            intColl.Add(1); intColl.Add(2);
            int num = intColl.GetAt(0);
            GenericCollection<string> stringColl = new GenericCollection<string>();
            stringColl.Add("a"); stringColl.Add("b"); 
            string x = stringColl.GetAt(0);
            //2 new classes are created - GenericCollection`int, GenericCollection`string
            int a = 10, b = 20; 
            Swap<int>(ref a, ref b); 
            double d1=0, d2=0;
            Swap<double>(ref d1, ref d2);
            string s1 = "", s2 = "";
            Swap(ref s1, ref s2);

        }
        
        //static void Swap(int a, int b) { }
        //static void Swap(double d1, double d2) { }
        //static bool Add<T>(T a, T b) where T : IEquatable<T>
        //{
        //    return a > b;
        //}
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a; a = b; b = temp; 
        }
    }
}

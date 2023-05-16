using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    //Event data Class 
    public class CustomEventArgs : System.EventArgs
    {
        public int Number { get; set; }
        public string Message { get; set; }=string.Empty;
    }
    //Delegate declaration 
    public delegate void CustomEventHandler(object sender, CustomEventArgs e); 
    public class Publisher
    {
        public CustomEventHandler CustomEvent;
        public void RaiseNotifications()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i == 5 || i == 6)
                {
                    CustomEventArgs e = new CustomEventArgs { Message = "Threshold reached", Number = i };
                    CustomEvent?.Invoke(null, e);
                    //?. -> Null Conditional -- check whether CustomEvent is not null, if not, then calls invoke
                    //else does not call invoke method 
                    //if (CustomEvent != null) CustomEvent.Invoke(null, e);
                }
            }
        }
    }
    public class Subscriber
    {
        public void HandleEvent(object sender, CustomEventArgs args)
        {
            Console.WriteLine($"SUBSCRIBER SAYS: [Message: {args.Message}, Number: {args.Number}]");
        }
    }
    internal class EventsAndDelegates
    {
        internal static void Test()
        {
            Publisher p = new Publisher();
            Subscriber s = new Subscriber();

            p.CustomEvent += new CustomEventHandler(s.HandleEvent);
            //Anonymous Methods - Inline function
            p.CustomEvent += delegate (object sender, CustomEventArgs e)
            {
                Console.WriteLine($"ANON SAYS: [Message: {e.Message}, Number: {e.Number}]");
            };
            //Lambdas  =>
            p.CustomEvent+=(s,a)=> Console.WriteLine($"LAMBDA SAYS: [Message: {a.Message}, Number: {a.Number}]");
            //Lambda Expression -> single line of code 
            //Lambda Statement -> multiple lines of code { ... } 
            //() => { } == no arguments 
            //(a)=>{} or a=>{} == for single argument 
            //(a,b)=>{} == for more than 2 arguments. 


            p.RaiseNotifications();
        }
    }
}

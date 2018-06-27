using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySuperLib;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");

            var fl = new FancyLib();
            
            Console.WriteLine(fl.SayHello("random"));
            Console.ReadLine();
        }
    }
}

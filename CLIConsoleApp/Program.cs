using System;
using System.Collections.Generic;

namespace CLIConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            List<string> a = new List<string>();

            string[] b = { };
            Console.WriteLine("Input for Slowmotionizer (Empty to continue): ");
            InputSlowmotionizer(b);

            Console.WriteLine("ForEach Loop over Range, which yields for every i from to ");
            YieldStatement(b);
            // Range(-10, 10);

            Console.WriteLine("\nTry and Catch division exception: 1 / 10 = no error->finally goodbye");
            string[] c = { "1", "10" };
            TryCatch(c);
            Console.WriteLine("Try and Catch division exception: 1 / ??? = too few args in string[] arg, caught->goodbye");
            string[] d = { "1" };
            TryCatch(d);
            Console.WriteLine("Try and Catch division exception: 1 / 0 = checked 0 division, uncaught");
            string[] e = { "1", "0" };
            string confirm = "Do you want to divide by zero now? (checked, no way to catch)";
            Console.WriteLine(confirm);
            confirm = Console.ReadLine();
            if(confirm == "yes" || confirm == "y")
            {
                TryCatch(e);
            }
            Console.WriteLine("\nNow: the unchecked results of accessing int.MaxValue + 1? (No crash, client should handle)");
            Unchecked();
            string confirm1 = "Do you want to crash the program using a checked exception now? (client can't handle, like dividing by 0)";
            Console.WriteLine(confirm1);
            confirm1 = Console.ReadLine();
            if(confirm1 == "yes" || confirm1 == "y")
            {
                Checked(); 
            }

        }


        static void InputSlowmotionizer(string[] args)
        {
            string s;
            do 
            {
                s = Console.ReadLine();
                s = Stretch(s);
                Console.WriteLine(s);
            } while (!string.IsNullOrEmpty(s));
        }
        static string Stretch(string s)
        {
            List<string> a = new List<string>();
            int i = 0;

            foreach (var q in s.ToCharArray())
            {
                a.Add(q.ToString());
                i++;
                if ("aeiousöüäèé".Contains(q.ToString().ToLower()))
                {
                    for (int j = i + 1; j < 5+i; j++)
                    {
                        a.Add(q.ToString());
                    }
                }
            }
            return string.Join("", a.ToArray());
        }


        static void YieldStatement(string[] args)
        {
            foreach (int i in Range(-10, 10))
            {
                Console.Write($"{ i}, ");
            }
        }
        static IEnumerable<int> Range(int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                yield return i;
            }
            yield break;
        }

        static double Divide(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException();
            return x / y;
        }
        static void TryCatch(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    throw new InvalidOperationException("Two numbers required");
                }
                double x = double.Parse(args[0]);
                double y = double.Parse(args[1]);
                Console.WriteLine(Divide(x, y));
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.WriteLine("Good bye!");
            }
        }


        static void Unchecked()
        {
            int x = int.MaxValue;
            unchecked
            {
                Console.WriteLine(x + 1);  // Overflow
            }
        }
        static void Checked()
        {
            int x = int.MaxValue;
            checked
            {
                Console.WriteLine(x + 1);  // Exception
            }
        }


    }
}

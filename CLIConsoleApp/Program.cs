using System;
using System.Collections.Generic;

namespace CLIConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string s = "Testing DoStatement()";
            List<string> a = new List<string>();
            int i = 0;

            string[] b = { };
            InputSlowmotionizer(b);

            YieldStatement(b);
            Range(-10, 10);
            string[] c = { "1", "10" };
            TryCatch(c);
            string[] d = { "1" };
            TryCatch(d);
            string[] e = { "1", "0" };
            TryCatch(e);
            CheckedUnchecked(b);

        }


        static void InputSlowmotionizer(string[] args)
        {
            string s;
            do 
            {
                s = Console.ReadLine().ToLower();
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
                if ("aeiousöüäèé".Contains(q.ToString()))
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
                Console.WriteLine(i);
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


        static void CheckedUnchecked(string[] args)
        {
            int x = int.MaxValue;

            checked
            {
                Console.WriteLine(x + 1);  // Exception
            }
            unchecked
            {
                Console.WriteLine(x + 1);  // Overflow
            }
        }


    }
}

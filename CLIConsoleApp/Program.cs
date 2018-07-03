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

            foreach(var q in s.ToCharArray())
            {
                a.Add(q.ToString());
                i++;
                if (q.ToString() == "a" | q.ToString() == "o" | q.ToString() == "i")
                {
                    for(int j = i + 1; j < 5; j++)
                    {
                        a.Add(q.ToString());
                    }
                }
            }
            string[] b = { };
            InputSlowmotionizer(b);
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {


        static void Main(string[] args)
        {
            string s = "aaabbcccdde";
            string res = Compression(s);
            Console.WriteLine(res);
            Console.ReadKey();
        }
        public static string Compression(string s)
        {
            StringBuilder res = new StringBuilder();

            int counter = 1;
            for (int i = 1; i < s.Length; i++)
            {
                char lastsymbol = s[i - 1];
                char symbol = s[i];

                if (lastsymbol == symbol)
                {

                    counter++;
                }
                else
                {
                    res.Append(lastsymbol);
                    if (counter != 1)
                    {
                        res.Append(counter);
                    }
                    counter = 1;
                }

            }
            res.Append(s[s.Length - 1]);
            if (counter != 1)
            {
                res.Append(counter);
            }
            return res.ToString();
        }
    }

}

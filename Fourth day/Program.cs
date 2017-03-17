using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Security.Cryptography;
using System.Diagnostics;

namespace Fourth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string instructions = "abcde";
            int time = 0;
            Console.WriteLine(adventCoinsmining(instructions, ref time));
            Console.WriteLine(time);
            Console.ReadLine();
        }
        private static int adventCoinsmining(string hash, ref int time)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int number = 0;
            string input = "ayylmao";
            while (input.Substring(0, 5) != "00000")
            {
                input = getHexadecimalHash(hash + number);
                number++;
            }
            sw.Stop();
            time = (int)sw.ElapsedMilliseconds;
            return number - 1;
        }
        private static string getHexadecimalHash(string input)
        {
            using (MD5 md5hash = MD5.Create())
            {
                byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}

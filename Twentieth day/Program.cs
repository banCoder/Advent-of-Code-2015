using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Twentieth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            //int i = 0;
            //while (true)
            //{
            //    int sum = 0;
            //    i++;

            //    for (int j = i; j > 0; j--)
            //        if (i % j == 0)
            //            sum += j * 10;

            //    if (sum >= 1000000)
            //    {
            //        Console.WriteLine("House: {0}  -  Visits: {1}", i, sum);
            //        break;
            //    }

            //    if (i % 10000 == 0)
            //        Console.WriteLine("House: {0}  -  Visits: {1}", i, sum);
            //}
            //Console.WriteLine(" === DONE ===");
            //Console.ReadLine();
            Console.WriteLine(numOfPresents(29000000));
            Console.ReadLine();
        }
        private static int numOfPresents(int lookFor)
        {
            int[] numberOfGifts = new int[200000000];
            for (int i = 1; i < int.MaxValue; i++)
            {
                int[] range = Enumerable.Range(1, 10 * i).ToArray();
                for (int j = 0; j < range.Length; j++)
                {
                    range[j] *= i;
                    numberOfGifts[range[j]] += i * 10;
                    if (numberOfGifts[range[j]] >= lookFor)
                        return range[j];
                    if (numberOfGifts[range[j]] % 1000000 == 0)
                        Console.WriteLine(numberOfGifts[range[j]]);
                }
            }
            return 0;
        }
    }
}

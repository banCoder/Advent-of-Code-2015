using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Twelfth_day
{
    class Program
    {
        static void Main(string[] args)
        {
            string instructions = File.ReadAllText(@"D:\Documents\day12instructions.txt");
            Console.WriteLine(addNumbers(instructions));

            dynamic idk = JsonConvert.DeserializeObject(instructions);
            Console.WriteLine(GetSum(idk, "red"));

            jsonStuff();

            Console.ReadLine();
        }
        private static void jsonStuff()
        {
            string json = @"{
                        'channel': {
                            'title': 'James Newton-King',
                            'link': 'http://james.newtonking.com',
                            'description': 'James Newton-Kings blog.',
                            'item': [
                            {
                                'title': 'Json.NET 1.3 + New license + Now on CodePlex',
                                'description': 'Annoucing the release of Json.NET 1.3, the MIT license and the source on CodePlex',
                                'link': 'http://james.newtonking.com/projects/json-net.aspx',
                                'categories': [
                                'Json.NET',
                                'CodePlex'
                                ]
                            },
                            {
                                'title': 'LINQ to JSON beta',
                                'description': 'Annoucing LINQ to JSON',
                                'link': 'http://james.newtonking.com/projects/json-net.aspx',
                                'categories': [
                                'Json.NET',
                                'LINQ'
                                ]
                            }
                            ]
                        }
                        }";
            JObject rss = JObject.Parse(json);
            string rssTitle = (string)rss["channel"]["title"];
            string itemTitle = (string)rss["channel"]["item"][0]["title"];
            JArray categories = (JArray)rss["channel"]["item"][0]["categories"];
            IList<string> categoriesText = categories.Select(c => (string)c).ToList();

            IEnumerable<string> postTitles = from p in rss["channel"]["item"]
                             select (string)p["title"];
            foreach (string s in postTitles)
            {
                Console.WriteLine(s);
            }
            var allCategories = from c in rss["channel"]["item"].Children()["categories"].Values<string>()
                                group c by c into g
                                orderby g.Count() descending
                                select new { Category = g.Key, Count = g.Count() };
            foreach (var c in allCategories)
            {
                Console.WriteLine($"{c.Category} {c.Count}");
            }
        }
        private static int addNumbers(string input)
        {
            List<int> intList = new List<int>();
            MatchCollection matches = Regex.Matches(input, @"\-*\d+");
            foreach (Match match in matches)
            {
                foreach (Capture capture in match.Captures)
                    intList.Add(int.Parse(capture.Value));
            }
            return intList.Sum();
        }
        private static long GetSum(JObject o, string avoid)
        {
            // gets all properties that are JValue
            IEnumerable<JValue> shouldAvoidayy = o.Properties()
                .Select(a => a.Value).OfType<JValue>();
            // finds out if they contain something
            bool shouldAvoid = shouldAvoidayy
                .Select(v => v.Value).Contains(avoid);
            if (shouldAvoid)
                return 0;
            return o.Properties()
                .Sum((dynamic a) => (long)GetSum(a.Value, avoid));
        }
        private static long GetSum(JArray arr, string avoid) => arr.Sum((dynamic a) => (long)GetSum(a, avoid));
        private static long GetSum(JValue val, string avoid) => val.Type == JTokenType.Integer ? (long)val.Value : 0;
    }
}

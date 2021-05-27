using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;


using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadPageHttpClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "C# console program");

            var content = await client.GetStringAsync("https://www.hplovecraft.com/writings/texts/fiction/no.aspx");
            string formatedtext = Regex.Replace(content, "<.*?>", string.Empty);
            string s = formatedtext.Substring(formatedtext.IndexOf("I went to Ellston"));
            int removeafter = s.IndexOf("&nbsp;");
            s = s.Remove(removeafter);
            Text(s);
        }

    
        
        static void Text(string s)
        {
            Console.Clear();


            Console.WriteLine("The top 10 words in the short story 'Lovecraft'");
            // string text = System.IO.File.ReadAllText(@"C:\Users\frank\OneDrive\Desktop\NightOcean.txt");
            Console.WriteLine("");
            string text = Regex.Replace(s, @"[^\w\d\s]", "");
            string[] casesensetive = text.Split(" ");

            string lowcase = text.ToLower();
            string[] lowercasewords = lowcase.Split(" ");

            List<string> casevalueslist = new List<string>(casesensetive);
            List<string> valuesList = new List<string>(lowercasewords);

            var casefreq = GetFrequencies(casevalueslist);
            var freq = GetFrequencies(valuesList);
            freq.Remove("");
            casefreq.Remove("");

            DisplaySortedFrequencies(freq, casefreq);



            Dictionary<string, int> GetFrequencies(List<string> values)
            {
                Dictionary<string, int> result = new Dictionary<string, int>();
                foreach (string value in values)
                {
                    if (result.TryGetValue(value, out int count))
                    {
                        result[value] = count + 1;
                    }
                    else
                    {
                        result.Add(value, 1);
                    }
                }
                return result;
            }

            void DisplaySortedFrequencies(Dictionary<string, int> frequencies, Dictionary<string, int> cases)
            {
                // Order pairs in dictionary from high to low frequency.
                var sorted = from pair in frequencies
                             orderby pair.Value descending
                             select pair;


                int i = 0;
                // Display first 10 results in order.
                foreach (var pair in sorted)
                {
                    if (i < 10)
                    {
                        if (cases.TryGetValue(UppercaseFirst(pair.Key), out int countfirstupper))
                        {
                            Console.Write($"{UppercaseFirst(pair.Key)}: {countfirstupper}");
                        }
                        if (cases.TryGetValue(pair.Key, out int countlower))
                        {
                            string firstline = countfirstupper > 0 ? ", " : "";
                            Console.Write($"{firstline}{pair.Key}: {countlower}");
                        }
                        if (cases.TryGetValue(pair.Key.ToUpper(), out int count) && pair.Key.Length > 1)
                        {
                            Console.Write($", {pair.Key.ToUpper()}: {count}");
                        }

                        Console.WriteLine("");
                    }

                    i++;
                }
            }

            string UppercaseFirst(string s)
            {
                // Check for empty string.
               if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                // Return char and concat substring.
                return char.ToUpper(s[0]) + s.Substring(1);
            }
        }
    }
}

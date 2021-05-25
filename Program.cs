using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

Main();

void Main()
{
    string text = System.IO.File.ReadAllText(@"C:\Users\frank\OneDrive\Desktop\NightOcean.txt");
    string lowcase = text.ToLower();
    lowcase = Regex.Replace(lowcase, @"[^\w\d\s]", "");
    
    string[] words = lowcase.Split(" ");

    List<string> valuesList = new List<string>(words);
    var freq = GetFrequencies(valuesList);
    freq.Remove("");
    DisplaySortedFrequencies(freq);

}

Dictionary<string, int> GetFrequencies(List<string> values)
{
    Dictionary<string,int> result = new Dictionary<string, int>();
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

void DisplaySortedFrequencies(Dictionary<string, int> frequencies)
{
    // Order pairs in dictionary from high to low frequency.
    var sorted = from pair in frequencies
                 orderby pair.Value descending
                 select pair;
    int i = 0;
    // Display all results in order.
    foreach (var pair in sorted)
    {
        if (i < 10)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
        i++;
    }
}
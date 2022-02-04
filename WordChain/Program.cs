using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WordChain;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Word Chain");
        Console.WriteLine("----------");

        var dictionary = File.ReadAllLines("resources/dictionary.txt");

        Console.WriteLine("Start Word:");
        var start = Console.ReadLine().ToLower();
        Console.WriteLine("End Word:");
        var end = Console.ReadLine().ToLower();

        var stopWatch = new Stopwatch();
        stopWatch.Start();

        if (!ValidateWords(start, end))
        {
            Console.WriteLine("Please enter valid start and end words");
        }
        else
        {
            var length = start.Length;
            var subDictionary = dictionary.Where(x => x.Length == length).ToList();

            var chain = GetChain(start, end, subDictionary);

            stopWatch.Stop();

            if (chain == null)
            {
                Console.WriteLine("Unable to find a Chain :-(");
            }
            else
            {
                Console.WriteLine($"The Chain (Completed in {stopWatch.ElapsedMilliseconds} milliseconds):");

                foreach (var word in chain)
                {
                    Console.WriteLine(word);
                }
            }
        }

        Console.ReadLine();
    }

    private static bool ValidateWords(string start, string end)
    {
        return start != end && start.Length == end.Length;
    }

    public static ICollection<string> GetChain(string start, string end, ICollection<string> dictionary)
    {
        dictionary.Remove(start);
        var found = false;
        var chains = ExpandChains(new List<ICollection<string>> {new[] {start}}, end, dictionary, ref found);

        while (!found)
        {
            chains = ExpandChains(chains, end, dictionary, ref found);

            if (!chains.Any())
            {
                return null;
            }
        }

        return chains[chains.Count - 1];
    }

    public static IList<ICollection<string>> ExpandChains(IList<ICollection<string>> chains, string end,
        ICollection<string> dictionary, ref bool found)
    {
        var newChains = new List<ICollection<string>>();
        var chainsLength = chains.Count;
        // Note, a for loop is used throughout the code instaed of a foreach or a LINQ statements as for was the best performant when it came to speed testing
        for (var i = 0; i < chainsLength; i++)
        {
            var lastWord = chains[i].Last();
            var nextWords = GetPotentialNextWords(lastWord, dictionary);
            var wordsCount = nextWords.Count;
            for (var w = 0; w < wordsCount; w++)
            {
                var word = nextWords[w];
                dictionary.Remove(word);
                var newChain = chains[i].ToList();
                newChain.Add(word);
                newChains.Add(newChain);

                if (string.Equals(word, end))
                {
                    found = true;
                    break;
                }
            }

            if (found)
            {
                break;
            }
        }

        return newChains;
    }

    public static List<string> GetPotentialNextWords(string word, IEnumerable<string> dictionary)
    {
        return dictionary.Where(x => IsValid(word, x)).ToList();
    }

    public static bool IsValid(string word1, string word2)
    {
        var distance = 0;
        var length = word1.Length;
        for (var i = 0; i < length; i++)
        {
            if (word1[i] != word2[i])
            {
                distance++;

                if (distance > 1)
                {
                    return false;
                }
            }
        }

        return distance == 1;
    }
}
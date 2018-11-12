using System;
using System.Collections.Generic;
using System.Linq;

namespace TextGenerator
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var freqDict = GetFrequencyDictionary(text);

            foreach (var pair in freqDict)
                result[pair.Key] = GetContinuation(pair.Value);

            return result;
        }

        private static string GetContinuation(List<string> continuations)
        {
            var grouped = continuations
                .GroupBy(s => s)
                .Select(b => Tuple.Create(b.Key, b.Count()));
            
            var maxFrequency = grouped.Select(r => r.Item2).Max();

            var top = grouped.Where(i => i.Item2 == maxFrequency);

            var result = top.First().Item1;
            if (top.Count() == 1)
                return result;
            else
            {
                foreach (var word in top.Select(a => a.Item1))
                    if (string.CompareOrdinal(word, result) < 0)
                        result = word;
            }

            return result;
        }

        private static Dictionary<string, List<string>> GetFrequencyDictionary(List<List<string>> text)
        {
            var freqDict = new Dictionary<string, List<string>>();

            foreach (var sentance in text)
            {
                string previous1 = null;
                string previous2 = null;
                foreach (var word in sentance)
                {
                    if (previous2 != null)
                    {
                        var key = previous2 + " " + previous1;
                        Put(freqDict, key, word);
                    }

                    if (previous1 != null)
                        Put(freqDict, previous1, word);

                    previous2 = previous1;
                    previous1 = word;
                }
            }
            return freqDict;
        }

        private static void Put(Dictionary<string, List<string>> dictionary, string key, string value)
        {
            if (dictionary.ContainsKey(key))
                dictionary[key].Add(value);
            else
                dictionary[key] = new List<string> { value };
        }
    }
}

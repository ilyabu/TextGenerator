using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var resultString = new StringBuilder(phraseBeginning);

            while (wordsCount > 0)
            {
                var startWords = resultString.ToString().Split(' ');

                if (startWords.Length > 1 && nextWords.ContainsKey(GetLastTwoWords(startWords)))
                {
                    resultString.Append(" " + nextWords[GetLastTwoWords(startWords)]);
                }
                else if (nextWords.ContainsKey(startWords[startWords.Length - 1]))
                {
                    resultString.Append(" " + nextWords[startWords[startWords.Length - 1]]);
                }
                else
                {
                    return resultString.ToString();
                }
                wordsCount--;
            }

            return resultString.ToString();
        }

        private static string GetLastTwoWords(string[] startWords) => startWords[startWords.Length - 2] + " " + startWords[startWords.Length - 1];
    }
}

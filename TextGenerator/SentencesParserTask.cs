using System.Collections.Generic;
using System.Text;

namespace TextGenerator
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();

            var sentenceSeparators = new char[] {'.','!','?',';',':','(',')' };

            var sentences = text.Split(sentenceSeparators);

            foreach (var sentence in sentences)
            {
                var words = GetWords(sentence);
                if(words.Count>0)
                    sentencesList.Add(words);
            }

            return sentencesList;
        }

        private static List<string> GetWords(string sentence)
        {
            var result = new List<string>();

            var wordBuilder = new StringBuilder();
            foreach (var ch in sentence)
            {
                if (isPartWord(ch))
                    wordBuilder.Append(ch);
                else if(wordBuilder.Length > 0)
                {
                    result.Add(wordBuilder.ToString().ToLower());
                    wordBuilder.Clear();
                }
            }

            if(wordBuilder.Length>0)
                result.Add(wordBuilder.ToString().ToLower());

            return result;
        }

        private static bool isPartWord(char c) => char.IsLetter(c) || c == '\''; 
    }
}
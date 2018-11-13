using NUnitLite;
using System;
using System.IO;
using System.Text;

namespace TextGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            // Запуск автоматических тестов. Ниже список тестовых наборов, который нужно запустить.
            // Закомментируйте тесты на те задачи, к которым ещё не приступали, чтобы они не мешались в консоли.
            // Все непрошедшие тесты 
            var testsToRun = new string[]
            {
                "TextGenerator.SentencesParser_Tests",
                "TextGenerator.FrequencyAnalysis_Tests",
                "TextGenerator.TextGenerator_Tests",
            };
            /*new AutoRun().Execute(new[]
            {
                "--stoponerror", // Останавливать после первого же непрошедшего теста. Закомментируйте, чтобы увидеть все падающие тесты
                "--noresult",
                "--test=" + string.Join(",", testsToRun)
            });*/

            var text = File.ReadAllText("HarryPotterText.txt");
            //var text = File.ReadAllText("moskva-petushki.txt");
            var sentences = SentencesParserTask.ParseSentences(text);
            var frequency = FrequencyAnalysisTask.GetMostFrequentNextWords(sentences);
            
            while (true)
            {
                Console.Write("Введите первое слово (например, harry): ");
                var beginning = Console.ReadLine();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGeneratorTask.ContinuePhrase(frequency, beginning.ToLower(), 10);
                Console.WriteLine(phrase);
            }
        }
    }
}

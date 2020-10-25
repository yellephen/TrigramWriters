using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace TrigramWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "alice in wonderland.txt";
            Console.WriteLine("Enter file name containing the text that will be read to generate a new text.");
            Console.WriteLine(String.Format("Press enter for default, {0}", input));
            string chosenFile = Console.ReadLine();
            if(!string.IsNullOrEmpty(chosenFile))
            {
                input = chosenFile;
            }
            Console.WriteLine("How many words should the new text be?");
            int wordCount = 0;
            while(wordCount < 1)
            {
                try
                {
                    wordCount = int.Parse(Console.ReadLine());
                }
                catch 
                {
                    Console.WriteLine("Enter a valid integer for the word count.");
                }
            }
            Console.WriteLine("Enter the new filename of the new text including the file extension .txt");
            var outFile = Console.ReadLine();
            
            Reader reader = new Reader(input);
            reader.Process();
            Writer writer = new Writer(reader.trigrams, wordCount, outFile);
            writer.Write();
            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }

    class Writer
    {

        List<Trigram> trigrams;
        int wordCount;
        string outFile;

        public Writer(List<Trigram> trigrams, int wordCount, string outFile)
        {
            this.trigrams = trigrams;
            this.wordCount = wordCount;
            this.outFile = outFile;
        }

        public void Write()
        {
            Trigram currentTrigram = new Trigram("", "", "");
            using (StreamWriter sw = File.AppendText(outFile))
            {
                for (int i = 0; i <= wordCount; i++)
                {
                    currentTrigram = GetNextTrigram(currentTrigram);
                    sw.Write(currentTrigram.firstWord.Replace("\n", ""));
                    sw.Write(" ");
                }
                sw.Flush();
            }

        }

        private Trigram GetNextTrigram(Trigram currentTrigram)
        {
            List<Trigram> trigramOptions = trigrams.Where(checkedTrigram => checkedTrigram.firstWord == currentTrigram.secondWord && checkedTrigram.secondWord == currentTrigram.thirdWord).ToList<Trigram>();
            if (trigramOptions.Count == 0)
                trigramOptions = trigrams;
            Random r = new Random();
            return trigramOptions[r.Next(trigramOptions.Count -1)];
        }
    }

    class Reader
    {
        string filePath;
        public List<Trigram> trigrams { get; }

        public Reader(string filePath)
        {
            this.filePath = filePath;
            trigrams = new List<Trigram>();
        }

        public void Process()
        {
            string[] inputText = File.ReadAllText(filePath).Split(" ");
            for (int i = 0; i <= inputText.Length - 3; i++)
            {
                trigrams.Add(new Trigram(inputText[i], inputText[i + 1], inputText[i + 2]));

            }
        }
    }

    struct Trigram
    {
        public string firstWord { get; }
        public string secondWord { get; }
        public string thirdWord { get; }

        public Trigram(string firstWord, string secondWord, string thirdWord)
        {
            this.firstWord = firstWord;
            this.secondWord = secondWord;
            this.thirdWord = thirdWord;
        }

    }





}

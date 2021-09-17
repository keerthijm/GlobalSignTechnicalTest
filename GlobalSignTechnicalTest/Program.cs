using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GlobalSignTechnicalTest
{
    class Program
    {
        private const int MaxWord = 20;
        static void Main(string[] args)
        {
            WordFrequencyCount(args[0]);
            Console.ReadLine();
        }

        /// <summary>
        /// Counts frequence of all words in a text file.
        /// </summary>
        /// <param name="fileName">The name of the file to count the words in.</param>
        /// <remarks>This method will print the results in a table of word and count.
        /// The output will be in descending order of word frequency. Words are counted  without regard to case-sensitivity, and punctuation marks are ignored.
        /// The file can be a full path to a file, or can be the name of a file in the same directory as the executable. 
        /// Usage would be as follows: WordCounter.WordFrequencyCount("file.txt");
        /// </remarks>
        public static void WordFrequencyCount(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName", "You must specify a file");
            }

            string fullPath = FileUtility.FindFile(fileName);

            Dictionary<string, int> wordCounts = new Dictionary<string, int>();

            int highestFrequency = 0;

            // Use the "using()" construct to properly close the file stream and dispose of it.
            using (TextReader reader = File.OpenText(fullPath))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    string cleanedLine = TextUtility.CleanLine(line);
                    string[] words = cleanedLine.Split(' ');
                    foreach (string word in words)
                    {
                        // Ignore empty tokens.
                        if (!string.IsNullOrEmpty(word))
                        {
                            int frequency = 1;
                            if (wordCounts.ContainsKey(word))
                            {
                                frequency = wordCounts[word] + 1;
                            }

                            // N.B. If there is no key for the current word, an item is added to the dictionary for that key.
                            wordCounts[word] = frequency;
                            // Track the highest frequency value for output formatting purposes.
                            if (frequency > highestFrequency)
                            {
                                highestFrequency = frequency;
                            }
                        }
                    }

                    line = reader.ReadLine();
                }

                List<KeyValuePair<string, int>> pairList = new List<KeyValuePair<string, int>>(wordCounts);
                pairList.Sort(TextUtility.ComparePairs);
                Console.WriteLine("Word\t\t\t\tFrequency");
                for (int i = 0; i < pairList.Count; i++)
                {
                    KeyValuePair<string, int> pair = pairList[i];
                    Console.WriteLine(pair.Key + "\t\t\t\t" + pair.Value);
                    if (i == MaxWord - 1)
                    {
                        break;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    internal class Recommender
    {
        #region Fields
        private List<TF_IDF_Data> TF_IDF;
        private Dictionary<string, int> Word2idx;
        private Dictionary<int, int> TermFrequencyDict;
        private List<List<int>> DataAsIndex;
        private List<string> Data;
        private PorterStemmer Stemmer;
        #endregion

        #region Public properties
        public List<TF_IDF_Data> Tf_Idf
        {
            get { return TF_IDF; }
        }
        public List<string> RawData
        {
            get { return Data; }
        }

        #endregion // Public properties

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputfile">Input data file.</param>
        public Recommender(string inputfile)
        {
            Stemmer = new PorterStemmer();
            Data = new List<string>();
            TermFrequencyDict = new Dictionary<int, int>();
            DataAsIndex = new List<List<int>>();
            ExtractData(inputfile);
            Word2idx = new Dictionary<string, int>();
            InitWordToIndex();
            ConvertDataToIndex();
            TF_IDF = new List<TF_IDF_Data>();
            ComputeTfIdf();
            Console.WriteLine("All done!");
        }
        #endregion // Constructor

        #region Private methods
        /// <summary>
        /// This method extracts certain data from the input file (it is hardcoded, and can only be used with one data source).
        /// </summary>
        /// <param name="inputfile">Input file path.</param>
        private void ExtractData(string inputfile)
        {
            foreach (string item in File.ReadAllLines(inputfile).Skip(1))
            {
                string words = string.Empty;
                string[] helper = item.Split(',', ' ');
                if (helper.Length > 2)
                {
                    for (int i = 0; i < helper.Length; i++)
                    {
                        words += Stemmer.StemWord(helper[i].Trim('"','\\')) + " ";
                    }
                    Data.Add(words);
                }
                
            }
            Console.WriteLine("Extract Done!");
        }
        /// <summary>
        /// Computing TF-IDF (Term frequency - inverse document frequency
        /// </summary>
        private void ComputeTfIdf()
        {
            for (int i = 0; i < DataAsIndex.Count; i++)
            {
                List<int> tfidf = new List<int>();
                for (int j = 0; j < DataAsIndex[i].Count; j++)
                {
                    tfidf.Add(ComputeTermFrequency(DataAsIndex[i], DataAsIndex[i][j]) * (int)(Math.Log( Data.Count / ComputeDocumentFrequency(DataAsIndex[i][j]))) );
                }
                TF_IDF.Add(new TF_IDF_Data(Data[i], DataAsIndex[i], tfidf ));
                Debug.WriteLine(i);
            }
        }
        /// <summary>
        /// Initializes Word-To-Index mapping.
        /// </summary>
        private void InitWordToIndex()
        {
            int index = 1;
            for (int i = 0; i < Data.Count; i++)
            {
                string[] helper = Data[i].Split(" ");
                for (int j = 0; j < helper.Length; j++)
                {
                    string cleanedString = helper[j].CleanString();
                    if (!Word2idx.ContainsKey(cleanedString))
                    {
                        Word2idx.Add(cleanedString, index++);
                    }
                }
            }
            Console.WriteLine("Word To Index mapping Initialized!");
        }
        /// <summary>
        /// Converts the data to each word's corresponding index.
        /// </summary>
        private void ConvertDataToIndex()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                List<int> item = new List<int>();
                string[] helper = Data[i].Split(" ");
                
                for (int j = 0; j < helper.Length; j++)
                {
                    string cleanedString = helper[j].CleanString();
                    if (cleanedString is not "")
                    {
                        item.Add(Word2idx[cleanedString]);
                    }
                }

                DataAsIndex.Add(item);
                Debug.WriteLine(i);
                
            }
            Console.WriteLine("Data as Index done!");
        }
        /// <summary>
        /// Computes document frequency of a word.
        /// </summary>
        /// <param name="word">the word which we are searching for</param>
        /// <returns>a document frequency foor a certain word</returns>
        private int ComputeDocumentFrequency(int word)
        {
            if (TermFrequencyDict.ContainsKey(word))
            {
                return TermFrequencyDict[word];
            }
            else
            {
                int counter = 0;
                for (int i = 0; i < DataAsIndex.Count; i++)
                {
                    if (DataAsIndex[i].Contains(word))
                    {
                        counter++;
                    }
                }
                TermFrequencyDict.Add(word, counter);

                return counter;
            }
        }
        /// <summary>
        /// How often does a certain word appears in one given document.
        /// </summary>
        /// <param name="document">The given document.</param>
        /// <param name="word">The given word.</param>
        /// <returns>The number of times a word appears in the document</returns>
        private int ComputeTermFrequency(List<int> document, int word) 
        {
            int counter = 0;
            for (int i = 0; i < document.Count; i++)
            {
                if (document[i] == word)
                {
                    counter++;
                }
            }
            return counter;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    internal class Recommender
    {
        #region Fields
        private int[,] TF_IDF;
        #endregion
        #region Public properties
        public List<string> Data { get; private set; }

        #endregion // Public properties

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="inputfile">Input data file.</param>
        public Recommender(string inputfile)
        {
            Data = new List<string>();
            ExtractData(inputfile);
            TF_IDF = new int[Data.Count, Data.Count];
            ComputeTfIdf();
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
                string[] helper = item.Split(",");
                Data.Add(item);
            }
        }
        /// <summary>
        /// Computing TF-IDF (Term frequency - inverse document frequency
        /// </summary>
        private void ComputeTfIdf()
        {
            for (int i = 0; i < Data.Count; i++)
            {
                string[] helper = Data[i].Split(" ");

                for (int j = 0; j < helper.Length; j++)
                {
                    TF_IDF[i, j] = (int)(helper.Count(x => x.Contains(helper[j])) * Math.Log(Data.Count / Data.Count(x => x.Contains(helper[j]) )));
                }
            }
        }
        #endregion
    }
}

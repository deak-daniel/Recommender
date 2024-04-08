using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    internal class Recommender
    {
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
        }
        #endregion // Constructor

        #region Private methods
        /// <summary>
        /// This method extracts certain data from the input file (it is hardcoded, and can only be used with one data source).
        /// </summary>
        /// <param name="inputfile">Input file path.</param>
        private void ExtractData(string inputfile)
        {
            foreach (string item in File.ReadAllLines(inputfile))
            {
                string[] helper = item.Split(",");
                Data.Add($"{helper[0]} {helper[1]} {helper[2]} {helper[4]}");
            }
        }
        #endregion
    }
}

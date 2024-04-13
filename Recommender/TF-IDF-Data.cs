using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recommender
{
    internal class TF_IDF_Data
    {
        #region Properties
        /// <summary>
        /// The property representing a certain value's raw data.
        /// </summary>
        public string RawData { get; private set; }
        /// <summary>
        /// The dataVector, which means, the raw data's converted version.
        /// </summary>
        public List<int> DataVector { get; private set; }
        /// <summary>
        /// The calculated euclidean distance of the TF-IDF vector.
        /// </summary>
        public double Distances { get; private set; }
        /// <summary>
        /// The TF-IDF vector.
        /// </summary>
        public List<int> TF_IDF { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rawData">The raw string data.</param>
        public TF_IDF_Data(string rawData)
        {
            this.RawData = rawData;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataVector">The data converted into a vector format</param>
        /// <param name="tfidf">The TF-IDF (Term-Frequency Inverse-Docuemtn-Frequency) of the Data</param>
        public TF_IDF_Data(string rawData,List<int> dataVector, List<int> tfidf) : this(rawData)
        {
            this.DataVector = dataVector;
            this.TF_IDF = tfidf;
            CalculateDistance();
        }
        #endregion

        #region Private methods
        /// <summary>
        /// A method that calculates the euclidean distance of a vector.
        /// </summary>
        private void CalculateDistance()
        {
            double distance = 0;
            for (int i = 0; i < this.TF_IDF.Count; i++)
            {
                distance += Math.Pow(TF_IDF[i], 2);
            }
            this.Distances = Math.Sqrt(distance);
        }
        #endregion
    }
}

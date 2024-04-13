namespace Recommender
{
    internal class Program
    {
        public static Random rnd = new Random();
        static void Main(string[] args)
        {
            // Initializes the recommender object.
            Recommender recommender = new Recommender("movies1.csv");

            // Chooses a movie randomly
            int randomChoice = rnd.Next(0, recommender.Tf_Idf.Count);
            TF_IDF_Data selected = recommender.Tf_Idf[randomChoice];
            Console.WriteLine($"Selected movie: {selected.RawData}");

            // Chooses 5 more movies which are the closest to the selected movie.
            Console.WriteLine("Movies that you'll probably like:");
            List<TF_IDF_Data> movies = recommender.Tf_Idf.Where(x => x.Distances < selected.Distances).OrderByDescending(x=>x.Distances).Take(5).ToList();
            for (int i = 0; i < movies.Count; i++)
            {
                Console.WriteLine($"{movies[i].RawData}");
            }

        }
    }
}

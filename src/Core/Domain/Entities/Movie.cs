namespace Vue.Core.Domain.Entities
{
    public class Movie 
    {
        public int Id { get; set; } // (id)
        public string Title { get; set; } // (title)
        public string ImageUrl { get; set; } // (image_url)
        public string Description { get; set; } // (description)
        public string ReleaseDate { get; set; } // (release_date)
        public List<string> Cast { get; set; } // (cast)
        public string Director { get; set; } // (director)
        public string VueUrl { get; set; } // (vue_url)
        public List<string> Genres { get; set; } // (genres)
        public int Length { get; set; } // in minutes (playingtime)
        public string SpokenLanguage { get; set; } // (spoken_language)
        public string Kijkwijzer { get; set; } // Kijkwijzer rating (kijkwijzer)


        // only available via movie id https://www.vuecinemas.nl/movies.json?movie_id=38441
        public int Rating { get; set; } // (rating_average)
        public int RatingCount { get; set; } // (rating_count)
    }
}

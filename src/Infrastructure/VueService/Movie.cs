namespace Vue.Infrastructure.VueService
{
    internal class Movie
    {
        public int id { get; set; }
        public string oid { get; set; }
        public string edi { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string genres { get; set; }
        public string kijkwijzer { get; set; }
        public string cast { get; set; }
        public string director { get; set; }
        public string vue_url { get; set; }
        public string trailer_url_low { get; set; }
        public string trailer_url_high { get; set; }
        public string trailer_youtube_id { get; set; }
        public string trailer_youtube_thumbnail { get; set; }
        public string image { get; set; }
        public string release_date { get; set; }
        public string special_category { get; set; }
        public int playingtime { get; set; }
        public int is_edited { get; set; }
        public string edit_date { get; set; }
        public int vue_created { get; set; }
        public string og_title { get; set; }
        public string og_description { get; set; }
        public string og_image { get; set; }
        public int variant_grouping { get; set; }
        public string image_relative { get; set; }
        public object image_missing { get; set; }
        public object full_title { get; set; }
        public object rating_average { get; set; }
        public object rating_count { get; set; }
        public object stills { get; set; }
        public object performances { get; set; }
        public object all_performances { get; set; }
        public object variants { get; set; }
        public string spoken_language { get; set; }
        public int has_closed_captioning { get; set; }
        public int has_audio_description { get; set; }
    }
}

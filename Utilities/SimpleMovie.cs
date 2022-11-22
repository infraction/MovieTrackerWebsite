using System.ComponentModel;

namespace CJMovieTracker.Utilities
{
    public class SimpleMovie
    {
        [DisplayName("Title")]
        public string movieTitle { get; set; }
        [DisplayName("Release year")]
        public int movieReleaseYear { get; set; }
        public int? tmdbId { get; set; }
        public string? poster { get; set; }
        public string? description { get; set; }
        public string? director { get; set; }

        public SimpleMovie(string movieTitle, int movieReleaseYear)
        {
            this.movieTitle = movieTitle;
            this.movieReleaseYear = movieReleaseYear;
        }
        public SimpleMovie(DM.MovieApi.MovieDb.Movies.Movie m)
        {
            movieTitle = m.Title;
            movieReleaseYear = m.ReleaseDate.Year;
            description = m.Overview;
            tmdbId = m.Id;
            poster = m.PosterPath;
        }
    }
}

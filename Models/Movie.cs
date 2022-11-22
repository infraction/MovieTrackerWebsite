using CJMovieTracker.Utilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CJMovieTracker.Data
{
    public class Movie
    {
        public int MovieId { get; set; }
        [MaxLength(100)]
        [DisplayName("Title")]
        public string MovieTitle { get; set; }
        [MaxLength(20)]
        [DisplayName("When we watched it")]
        public string? DateWatched { get; set; }
        [DisplayName("Chris's rating")]
        public double? ChrisRating { get; set; }
        [DisplayName("Jacie's rating")]
        public double? JacieRating { get; set; }
        [MaxLength(10)]
        [DisplayName("Who picked the movie")]
        public string? WhoPicked { get; set; }
        public string? ImgLink { get; set; }
        public int? TmdbID { get; set; }
        [DisplayName("When we watched it")]
        public DateOnly? DateWatchedDateType { get; set; }
        public string? ReleaseYear { get; set; }
        public string? Director { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        private static string tmdbBaseUrl = @"https://image.tmdb.org/t/p";
        [NotMapped]
        private static string tmdbBasePicSize = @"/w300";


        public Movie(SimpleMovie m)
        {
            MovieTitle = m.movieTitle;
            ReleaseYear = Convert.ToString(m.movieReleaseYear);
            TmdbID = m.tmdbId;
            Description = m.description;
            ImgLink = m.poster;

        }
        public Movie() { }

        public void InternalUpdatesBeforeSave()
        {
            DateWatchedDateType = DateOnly.Parse(DateWatched);
            DateWatched = DateWatchedDateType.ToString();
        }
        public static int CompareByWatchDate(Movie m1, Movie m2)
        {
            DateOnly m1Date = m1.DateWatchedDateType ?? default;
            DateOnly m2Date = m2.DateWatchedDateType ?? default;
            return m1Date.CompareTo(m2Date);
        }

        public string getPoster()
        {
            return tmdbBaseUrl + tmdbBasePicSize + ImgLink;
        }

    }
}

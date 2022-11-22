using DM.MovieApi;
using DM.MovieApi.ApiResponse;
using DM.MovieApi.MovieDb.Movies;
namespace CJMovieTracker.Utilities
{
    public class TmdbAPIHelper
    {
        private static string tmdbToken = "Insert tmdb api v4 access token here";
        private static string tmdbBaseUrl = @"https://image.tmdb.org/t/p";
        private static string tmdbBasePicSize = @"/w500";
        public TmdbAPIHelper()
        {
            MovieDbFactory.RegisterSettings(tmdbToken);
        }
        public async Task<int> TmdbIdFinderConsole(Data.Movie m)
        {
            int foundID = -1;
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(m.MovieTitle);

            int i = 1;
            foreach (MovieInfo info in response.Results)
            {
                Console.WriteLine($"{i}:{info.Title} ({info.ReleaseDate.Year})");
                i++;
            }
            Console.WriteLine("Enter the id of the correct movie");
            int foundMovie = Convert.ToInt32(Console.ReadLine());
            foundMovie--;
            foundID = response.Results[foundMovie].Id;
            return foundID;
        }

        public async Task<List<SimpleMovie>> TmdbIdFinder(string newMovieTitle)
        {
            List<SimpleMovie> moviePotentials = new List<SimpleMovie>();
            string title;
            int year;
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;

            ApiSearchResponse<MovieInfo> response = await movieApi.SearchByTitleAsync(newMovieTitle);

            foreach (MovieInfo movieEntry in response.Results)
            {
                title = movieEntry.Title;
                year = movieEntry.ReleaseDate.Year;
                SimpleMovie simpleMovieEntry = new SimpleMovie(title, year);
                simpleMovieEntry.tmdbId = movieEntry.Id;
                moviePotentials.Add(simpleMovieEntry);
            }
            return moviePotentials;
        }

        public async Task<Movie> MovieGrabber(int id)
        {
            MovieDbFactory.RegisterSettings(tmdbToken);
            var movieApi = MovieDbFactory.Create<IApiMovieRequest>().Value;
            ApiQueryResponse<Movie> response = await movieApi.FindByIdAsync(id);
            return response.Item;
        }

        public async Task<string> TmdbImgString(int id)
        {

            Movie movie = await MovieGrabber(id);
            string imgString = tmdbBaseUrl + tmdbBasePicSize + movie.PosterPath;
            return imgString;
        }

        public async Task<string> TmdbDirectorString(int id)
        {
            Movie movie = await MovieGrabber(id);
            string directorString = "";
            return directorString;
        }

        public async Task<string> TmdbDescriptionString(int id)
        {
            Movie movie = await MovieGrabber(id);
            string descrpitionString = movie.Overview;
            return descrpitionString;
        }
        public async Task<int> TmdbReleaseYear(int id)
        {
            Movie movie = await MovieGrabber(id);
            int year = movie.ReleaseDate.Year;
            return year;
        }
    }
}

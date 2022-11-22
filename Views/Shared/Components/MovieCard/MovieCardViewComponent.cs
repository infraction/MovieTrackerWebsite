using Microsoft.AspNetCore.Mvc;
using CJMovieTracker.Data;

namespace CJMovieTracker.Views.Shared.Components
{
    public class MovieCardViewComponent : ViewComponent
    {
         
        public async Task<IViewComponentResult> InvokeAsync(List<Movie> movieList)
        {
            return View(movieList);
        }
    }

}

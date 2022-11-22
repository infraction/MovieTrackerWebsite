using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CJMovieTracker.Data;
using Microsoft.AspNetCore.Authorization;

using CJMovieTracker.Utilities;

namespace CJMovieTracker.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieDbContext _context;
        TmdbAPIHelper helperApiClass = new TmdbAPIHelper();

        public MoviesController(MovieDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateMovieMatch(string newMovieTitle)
        {
            List<SimpleMovie> newMovieList = await helperApiClass.TmdbIdFinder(newMovieTitle);

            return View(newMovieList);
        }

        public async Task<IActionResult> CreateMovieStart()
        {
            return View();
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            List<Movie> moviesList = await _context.Movies.ToListAsync();
            moviesList.Sort(Movie.CompareByWatchDate);
            return View(moviesList);
        }




        // GET: SearchInput
        public async Task<IActionResult> SearchInput()
        {
            return View(await _context.Movies.ToListAsync());
        }
        //POST: SearchResults
        public async Task<IActionResult> SearchResults(String searchTerm)
        {
            return View("Index", await _context.Movies.Where(j => j.MovieTitle.Contains(searchTerm)).ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public async Task<IActionResult> Create(int id)
        {
            SimpleMovie addedMovieBase = new SimpleMovie(await helperApiClass.MovieGrabber(id));
            Movie addedMovie = new Movie(addedMovieBase);
            

            return View(addedMovie);
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,MovieTitle,DateWatched,ChrisRating,JacieRating,WhoPicked,ReleaseYear,TmdbID,ImgLink,Description,DateWatchedDateType")] Movie movie)
        {

            if (ModelState.IsValid)
            {
                movie.InternalUpdatesBeforeSave();
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,MovieTitle,DateWatched,ChrisRating,JacieRating,WhoPicked,ReleaseYear,TmdbID,ImgLink,Description")] Movie movie)
        {
            if (id != movie.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    movie.InternalUpdatesBeforeSave();
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.MovieId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'MovieDbContext.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.MovieId == id);
        }


        private async Task<int> DbUpdates()
        {
            List<Movie> moviesList = await _context.Movies.ToListAsync();
            int length = moviesList.Count();
            
            for (int i = 0; i < length; i++)
            {
                Movie m = moviesList[i];
                string testString = m.ImgLink.Substring(31);
                m.ImgLink = testString;
                m.InternalUpdatesBeforeSave();
                _context.Update(m);
                await _context.SaveChangesAsync();
            }
            return 0;


        }
        private async void TmdbIdConsole()
        {
            List<Movie> moviesList = await _context.Movies.ToListAsync();
            int length = moviesList.Count();

            TmdbAPIHelper helperApiClass = new TmdbAPIHelper();
            for (int i = 0; i < length; i++)
            {
                if (moviesList[i].TmdbID != null) continue;
                Movie m = moviesList[i];
                Console.WriteLine(m.MovieTitle + '\n');
                int id = await helperApiClass.TmdbIdFinderConsole(m);
                m.TmdbID = id;
                m.InternalUpdatesBeforeSave();
                _context.Update(m);
                await _context.SaveChangesAsync();
                Console.Clear();
                Console.WriteLine($"{m.MovieTitle} updated with id {id}\n");
            }
        }
    }
}

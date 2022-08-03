using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("top-revenue")]
        // Attribute Routing
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if(movies == null || !movies.Any())
            {
                // 404
                return NotFound(new {errorMessage = "No Movies Found"});
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if(movie==null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {id}" });
            }

            return Ok(movie);
        }
    }
}

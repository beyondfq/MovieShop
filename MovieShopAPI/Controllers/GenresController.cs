using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenres();
            if((genres == null) || !genres.Any())
            {
                return NotFound(new { errorMessage = "No genres Found" });
            }

            return Ok(genres);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddGenre([FromBody] GenreModel genre)
        {
            GenreModel model = new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name
            };

            var addedGenre = await _genreService.AddGenre(model);
            return Ok(addedGenre);
        }
    }
}

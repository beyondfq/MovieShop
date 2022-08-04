using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieShopAPI.Infra;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;

        public UserController(IUserService userService, ICurrentUser currentUser)
        {
            _userService = userService;
            _currentUser = currentUser;
        }

        //---------------------------------- Purchase --------------------------------------

        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            var userId = _currentUser.UserId;
            var Movies = await _userService.GetAllPurchasesForUser(userId);
            if (Movies == null)
            {
                return NotFound(new { errorMessage = "No Movies Purchased" });
            }

            return Ok(Movies);
        }

        [HttpPost]
        [Route("purchase-movie")]
        public async Task<IActionResult> BuyMovie([FromBody] UserRequestModel userRequest)
        {
            //var userId = _currentUser.UserId;
            PurchaseRequestModel model = new PurchaseRequestModel
            {
                MovieId = userRequest.movieId,
                UserId = userRequest.userId
            };

            var movie = await _userService.PurchaseMovie(model, model.UserId);
            if (model.MovieId == null)
            {
                return NotFound(new { errorMessage = "No Movie Found" });
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("check-movie-purchased/{movieId:int}")]
        public async Task<IActionResult> CheckMoviePurchased(int movieId)
        {
            var userId = _currentUser.UserId;
            PurchaseRequestModel model = new PurchaseRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };

            var movie = await _userService.IsMoviePurchased(model, userId);
            if (!movie)
            {
                return NotFound(new { errorMessage = "Movie is not purchased" });
            }

            return Ok();
        }

        [HttpGet]
        [Route("purchase-details/{movieId:int}")]
        public async Task<IActionResult> MoviePurchaseDetails(int movieId)
        {
            var userId = _currentUser.UserId;

            var movieDetails = await _userService.GetPurchasesDetails(userId, movieId);
            if (movieDetails == null)
            {
                return NotFound(new { errorMessage = "Movie is not purchased" });
            }

            return Ok(movieDetails);
        }

        //-------------------------------- Favorite --------------------------------------


    }
}

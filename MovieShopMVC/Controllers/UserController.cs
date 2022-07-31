using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // get all the movies purchased by user, user id
            // httpcontext.user.claims and then call the database and get the information to the view
            var userId = _currentUser.UserId;
            var movies = await _userService.GetAllPurchasesForUser(userId);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUser.UserId;
            var movies = await _userService.GetAllFavoritesForUser(userId);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> BuyMovie(int movieId)
        {
            var userId = _currentUser.UserId;
            //var purchased = await _userService.IsMoviePurchased(model, userId);

            //if (!purchased)
           // {
                PurchaseRequestModel model = new PurchaseRequestModel
                {
                    MovieId = movieId,
                    //Price = model.Price
                    UserId = userId
                };

                var purchases = await _userService.PurchaseMovie(model, userId);
           // }

            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpGet]
        public async Task<IActionResult> FavoriteMovie(int movieId)
        {
            var userId = _currentUser.UserId;
            FavoriteRequestModel model = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };

            await _userService.AddFavorite(model);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFavorite(int movieId)
        {
            var userId = _currentUser.UserId;
            FavoriteRequestModel model = new FavoriteRequestModel
            {
                MovieId = movieId,
                UserId = userId
            };

            await _userService.RemoveFavorite(model);
            return RedirectToAction("Details", "Movies", new { id = movieId });
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}

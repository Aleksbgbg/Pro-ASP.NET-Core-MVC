namespace UrlsAndRouting.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using UrlsAndRouting.Models;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("Target", new Target(nameof(HomeController), nameof(Index)));
        }
    }
}
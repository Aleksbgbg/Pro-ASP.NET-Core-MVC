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

        public ViewResult SomeId(int id)
        {
            Target target = new Target(nameof(HomeController), nameof(SomeId));

            target.Data[nameof(id)] = id;

            return View("Target", target);
        }

        public ViewResult VariableLength()
        {
            Target target = new Target(nameof(HomeController), nameof(VariableLength));

            target.Data["catchall"] = RouteData.Values["catchall"] ?? "<no value>";

            return View("Target", target);
        }
    }
}
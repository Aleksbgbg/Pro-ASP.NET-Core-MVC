namespace SportsStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        public ViewResult Error()
        {
            return View();
        }
    }
}
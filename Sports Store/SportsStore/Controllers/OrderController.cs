namespace SportsStore.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SportsStore.Models;

    public class OrderController : Controller
    {
        public ViewResult Checkout()
        {
            return View(new Order());
        }
    }
}
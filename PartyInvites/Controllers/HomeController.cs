namespace PartyInvites.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    using PartyInvites.Models;

    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            ViewBag.Greeting = DateTime.Now.Hour < 12 ? "Morning" : "Afternoon";

            return View();
        }

        [HttpGet]
        public ViewResult RsvpForm()
        {
            return View();
        }

        [HttpPost]
        public ViewResult RsvpForm(GuestResponse guestResponse)
        {
            if (ModelState.IsValid)
            {
                Repository.AddResponse(guestResponse);

                return View("Thanks", guestResponse);
            }

            return View();
        }

        public ViewResult ListResponses()
        {
            return View(Repository.Responses.Where(response => response.WillAttend == true));
        }
    }
}
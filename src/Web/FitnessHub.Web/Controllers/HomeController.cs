﻿namespace FitnessHub.Web.Controllers
{
    using System.Diagnostics;

    using FitnessHub.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult GoToHome()
        {
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

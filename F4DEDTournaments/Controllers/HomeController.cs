using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using F4DEDTournaments.Models;

namespace F4DEDTournaments.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error(string errorMessage, DateTime errorDate)
        {
            ViewData["ErrorMessage"] = errorMessage;
            ViewData["ErrorDate"] = errorDate;
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models;
using Microsoft.AspNetCore.Mvc;

namespace F4DEDTournaments.Controllers
{
    public class TeamController : Controller
    {
        [HttpGet]
        public IActionResult CreateTeam()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTeam(CreateTeamViewModel model)
        {


            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace F4DEDTournaments.Controllers
{
    public class TeamController : Controller
    {
        TeamManager teamManager = new TeamManager();


        [HttpGet]
        public IActionResult CreateTeam()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTeam(CreateTeamViewModel model)
        {
            TeamDTO teamDTO = new TeamDTO()
            {
                TeamName = model.TeamName,
                Description = model.Description,
                Country = model.Country,
                Language = model.Language,
                MinimumAge = model.MinimumAge,
                MinimumElo = model.MinimumElo,
                IsPrivate = model.IsPrivate,
                PlayedGame = model.PlayedGame
            };
            var result = teamManager.CreateTeam(teamDTO);
            switch (result)
            {
                case TeamErrorCodes.NoError:
                    return View();
                case TeamErrorCodes.NameAlreadyExists:
                    ModelState.AddModelError("TeamName","This name already exists!");
                    return View();
            }

            return View();

            //Return to team view;
        }
    }
}

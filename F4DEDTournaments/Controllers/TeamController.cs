﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models;
using Logic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace F4DEDTournaments.Controllers
{
    public class TeamController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        TeamManager teamManager = new TeamManager();

        public TeamController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateTeam()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateTeam(CreateTeamViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);

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
            var result = teamManager.CreateTeam(teamDTO,currentUser.Id);
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


        public IActionResult ViewTeam(string TeamID)
        {
            if(TeamID == string.Empty || TeamID == null)
            {
                return Redirect("CreateTeam");
            }

            TeamID = "6Zwk5q1WEYHS";
            var team = teamManager.GetTeamByID(TeamID);

            TeamViewModel model = new TeamViewModel()
            {
                TeamID = team.TeamID,
                TeamName = team.TeamName,
                MinimumAge = team.MinimumAge,
                MinimumElo = team.MinimumElo,
                Country = team.Country,
                Language = team.Language,
                Description = team.Description,
                IsPrivate = team.IsPrivate,
                PlayedGame = team.PlayedGame
            };
            return View(model);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models;
using F4DEDTournaments.Models.Team;
using Logic.Teams;
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
                    return RedirectToAction("ViewTeam");
                case TeamErrorCodes.NameAlreadyExists:
                    ModelState.AddModelError("TeamName","This name already exists!");
                    return View();
                case TeamErrorCodes.UnknownException:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An Unknown error occured while creating a team", errorDate = DateTime.Now });
            }

            return View();
        }


        public async Task<IActionResult> ViewTeam()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var userTeamDTO = teamManager.GetUserTeam(currentUser.Id);
            var members = ((Team)userTeamDTO.Team).GetMembers();
            TeamViewModel model = new TeamViewModel()
            {
                Team = (Team)userTeamDTO.Team,
                Role = userTeamDTO.Role,
                Members = members
            };

            return View(model);
        }

        public IActionResult Index()
        {
            var teams = teamManager.GetTop10Teams();
            IndexViewModel model = new IndexViewModel()
            {
                Teams = teams
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditTeam(string TeamID)
        {
            Team team;
            if(TeamID == null)
            {
                var currentUser = await userManager.GetUserAsync(User);
                team = teamManager.GetTeamByUser(currentUser.Id);
            } else
            {
                team = teamManager.GetTeamByID(TeamID);
            }

            return View(team);
        }


        [HttpPost]
        public IActionResult EditTeam(Team model)
        {
            model.UpdateTeam();
            return RedirectToAction("ViewTeam");
        }
    }
}

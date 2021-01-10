using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F4DEDTournaments.Models.Tournament;
using Model;
using Logic.Tournament;
using Logic.Organisation;
using Microsoft.AspNetCore.Identity;
using F4DEDTournaments.Models;
using Logic.Teams;

namespace F4DEDTournaments.Controllers
{
    public class TournamentController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        TournamentManager tournamentManager = new TournamentManager();
        OrganisationManager organisationManager = new OrganisationManager();
        TeamManager teamManager = new TeamManager();


        public TournamentController(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> CreateTournament(string OrganisationID)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (OrganisationID == null && currentUser == null)
            {
                return RedirectToAction("Register", "Account");
            }

            ViewData["Creator"] = OrganisationID == null ? "User" : "Org";

            CreateTournamentViewModel model = new CreateTournamentViewModel()
            {
                OrganisationID = OrganisationID == null ? "null" : OrganisationID,
                UserID = currentUser == null ? "null" : currentUser.Id
            };
            ViewData["OrgName"] = OrganisationID == null ? null : organisationManager.GetOrganisationByID(OrganisationID).Name;
            ViewData["UserName"] = currentUser.UserName;

            model.StartTime = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament(CreateTournamentViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);

            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                Name = model.Name,
                OrganisationID = model.OrganisationID,
                UserID = currentUser.Id,
                Size = model.Size,
                Prize = model.Prize,
                BuyIn = model.BuyIn,
                Game = model.Game,
                StartTime = model.StartTime,
                TeamSize = model.TeamSize
            };
            var result = tournamentManager.CreateTournament(tournamentDTO);

            ViewData["Creator"] = model.OrganisationID == "null" ? "User" : "Org";
            ViewData["OrgName"] = model.OrganisationID == "null" ? null : organisationManager.GetOrganisationByID(model.OrganisationID).Name;
            ViewData["UserName"] = currentUser.UserName;
            switch (result)
            {
                case TournamentManagerErrorCodes.NoError:
                    return RedirectToAction("Index");
                case TournamentManagerErrorCodes.BuyInLessOrEqualToPrize:
                    ModelState.AddModelError("BuyIn", "Buy In can't be less or equal to prize!");
                    return View(model);
                case TournamentManagerErrorCodes.NoHost:
                    return RedirectToAction("Error", "Home", new { errorMessage = "There was no Host assigned to the tournament!", errorDate = DateTime.Now });
                case TournamentManagerErrorCodes.NotEnoughMoney:
                    ModelState.AddModelError("Prize", "You don't have enough currency to create a tournament with such a prize pool!");
                    return View(model);
                case TournamentManagerErrorCodes.UnexpectedError:
                default:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An Unknown error occured while creating a tournament", errorDate = DateTime.Now });
            }
        }
        public IActionResult Index()
        {
            var Next10 = tournamentManager.Get10NextTournaments();
            var Active = tournamentManager.GetActiveTournaments();
            IndexViewModel model = new IndexViewModel()
            {
                PlannedTournaments = Next10,
                ActiveTournaments = Active
            };
            return View(model);
        }

        public async Task<IActionResult> ViewTournament(string ID)
        {
            var currentUser = await userManager.GetUserAsync(User);

            ViewTournamentModel model = new ViewTournamentModel();
            if(ID != null)
            {
                model.Tournament = tournamentManager.GetTournamentById(ID);
                model.IsOwner = model.Tournament.UserID == currentUser.Id;
                model.CurrentUsers = model.Tournament.GetUsers();
            } else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> JoinTournament(string tournamentID)
        {
            var tournament = tournamentManager.GetTournamentById(tournamentID);
            var currentUser = await userManager.GetUserAsync(User);
            TournamentErrorCode result = TournamentErrorCode.UnknownError;
            switch (tournament.TeamSize)
            {
                case 1:
                    result = tournament.AddUser(currentUser.Id);
                    break;
                case 2:
                    result = TournamentErrorCode.UnknownError;
                    //Invite player screen
                    break;
                case 5:
                    var Team = teamManager.GetTeamByUser(currentUser.Id);
                    if(Team != null)
                    {
                        result = tournament.AddTeam(Team.TeamID);
                    }
                    break;
                default:
                    result = TournamentErrorCode.UnknownError;
                    break;
            }

            switch (result)
            {
                case TournamentErrorCode.NoError:
                    return RedirectToAction("ViewTournament", new {tournament.ID});
                case TournamentErrorCode.CouldntAddUserToTournament:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An user couldn't get added for unknown reason", errorDate = DateTime.Now });
                case TournamentErrorCode.CouldntAddTeamToTournament:
                    return RedirectToAction("Error", "Home", new { errorMessage = "A team couldn't get added for unknown reason", errorDate = DateTime.Now });
                case TournamentErrorCode.UnknownError:
                default:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An unknown error occured", errorDate = DateTime.Now });
            }
        }

        public IActionResult StartTournament(string tournamentID)
        {
            var tournament = tournamentManager.GetTournamentById(tournamentID);
            //var currentUser = await userManager.GetUserAsync(User);

            var result = tournament.Start();

            switch (result)
            {
                case TournamentErrorCode.NoError:
                    return RedirectToAction("ViewTournament", new { tournament.ID });
                case TournamentErrorCode.CouldntUpdate:
                    return RedirectToAction("Error", "Home", new { errorMessage = "The Tournament couldn't start", errorDate = DateTime.Now });
                case TournamentErrorCode.UnknownError:
                default:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An unknown error occured", errorDate = DateTime.Now });
            }
        }
        public IActionResult EndTournament(string tournamentID)
        {
            var tournament = tournamentManager.GetTournamentById(tournamentID);
            //var currentUser = await userManager.GetUserAsync(User);

            var result = tournament.End();

            switch (result)
            {
                case TournamentErrorCode.NoError:
                    return RedirectToAction("Index");
                case TournamentErrorCode.CouldntUpdate:
                    return RedirectToAction("Error", "Home", new { errorMessage = "The Tournament couldn't end", errorDate = DateTime.Now });
                case TournamentErrorCode.UnknownError:
                default:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An unknown error occured", errorDate = DateTime.Now });
            }
        }
    }
}

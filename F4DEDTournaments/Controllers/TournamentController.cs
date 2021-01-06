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

namespace F4DEDTournaments.Controllers
{
    public class TournamentController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        TournamentManager tournamentManager = new TournamentManager();
        OrganisationManager organisationManager = new OrganisationManager();


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
                StartTime = model.StartTime
            };
            var result = tournamentManager.CreateTournament(tournamentDTO);

            ViewData["Creator"] = model.OrganisationID == "null" ? "User" : "Org";
            ViewData["OrgName"] = model.OrganisationID == "null" ? null : organisationManager.GetOrganisationByID(model.OrganisationID).Name;
            ViewData["UserName"] = currentUser.UserName;
            switch (result)
            {
                case TournamentErrorCodes.NoError:
                    return RedirectToAction("Index");
                case TournamentErrorCodes.BuyInLessOrEqualToPrize:
                    ModelState.AddModelError("BuyIn", "Buy In can't be less or equal to prize!");
                    return View(model);
                case TournamentErrorCodes.NoHost:
                    return RedirectToAction("Error", "Home", new { errorMessage = "There was no Host assigned to the tournament!", errorDate = DateTime.Now });
                case TournamentErrorCodes.NotEnoughMoney:
                    ModelState.AddModelError("Prize", "You don't have enough currency to create a tournament with such a prize pool!");
                    return View(model);
                case TournamentErrorCodes.UnexpectedError:
                default:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An Unknown error occured while creating a tournament", errorDate = DateTime.Now });
            }
        }
        public IActionResult Index()
        {
            var Tournaments = tournamentManager.Get10NextTournaments();
            IndexViewModel model = new IndexViewModel()
            {
                Tournaments = Tournaments
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
    }
}

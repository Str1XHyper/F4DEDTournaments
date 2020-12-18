using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F4DEDTournaments.Models.Tournament;
using Model;
using Logic.Tournament;
using Logic.Organisation;

namespace F4DEDTournaments.Controllers
{
    public class TournamentController : Controller
    {
        TournamentManager tournamentManager = new TournamentManager();
        OrganisationManager organisationManager = new OrganisationManager();
        [HttpGet]
        public IActionResult CreateTournament(string OrganisationID)
        {
            ViewData["Creator"] = OrganisationID == null ? "User" : "Org";
            CreateTournamentViewModel model = new CreateTournamentViewModel()
            {
                OrganisationID = OrganisationID == null ? "null" : OrganisationID
            };
            ViewData["OrgName"] = OrganisationID == null ? null : organisationManager.GetOrganisationByID(OrganisationID).Name;
            model.StartTime = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateTournament(CreateTournamentViewModel model)
        {
            TournamentDTO tournamentDTO = new TournamentDTO()
            {
                Name = model.Name,
                OrganisationID = model.OrganisationID,
                Size = model.Size,
                Prize = model.Prize,
                BuyIn = model.BuyIn,
                Game = model.Game,
                StartTime = model.StartTime
            };
            var result = tournamentManager.CreateTournament(tournamentDTO);
            switch (result)
            {
                case TournamentErrorCodes.NoError:
                    return RedirectToAction("Index");
                case TournamentErrorCodes.BuyInLessOrEqualToPrize:
                    ModelState.AddModelError("BuyIn", "Buy In can't be less or equal to prize!");
                    ViewData["Creator"] = model.OrganisationID == null ? "User" : "Org";
                    return View();
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
    }
}

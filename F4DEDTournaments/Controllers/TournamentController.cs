using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F4DEDTournaments.Models.Tournament;
using Model;
using Logic.Tournament;

namespace F4DEDTournaments.Controllers
{
    public class TournamentController : Controller
    {
        TournamentManager tournamentManager = new TournamentManager();
        [HttpGet]
        public IActionResult CreateTournament()
        {
            return View();
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
                Game = model.Game
            };
            tournamentManager.CreateTeam(tournamentDTO);
            return View();
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

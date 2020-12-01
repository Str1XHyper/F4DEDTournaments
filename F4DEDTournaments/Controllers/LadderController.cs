using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models.Ladder;
using Logic.Ladders;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace F4DEDTournaments.Controllers
{
    public class LadderController : Controller
    {
        LadderManager ladderManager = new LadderManager();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateLadder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateLadder(CreateLadderViewModel model)
        {
            LadderDTO ladderDTO = new LadderDTO()
            {
                Game = model.Game,
                MinimumElo = model.MinimumElo,
                MaximumElo = model.MaximumElo,
                Name = model.Name
            };
            var result = ladderManager.CreateLadder(ladderDTO);
            switch (result)
            {
                case LadderErrorCodes.NoError:
                    return RedirectToAction("Index");
                case LadderErrorCodes.UnknownException:
                    return RedirectToAction("Error", "Home", new { errorMessage = "An Unknown error occured while creating a ladder", errorDate = DateTime.Now });
            }
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models.Organisation;
using Logic.Organisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace F4DEDTournaments.Controllers
{

    [Authorize(Roles = "Admin")]
    public class OrganisationController : Controller
    {
        OrganisationManager organisationManager = new OrganisationManager();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateOrg()
        {
            return View("CreateOrganisation");
        }
        [HttpPost]
        public IActionResult CreateOrg(CreateOrganisationViewModel model)
        {
            OrganisationDTO organisationDTO = new OrganisationDTO()
            {
                Name = model.Name,
                Description = model.Description,
                CountryOfOrigin = model.CountryOfOrigin
            };
            var result = organisationManager.CreateOrganisation(organisationDTO);
            switch (result)
            {
                case OrganisationErrorCodes.NameAlreadyExists:
                    ModelState.AddModelError("Name", "Name Already Exists!");
                    return View("CreateOrganisation");
                case OrganisationErrorCodes.NoError:
                    return RedirectToAction("Index");
                case OrganisationErrorCodes.UnknownException:
                    return RedirectToAction("Error","Home", new {errorMessage = "An Unknown error occured while creating an organisation", errorDate = DateTime.Now});
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateTournamentHostLink()
        {
            List<OrganisationDTO> organisation = organisationManager.GetAllOrganisations();
            ViewData["HostLink"] = "null";
            CreateHostLinkViewModel model = new CreateHostLinkViewModel()
            {
                Organisations = organisation,
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateTournamentHostLink(CreateHostLinkViewModel model)
        {
            string HostLink = organisationManager.CreateHostLink(model.OrganisationID);
            ViewData["HostLink"] = HostLink;
            return View(model);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model;

namespace F4DEDTournaments.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Username, Email = model.Email };
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if(signInManager.IsSignedIn(User))
                await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var LoggedInUser = await userManager.GetUserAsync(User);

            if (LoggedInUser == null)
            {
                return RedirectToAction("Register");
            }

            EditProfileViewModel model = new EditProfileViewModel()
            {
                Email = LoggedInUser.Email,
                Username = LoggedInUser.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var currentUser = await userManager.GetUserAsync(User);


            currentUser.SpokenLanguages = Languages.Dutch;
            currentUser.CountryOfHeritage = Countries.Netherlands;
            currentUser.Description = "Lorem Ipsum";
            currentUser.FirstName = "Tijn";
            currentUser.LastName = "van Veghel";

            var result = await userManager.UpdateAsync(currentUser);

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(currentUser, model.OldPassword);
            if (isPasswordCorrect)
            {
                var emailResult = await userManager.FindByEmailAsync(model.Email);
                if (emailResult == null || emailResult == currentUser)
                {
                    if (currentUser.Email != model.Email)
                    {
                        await userManager.SetEmailAsync(currentUser, model.Email);
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email already exists!");
                }

                var usernameResult = await userManager.FindByNameAsync(model.Username);
                if (usernameResult == null || usernameResult == currentUser)
                {
                    if (User.Identity.Name != model.Username)
                        await userManager.SetUserNameAsync(currentUser, model.Username);
                }
                else
                {
                    ModelState.AddModelError("Username", "Username already exists!");
                }

                if (model.NewPassword != null)
                {
                    await userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.NewPassword);
                }

                /*
                 currentUser has all properties needed
                 */


            }

            else // If old password isn't correct
            {
                ModelState.AddModelError("OldPassword", "Old password isn't correct!");
            }
            return View(model);
        }

        public async Task<IActionResult> NewProfile()
        {
            var currentUser = await userManager.GetUserAsync(User);

            EditProfileViewModel model = new EditProfileViewModel()
            {
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                Username = currentUser.UserName,
                Email = currentUser.Email,
                Description = currentUser.Description,
                CountryOfHeritage = currentUser.CountryOfHeritage,
                SpokenLanguages = currentUser.SpokenLanguages,
            };
            return View(model);
        }
    }
}
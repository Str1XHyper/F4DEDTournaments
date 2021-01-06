using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using F4DEDTournaments.Models;
using F4DEDTournaments.Models.Account;
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
        public async Task<IActionResult> ViewProfile(string UserID)
        {
            var currentUser = await userManager.GetUserAsync(User);

            AppUser viewedUser;

            if (UserID != null)
            {
                viewedUser = await userManager.FindByIdAsync(UserID);
            } else
            {
                viewedUser = currentUser;
            }
            
            if (currentUser == null && UserID == null)
            {
                return RedirectToAction("Register");
            }



            ProfileViewModel model = new ProfileViewModel()
            {
                FirstName = viewedUser.FirstName,
                LastName = viewedUser.LastName,
                Username = viewedUser.UserName,
                Email = viewedUser.Email,
                Description = viewedUser.Description,
                CountryOfHeritage = viewedUser.CountryOfHeritage,
                MainLanguage = viewedUser.SpokenLanguages,
                IsLoggedInUser = viewedUser == currentUser
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return RedirectToAction("Register");
            }
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

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel model)
        {
            var currentUser = await userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(currentUser, model.OldPassword);

            if (isPasswordCorrect)
            {
                bool modelIsValid = true;

                var emailResult = await userManager.FindByEmailAsync(model.Email);
                var usernameResult = await userManager.FindByNameAsync(model.Username);

                if (emailResult != null && emailResult != currentUser)
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                    modelIsValid = false;
                }
                if (usernameResult != null && emailResult != currentUser)
                {
                    ModelState.AddModelError("Username", "Username is already in use!");
                    modelIsValid = false;
                }
                if (model.NewPassword != model.ConfirmNewPassword)
                {
                    ModelState.AddModelError("ConfirmNewPassword", "Passwords do not match!");
                    modelIsValid = false;
                }

                if (!modelIsValid)
                {
                    return View(model);
                }

                if (model.Email != string.Empty && model.Email != currentUser.Email)
                {
                    await userManager.SetEmailAsync(currentUser, model.Email);
                }
                if(model.Username != string.Empty && model.Username != currentUser.UserName)
                {
                    await userManager.SetUserNameAsync(currentUser, model.Username);
                }
                if (model.NewPassword != null)
                {
                    await userManager.ChangePasswordAsync(currentUser, model.OldPassword, model.NewPassword);
                }

                currentUser.SpokenLanguages = model.SpokenLanguages;
                currentUser.CountryOfHeritage = model.CountryOfHeritage;
                currentUser.Description = model.Description;
                currentUser.FirstName = model.FirstName;
                currentUser.LastName = model.LastName;

                var result = await userManager.UpdateAsync(currentUser);

            }
            else
            {
                ModelState.AddModelError("OldPassword", "Password isn't correct!");
            }

            return View(model);
        }
    }
}
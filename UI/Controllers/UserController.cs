using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Core;
using Core.Interfaces;
using DataAccessInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using BusinessLogicInterfaces;
using DataAccess.DB;
using UI.ViewModels;
using BusinessEntities;
using BusinessLogic;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        private ILogError _logError;
        private IUserDA _userData;
        private IUserBL _userBL;
        private SignInManager<UserBE> _signManager; 
        private UserManager<UserBE> _userManager;

        public UserController(UserManager<UserBE> userManager, SignInManager<UserBE> signManager, IUserDA userData,  ILogError logError)
        {
            _logError = logError;
            _userData = userData;
            _signManager = signManager;
            _userManager = userManager;
            _userBL = new UserBL(_userData, _logError);
        }

        public IActionResult LogIn()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(UserLoginViewModel userlogin, string returnUrl, CancellationToken token)
        {
            if (ModelState.IsValid)
            { 
                UserBE userExistence = await _userBL.GetUserByUserNameBL(userlogin.UserName, token);

                var result = await _signManager.PasswordSignInAsync(userlogin.UserName, userlogin.Password, false, false); 
                
                if (result.Succeeded) 
                { 
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) {
                        return Redirect(returnUrl);
                    } else { 
                        return RedirectToAction("Index", "Home"); 
                    } 
                } 
            }
            ModelState.AddModelError("UserName", "Login et/ou mot de passe incorrect(s)");

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Registration()
        {
            UserRegisterViewModel userviewmodel = new UserRegisterViewModel();

            return View(userviewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegisterViewModel userviewmodel, CancellationToken token)
        {
            if (ModelState.IsValid)
            {
                UserBE userExistence = await _userBL.GetUserByUserNameAndEmailBL(userviewmodel.UserName, userviewmodel.Email, token);
                UserBE userToAdd = new UserBE();
                
                if (userExistence == null)
                {
                    userToAdd.UserName = userviewmodel.UserName;
                    userToAdd.Email = userviewmodel.Email;
                    userToAdd.NormalizedUserName = userviewmodel.UserName.ToUpper();
                    userToAdd.NormalizedEmail = userviewmodel.Email.ToUpper();

                    var resultRegistration = await _userManager.CreateAsync(userToAdd, userviewmodel.Password);

                    if(resultRegistration.Succeeded)
                    {
                        var userAfterAdd = await _userManager.FindByEmailAsync(userviewmodel.Email);
                        await _signManager.SignInAsync(userAfterAdd, false);
                        
                        return RedirectToAction("UserFlights", "Flight");
                    }
                    else
                    {
                        foreach (var error in resultRegistration.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                ModelState.AddModelError("Login", "Vous avez deja un compte");
                ModelState.AddModelError("Email", "Vous avez deja un compte");

                return View(userviewmodel);
            }

            return View(userviewmodel);


        }

        public async Task <IActionResult> LogOut()
        {
            await _signManager.SignOutAsync(); 
            return Redirect("/");
        }
    }
}
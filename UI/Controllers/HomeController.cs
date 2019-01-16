using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UI.Models;
using DataAccessInterfaces;
using BusinessLogicInterfaces;
using Core;
using Core.Interfaces;
using BusinessLogic;
using UI.ViewModels;
using System.Web;
using System.Security.Claims;


namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private IUserDA _userData;

        private ILogError _logError;
        private IUserBL _userBL;

        public HomeController(IUserDA userData, ILogError logError)
        {
            _logError = logError;
            _userData = userData;

            _userBL = new UserBL(_userData, _logError);
        }

        public async Task<IActionResult> Index(CancellationToken token)
        {
            string idCurrentUserString;
            UserLoginViewModel userviewmodel = new UserLoginViewModel();

            try
            {
                idCurrentUserString = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch(Exception ex)
            {
                _logError.Log(ex);
                idCurrentUserString = null;
            }
            
            if(idCurrentUserString != null)
            {
                userviewmodel.UserName = (await _userBL.FindByIdAsync(idCurrentUserString, token)).UserName;
            }

            return View(userviewmodel);
        }
        

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
    }
}

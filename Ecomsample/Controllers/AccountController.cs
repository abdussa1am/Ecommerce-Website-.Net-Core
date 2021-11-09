using Ecomsample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecomsample.Controllers
{
    public class AccountController : Controller
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager  , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [Route("signup")]
        public ActionResult SignUp()
        {
            return View();
        }
    
        [AcceptVerbs("Get","Post")]
        [HttpPost]

        public async Task<ActionResult> IsEmailInUse(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if(user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Emwil {Email} is in use");
            }


        }
        [Route("signup")]
        [HttpPost]

        public async Task<ActionResult> SignUp(SignUpUserModel signUpUserModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Name = signUpUserModel.Name,
                    Email = signUpUserModel.Email,
                    UserName = signUpUserModel.Email,
                };

               await _userManager.CreateAsync(user, signUpUserModel.Password);
            }

            return View();
        }
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login(SigninModel signinModel , string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signinModel.Email, signinModel.Password, false, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl) )
                    {
                        return Redirect(returnUrl);
                    }
                  
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "dasdsa");
            }
         
             
            return View(signinModel);
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();


            return RedirectToAction("Index", "Home"); 
        }
        public string YourMethodName()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var user = User.FindFirstValue(ClaimTypes.Email);// will give the user's userName

            


            

          
            return $"name is {userId} {userName}  {user}";
     
        }


    }
}

using Bloggie.Web.Models.VIewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public AccountController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(); 
        }

        [HttpPost]
        /*
         * User manager service allows us to add some users.
         */
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

           var identityResult =  await userManager.CreateAsync(identityUser, registerViewModel.Password);

            if (identityResult.Succeeded)
            {
                //assign user role.
               var roleIdentityResult =  await userManager.AddToRoleAsync(identityUser, "User"); 
                if(roleIdentityResult.Succeeded)
                {
                    //success.
                    return RedirectToAction("Register"); 
                }

            }

            return View(); 

        }
    }
}

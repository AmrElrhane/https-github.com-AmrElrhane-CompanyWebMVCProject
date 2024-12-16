using Company.Data.Entities;
using Company.Service.Helper;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel input)
        {
            if (ModelState.IsValid)
            {

                var user = new ApplicationUser
                {
                    UserName = input.Email.Split("@")[0],
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Email = input.Email,
                    IsActive = true
                };

                // hashing password 
                var result = await _userManager.CreateAsync(user, input.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

            }

            return View(input);
        }
        #endregion


        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel input)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);

                if (user is not null)
                {
                    if (await _userManager.CheckPasswordAsync(user, input.Password))
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, input.Password
                            , input.RememberMe, true);

                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");

                    }
                    ModelState.AddModelError("", "Incorrect Password");
                }
                ModelState.AddModelError("", "Incorrect Email or Password");
                return View(input);
            }
            return View(input);
        }
        
        public new async Task<IActionResult> SignOut()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));

        }

        #endregion

        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(input.Email);

                if (user is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url = Url.Action("ResetPassword", "Account", new
                    {
                        Email = input.Email,
                        Token = token
                    } , Request.Scheme);

                    Email email = new Email
                    {
                        Body = url,
                        To  = input.Email,
                        Subject= "Reset Password" 
                    };
                    EmailSetting.SendEmail(email);
                    
                    return RedirectToAction(nameof(CheckYourInbox));

                }

            }

            return View(input);
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        public IActionResult ResetPassword(string Email , string Token)
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user , input.Token, input.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Login));
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(input);


        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}

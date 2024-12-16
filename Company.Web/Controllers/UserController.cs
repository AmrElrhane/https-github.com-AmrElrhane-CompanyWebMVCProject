using Company.Data.Entities;
using Company.Service.Helper;
using Company.Service.Interfaces.Department.Dto;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController(UserManager<ApplicationUser> userManager
            , ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchInp)
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(searchInp))
                users = await _userManager.Users.ToListAsync();
            else
                users = await _userManager.Users.Where(user => user.NormalizedEmail.Trim().
                Contains(searchInp.Trim().ToUpper())).ToListAsync();


            return View(users);
        }

        public async Task<IActionResult> Details(string? id, string viewname = "Details")
        {

            var User = await _userManager.FindByIdAsync(id);
            if (User is null)
            {
                return NotFound();
            }

            if (viewname == "Update")
            {
                var user = new UserUpdateViewModel
                {
                    Id = User.Id,
                    UserName = User.UserName
                };
                return View(viewname, user);
            }
            return View(viewname, User);
        }

        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");

        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id, UserUpdateViewModel applicationUser)
        {
            if (id != applicationUser.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();

                    user.UserName = applicationUser.UserName;
                    user.NormalizedUserName = applicationUser.UserName.ToUpper();

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Updated Successfully!");
                        return RedirectToAction("Index");
                    }
                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);

                    // Add a return statement here to handle the case where update fails
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    // Add a return statement here to handle exceptions
                    return View(applicationUser);
                }
            }

            return View(applicationUser);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user is null)
                    return NotFound();
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User Deleted Successfully!");
                    return RedirectToAction("Index");
                }
                foreach (var item in result.Errors)
                    _logger.LogError(item.Description);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                // Add a return statement here to handle exceptions
            }
            return RedirectToAction("Index");

        }
    }
}

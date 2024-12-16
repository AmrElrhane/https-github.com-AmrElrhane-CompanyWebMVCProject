using Company.Data.Entities;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company.Web.Controllers
{
    [Authorize(Roles = "Admin")]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController(RoleManager<IdentityRole> roleManager,
            ILogger<RoleController> logger,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            return View(roles);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = roleModel.Name
                };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                    _logger.LogError(error.Description);
            }

            return View(roleModel);
        }

        public async Task<IActionResult> Details(string? id, string viewname = "Details")
        {

            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return NotFound();
            }
            var Role = new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(viewname, Role);
        }

        public async Task<IActionResult> Update(string? id)
        {
            return await Details(id, "Update");

        }
        [HttpPost]
        public async Task<IActionResult> Update(string? id, RoleViewModel roleModel)
        {
            if (id != roleModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _roleManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();

                    user.Name = roleModel.Name;
                    user.NormalizedName = roleModel.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role Updated Successfully!");
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
                    return View(roleModel);
                }
            }

            return View(roleModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                if (role is null)
                    return NotFound();
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Role Deleted Successfully!");
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

        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();
            ViewBag.RoleId = roleId;
            var users = await _userManager.Users.ToListAsync();
            var UsersInRole = new List<UserInRoleViewModel>();
            foreach (var user in users)
            {
                var UserInRole = new UserInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    UserInRole.IsSelected = true;
                else
                    UserInRole.IsSelected = false;

                UsersInRole.Add(UserInRole);

            }

            return View(UsersInRole);

        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UserInRoleViewModel> users)
        {

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = _userManager.FindByIdAsync(user.UserId);

                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(await appUser, role.Name))
                            await _userManager.AddToRoleAsync(await appUser, role.Name);
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(await appUser, role.Name))
                            await _userManager.RemoveFromRoleAsync(await appUser, role.Name);

                    }
                }
                return RedirectToAction("Update", new { id = roleId });
            }
            return View(users);


        }


    }
}

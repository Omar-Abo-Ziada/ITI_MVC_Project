using InstitueProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InstitueProject.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> RoleManager)
        {
            roleManager = RoleManager;
        }

        [HttpGet]
        public IActionResult AddRole()
        {
            return View("AddRole");
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                { 
                    Name = roleVM.RoleName,
                };

               IdentityResult result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return Content("Role Added Successfully :D ");
                    // suppose to redirect to the role dashboard to make me assign people to this role
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }

                    return View("AddRole");
                }
            }
            return View("AddRole");
        }
    }
}

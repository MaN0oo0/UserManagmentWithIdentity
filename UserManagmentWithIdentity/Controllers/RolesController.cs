using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserManagmentWithIdentity.ViewModels;

namespace UserManagmentWithIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        private readonly  RoleManager<IdentityRole> roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var Roles = await roleManager.Roles.ToListAsync();
            return View(Roles);
        }
        [HttpPost]
         public async Task<IActionResult> Add(RoleFormVM model)
        {
            if(!ModelState.IsValid)
                return View("Index",await roleManager.Roles.ToListAsync());

            var RolesExists = await roleManager.RoleExistsAsync(model.Name);
            if (RolesExists)
            {
                ModelState.AddModelError("Name", "Role Is Exists Already!");
                return View("Index", await roleManager.Roles.ToListAsync());
            }

            await roleManager.CreateAsync(new IdentityRole(model.Name.Trim()));
            return RedirectToAction(nameof(Index));
        }
    }
}

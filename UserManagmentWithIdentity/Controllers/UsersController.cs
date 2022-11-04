using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using UserManagmentWithIdentity.Models;
using UserManagmentWithIdentity.ViewModels;

namespace UserManagmentWithIdentity.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager = null)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        public async Task<IActionResult> Index()
        {
            var Users = await _userManager.Users.Select(user => new UserVM 
            { 
            Id=user.Id,
            Email=user.Email,
            FirstName=user.FirstName,
             LastName=user.LastName,
             UserName=user.UserName,
             Roles=_userManager.GetRolesAsync(user).Result
            
            
            }).ToListAsync();
            return View(Users);
        }


        public async Task<IActionResult> Create()
        {
            
           
            var roles = await _roleManager.Roles.Select(R=>new RoleVM { RoleId=R.Id,RoleName=R.Name}).ToListAsync();
            var viewModel = new AddUserVM()
            {
                Roles = roles
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserVM model)
        {


            if (!ModelState.IsValid)
                return View(model);

            if(!model.Roles.Any(r=>r.IsSelected))
            {
                ModelState.AddModelError("Roles", "Ples Select At Lest Role");
                return View(model);
            }
            if(await _userManager.FindByEmailAsync(model.Email) != null)
            {
                ModelState.AddModelError("Email", "Email Is Exsits");
                return View(model);

            }
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = new MailAddress(model.Email).User,
                Email = model.Email

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("Roles", error.Description);
                }
                return View(model);
            }
            await _userManager.AddToRolesAsync(user, model.Roles.Where(r=>r.IsSelected).Select(r => r.RoleName));
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ManageRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();
            var roles = await _roleManager.Roles.ToListAsync();
            var viewModel = new UserRolesVM()
            {
            UserId=user.Id,
            UserName=user.UserName,
            Roles=roles.Select(role=>new RoleVM 
            { 
                IsSelected=_userManager.IsInRoleAsync(user,role.Name).Result, 
                RoleId=role.Id,RoleName= role.Name}).ToList()

            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageRoles(UserRolesVM model)
        {
           var user= await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return NotFound();


            var UserRoles=await _userManager.GetRolesAsync(user);

            foreach (var item in model.Roles)
            {
                if(UserRoles.Any(r=>r ==item.RoleName)&& !item.IsSelected)
                    await _userManager.RemoveFromRoleAsync(user,item.RoleName);

                if (!UserRoles.Any(r => r == item.RoleName) && item.IsSelected)
                    await _userManager.AddToRoleAsync(user, item.RoleName);



            }
            return RedirectToAction(nameof(Index));


        }
    }
}

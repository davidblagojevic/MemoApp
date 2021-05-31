using MemoApp.Data;
using MemoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoApp.Controllers
{
    //[Authorize(Roles = "Admin,User")]//moze i vise rola odjednom ili ako ima dva authorize onda user mora biti deo obe role
    //[AllowAnonymous] //moze iznad neke akcije pa ce onda ignorisati autentifikaaciju i autorizaciju za tu akciju
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> logger;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext appDbContext;

        public AdminController(ILogger<AdminController> logger, RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager, ApplicationDbContext appDbContext)
        {
            this.logger = logger;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.appDbContext = appDbContext;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRoleAsync(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    //ako ne uspe da doda koji su errori u modelstate
                    ModelState.AddModelError("", error.Description);
                }

            }

            //krosinik moze da isprravi validation errore

            return View(model);
        }


        public IActionResult Roles()
        {
            IEnumerable<IdentityRole> identityRoles = roleManager.Roles.ToList();

            return View(identityRoles);
        }

        public IActionResult Users()
        {
            IEnumerable<IdentityUser> identityUsers = userManager.Users.ToList();
            List<CreateUserViewModel> userViewModels = new List<CreateUserViewModel>();
            foreach (var user in identityUsers)
            {

                var userViewModel = new CreateUserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    IdentityRoleName = userManager.GetRolesAsync(user).Result.FirstOrDefault()
                };
                userViewModels.Add(userViewModel);


            }
            return View(userViewModels);
        }

        [HttpGet]
        public IActionResult ChangeUserRole(string userId)
        {
            var user = userManager.FindByIdAsync(userId).Result;

            var userRoleViewModel = new UserRoleViewModel()
            {
                UserId = user.Id,
                RoleName = userManager.GetRolesAsync(user).Result.FirstOrDefault(),
                SelectRoleName = roleManager.Roles.Select(r =>
                    //pretvaranje rola u ovo sto ce popuniti select
                    new SelectListItem
                    {
                        Value = r.Name,
                        Text = r.Name
                    })
                .ToList()
            };
            //brise usera iz svih prethodnih rola
            //new valja nista treba drugi viewmodel i ovo iz pluralsight objasnjenje i ne treba ovo u get
            return View(userRoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(UserRoleViewModel userRoleViewModel)
        {
            var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);

            
            _ = await userManager.RemoveFromRolesAsync(user, userManager.GetRolesAsync(user).Result.ToArray());
            await userManager.AddToRoleAsync(user, userRoleViewModel.RoleName);



            return RedirectToAction("Users", "Admin");
        }
        [HttpGet]
        public async Task<IActionResult> EditRoleAsync(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            var editRoleViewModel = new EditRoleViewModel() 
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
            return View(editRoleViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditRoleAsync(EditRoleViewModel editRoleViewModel) 
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(editRoleViewModel.RoleId);
                role.Name = editRoleViewModel.RoleName;
                var result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Admin");
                }
                foreach (IdentityError error in result.Errors)
                {
                    //ako ne uspe da doda koji su errori u modelstate
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(editRoleViewModel);

        }
    }
}

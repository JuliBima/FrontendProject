using FrontendProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FrontendProject.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole myRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(myRole);
                if (result.Succeeded)
                {
                    ViewData["pesan"] = $"<div class='alert alert-success alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'>" +
                    $"</button> Berhasil menambahkan data Role {model.RoleName}</div>";
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public IActionResult ListRole()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        // menampilkan edit role (get)
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'>" +
                    $"</button> data dengan id {id} tidak ditemukan</div>";
                return View();
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            if (_userManager.Users.Any())
            {
                var users = await _userManager.Users.ToListAsync();
                model.Users = new List<string>();
                ;
                foreach (var user in users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        model.Users.Add(user.UserName);
                    }
                }
            }
            return View(model);
        }

        //Jika Tombol Edit Ditekan
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role == null)
            {
                ViewData["pesan"] = $"<div class='alert alert-danger alert-dismissible fade show'>" +
                    $"<button type='button' class='btn-close' data-bs-dismiss='alert'>" +
                    $"</button> data tidak ditemukan</div>";
                return View();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> AddUserToRole()
        {
            ViewBag.Username = new SelectList(await _userManager.Users.ToListAsync());
            ViewBag.Role = new SelectList(await _roleManager.Roles.ToListAsync());
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
           
            var user = await _userManager.FindByNameAsync(model.Username);
            try
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
                return RedirectToAction("ListRole");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        
        public async Task<IActionResult> DeleteInRole(string id, string role)
        {
            var user = await _userManager.FindByNameAsync(id);
            await _userManager.RemoveFromRoleAsync(user, role);
            var myRole = await _roleManager.FindByNameAsync(role);

            //await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim { })

            return RedirectToAction("EditRole", new { id = myRole.Id });
        }


        public async Task<IActionResult> HapusRole(string id)
        {
            var user = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(user);
           

            //await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim { })

            return RedirectToAction("ListRole");
        }
    }
}

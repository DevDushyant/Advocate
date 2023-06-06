using Advocate.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class RoleController : BaseController
    {
        RoleManager<IdentityRole> roleManager;
        UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }
        [Authorize(Policy = "managerole")]
        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        [Authorize(Policy = "managerole")]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            
               IdentityResult result=await roleManager.CreateAsync(role);
            if (result.Succeeded)
                notify("Role created successfully!", Common.NotificationType.error);
            else
                foreach (IdentityError error in result.Errors)
                {
                    notify("There is problem in creating the role! \n" + error.Description, Common.NotificationType.error);
                }
            return View();

        }

        /// <summary>
        /// this is used for assigning the role to user
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public async Task<ActionResult> UpdateRoleAsync(string UserId, string RoleId)
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return View();
            }
            var roles = await userManager.GetRolesAsync(user);
            var result = await userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View();
            }
            List<string> userRole = new List<string>();
            userRole.Add(RoleId);
            try
            {
                result = await userManager.AddToRolesAsync(user, userRole);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected roles to user");
                    return View();
                }
            }
            catch (Exception e)
            {

            }
            return Json(result.Succeeded);
        }

        [HttpPost("Delete")]
        
        public async Task<IActionResult> DeleteAsync(string id) {

            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    foreach (IdentityError error in result.Errors)
                    {
                        notify("There is problem in deleting the role! \n" + error.Description, Common.NotificationType.error);
                    }

            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        [HttpGet("update-role")]
        public IActionResult Edit(string id)
        {
            IdentityRole role =  roleManager.FindByIdAsync(id).Result;
            return View(role);
        }

        public IActionResult Details(String id) {
            return View();
        }



    }
}

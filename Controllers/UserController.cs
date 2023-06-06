using Advocate.Data;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserController> _logger;
        private RoleManager<IdentityRole> roleManager;
        private readonly IMapper _mapper;
        public UserController(UserManager<ApplicationUser> _userManager, ILogger<UserController> _logger, IMapper _mapper, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = _userManager;
            this._mapper = _mapper;
            this._logger = _logger;
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var usersdata = _userManager.Users.ToList();
            List<UserViewModel> userviewmodel = new List<UserViewModel>();

            foreach (var item in usersdata)
            {
                UserViewModel obj = new UserViewModel();
                obj.Id = item.Id;
                obj.FirstName = item.FirstName;
                obj.MiddleName = item.MiddleName;
                obj.LastName = item.LastName;
                obj.Email = item.Email;
                obj.UserName = item.UserName;
                obj.AssinedRole = await GetUserRoles(item);
                obj.Roles = getallRoles();
                userviewmodel.Add(obj);
            }

            return View(userviewmodel);
        }

        public SelectList getallRoles(string selectedRoleId = "-1")
        {
            return new SelectList(roleManager.Roles.ToList(), "Id", "Name", selectedRoleId);
        }

        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName
                };

                IdentityResult result = await _userManager.CreateAsync(appUser, "Dev@12345");
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(string Id)
        {
            if (Id == null || Id == "")
            {
                notify("There is not refrence id is supplied for fetching the detail", Common.NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                var userDetail = _userManager.FindByIdAsync(Id).Result;
                var userviewModel = _mapper.Map<ApplicationUser, UserViewModel>(userDetail);
                return View(userviewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(UserViewModel user)
        {
            if (user == null)
            {
                notify("Supplied user detail is null", Common.NotificationType.error);
                return View(user);
            }
            else
            {
                var userDetail = _userManager.FindByIdAsync(user.Id).Result;
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    PasswordHash = "Dev@12345"
                };
                IdentityResult result = await _userManager.UpdateAsync(appUser);
                if (result.Succeeded)
                {
                    notify("Record successfully updated!", Common.NotificationType.success);
                }
                else
                    foreach (IdentityError error in result.Errors)
                    {
                        notify("There is problem in updating the user! \n" + error.Description, Common.NotificationType.error);
                    }
                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Details(string Id)
        {
            if (Id == null || Id == "")
            {
                notify("There is not refrence id is supplied for fetching the detail", Common.NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                var userDetail = _userManager.FindByIdAsync(Id).Result;
                var userviewModel = _mapper.Map<ApplicationUser, UserViewModel>(userDetail);
                return View(userviewModel);
            }
        }


        public async Task<IActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    notify("There is not refrence id is supplied for fetching the detail", Common.NotificationType.error);
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View("Index", _userManager.Users);
        }
    }
}

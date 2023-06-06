using Advocate.Common;
using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
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
    public class SubGazetteController : BaseController
    {
        private readonly ISubGazzetServiceAsync subGazzetServiceAsync;
        private readonly IGazzetServiceAsync gazzetServiceAsync;
        private readonly ILogger<NavigationController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public SubGazetteController(ISubGazzetServiceAsync subGazzetServiceAsync,
            ILogger<NavigationController> _logger,
            IMapper _mapper,
            IGazzetServiceAsync gazzetServiceAsync, UserManager<ApplicationUser> _userManager)
        {
            this.subGazzetServiceAsync = subGazzetServiceAsync;
            this._logger = _logger;
            this._mapper = _mapper;
            this.gazzetServiceAsync = gazzetServiceAsync;
            this._userManager = _userManager;

        }

        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        public IActionResult Index()
        {
            var result = _mapper.Map<IEnumerable<PartEntity>, IEnumerable<SubGazetteViewModel>>(subGazzetServiceAsync.GetAllPartsWithGazette(GetCurrentUserAsync().Result.Id));
            return View(result);
        }

        public IActionResult Create()
        {
            var subgazetteViewModel = new SubGazetteViewModel();
            subgazetteViewModel.GazzetList = new SelectList(gazzetServiceAsync.GetAllAsync(), "Id", "GazetteName");
            return View(subgazetteViewModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(SubGazetteViewModel obj)
        {
            try
            {
                obj.GazzetList= new SelectList(gazzetServiceAsync.GetAllAsync(), "Id", "GazetteName");
                if (ModelState.IsValid)
                {
                    obj.UserId = GetCurrentUserAsync().Result.Id;
                    obj.IsActive = true;
                    var data = _mapper.Map<SubGazetteViewModel, PartEntity>(obj);
                    var isExist = subGazzetServiceAsync.isSubGazzetExist(obj.GazzetId, obj.PartName);
                    if (isExist == false)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return View(obj);
                    }
                    else
                    {
                        int result = subGazzetServiceAsync.SaveAsync(data);
                        notify("Record saved successfully!", NotificationType.warning);
                        return View(obj);
                    }
                }
                else
                {
                    return View(obj);
                }
            }
            catch (Exception e)
            {
                return View(obj);
            }
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for fetching the detail", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                var data = subGazzetServiceAsync.GetPartDetailByid(GetCurrentUserAsync().Result.Id, id);
                var result = _mapper.Map<PartEntity, SubGazetteViewModel>(data);
                return View(result);
            }
        }

        public IActionResult Edit(int id)
        {
            var gazetteViewModel = subGazzetServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<PartEntity, SubGazetteViewModel>(gazetteViewModel);
            data.GazzetList = new SelectList(gazzetServiceAsync.GetAllAsync(), "Id", "GazetteName");
            return View(data);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, SubGazetteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExist = subGazzetServiceAsync.isSubGazzetExist(obj.GazzetId, obj.PartName);
                    if (isExist == false)
                    {
                        notify("Record is already exist!", NotificationType.warning);
                        return View(obj);
                    }
                    else
                    {
                        var actDetail = subGazzetServiceAsync.FindByIdAsync(id);
                        if (actDetail != null)
                        {
                            actDetail.GazettId = obj.GazzetId;
                            actDetail.PartName = obj.PartName;
                            actDetail.Description = obj.Description;
                            actDetail.ModifiedDate = DateTime.Now;
                        }
                        int result = subGazzetServiceAsync.UpdateAsync(actDetail);
                        if (result == -1)
                        {
                            notify("There is some problem while updating the record!", NotificationType.warning);
                            return View(obj);
                        }
                        else
                        {
                            obj.GazzetList = new SelectList(gazzetServiceAsync.GetAllAsync(), "Id", "GazetteName");
                            notify("Record updated successfully!", NotificationType.success);
                            return View(obj);
                        }
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception e)
            {
                return View(obj);
            }
        }

        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for deleting the record!", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                PartEntity obj = new PartEntity
                {
                    Id = id
                };

                var result = subGazzetServiceAsync.DeleteAsync(obj);
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction("index");
            }
        }
    }
}

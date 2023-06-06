using Advocate.Common;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class ActTypeController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IActTypeServiceAsync _actTypeServiceAsync;
        public ActTypeController(IMapper _mapper, IActTypeServiceAsync _actTypeServiceAsync)
        {
            this._mapper = _mapper;
            this._actTypeServiceAsync = _actTypeServiceAsync;
        }
        public IActionResult Index()
        {
            var result = _mapper.Map<IEnumerable<ActTypeEntity>, IEnumerable<ActTypeViewModel>>(_actTypeServiceAsync.GetAllAsync().OrderByDescending(ord=>ord.Id));
            return View(result);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ActTypeViewModel actTypeViewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<ActTypeViewModel, ActTypeEntity>(actTypeViewModel);
                    var isExist = _actTypeServiceAsync.isActExist(actTypeViewModel.ActType);
                    if (isExist==true)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        int result = _actTypeServiceAsync.SaveAsync(data);
                        notify("Record saved successfully!", NotificationType.warning);
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            var acttypeViewModel = _actTypeServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<ActTypeEntity, ActTypeViewModel>(acttypeViewModel);
            return View(data);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, ActTypeViewModel actTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExist = _actTypeServiceAsync.isActExist(actTypeViewModel.ActType);
                    if (isExist==false)
                    {
                        notify("Record is already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        var actDetail = _actTypeServiceAsync.FindByIdAsync(id);
                        if (actDetail != null)
                        {
                            actDetail.ActType = actTypeViewModel.ActType;
                            actDetail.Description = actTypeViewModel.Description;
                        }
                        int result = _actTypeServiceAsync.UpdateAsync(actDetail);
                        if (result == -1)
                        {
                            notify("There is some problem while updating the record!", NotificationType.warning);
                            return View();
                        }
                        else
                        {
                            notify("Record updated successfully!", NotificationType.success);
                            return View();
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
                return View();
            }

        }
        public IActionResult Details(int id)
        {
            var subjectdetail = _actTypeServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<ActTypeEntity, ActTypeViewModel>(subjectdetail);
            return View(data);
        }


        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for deleting the record!", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                ActTypeEntity obj = new ActTypeEntity
                {
                    Id = id
                };

                var result = _actTypeServiceAsync.DeleteAsync(obj);
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction("index");
            }
        }
    }
}

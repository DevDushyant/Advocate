using Advocate.Common;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Advocate.Controllers
{
    public class NotificationTypesController : BaseController
    {
        private readonly IMapper _mapper;
        // private readonly ILogger _logger;
        private readonly INotifcationTypeAsyncService notifcationTypeAsyncService;

        public NotificationTypesController(INotifcationTypeAsyncService notifcationTypeAsyncService, /*ILogger _logger,*/ IMapper _mapper)
        {
            this.notifcationTypeAsyncService = notifcationTypeAsyncService;
            this._mapper = _mapper;
            //this._logger = _logger;
        }
        public IActionResult Index()
        {
            var result = _mapper.Map<IEnumerable<NotificationTypeEntity>, IEnumerable<NotificationTypesViewModel>>(notifcationTypeAsyncService.GetAllAsync());
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var subjectdetail = notifcationTypeAsyncService.FindByIdAsync(id);
            var data = _mapper.Map<NotificationTypeEntity, NotificationTypesViewModel>(subjectdetail);
            return View(data);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Create(NotificationTypesViewModel notificationTypesViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<NotificationTypesViewModel, NotificationTypeEntity>(notificationTypesViewModel);
                    int result = notifcationTypeAsyncService.SaveAsync(data);
                    if (result == -1)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        notify("Record saved successfully!", NotificationType.warning);
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception exception)
            {
                //if (((Microsoft.Data.SqlClient.SqlException)exception.InnerException).Number == 2601)
                //    notify("Record is duplicate!", NotificationType.warning);
                //else
                //    notify(exception.Message.ToString(), NotificationType.warning);

                return View();
            }
        }

        // GET: SubjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var notificationDetail = notifcationTypeAsyncService.FindByIdAsync(id);
            var data = _mapper.Map<NotificationTypeEntity, NotificationTypesViewModel>(notificationDetail);
            return View(data);
        }

        // POST: SubjectController/Edit/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Edit(NotificationTypesViewModel notificationTypesViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subjectdetail = notifcationTypeAsyncService.FindByIdAsync(notificationTypesViewModel.Id);
                    if (subjectdetail != null)
                    {
                        subjectdetail.Name = notificationTypesViewModel.Name;
                        subjectdetail.Description = notificationTypesViewModel.Description;
                    }
                    int result = notifcationTypeAsyncService.UpdateAsync(subjectdetail);
                    if (result == -1)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        notify("Record updated successfully!", NotificationType.success);
                        return View();
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



        // POST: SubjectController/Delete/5

        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for deleting the record!", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                NotificationTypeEntity obj = new NotificationTypeEntity
                {
                    Id = id
                };

                var result = notifcationTypeAsyncService.DeleteAsync(obj);
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction("index");
            }
        }
    }
}

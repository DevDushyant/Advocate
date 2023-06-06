using Advocate.Common;
using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly INotificationServiceAsync _notificationServiceAsync;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public NotificationController(INotificationServiceAsync _notificationServiceAsync, IMapper _mapper, UserManager<ApplicationUser> _userManager)
        {
            this._notificationServiceAsync = _notificationServiceAsync;
            this._mapper = _mapper;
            this._userManager = _userManager;
        }

        /// <summary>
        /// for getting user information from logged in user.
        /// </summary>
        /// <returns></returns>
        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public IActionResult Index()
        {
            var result = _notificationServiceAsync.GetNotification();
            return View(result);
        }

        public IActionResult Create()
        {
            ViewBag.Act = _notificationServiceAsync.GetNotification();
            return View();
        }

        [HttpPost]
        public ActionResult Create(NotificationViewModel notiViewModel)
        {
            try
            {
                var data = _mapper.Map<NotificationViewModel, NotificationEntity>(notiViewModel);
                data.UserID = GetCurrentUserAsync().Result.Id;
                data.IsActive = true;
                data.Notification_date = Convert.ToDateTime(notiViewModel.Notification_date);
                data.GazetteDate = Convert.ToDateTime(notiViewModel.GazetteDate);
                data.PublishedInGazeteDate = Convert.ToDateTime(notiViewModel.PublishedInGazeteDate);
                data.Id = _notificationServiceAsync.LastInsertedRecord() + 1;
                int ruleInsertedId = _notificationServiceAsync.SaveAsync(data);
                if (ruleInsertedId != 0)
                {
                    if (notiViewModel.notificationBooks != null)
                    {
                        if (notiViewModel.notificationBooks.Count > 0)
                        {
                            List<NotificationBookEntity> notiBookList = new List<NotificationBookEntity>();
                            foreach (var item in notiViewModel.notificationBooks)
                                notiBookList.Add(new NotificationBookEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    RuleId = ruleInsertedId,
                                    BookId = item.BookId,
                                    BookYear = item.BookYear,
                                    BookPageNo = item.BookPageNo,
                                    BookSrNo = item.BookSrNo,
                                    Volume = item.Volume,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            _notificationServiceAsync.SaveOrUpdateNotiBook(notiBookList, 0);
                        }
                    }
                    if (notiViewModel.AmendedRules != null && notiViewModel.AmendedRules != string.Empty)
                    {
                        List<int> amendedRuleList = notiViewModel.AmendedRules.Split(',').Select(int.Parse).ToList();
                        List<NotificationAmendedEntity> amendedList = new List<NotificationAmendedEntity>();
                        foreach (var item in amendedRuleList)
                            amendedList.Add(new NotificationAmendedEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                NotificationId = item,
                                AmendedNotificationID = ruleInsertedId,
                                IsActive = true,
                                CreatedDate = DateTime.Now
                            });
                        _notificationServiceAsync.SaveorUpdateNotiAmended(amendedList, 0);
                    }
                    if (notiViewModel.RepealedRules != null && notiViewModel.RepealedRules != string.Empty)
                    {
                        List<int> repealedRuleList = notiViewModel.RepealedRules.Split(',').Select(int.Parse).ToList();
                        List<NotificationRepealedEntity> repealedList = new List<NotificationRepealedEntity>();
                        foreach (var item in repealedRuleList)
                            repealedList.Add(new NotificationRepealedEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                NotificationId = item,
                                RepealedNotificationID = ruleInsertedId,
                                IsActive = true,
                                CreatedDate = DateTime.Now
                            });
                        _notificationServiceAsync.SaveOrUpdateNotiRepealed(repealedList, 0);
                    }
                    if (notiViewModel.ExtraRulesAct != null && notiViewModel.ExtraRulesAct != string.Empty)
                    {
                        List<int> extActRuleList = notiViewModel.ExtraRulesAct.Split(',').Select(int.Parse).ToList();
                        List<NotificationExtActEntity> extRuleActList = new List<NotificationExtActEntity>();
                        foreach (var item in extActRuleList)
                            extRuleActList.Add(new NotificationExtActEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                NotificationId = ruleInsertedId,
                                ActId = item,
                                IsActive = true,
                                CreatedDate = DateTime.Now
                            });
                        _notificationServiceAsync.SaveOrUpdateRuleExtraAct(extRuleActList, 0);
                    }

                    return Json(new Message() { statuscode = 200, resultmessage = "Notification saved successfully!", data = ruleInsertedId });
                }
                else
                {
                    return Json(new Message() { statuscode = 500, resultmessage = "There is some problem while saving the notification information!", data = -1 });
                }
            }
            catch (Exception ee)
            {
                return Json(new Message() { statuscode = 500, resultmessage = ee.Message, data = -1 });
            }
        }

        public ActionResult Edit(int id)
        {

            return View();
        }

        // POST: RuleController/Edit/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NotificationViewModel notiViewModel)
        {
            try
            {

                int result = 0;
                var ruleDetail = _notificationServiceAsync.FindByIdAsync(notiViewModel.Id);
                if (ruleDetail != null)
                {
                    var editableData = _mapper.Map<NotificationViewModel, NotificationEntity>(notiViewModel);
                    ruleDetail = editableData;
                    ruleDetail.IsActive = true;
                    ruleDetail.ModifiedDate = DateTime.Now;
                    result = _notificationServiceAsync.UpdateAsync(ruleDetail);
                    if (result != 0)
                    {
                        if (notiViewModel.notificationBooks != null)
                        {
                            if (notiViewModel.notificationBooks.Count > 0)
                            {
                                List<NotificationBookEntity> rulBookList = new List<NotificationBookEntity>();
                                foreach (var item in notiViewModel.notificationBooks)
                                    rulBookList.Add(new NotificationBookEntity()
                                    {
                                        UserID = GetCurrentUserAsync().Result.Id,
                                        RuleId = result,
                                        BookId = item.BookId,
                                        BookYear = item.BookYear,
                                        BookPageNo = item.BookPageNo,
                                        BookSrNo = item.BookSrNo,
                                        Volume = item.Volume,
                                        IsActive = true,
                                        CreatedDate = DateTime.Now
                                    });
                                _notificationServiceAsync.SaveOrUpdateNotiBook(rulBookList, result);
                            }
                        }
                        if (notiViewModel.AmendedRules != string.Empty)
                        {
                            List<int> amendedRuleList = notiViewModel.AmendedRules.Split(',').Select(int.Parse).ToList();
                            List<NotificationAmendedEntity> amendedList = new List<NotificationAmendedEntity>();
                            foreach (var item in amendedRuleList)
                                amendedList.Add(new NotificationAmendedEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    NotificationId = result,
                                    AmendedNotificationID = item,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            _notificationServiceAsync.SaveorUpdateNotiAmended(amendedList, result);
                        }
                        if (notiViewModel.RepealedRules != string.Empty)
                        {
                            List<int> repealedRuleList = notiViewModel.RepealedRules.Split(',').Select(int.Parse).ToList();
                            List<NotificationRepealedEntity> repealedList = new List<NotificationRepealedEntity>();
                            foreach (var item in repealedRuleList)
                                repealedList.Add(new NotificationRepealedEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    NotificationId = result,
                                    RepealedNotificationID = item,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            _notificationServiceAsync.SaveOrUpdateNotiRepealed(repealedList, result);
                        }
                        if (notiViewModel.ExtraRulesAct != string.Empty)
                        {
                            List<int> extActRuleList = notiViewModel.ExtraRulesAct.Split(',').Select(int.Parse).ToList();
                            List<NotificationExtActEntity> extRuleActList = new List<NotificationExtActEntity>();
                            foreach (var item in extActRuleList)
                                extRuleActList.Add(new NotificationExtActEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    NotificationId = result,
                                    ActId = item,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            _notificationServiceAsync.SaveOrUpdateRuleExtraAct(extRuleActList, result);
                        }
                        return Json(new Message() { statuscode = 200, resultmessage = "Notification updated successfully!", data = result });
                    }
                    else
                    {
                        return Json(new Message() { statuscode = 500, resultmessage = "There is some problem while updating the rule information!", data = -1 });
                    }
                }
                else
                {
                    return Json(new Message() { statuscode = 500, resultmessage = "There is some problem while getting the supplied rule id detail!", data = -1 });
                }

            }
            catch (Exception ee)
            {
                return Json(new Message() { statuscode = 500, resultmessage = ee.Message, data = -1 });
            }
        }


        [HttpGet]
        public JsonResult RepeatAction()
        {
            int ruleId = _notificationServiceAsync.LastInsertedRecord();
            var result = _notificationServiceAsync.GetNotificationDetailByNotificationId(ruleId);
            return Json(result);
        }

        [HttpPost]
        public JsonResult FindByNotificationId(int notificationId)
        {
            if (notificationId == 0)
                return null;
            var result = _notificationServiceAsync.GetNotificationDetailByNotificationId(notificationId);
            return Json(result);
        }

        public IActionResult Delete(int notificationId)
        {
            if (notificationId != 0)
            {
                var result = _notificationServiceAsync.DeleteNotification(notificationId);
                if (result > 0)
                {
                    notify("Record succefully deleted!", NotificationType.success);
                    return RedirectToAction("index");
                }
                else
                {
                    notify("There is problem for deleting the record!", NotificationType.success);
                    return RedirectToAction("index");
                }

            }
            else
            {
                notify("Notification id is not supplied!", NotificationType.warning);
                return RedirectToAction("index");
            }
        }
    }
}

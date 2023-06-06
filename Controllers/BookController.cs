using Advocate.Common;
using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class BookController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBookServiceAsync bookServiceAsync;
        private readonly UserManager<ApplicationUser> _userManager;
        public BookController(IMapper _mapper, IBookServiceAsync bookServiceAsync,
            UserManager<ApplicationUser> _userManager)
        {
            this._mapper = _mapper;
            this.bookServiceAsync = bookServiceAsync;
            this._userManager = _userManager;
        }
        public IActionResult Index()
        {
            var result = _mapper.Map<IEnumerable<BookEntity>, IEnumerable<BookViewModel>>(bookServiceAsync.GetAllAsync().OrderByDescending(ord => ord.Id));
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit(int id)
        {
            var bookViewModel = bookServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<BookEntity, BookViewModel>(bookViewModel);
            return View(data);
        }

        public IActionResult Details(int id)
        {
            var bookViewModel = bookServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<BookEntity, BookViewModel>(bookViewModel);
            return View(data);
        }

        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(BookViewModel bookViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookViewModel.IsActive = true;
                    bookViewModel.UserId = GetCurrentUserAsync().Result.Id;
                    var data = _mapper.Map<BookViewModel, BookEntity>(bookViewModel);
                    var isExist = bookServiceAsync.isActExist(bookViewModel.BookName);
                    if (isExist == true)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        int result = bookServiceAsync.SaveAsync(data);
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

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, BookViewModel bookViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExist = bookServiceAsync.isActExist(bookViewModel.BookName);
                    if (isExist == false)
                    {
                        notify("Record is already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        var actDetail = bookServiceAsync.FindByIdAsync(id);
                        if (actDetail != null)
                        {
                            actDetail.ShortName = bookViewModel.ShortName;
                            actDetail.Description = bookViewModel.Description;
                            actDetail.BookName = bookViewModel.BookName;
                        }
                        int result = bookServiceAsync.UpdateAsync(actDetail);
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
                BookEntity obj = new BookEntity
                {
                    Id = id
                };

                var result = bookServiceAsync.DeleteAsync(obj);
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction("index");
            }
        }

        public IActionResult BookEntry()
        {
            return View();
        }

        [HttpGet]
        [Route("get-tallydata/{tallytype}/{datetype}/{date}")]
        public JsonResult GetAllTallyData(string tallytype, string datetype, string date)
        {
            var ruleItem = bookServiceAsync.GetTallyData(tallytype, datetype, date);
            return Json(new SelectList(ruleItem, "Id", "text"));
        }


        [HttpPost]
        public ActionResult SaveBookEntryDetail(BookEntryDetailViewModel bookentryviewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookentryviewmodel.IsActive = true;
                    bookentryviewmodel.UserId = GetCurrentUserAsync().Result.Id;
                    var data = _mapper.Map<BookEntryDetailViewModel, BookEntryDetailEntity>(bookentryviewmodel);
                    int result = bookServiceAsync.SaveBookEntryDetail(data);
                    if (result>0)
                        return Json(new Message() { statuscode = 200, resultmessage = "Notification saved successfully!", data = result });
                    else
                        return Json(new Message() { statuscode = 500, resultmessage = "Internal server error!", data = result });
                   
                }
                else
                {
                    return Json(new Message() { statuscode = 500, resultmessage = "Internal server error!", data = 0 });
                }
            }
            catch (Exception e)
            {
                return Json(new Message() { statuscode = 500, resultmessage = "Internal server error!", data = 0 });
            }
        }

        [HttpPost]
        public ActionResult EditBookEntryDetail(BookEntryDetailViewModel bookentryviewmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bookentryviewmodel.IsActive = true;
                    bookentryviewmodel.UserId = GetCurrentUserAsync().Result.Id;
                    var data = _mapper.Map<BookEntryDetailViewModel, BookEntryDetailEntity>(bookentryviewmodel);
                    int result = bookServiceAsync.SaveBookEntryDetail(data);
                    if (result > 0)
                        return Json(new Message() { statuscode = 200, resultmessage = "Notification saved successfully!", data = result });
                    else
                        return Json(new Message() { statuscode = 500, resultmessage = "Internal server error!", data = result });

                }
                else
                {
                    return Json(new Message() { statuscode = 500, resultmessage = "Internal server error!", data = 0 });
                }
            }
            catch (Exception e)
            {
                return Json(new Message() { statuscode = 500, resultmessage = "Internal server error!", data = 0 });
            }
        }
    }
}

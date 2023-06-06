using Advocate.Common;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class SubjectController : BaseController
    {
        private readonly IMapper _mapper;
        // private readonly ILogger _logger;
        private readonly ISubjectServiceAsync _subjectServiceAsync;

        public SubjectController(ISubjectServiceAsync _subjectServiceAsync, /*ILogger _logger,*/ IMapper _mapper)
        {
            this._subjectServiceAsync = _subjectServiceAsync;
            this._mapper = _mapper;
            //this._logger = _logger;
        }
        // GET: SubjectController
        public ActionResult Index()
        {
            var result = _mapper.Map<IEnumerable<SubjectEntity>, IEnumerable<SubjectViewModel>>(_subjectServiceAsync.GetAllAsync());
            return View(result);
        }

        // GET: SubjectController/Details/5
        public ActionResult Details(int id)
        {
            var subjectdetail = _subjectServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<SubjectEntity, SubjectViewModel>(subjectdetail);
            return View(data);           
        }

        // GET: SubjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubjectController/Create
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Create(SubjectViewModel subjectViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<SubjectViewModel, SubjectEntity>(subjectViewModel);
                    int result = _subjectServiceAsync.SaveAsync(data);
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
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: SubjectController/Edit/5
        public ActionResult Edit(int id)
        {
            var subjectdetail = _subjectServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<SubjectEntity, SubjectViewModel>(subjectdetail);
            return View(data);
        }

        // POST: SubjectController/Edit/5
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Edit(SubjectViewModel subjectViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subjectdetail = _subjectServiceAsync.FindByIdAsync(subjectViewModel.Id);
                    if (subjectdetail != null)
                    {
                        subjectdetail.Name = subjectViewModel.Name;
                        subjectdetail.Description = subjectViewModel.Description;
                    }                   
                    int result = _subjectServiceAsync.UpdateAsync(subjectdetail);
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
                SubjectEntity obj = new SubjectEntity
                {
                    Id = id
                };

                var result = _subjectServiceAsync.DeleteAsync(obj);
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction("index");
            }
        }
    }
}

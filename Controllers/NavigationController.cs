using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advocate.Entities;
using Advocate.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Advocate.Common;
using Advocate.Models;
using AutoMapper;

namespace Advocate.Controllers
{
    public class NavigationController : BaseController
    {
        private readonly INavigationAsync _service;
        private readonly ILogger<NavigationController> _logger;
        private readonly IMapper _mapper;
        public NavigationController(INavigationAsync _service,
            ILogger<NavigationController> _logger, IMapper _mapper)
        {
            this._service = _service;
            this._logger = _logger;
            this._mapper = _mapper;
        }
        public IActionResult Index()
        {
            var data = _mapper.Map<IEnumerable<NavigationEntity>,IEnumerable<NavigationViewModel>>(_service.GetAllAsync());
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(NavigationViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<NavigationViewModel, NavigationEntity>(obj);
                int result = _service.SaveAsync(data);
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
                return View();
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for fetching the detail", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                var result = _service.FindByIdAsync(id);
                return View(result);
            }
        }

        [HttpPost]
        public IActionResult Edit(NavigationEntity obj)
        {
            if (obj.Id != 0)
            {
                int result = _service.UpdateAsync(obj);
                if (result > 0)
                {
                    notify("Record successfully updated!", NotificationType.success);
                    return View();
                }
                else
                {
                    notify("There is some error during update the detail!", NotificationType.success);
                    return View();
                }
            }
            else
            {
                notify("Record key id is not supplied for updating the detail", NotificationType.error);
                return RedirectToAction("index");
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
                var result = _service.FindByIdAsync(id);
                return View(result);
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
                NavigationEntity obj = new NavigationEntity
                {
                    Id = id
                };

                var result = _service.DeleteAsync(obj);
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction("index");
            }
        }
    }
}

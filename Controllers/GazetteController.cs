using Advocate.Common;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using Advocate.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class GazetteController : BaseController
    {
        private readonly IGazzetServiceAsync gazzetServiceAsync;
        private readonly IEGazzetDataServiceAsync _eGazzetService;
        private readonly ISubGazzetServiceAsync subGazzetServiceAsync;
        private readonly ILogger<NavigationController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMapper _mapper;

        public GazetteController(IGazzetServiceAsync gazzetServiceAsync, IWebHostEnvironment webHostEnvironment, ISubGazzetServiceAsync subGazzetServiceAsync, IEGazzetDataServiceAsync _eGazzetService, ILogger<NavigationController> _logger, IMapper _mapper)
        {
            this.gazzetServiceAsync = gazzetServiceAsync;
            this._logger = _logger;
            this._mapper = _mapper;
            this.subGazzetServiceAsync = subGazzetServiceAsync;
            this._eGazzetService = _eGazzetService;
            this.webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            var result = _mapper.Map<IEnumerable<GazetteTypeEntity>, IEnumerable<GazetteViewModel>>(gazzetServiceAsync.GetAllAsync().OrderByDescending(ord => ord.Id));
            return View(result);
        }

        public IActionResult Gdata()
        {
            GazzetSearchViewModel viewModel = new GazzetSearchViewModel();
            var gztViewModel = _mapper.Map<IEnumerable<GazetteTypeEntity>, IEnumerable<GazetteViewModel>>(gazzetServiceAsync.GetAllAsync());
            var LstPart = _eGazzetService.GetPart();
            var LstCategory = _eGazzetService.GetCategory();
            var LstDepartment = _eGazzetService.GetDeaprtmentDataList();
            viewModel.LstNature = new SelectList(LstCategory, nameof(DdlDto.value), nameof(DdlDto.text), null, null);
            viewModel.LstYear = new SelectList(StaticDropDownDictionaries.YearDictionary(), "Key", "Value");
            viewModel.LstGazzets = new SelectList(gztViewModel, nameof(GazetteViewModel.Id), nameof(GazetteViewModel.GazetteName), null, null);
            viewModel.LstParts = new SelectList(LstPart, nameof(DdlDto.value), nameof(DdlDto.text), null, null);
            viewModel.LstDepartment = new SelectList(LstDepartment, nameof(DdlDto.value), nameof(DdlDto.text), null, null);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SearchEGazzet(GazzetSearchViewModel search)
        {
            string fileFolderPath = "\\upload\\EGZT\\";
            var searchViewModel = _mapper.Map<GazzetSearchViewModel, EGazzetSearchDto>(search);
            var EGztData = _eGazzetService.EGazzetData(searchViewModel);
            var result = _mapper.Map<List<EGazzetDataEntity>, List<GazzetDataDto>>(EGztData);
            var finaldt = result.Select(s => new GazzetDataDto
            {
                oraganization = s.oraganization,
                subject = s.subject,
                issue_date = s.issue_date,
                publish_date = s.publish_date,
                reference_no = s.reference_no,
                file_name = fileFolderPath+s.file_name
            });

            return PartialView("_ViewGazzetData", finaldt.ToList());
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(GazetteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<GazetteViewModel, GazetteTypeEntity>(obj);
                    var isExist = gazzetServiceAsync.isGazzetExist(obj.GazetteName);
                    if (isExist == true)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        int result = gazzetServiceAsync.SaveAsync(data);
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

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for fetching the detail", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                var result = gazzetServiceAsync.FindByIdAsync(id);
                var data = _mapper.Map<GazetteTypeEntity, GazetteViewModel>(result);
                return View(data);
            }
        }

        public IActionResult Edit(int id)
        {
            var gazetteViewModel = gazzetServiceAsync.FindByIdAsync(id);
            var data = _mapper.Map<GazetteTypeEntity, GazetteViewModel>(gazetteViewModel);
            return View(data);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, GazetteViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var isExist = gazzetServiceAsync.isGazzetExist(obj.GazetteName);
                    if (isExist == false)
                    {
                        notify("Record is already exist!", NotificationType.warning);
                        return View();
                    }
                    else
                    {
                        var actDetail = gazzetServiceAsync.FindByIdAsync(id);
                        if (actDetail != null)
                        {
                            actDetail.GazetteName = obj.GazetteName;
                            actDetail.Description = obj.Description;
                        }
                        int result = gazzetServiceAsync.UpdateAsync(actDetail);
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

        public IActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                notify("Record key id is not supplied for deleting the record!", NotificationType.error);
                return RedirectToAction("index");
            }
            else
            {
                GazetteTypeEntity obj = new GazetteTypeEntity
                {
                    Id = id
                };

                var result = gazzetServiceAsync.DeleteAsync(obj);
                if (result > 0)
                    notify("Record succefully deleted!", NotificationType.success);
                else
                    notify("There is an issue while deleting the record!", NotificationType.success);
                return RedirectToAction("index");
            }
        }

    }
}

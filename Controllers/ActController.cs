using Advocate.Common;
using Advocate.Data;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using System.IO;
using System.Text;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Advocate.Filters;
using System.Linq;
using Advocate.Helpers;
using Advocate.Dtos;

namespace Advocate.Controllers
{
    public class ActController : BaseController
    {
        private readonly IActTypeServiceAsync actTypeServiceAsync;
        private readonly ISubjectServiceAsync subjectServiceAsync;
        private readonly IGazzetServiceAsync gazzetServiceAsync;
        private readonly ISubGazzetServiceAsync subGazzetServiceAsync;
        private readonly IMapper _mapper;
        private readonly IActServiceAsync actServiceAsync;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFileServiceAsync fileServiceAsync;
        private readonly IUriService uriService;
        public ActController(IActTypeServiceAsync actTypeServiceAsync,
            ISubjectServiceAsync subjectServiceAsync,
            IGazzetServiceAsync gazzetServiceAsync,
            ISubGazzetServiceAsync subGazzetServiceAsync,
            IMapper _mapper,
            IActServiceAsync actServiceAsync,
            UserManager<ApplicationUser> _userManager,
            IFileServiceAsync fileServiceAsync, IUriService uriService)
        {
            this.gazzetServiceAsync = gazzetServiceAsync;
            this.actTypeServiceAsync = actTypeServiceAsync;
            this.subjectServiceAsync = subjectServiceAsync;
            this.subGazzetServiceAsync = subGazzetServiceAsync;
            this._mapper = _mapper;
            this.actServiceAsync = actServiceAsync;
            this._userManager = _userManager;
            this.fileServiceAsync = fileServiceAsync;
            this.uriService = uriService;
        }

        public Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        public IActionResult Index(int PageNumber=1)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(PageNumber, 10);
            var pagedData =  actServiceAsync.GetAllActs()
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToList();
            var totalRecords = actServiceAsync.GetAllActs().Count();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ActDto>(pagedData, validFilter, totalRecords, uriService, route);
            return View(pagedReponse);
            //var result = actServiceAsync.GetAllActs();
            //return View(result);

        }

        [HttpGet]
        public IActionResult Create(int actTypeId = 0)
        {
            ViewBag.Act = actServiceAsync.GetAllActs();
            ViewBag.AcessType = "notactcategory";
            ViewBag.Screen = "create_year";
            return View();
        }

        [HttpPost]
        public IActionResult Create(ActViewModel actViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = _mapper.Map<ActViewModel, ActEntity>(actViewModel);
                    data.UserID = GetCurrentUserAsync().Result.Id;
                    data.IsActive = true;
                    data.AssentDate = Convert.ToDateTime(actViewModel.AssentDate);
                    data.GazetteDate = Convert.ToDateTime(actViewModel.GazetteDate);
                    int result = actServiceAsync.SaveAsync(data);
                    if (result == -1)
                    {
                        notify("Record already exist!", NotificationType.warning);
                        return Ok();
                    }
                    else
                    {
                        if (actViewModel.ActCategory == "repealed")
                        {
                            List<RepealedActEntity> repealdList = new List<RepealedActEntity>();
                            foreach (var item in actViewModel.selectedActListId)
                                repealdList.Add(new RepealedActEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    ActID = result,
                                    RepealedActID = item,
                                    IsActive = true
                                });
                            actServiceAsync.SaveRepealedAct(repealdList);
                        }
                        if (actViewModel.ActCategory == "amaned")
                        {
                            List<AmendedActEntity> amendedList = new List<AmendedActEntity>();
                            foreach (var item in actViewModel.selectedActListId)
                                amendedList.Add(new AmendedActEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    ActID = result,
                                    AmendedActID = item,
                                    IsActive = true
                                });
                            actServiceAsync.SaveAmendedAct(amendedList);
                        }
                        if (actViewModel.actBookList.Count > 0)
                        {
                            List<ActBookEntity> actbookList = new List<ActBookEntity>();
                            foreach (var item in actViewModel.actBookList)
                                actbookList.Add(new ActBookEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    ActId = result,
                                    BookId = item.bookid,
                                    BookYear = item.bookyear,
                                    BookPageNo = item.bookpageno,
                                    BookSrNo = item.booksrno,
                                    IsActive = true
                                });
                            actServiceAsync.SaveActBook(actbookList);
                        }
                        //FileBasicInformation file = new FileBasicInformation();
                        //file.fileName = actViewModel.ActNumber.ToString();
                        //file.PublishedDate = actViewModel.GazetteDate.ToString();
                        //file.PublishingGazette = actViewModel.PublishedGazetteName;
                        //file.PageNumber = actViewModel.PageNumber.ToString();
                        //file.GazetteNature = actViewModel.GazetteNuture.ToString();
                        //string htmltemplate = fileServiceAsync.CreateAndReplaceWordTemplate(file); ;
                        //fileServiceAsync.Upload(GenerateStringToWord(htmltemplate), "");
                        return Json(new Message() { statuscode = 200, resultmessage = "Record saved successfully!", data = result });
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return Json(new Message() { statuscode = 502, resultmessage = "Exception!", data = null });
            }
        }



        [HttpPost]
        [Route("download-act-file")]
        public FileResult DownloadGeneratedFile(string fileName)
        {
            byte[] bytes = fileServiceAsync.Download(fileName, "fileformats");
            return File(bytes, "application/octet-stream", fileName);
        }

        [HttpGet]
        public IActionResult Edit(int actId)
        {
            ViewBag.Act = actServiceAsync.GetAllActs();
            ViewBag.AcessType = "notactcategory";
            ViewBag.Screen = "edit_year";
            return View();
        }

        [HttpPost]
        public IActionResult Edit(ActViewModel actViewModel)
        {
            try
            {
               
                int result = 0;
                var actDetail = actServiceAsync.FindByIdAsync(actViewModel.Id);
                if (actDetail != null)
                {
                    actDetail.ActTypeId = actViewModel.ActTypeId;
                    actDetail.ActNumber = actViewModel.ActNumber;
                    actDetail.ActYear = actViewModel.ActYear;
                    actDetail.AssentBy = actViewModel.AssentBy;
                    actDetail.AssentDate = Convert.ToDateTime(actViewModel.AssentDate);
                    actDetail.ActName = actViewModel.ActName;
                    actDetail.GazetteId = actViewModel.PublishedInId;
                    actDetail.PartId = actViewModel.PartId;
                    actDetail.Nature = actViewModel.GazetteNuture;
                    actDetail.GazetteDate = Convert.ToDateTime(actViewModel.GazetteDate);
                    actDetail.PageNo = actViewModel.PageNumber;
                    actDetail.ComeInforce = actViewModel.ComeInforce;
                    actDetail.ActCategory = actViewModel.ActCategory;
                    actDetail.SubjectAct = actViewModel.SubjectAct;
                    result = actServiceAsync.UpdateAsync(actDetail);
                    if (actViewModel.ActCategory == "repealed")
                    {
                        var repealedAct = actServiceAsync.Update_RepealedActByID(actViewModel.Id, actViewModel.selectedActListId, GetCurrentUserAsync().Result.Id);
                    }
                    if (actViewModel.ActCategory == "amaned")
                    {
                        var repealedAct = actServiceAsync.Update_AmendedActByID(actViewModel.Id, actViewModel.selectedActListId, GetCurrentUserAsync().Result.Id);
                    }
                    if (actViewModel.actBookList.Count > 0)
                    {
                        List<ActBookEntity> actbookList = new List<ActBookEntity>();
                        foreach (var item in actViewModel.actBookList)
                            actbookList.Add(new ActBookEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                ActId = actViewModel.Id,
                                BookId = item.bookid,
                                BookYear = item.bookyear,
                                BookPageNo = item.bookpageno,
                                BookSrNo = item.booksrno,
                                IsActive = true
                            });
                        actServiceAsync.Update_ActBookByID(actViewModel.Id,actbookList, GetCurrentUserAsync().Result.Id);
                    }

                }
                ViewBag.Act = actServiceAsync.GetAllActs();
                return Json(new Message() { statuscode = 200, resultmessage = "Record saved successfully!", data = result });               
            }
            catch (Exception e)
            {
                return View();
            }

        }


        [HttpPost]
        public JsonResult ActDetail(int actId, string accessType)
        {
            return Json(actServiceAsync.GetActDetailByActId(GetCurrentUserAsync().Result.Id, actId));
        }

        public IActionResult Details(int id)
        {
            var result = actServiceAsync.GetActDetailInfoByActId(GetCurrentUserAsync().Result.Id, id);
            return View(result);
        }

        [HttpGet]
        public JsonResult RepeateActDetail()
        {
            ViewBag.Screen = "edit_year";
            return Json(actServiceAsync.LastInsertedData(GetCurrentUserAsync().Result.Id));
        }
        //private IFormFile GenerateStringToWord(string htmltemplate)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        using (WordprocessingDocument doc =
        //            WordprocessingDocument.Create(ms, WordprocessingDocumentType.Document))
        //        {
        //            MainDocumentPart mdp = doc.MainDocumentPart;
        //            if (mdp == null)
        //            {
        //                mdp = doc.AddMainDocumentPart();
        //                Document document = new Document(new Body());
        //                document.Save(mdp);
        //            }

        //            string altChunkId = "AltChunkId1";
        //            AlternativeFormatImportPart afip =
        //                mdp.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Xhtml, altChunkId);
        //            using (Stream stream = afip.GetStream(FileMode.Create, FileAccess.Write))
        //            {
        //                using (StreamWriter sw = new StreamWriter(stream, Encoding.UTF8))
        //                {
        //                    sw.Write(htmltemplate);
        //                }
        //            }

        //            AltChunk altChunk = new AltChunk();
        //            altChunk.Id = altChunkId;
        //            mdp.Document.Body.InsertAt(altChunk, 0);
        //            mdp.Document.Save();
        //        }
        //        return new FormFile(ms, 0, 0, "application/vnd.ms-word", "Grid.doc");
        //    }
        //}

        [HttpPost]
        public int ActDetailByActType_Number_Year(int TypeId, int ActNumber, int Year)
        {
            return actServiceAsync.GetActIdByActType_Number_year(TypeId, ActNumber, Year);
        }

        [HttpPost]
        public JsonResult GetActRepealedBy(int actId)
        {

            var repealedActBy = actServiceAsync.GetActInfoByRepealedAct(actId);
            return Json(repealedActBy);
        }
    }
}

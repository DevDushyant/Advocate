using Advocate.Common;
using Advocate.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class ApplicationDropdownController : Controller
    {
        private readonly IActTypeServiceAsync actTypeServiceAsync;
        private readonly ISubjectServiceAsync subjectServiceAsync;
        private readonly IGazzetServiceAsync gazzetServiceAsync;
        private readonly ISubGazzetServiceAsync subGazzetServiceAsync;
        private readonly IBookServiceAsync bookServiceAsync;
        private readonly IActServiceAsync actServiceAsync;
        private readonly IRuleServiceAsync ruleServiceAsync;
        private readonly INotifcationTypeAsyncService notifcationTypeAsyncService;
        public readonly IWebHostEnvironment Environment;
        public ApplicationDropdownController(IActTypeServiceAsync actTypeServiceAsync,
            ISubjectServiceAsync subjectServiceAsync,
            IGazzetServiceAsync gazzetServiceAsync,
            ISubGazzetServiceAsync subGazzetServiceAsync, IBookServiceAsync bookServiceAsync
            , IActServiceAsync actServiceAsync, IRuleServiceAsync ruleServiceAsync
            , IWebHostEnvironment Environment
            , INotifcationTypeAsyncService notifcationTypeAsyncService)
        {
            this.gazzetServiceAsync = gazzetServiceAsync;
            this.actTypeServiceAsync = actTypeServiceAsync;
            this.subjectServiceAsync = subjectServiceAsync;
            this.subGazzetServiceAsync = subGazzetServiceAsync;
            this.bookServiceAsync = bookServiceAsync;
            this.actServiceAsync = actServiceAsync;
            this.ruleServiceAsync = ruleServiceAsync;
            this.notifcationTypeAsyncService = notifcationTypeAsyncService;
            this.Environment = Environment;
        }

        [HttpGet]
        [Route("get-all-acttype")]
        public JsonResult GetActType()
        {
            return Json(new SelectList(actTypeServiceAsync.GetAllAsync(), "Id", "ActType"));
        }

        [HttpGet]
        [Route("get-all-year")]
        public JsonResult GetYears()
        {
            return Json(new SelectList(StaticDropDownDictionaries.YearDictionary(), "Key", "Value"));
        }

        [HttpGet]
        [Route("get-all-assentby")]
        public JsonResult GetAssentBy()
        {
            return Json(new SelectList(StaticDropDownDictionaries.AssentByDictionary(), "Key", "Value"));
        }

        
        [HttpGet]
        [Route("get-all-gazette-part")]
        public JsonResult GetPart()
        {
            return Json(new SelectList(subGazzetServiceAsync.GetAllAsync(), "Id", "PartName"));
        }

        [HttpGet]
        [Route("get-all-gazette")]
        public JsonResult GetGazette()
        {
            return Json(new SelectList(gazzetServiceAsync.GetAllAsync(), "Id", "GazetteName"));
        }

        [HttpGet]
        [Route("get-all-comeinoforce")]
        public JsonResult GetComeInforce()
        {
            return Json(new SelectList(StaticDropDownDictionaries.ComeInforce(), "Key", "Value"));
        }

        [HttpGet]
        [Route("get-book-volume")]
        public JsonResult GetBookVolume()
        {
            return Json(new SelectList(StaticDropDownDictionaries.VolumeDictionary(), "Key", "Value"));
        }

        [HttpGet]
        [Route("get-rule-gsr")]
        public JsonResult GetRuleGSR()
        {
            return Json(new SelectList(StaticDropDownDictionaries.Rule_GSR_SO(), "Key", "Value"));
        }

        [HttpGet]
        [Route("get-all-nature")]
        public JsonResult GetNature()
        {
            return Json(new SelectList(StaticDropDownDictionaries.GazetteNature(), "Key", "Value"));
        }

        [HttpGet]
        [Route("get-all-subject")]
        public JsonResult GetSubject()
        {
            return Json(new SelectList(subjectServiceAsync.GetAllAsync(), "Id", "Name"));
        }
        [HttpGet]
        [Route("get-all-book")]
        public JsonResult GetBook()
        {
            return Json(new SelectList(bookServiceAsync.GetAllAsync(), "Id", "BookName"));
        }

        [HttpGet]
        [Route("get-all-actbyacttype/{acttype}")]
        public JsonResult GetActByActType(int acttype)
        {
            var actItem = actServiceAsync.GetAllActs()
                .Where(type => type.ActTypeId == acttype);
            return Json(new SelectList(actItem, "Id", "ActName"));
        }

        [HttpGet]
        [Route("get-all-managetype")]
        public JsonResult GetAllManageType()
        {
            return Json(new SelectList(StaticDropDownDictionaries.ManageType(), "Key", "Value"));
        }        

        [HttpGet]
        [Route("GetSearchRuleKind")]
        public JsonResult GetSearchRuleKind()
        {
            return Json(new SelectList(StaticDropDownDictionaries.RuleSearchingKind(), "Key", "Value"));
        }

        [HttpGet]
        [Route("get-all-rulebyActId/{actId}/{ruleKind}")]
        public JsonResult GetAllRuleByActID(int actId,string ruleKind)
        {
            var ruleItem = ruleServiceAsync.GetRuleByActId(actId, ruleKind);               
            return Json(new SelectList(ruleItem, "Id", "RuleName"));
        }

        [HttpGet]
        [Route("get-all-notificationType")]
        public JsonResult GetAllNotificationType()
        {
            var notTypeItem = notifcationTypeAsyncService.GetAllAsync();
            return Json(new SelectList(notTypeItem, "Id", "Name"));
        }

		[HttpGet]
		[Route("ddl-data-ext-type")]
		public JsonResult GetFileType()
		{
			return Json(new SelectList(StaticDropDownDictionaries.DataToolType(), "Key", "Value"));
		}
	}
}

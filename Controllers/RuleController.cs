using Advocate.Common;
using Advocate.Data;
using Advocate.Dtos;
using Advocate.Entities;
using Advocate.Interfaces;
using Advocate.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Controllers
{
    public class RuleController : BaseController
    {
        private readonly IRuleServiceAsync ruleServiceAsync;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public RuleController(IRuleServiceAsync ruleServiceAsync, IMapper _mapper, UserManager<ApplicationUser> _userManager)
        {
            this.ruleServiceAsync = ruleServiceAsync;
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


        /// <summary>
        /// The purpose of this method for getting all the rules.
        /// </summary>
        /// <returns></returns>
        // GET: RuleController
        public ActionResult Index()
        {
            var result = ruleServiceAsync.GetRule();
            return View(result);
        }

        // GET: RuleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RuleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RuleController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RuleViewModel ruleViewModel)
        {
            try
            {
                var data = _mapper.Map<RuleViewModel, RuleEntity>(ruleViewModel);
                data.UserID = GetCurrentUserAsync().Result.Id;
                data.IsActive = true;
                data.RuleDate = Convert.ToDateTime(ruleViewModel.RuleDate);
                data.ComeInforceEFDate = Convert.ToDateTime(ruleViewModel.ComeInforceEFDate);
                data.Id = ruleServiceAsync.LastInsertedRecord() + 1;
                int ruleInsertedId = ruleServiceAsync.SaveAsync(data);
                if (ruleInsertedId != 0)
                {
                    if (ruleViewModel.ruleBooks != null)
                    {
                        if (ruleViewModel.ruleBooks.Count > 0)
                        {
                            List<RuleBookEntity> rulBookList = new List<RuleBookEntity>();
                            foreach (var item in ruleViewModel.ruleBooks)
                                rulBookList.Add(new RuleBookEntity()
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
                            ruleServiceAsync.SaveOrUpdateRuleBook(rulBookList, 0);
                        }
                    }
                    if (ruleViewModel.AmendedRules != string.Empty)
                    {
                        List<int> amendedRuleList = ruleViewModel.AmendedRules.Split(',').Select(int.Parse).ToList();
                        List<RuleAmendedEntity> amendedList = new List<RuleAmendedEntity>();
                        foreach (var item in amendedRuleList)
                            amendedList.Add(new RuleAmendedEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                RuleId = ruleInsertedId,
                                AmendedRuleID = item,
                                IsActive = true,
                                CreatedDate = DateTime.Now
                            });
                        ruleServiceAsync.SaveorUpdateRuleAmended(amendedList, 0);
                    }
                    if (ruleViewModel.RepealedRules != string.Empty)
                    {
                        List<int> repealedRuleList = ruleViewModel.RepealedRules.Split(',').Select(int.Parse).ToList();
                        List<RuleRepealedEntity> repealedList = new List<RuleRepealedEntity>();
                        foreach (var item in repealedRuleList)
                            repealedList.Add(new RuleRepealedEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                RuleId = ruleInsertedId,
                                RepealedRuleID = item,
                                IsActive = true,
                                CreatedDate = DateTime.Now
                            });
                        ruleServiceAsync.SaveOrUpdateRuleRepealed(repealedList, 0);
                    }
                    if (ruleViewModel.ExtraRulesAct != string.Empty)
                    {
                        List<int> extActRuleList = ruleViewModel.ExtraRulesAct.Split(',').Select(int.Parse).ToList();
                        List<RuleActExtraEntity> extRuleActList = new List<RuleActExtraEntity>();
                        foreach (var item in extActRuleList)
                            extRuleActList.Add(new RuleActExtraEntity()
                            {
                                UserID = GetCurrentUserAsync().Result.Id,
                                RuleId = ruleInsertedId,
                                ActId = item,
                                IsActive = true,
                                CreatedDate = DateTime.Now
                            });
                        ruleServiceAsync.SaveOrUpdateRuleExtraAct(extRuleActList, 0);
                    }

                    return Json(new Message() { statuscode = 200, resultmessage = "Rule information saved successfully!", data = ruleInsertedId });
                }
                else
                {
                    return Json(new Message() { statuscode = 500, resultmessage = "There is some problem while saving the rule information!", data = -1 });
                }
            }
            catch (Exception ee)
            {
                return Json(new Message() { statuscode = 500, resultmessage = ee.Message, data = -1 });
            }
        }

        // GET: RuleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RuleController/Edit/5
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RuleViewModel ruleViewModel)
        {
            try
            {

                int result = 0;
                var ruleDetail = ruleServiceAsync.FindByIdAsync(ruleViewModel.Id);
                if (ruleDetail != null)
                {
                    var editableData = _mapper.Map<RuleViewModel, RuleEntity>(ruleViewModel);
                    ruleDetail = editableData;
                    //ruleDetail.ActTypeId = ruleViewModel.ActTypeId;
                    //ruleDetail.ActId = ruleViewModel.ActId;                    
                    //ruleDetail.RuleNo = ruleViewModel.RuleNo;
                    //ruleDetail.GSRSO_Prefix = ruleViewModel.GSRSO_Prefix;
                    //ruleDetail.GSRSO_No = ruleViewModel.GSRSO_No;
                    //ruleDetail.RuleType = ruleViewModel.RuleType;
                    //ruleDetail.RuleName = ruleViewModel.RuleName;
                    //ruleDetail.RuleDate = ruleViewModel.RuleDate;
                    //ruleDetail.GazzetId = ruleViewModel.GazzetId;
                    //ruleDetail.PartId = ruleViewModel.PartId;
                    //ruleDetail.PartId = ruleViewModel.PartId;
                    result = ruleServiceAsync.UpdateAsync(ruleDetail);
                    if (result != 0)
                    {
                        if (ruleViewModel.ruleBooks != null)
                        {
                            if (ruleViewModel.ruleBooks.Count > 0)
                            {
                                List<RuleBookEntity> rulBookList = new List<RuleBookEntity>();
                                foreach (var item in ruleViewModel.ruleBooks)
                                    rulBookList.Add(new RuleBookEntity()
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
                                ruleServiceAsync.SaveOrUpdateRuleBook(rulBookList, result);
                            }
                        }
                        if (ruleViewModel.AmendedRules != string.Empty)
                        {
                            List<int> amendedRuleList = ruleViewModel.AmendedRules.Split(',').Select(int.Parse).ToList();
                            List<RuleAmendedEntity> amendedList = new List<RuleAmendedEntity>();
                            foreach (var item in amendedRuleList)
                                amendedList.Add(new RuleAmendedEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    RuleId = result,
                                    AmendedRuleID = item,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            ruleServiceAsync.SaveorUpdateRuleAmended(amendedList, result);
                        }
                        if (ruleViewModel.RepealedRules != string.Empty)
                        {
                            List<int> repealedRuleList = ruleViewModel.RepealedRules.Split(',').Select(int.Parse).ToList();
                            List<RuleRepealedEntity> repealedList = new List<RuleRepealedEntity>();
                            foreach (var item in repealedRuleList)
                                repealedList.Add(new RuleRepealedEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    RuleId = result,
                                    RepealedRuleID = item,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            ruleServiceAsync.SaveOrUpdateRuleRepealed(repealedList, result);
                        }
                        if (ruleViewModel.ExtraRulesAct != string.Empty)
                        {
                            List<int> extActRuleList = ruleViewModel.ExtraRulesAct.Split(',').Select(int.Parse).ToList();
                            List<RuleActExtraEntity> extRuleActList = new List<RuleActExtraEntity>();
                            foreach (var item in extActRuleList)
                                extRuleActList.Add(new RuleActExtraEntity()
                                {
                                    UserID = GetCurrentUserAsync().Result.Id,
                                    RuleId = result,
                                    ActId = item,
                                    IsActive = true,
                                    CreatedDate = DateTime.Now
                                });
                            ruleServiceAsync.SaveOrUpdateRuleExtraAct(extRuleActList, result);
                        }
                        return Json(new Message() { statuscode = 200, resultmessage = "Rule information updated successfully!", data = result });
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

        // GET: RuleController/Delete/5
        public ActionResult Delete(int id)
        {
            var result = ruleServiceAsync.GetRule().Where(rule => rule.Id == id).FirstOrDefault();
            return View(result);
        }

        // POST: RuleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                RuleEntity obj = new RuleEntity
                {
                    Id = id
                };
                var result = ruleServiceAsync.DeleteAsync(obj);
                if (result > 0)
                {
                    RuleBookEntity rbook = new RuleBookEntity
                    {
                        RuleId = id
                    };
                    int rb = ruleServiceAsync.DeleteRuleBook(rbook);
                    RuleAmendedEntity ruleamd = new RuleAmendedEntity
                    {
                        RuleId = id
                    };
                    int ramnd = ruleServiceAsync.DeleteAmendedRule(ruleamd);
                    RuleRepealedEntity rulerpd = new RuleRepealedEntity
                    {
                        RuleId = id
                    };
                    int rrpd = ruleServiceAsync.DeleteRepealedRule(rulerpd);
                    RuleActExtraEntity ruleaxtra = new RuleActExtraEntity
                    {
                        RuleId = id
                    };
                    int rext = ruleServiceAsync.DeleteExtraAct(ruleaxtra);
                }
                notify("Record succefully deleted!", NotificationType.success);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ee)
            {
                notify("There is some probelm while deleting the record!", NotificationType.success);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public JsonResult GetRulesByActType(int actTypeId = 0, int act = 0)
        {
            IEnumerable<RuleDto> result;
            if (actTypeId == 0 || act == 0)
                result = ruleServiceAsync.GetRule();
            else if (act != 0)
                result = ruleServiceAsync.GetRule().Where(ruled => ruled.ActId == act);
            else
                result = ruleServiceAsync.GetRule().Where(ruled => ruled.ActTypeId == actTypeId);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetRuleDetailByRuleId(int ruleId)
        {
            var result = ruleServiceAsync.GetRuleDetailByRuleId(ruleId);
            return Json(result);
        }

        [HttpGet]
        public JsonResult RepeatAction()
        {
            int ruleId = ruleServiceAsync.LastInsertedRecord();
            var result = ruleServiceAsync.GetRuleDetailByRuleId(ruleId);
            return Json(result);
        }

        public ActionResult GetRuleDetail(int id)
        {
            var data = ruleServiceAsync.GetRuleDetailReportbyRuleId(id);
            return View(data);
        }
    }
}

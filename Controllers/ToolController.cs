using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Advocate.Dtos;
using AutoMapper;
using Advocate.Entities;
using Advocate.Interfaces;
using OfficeOpenXml;
using System;

namespace Advocate.Controllers
{
    public class ToolController : Controller
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		private readonly IMapper _mapper;
		private readonly IEGazzetDataServiceAsync iEGazzetData;
		public ToolController(IWebHostEnvironment webHostEnvironment, IMapper _mapper, IEGazzetDataServiceAsync iEGazzetData)
		{
			this.webHostEnvironment = webHostEnvironment;
			this._mapper = _mapper;
			this.iEGazzetData = iEGazzetData;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult OnPostMyUploader(IFormFile FileUpload, string FileType)
		{
            DataTable table = new DataTable();
            try
            {
                if (FileUpload != null)
                {
                    //if you want to read data from a local excel file use this
                    //using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    using (var stream = FileUpload.OpenReadStream())
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        ExcelPackage package = new ExcelPackage();
                        package.Load(stream);
                        if (package.Workbook.Worksheets.Count > 0)
                        {
                            using (ExcelWorksheet workSheet = package.Workbook.Worksheets.First())
                            {
                                int noOfCol = workSheet.Dimension.End.Column;
                                int noOfRow = workSheet.Dimension.End.Row;
                                int rowIndex = 1;
                                for (int c = 1; c <= noOfCol; c++)
                                {
                                    table.Columns.Add(workSheet.Cells[rowIndex, c].Text);
                                }
                                rowIndex = 2;
                                for (int r = rowIndex; r <= noOfRow; r++)
                                {
                                    DataRow dr = table.NewRow();
                                    for (int c = 1; c <= noOfCol; c++)
                                    {
                                        dr[c - 1] = workSheet.Cells[r, c].Value;
                                    }
                                    table.Rows.Add(dr);
                                }
                                ViewBag.SuccessMessage = "Excel Successfully Converted to Data Table";
                            }
                        }
                        else
                            ViewBag.ErrorMessage = "No Work Sheet available in Excel File";
                    }
                }
                List<GazzetDataDto> gazzetList = new List<GazzetDataDto>();

                gazzetList = (from DataRow dr in table.Rows
                              select new GazzetDataDto()
                              {
                                  gazzetTypeId = 1,
                                  oraganization = dr["organization"].ToString(),
                                  department = dr["department"].ToString(),
                                  office = dr["office"].ToString(),
                                  subject = dr["subject"].ToString(),
                                  category = dr["category"].ToString(),
                                  part_section = dr["part_section"].ToString(),
                                  issue_date = dr["issue_date"].ToString(),
                                  publish_date = dr["publish_date"].ToString(),
                                  reference_no = dr["reference_no"].ToString(),
                                  file_size = dr["file_size"].ToString()
                              }).ToList();
                var mapEntity = _mapper.Map<List<GazzetDataDto>, List<EGazzetDataEntity>>(gazzetList);
                var uploadedData = iEGazzetData.BulkUpload(mapEntity);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            return null;
        }


		private byte[] PdfToExcel(IFormFile MyUploader)
		{
			byte[] excel = null;
			// Activate your license here
			// SautinSoft.PdfFocus.SetLicense("1234567890");
			SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();

			f.ExcelOptions.ConvertNonTabularDataToSpreadsheet = false;

			// 'true'  = Preserve original page layout.
			// 'false' = Place tables before text.
			f.ExcelOptions.PreservePageLayout = true;

			// The information includes the names for the culture, the writing system, 
			// the calendar used, the sort order of strings, and formatting for dates and numbers.
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
			ci.NumberFormat.NumberDecimalSeparator = ",";
			ci.NumberFormat.NumberGroupSeparator = ".";
			f.ExcelOptions.CultureInfo = ci;

			if (f.PageCount > 0)
				excel = f.ToExcel();
			return excel;

		}

		//public Task<IActionResult> ScrapDataAsync(string url)
		//{
		//	var data = GetPageDataAsync(url);
		//	return null;
		//}

		//private async Task<List<dynamic>> GetPageDataAsync(string url)
		//{
		//	// Load default configuration
		//	var config = Configuration.Default.WithDefaultLoader();
		//	// Create a new browsing context
		//	var context = BrowsingContext.New(config);
		//	// This is where the HTTP request happens, returns <IDocument> that // we can query later
		//	var document = await context.OpenAsync(url);
		//	var tableData = document.QuerySelectorAll("table").Select(s => s.InnerHtml);
		//	return (List<dynamic>)tableData;

		//}
	}
}

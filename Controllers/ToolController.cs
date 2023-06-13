using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using AngleSharp;
using AngleSharp.Html.Parser;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using System.Data;
using System.Data.OleDb;
using Advocate.Dtos;
using AutoMapper;
using Advocate.Entities;
using Advocate.Interfaces;

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
			if (FileUpload != null)
			{
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "upload", FileType);
				bool isExist = Directory.Exists(uploadsFolder);
				if (!isExist)
					Directory.CreateDirectory(uploadsFolder);

				if (FileUpload.ContentType == "application/vnd.ms-excel" || FileUpload.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
				{
					string filePath = Path.Combine(uploadsFolder, FileUpload.FileName);
					string connectionString = string.Empty;
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						FileUpload.CopyTo(fileStream);
					}
					if (FileUpload.FileName.EndsWith(".xls"))
					{
						connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", filePath);
					}
					else if (FileUpload.FileName.EndsWith(".xlsx"))
					{
						connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath);
					}
					var adapter = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", connectionString);
					var ds = new DataSet();
					adapter.Fill(ds, "GazzetExcelData");
					DataTable dtable = ds.Tables["GazzetExcelData"];

					List<GazzetDataDto> gazzetList = new List<GazzetDataDto>();

					gazzetList = (from DataRow dr in dtable.Rows
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
					var mapEntity = _mapper.Map<List<GazzetDataDto>,List<EGazzetDataEntity>>(gazzetList);
					var uploadedData = iEGazzetData.BulkUpload(mapEntity);
				}

			}
			return new ObjectResult(new { status = "success" });
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

		public Task<IActionResult> ScrapDataAsync(string url)
		{
			var data = GetPageDataAsync(url);
			return null;
		}

		private async Task<List<dynamic>> GetPageDataAsync(string url)
		{
			// Load default configuration
			var config = Configuration.Default.WithDefaultLoader();
			// Create a new browsing context
			var context = BrowsingContext.New(config);
			// This is where the HTTP request happens, returns <IDocument> that // we can query later
			var document = await context.OpenAsync(url);
			var tableData = document.QuerySelectorAll("table").Select(s => s.InnerHtml);
			return (List<dynamic>)tableData;

		}
	}
}

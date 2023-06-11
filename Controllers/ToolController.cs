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

namespace Advocate.Controllers
{
	public class ToolController : Controller
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		public ToolController(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult OnPostMyUploader(IFormFile MyUploader, string FileType)
		{
			if (MyUploader != null)
			{
				string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "upload", FileType);
				bool isExist = Directory.Exists(uploadsFolder);
				if (!isExist)
					Directory.CreateDirectory(uploadsFolder);

				string filePath = Path.Combine(uploadsFolder, MyUploader.FileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					MyUploader.CopyTo(fileStream);
				}
				return new ObjectResult(new { status = "success" });
			}
			return new ObjectResult(new { status = "fail" });

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

		public  Task<IActionResult> ScrapDataAsync(string url)
		{
			var data =GetPageDataAsync(url);
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
			var tableData = document.QuerySelectorAll("table").Select(s=>s.InnerHtml);
			return (List<dynamic>)tableData;
			
		}
	}
}

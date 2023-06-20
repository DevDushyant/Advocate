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
using System.IO;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using System.Net;

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
                ExtractEGazzetFiles(table);

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
                                  file_size = dr["file_size"].ToString(),
                                  file_name= dr["reference_no"].ToString().Split("-").Length !=1? dr["reference_no"].ToString().Split("-")[4]+".pdf": dr["reference_no"].ToString().Split("-")[0]
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

        private bool ExtractEGazzetFiles(DataTable eGazzet)
        {

			bool bFlag = false;
            if (eGazzet != null)
            {
				int eIndex = 0, iCount = 0;
				string strUGID = string.Empty, strSourcePath = string.Empty, strDestPath = string.Empty;
				foreach (DataRow drData in eGazzet.Rows)
				{
					 strUGID = drData["reference_no"].ToString();    //strUGID = drData["Gazette ID"].ToString();
					strUGID = strUGID.Replace("\n", String.Empty);

					if (strUGID.Contains("-"))
					{
						using (WebClient client = new WebClient())
						{
							//strItem = "CG-DL-E-22022021-225323";
							eIndex = strUGID.LastIndexOf("-");
							strSourcePath = "https://egazette.gov.in/WriteReadData/" + strUGID.Substring(eIndex - 4).Replace("-", "/") + ".pdf";
                            //strDestPath = appPath + @"\Files\Gazzets\" + strUGID.Substring(eIndex - 4, 4);
                            string fileName = strUGID.Substring(eIndex - 4).Replace("-", "/") + ".pdf";
							string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "upload", strUGID.Substring(eIndex - 4, 4));
							bool isExist = Directory.Exists(uploadsFolder);
							if (!isExist)
								Directory.CreateDirectory(uploadsFolder);

							try
							{
								client.DownloadFile(strSourcePath, fileName);
                                client.
								//drData["FilePath"] = strDestPath;
							}
							catch (Exception)
							{
								continue;
							}
						}

						iCount++;
					}
				}
				
			}
            return bFlag;
		}
	}
}

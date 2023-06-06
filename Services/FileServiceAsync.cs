using Advocate.Common;
using Advocate.Interfaces;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
//using Microsoft.Office.Interop.Word;
using System;
using System.IO;

namespace Advocate.Services
{
    public class FileServiceAsync : IFileServiceAsync
    {
        public readonly IWebHostEnvironment Environment;
        public FileServiceAsync(IWebHostEnvironment Environment)
        {
            this.Environment = Environment;
        }

        public string CreateAndReplaceWordTemplate(FileBasicInformation fileBasicInformation)
        {
            string actTemplatePath = this.Environment.WebRootPath + "\\templates\\act\\" + "act.html";
           
            StreamReader sr = new StreamReader(actTemplatePath);
            string template = sr.ReadToEnd();
            template.Replace("@actName", "123456")
                .Replace("@publishedInGazette", "123456")
                .Replace("@gazetteNature", "123456")
                .Replace("@gazetteDate", "123456")
                .Replace("@gazettePage", "123456");
            sr.Close();
            return template;
        }
        public int CreateFile(FileBasicInformation fileBasicInformation, string fileLocation)
        {
            return 1;
        }
        public byte[] Download(string file, string fileLocation)
        {
            string path = Path.Combine(this.Environment.WebRootPath, fileLocation + "\\") + file;
            return File.ReadAllBytes(path);
        }

        public string Upload(IFormFile file, string fileLocation)
        {
            string outputFilePath = this.Environment.WebRootPath + "\\documents\\act\\";

            if (!Directory.Exists(outputFilePath))
                Directory.CreateDirectory(outputFilePath);

            var fileName = "testAct" + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(outputFilePath, fileName);

            using (var strem = File.Create(filePath))
            {                
                file.CopyTo(strem);
            }
            return fileName;
        }
    }
}

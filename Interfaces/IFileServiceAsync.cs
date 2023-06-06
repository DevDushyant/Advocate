using Advocate.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Advocate.Interfaces
{
    public interface IFileServiceAsync
    {
        string Upload(IFormFile pdffile,string fileLocation);
        byte[] Download(string file, string fileLocation);
        string CreateAndReplaceWordTemplate(FileBasicInformation fileBasicInformation);
        int CreateFile(FileBasicInformation fileBasicInformation, string fileLocation);
    }
}

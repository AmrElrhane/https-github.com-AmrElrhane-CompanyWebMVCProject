using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Helper
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            //1- Get Folder Path
            //var folderpath = @"E:\\BackEnd\\01 DataBase\\05 MVC\\Session 03\\New folder\\Company.Web\\wwwroot\\Files\\Images\\";

            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files" , folderName);

            //2- Get File Name
                   // must be unique
            var fileName = $"{Guid.NewGuid()}-{file.FileName}";

            //3- Combine Folder Path + File Path 

            var filePath = Path.Combine(folderpath, fileName);

            //4- Save File

            using var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;


        }
    }
}

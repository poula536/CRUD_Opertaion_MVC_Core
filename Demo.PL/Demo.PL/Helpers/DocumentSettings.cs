using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile(IFormFile file , string foldreName)
        {
            // 1- Get Located Folder Path
            //string folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + foldreName;
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files", foldreName);

            // 2- Get File Name and make it Unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3- Get File Path
            string filePath = Path.Combine(folderpath,fileName);

            // 4- Save File As Streams (Stream: Data Per Time)
            using var fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fs);

            return fileName;
        }
        public static void DeleteFile(string fileName, string foldreName)
        {
            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", foldreName , fileName);
            if(File.Exists(folderpath))
                File.Delete(folderpath);
        }

    }
}

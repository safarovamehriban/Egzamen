using Business.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extentions
{
    public static class Helper
    {
        public static string SaveFile(string rootPath, string folder, IFormFile file)
        {
            if (file.ContentType.Contains("image/"))
                throw new ImageContentTypeException("Fayl formati duzgun deyil!");
            if (file.Length > 2097152)
                throw new ImageSizeException("Sheklin olcusu max 2mb ola biler");

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"portfolios{Guid.NewGuid().ToString().ToLower()}{extension}";

            string path = rootPath + @$"\{folder}\" + fileName;

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fileName;
        }

        public static void DeleteFile(string rootPath, string folder, string fileName)
        {
            string path = rootPath + @$"\{folder}\" + fileName;
            if (!File.Exists(path))
                throw new Exceptions.FileNotFoundException("fayl tapilmadi!");

            File.Delete(path);


        }

    }
}

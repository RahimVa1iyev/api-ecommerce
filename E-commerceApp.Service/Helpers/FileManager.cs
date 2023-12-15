﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerceApp.Service.Helpers
{
    public class FileManager
    {
        public static string Save(IFormFile file, string rootPath, string folder)
        {
            var newPath = Guid.NewGuid().ToString() + (file.FileName.Length >= 64 ? file.FileName.Substring(file.FileName.Length - 64) : file.FileName);
            var path = Path.Combine(rootPath,folder,newPath);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newPath;

        }

      

        public static void Delete(string rootPath, string folder, string fileName)
        {
            var path = Path.Combine(rootPath, folder, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

        }

        public static void DeleteAll(string rootPath, string folder, List<string> fileNames)
        {

            foreach (var fileName in fileNames)
            {
                var path = Path.Combine(rootPath, folder, fileName);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }


        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaAPI.infra
{
    public class SharedClass
    {
        public async Task<string> PostFile(IFormFile formFile, string directory, string fileName)
        {
            string extension = Path.GetExtension(formFile.FileName);
            var filePath = directory + fileName + extension;

            if (formFile.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return filePath;
        }
    }
}

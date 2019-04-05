using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Sale_Dance.Utility
{
    public static class ImageHelper
    {

        public const string DefaultProductImage = "default_product.jpg";
        public const string ImageFolder = @"images\ProductImage";

        public static string CopyFilesToFolder(IHostingEnvironment hostingEnvironment, IFormFileCollection files, int id)
        {
            string webRootPath = hostingEnvironment.WebRootPath;
            var upload = Path.Combine(webRootPath, ImageFolder);

            if(files.Count != 0)
            {
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, id + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                return @"\" + ImageFolder + @"\" + id + extension;

            }
            else
            {
                upload = Path.Combine(webRootPath, ImageFolder + @"\" + DefaultProductImage);
                System.IO.File.Copy(upload, webRootPath + @"\" + ImageFolder + @"\" + id + "jpg");
            }
            return @"\" + ImageFolder + @"\" + id + "jpg";

        }

       public static string ReplaceFiles(IHostingEnvironment hosting, IFormFileCollection files, string filePath, int id)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return CopyFilesToFolder(hosting, files, id);
        }

       public static byte[] FormImageToResizedPng(this IFormFile formImage, int width, int height)
       {
           using (Image<Rgba32> image = Image.Load(formImage.OpenReadStream()))
           using (MemoryStream memoryStream = new MemoryStream())
           {
               image.Mutate(x => x.Resize(width, height));
               image.SaveAsPng(memoryStream);
               return memoryStream.ToArray();
           }
       }

       public static string ImageToBase(this byte[] bytes)
       {
           string base64 = Convert.ToBase64String(bytes);
           return $"data:image/gif;base64,{base64}";
        }

    }
}

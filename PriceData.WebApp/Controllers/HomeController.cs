using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceData.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace PriceData.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index(IFormFile postedFile)
        {
            var filePath = string.Empty;
            var runningPath = AppDomain.CurrentDomain.BaseDirectory;
            var file = string.Format(@"{0}Resources\PriceData_5.csv", Path.GetFullPath(Path.Combine(runningPath, @"..\..\..\")));

            if (postedFile == null)
                filePath = file;
            else
            {
                var tempFilePath = Path.GetTempPath();
                using (var stream = System.IO.File.Create(Path.Combine(tempFilePath, postedFile.FileName)))
                {
                    await postedFile.CopyToAsync(stream);
                    filePath = Path.Combine(tempFilePath, postedFile.FileName);
                }
            }
          
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/");
                var result = await client.GetAsync($"PriceData/pricedata?path={filePath}");
                if (result.IsSuccessStatusCode)
                {
                    var readTask = await result.Content.ReadAsAsync<List<ResultModel>>();
                    if (postedFile != null) System.IO.File.Delete(filePath);
                    return View(readTask);
                }
            }
            return View();
        }
    }
}

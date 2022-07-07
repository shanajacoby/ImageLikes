using ImageShareLikesEF.Data;
using ImageShareLikesEF.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ImageShareLikesEF.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            var vm = new HomePageViewModel
            {
                Images = repo.GetAll()
            };
            return View(vm);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string title, IFormFile imageFile)
        {
            string fileName = $"{Guid.NewGuid()}-{imageFile.FileName}";
            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            imageFile.CopyTo(fs);
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            var pic = new Image()
            {
                Title = title,
                ImageName = fileName,
                DateUploaded = DateTime.Now,
                NumberOfLikes = 0

            };
            repo.Add(pic);
            return Redirect("/home/index");
        }
        public IActionResult ViewImage(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            var image = repo.GetImageForId(id);
            return View(new ImageViewModel
            {
                Image = image,
                ImagesViewed = HttpContext.Session.Get<List<int>>("Ids")
            });
        }

        public IActionResult GetLikes(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            int count = repo.GetLikes(id);
            return Json(count);
        }
        [HttpPost]
        public void LikeImage(int id)
        {
            var connectionString = _configuration.GetConnectionString("ConStr");
            var repo = new ImageRepository(connectionString);
            repo.LikeIt(id);
            List<int> imagesLiked = HttpContext.Session.Get<List<int>>("Ids");
            if (imagesLiked == null)
            {
                imagesLiked = new List<int>();
            }
            imagesLiked.Add(id);
            HttpContext.Session.Set("Ids", imagesLiked);
        }


    }
}

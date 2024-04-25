using Bloggie.Web.Models;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository blogRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> Index()
        {

            //get all the blogs.
            var blogs = await blogRepository.GetAll(); 
            return View(blogs);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

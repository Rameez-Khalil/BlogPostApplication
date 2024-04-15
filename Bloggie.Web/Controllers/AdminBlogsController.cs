using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogsController : Controller

    {
        private readonly TagRepository tagRepository;

        public AdminBlogsController(TagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Capture tags.
            var tags =await tagRepository.GetAllAsync(); 

            //assign the tags to the var.
            var model
            return View(); 
        }
    }
}

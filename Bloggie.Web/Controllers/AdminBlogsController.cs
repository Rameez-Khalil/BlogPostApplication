using Bloggie.Web.Models.VIewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogsController : Controller

    {
        private readonly ITagRepository tagRepository;

        public AdminBlogsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Capture tags.
            var tags = await tagRepository.GetAllAsync();

            //assign the tags to the var.
            var model = new AddBlogRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            }; 

            return View(model); 

        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogRequest addBlogRequest)
        {
            return RedirectToAction("Add");
        }
    }
}

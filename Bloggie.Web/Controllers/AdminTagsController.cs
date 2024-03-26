using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.VIewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller

    {
        private BloggieDbContext bloggieDbContext; 

        //CONSTRUCTOR INJECTION: 
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        [HttpGet]

        //AdminTags/Add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
       
        public IActionResult Add(AddTagRequest addTagRequest)
        {

            //Mapping the tag.
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            //Store the tag.
            bloggieDbContext.Tags.Add(tag);

            //
            bloggieDbContext.SaveChanges();
            return RedirectToAction("List"); 
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();

            //The result list will be passed to our view.
            return View(tags); 
        }
    }
}

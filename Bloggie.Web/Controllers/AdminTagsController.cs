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

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = bloggieDbContext.Tags.FirstOrDefault(x => x.Id == id);

            if (tag != null)
            {
                //We do have a tag.
                var editTagDetails = new EditTagRequest
                {
                    Id = id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };

                //Send the details back to the view.
                return View(editTagDetails);
            }
            return null;  
        }

        [HttpPost]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = bloggieDbContext.Tags.Find(tag.Id); 
            if(updatedTag != null)
            {
                updatedTag.Name = tag.Name;
                updatedTag.DisplayName = tag.DisplayName;
                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id }); 
             
        }

        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = bloggieDbContext.Tags.Find(editTagRequest.Id); 
            if(tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                bloggieDbContext.SaveChanges(); 

                //Success - Redirect to list page.
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = editTagRequest.Id});  
        }
    }
}

using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.VIewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller

    {
        
        private readonly ITagRepository tagRepository;



        //CONSTRUCTOR INJECTION: 
        public AdminTagsController(ITagRepository tagRepository)
        {
          
            this.tagRepository = tagRepository;
        }
        [HttpGet]

        //AdminTags/Add
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {

            //Mapping the tag.
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            //Store the tag.
            await tagRepository.AddAsync(tag);

            return RedirectToAction("List"); 
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();

            //The result list will be passed to our view.
            return View(tags); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id); 

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
            return View(null);  
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await tagRepository.UpdateAsync(tag); 
            if(updatedTag != null)
            {
                //show success.
                return RedirectToAction("List"); 
            }
            else
            {
                //show an error.
            }
            return RedirectToAction("Edit", new { id = editTagRequest.Id });

        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
           var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //show success
                return RedirectToAction("List"); 
            }
            return RedirectToAction("Edit", new {id=editTagRequest.Id});
        }
    }
}

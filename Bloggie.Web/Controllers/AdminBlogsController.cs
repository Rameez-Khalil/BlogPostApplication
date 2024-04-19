﻿using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.VIewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Controllers
{
    public class AdminBlogsController : Controller

    {
        private readonly ITagRepository tagRepository;
        private readonly IBlogPostRepository blogRepository;

        public AdminBlogsController(ITagRepository tagRepository, IBlogPostRepository blogRepository)
        {
            this.tagRepository = tagRepository;
            this.blogRepository = blogRepository;
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
            var blog = new BlogPost
            {
                Heading = addBlogRequest.Heading,
                PageTitle = addBlogRequest.PageTitle,
                Content = addBlogRequest.Content,
                ShortDescription = addBlogRequest.ShortDescription,
                UrlHandle = addBlogRequest.UrlHandle,
                PublishedDate = addBlogRequest.PublishedDate,
                Author = addBlogRequest.Author,
                Visible = addBlogRequest.Visible,
                FeaturedImageUrl = addBlogRequest.FeaturedImageUrl

            };

            //Map ags from slected tags.
            var selectedTags = new List<Tag>(); 
            foreach(var selectedTagId in addBlogRequest.SelectedTags)
            {
                var selectedId = Guid.Parse(selectedTagId);
                //find tag under our database.
                var existingTag = await tagRepository.GetAsync(selectedId);

                if (existingTag!=null)
                {
                    selectedTags.Add(existingTag); 
                }
                blog.Tags = selectedTags; 
                await blogRepository.AddAsync(blog);
            }
            return RedirectToAction("Add");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {

            //Call to the repo.
            var blogs = await blogRepository.GetAll();

            return View(blogs); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blog = await blogRepository.GetAsync(id);
            var tags = await tagRepository.GetAllAsync(); 

            if (blog != null)
            {
                var updatedBlog = new EditBlogRequest
                {
                    Heading = blog.Heading,
                    PageTitle = blog.PageTitle,
                    Content = blog.Content,
                    ShortDescription = blog.ShortDescription,
                    UrlHandle = blog.UrlHandle,
                    PublishedDate = blog.PublishedDate,
                    Author = blog.Author,
                    Visible = blog.Visible,
                    FeaturedImageUrl = blog.FeaturedImageUrl,
                    Tags = tags.Select(x => new SelectListItem
                    {
                        Text = x.Name, Value = x.Id.ToString()
                    }), 

                    SelectedTags = blog.Tags.Select(x=>x.Id.ToString()).ToArray()
                };
                return View(updatedBlog);
            }
            


            return View(null); 
        }
    }
}

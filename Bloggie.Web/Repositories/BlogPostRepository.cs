﻿using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.VIewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await bloggieDbContext.BlogPosts.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();
            return blogPost; 
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var blog = await bloggieDbContext.BlogPosts.FindAsync(id);
            if(blog != null) {
                bloggieDbContext.BlogPosts.Remove(blog); 
                await bloggieDbContext.SaveChangesAsync();  
                return blog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAll()
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).ToListAsync(); 
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await bloggieDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if(existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle= blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle= blogPost.UrlHandle;
                existingBlog.Visible= blogPost.Visible;
                existingBlog.PublishedDate= blogPost.PublishedDate;
                existingBlog.Tags= blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();
                return existingBlog; 
            }

            return null;
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await bloggieDbContext.BlogPosts.Include(x=>x.Tags).FirstOrDefaultAsync(x=>x.UrlHandle== urlHandle); 
        }

        
    }
}

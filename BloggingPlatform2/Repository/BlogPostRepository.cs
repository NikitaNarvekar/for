
using BloggingPlatform2.Data;
using BloggingPlatform2.Models.Domain;
using Microsoft.EntityFrameworkCore;


namespace BloggingPlatform2.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggerDbContext context;

        public BlogPostRepository(BloggerDbContext Context)
        {
            context = Context;
        }
        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await context.AddAsync(blogPost);
            await context.SaveChangesAsync();
            return blogPost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await context.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> UpadteAsync(BlogPost blogPost)
        {
            var existingBlog = await context.BlogPosts.FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Auther = blogPost.Auther;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.visible = blogPost.visible;

                await context.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await context.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                context.BlogPosts.Remove(existingBlog);
                await context.SaveChangesAsync();
                return existingBlog;
            }

            else
                return null;
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await context.BlogPosts.FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }



    }
}

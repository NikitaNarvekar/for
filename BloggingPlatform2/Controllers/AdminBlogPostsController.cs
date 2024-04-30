using BloggingPlatform2.Models.Domain;
using BloggingPlatform2.Models.ViewModels;
using BloggingPlatform2.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform2.Controllers
{

    public class AdminBlogPostsController : Controller
    {
        private readonly IBlogPostRepository repository;

        public AdminBlogPostsController(IBlogPostRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            string userRole = HttpContext.Session.GetString("URole");

            if (userRole == "Admin")
            { 
                return View();
            }
            else
            { 
                return RedirectToAction("AccessDenied", "Account");
            }

    }

        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogRequest)
        {
            var blogPost = new BlogPost
            {
                Heading = addBlogRequest.Heading,
                PageTitle = addBlogRequest.PageTitle,
                Content = addBlogRequest.Content,
                ShortDescription = addBlogRequest.ShortDescription,
                FeaturedImageUrl = addBlogRequest.FeaturedImageUrl,
                UrlHandle = addBlogRequest.UrlHandle,
                PublishedDate = addBlogRequest.PublishedDate,
                Auther = addBlogRequest.Auther,
                visible = addBlogRequest.visible

            };
            await repository.AddAsync(blogPost);
            return RedirectToAction("List");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            string userRole = HttpContext.Session.GetString("URole");
            if (userRole == "Admin")
            {
                var blogPost = await repository.GetAllAsync();
                return View(blogPost);
            }
            else
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost=await repository.GetAsync(id);
            if (blogPost != null)
            {
                var model = new EditBlogPostRequest
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    ShortDescription = blogPost.ShortDescription,
                    Auther = blogPost.Auther,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    visible = blogPost.visible,

                };

                return View(model);
            }
            else
             return View(null); 

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var model = new BlogPost
            {
                Id = editBlogPostRequest.Id,
                Heading = editBlogPostRequest.Heading,
                PageTitle = editBlogPostRequest.PageTitle,
                Content = editBlogPostRequest.Content,
                Auther = editBlogPostRequest.Auther,
                ShortDescription = editBlogPostRequest.ShortDescription,
                FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl,
                PublishedDate = editBlogPostRequest.PublishedDate,
                UrlHandle = editBlogPostRequest.UrlHandle,
                visible = editBlogPostRequest.visible,

            };
           var updatedBlog= await repository.UpadteAsync(model);

            if (updatedBlog != null)
            {
                return RedirectToAction("List");
            }

            else
            {
                return RedirectToAction("List");
            }

        }

        public async Task<IActionResult>Delete(EditBlogPostRequest editBlogPostRequest)
        {

            var deletedBlogPost = await repository.DeleteAsync(editBlogPostRequest.Id);

            if (deletedBlogPost != null)
            {
                return RedirectToAction("List");
            }

            else
            {
                return RedirectToAction("Edit",new { id= editBlogPostRequest.Id});
            }

        }

    }
}

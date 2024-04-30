using BloggingPlatform2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository repository;

        public BlogsController(IBlogPostRepository repository)
        {
            this.repository = repository;
        }
       
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            
            string userRole = HttpContext.Session.GetString("URole");

            if (userRole=="User" || userRole == "Admin")
            {
                var blogPost = await repository.GetByUrlHandleAsync(urlHandle);
                return View(blogPost);
               
            }
            else {
                TempData["AccessDenied"] = true;
                return RedirectToAction("Register", "Account");
            }
        }
    }
}


using BloggingPlatform2.Repository;
using BloggingPlatform2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BloggingPlatform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository repository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository repository)
        {
            _logger = logger;
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var blogPost = await repository.GetAllAsync();
            return View(blogPost);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

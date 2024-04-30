using BloggingPlatform2.Models.Domain;

namespace BloggingPlatform2.Repository
{
    public interface IBlogPostRepository
    {
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetAsync(Guid id);
        Task<BlogPost?> GetByUrlHandleAsync(string urlHandle);

        Task<BlogPost> AddAsync(BlogPost blogPost);
        Task<BlogPost?> UpadteAsync(BlogPost blogPost);
        Task<BlogPost?> DeleteAsync(Guid id);

    }
}

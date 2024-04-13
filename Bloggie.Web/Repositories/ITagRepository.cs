using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Repositories
{
    public interface ITagRepository
    {
        /*
         * Definitions for how we will be accessing the database.
         * Add.
         * Delete.
         * Edit.
         * Show
         * Updatae
         * */

        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task <Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag> DeleteAsync(Guid id);

    }
}

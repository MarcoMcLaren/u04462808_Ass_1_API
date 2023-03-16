using Courses.Models;

namespace Courses.Repository
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();
        Task<Module[]> GetAllModulesAsync();
        Task<Module> GetModuleAsyncById(int ModuleId);
    }
}

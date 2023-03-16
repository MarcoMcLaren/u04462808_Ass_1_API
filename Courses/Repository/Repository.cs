using Courses.Data;
using Courses.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Courses.Repository
{
    public class Repository
    {
        private readonly MyDbContext _context;

        public Repository(MyDbContext MyDbContext)
        {
            _context = MyDbContext;
        }

        public void Add<T>(T entity) where T : class //ADD
        {
            _context.Add(entity);
        }
        public void Delete<T>(T entity) where T : class //DELETE
        {
            _context.Remove(entity);
        }
        public async Task<bool> SaveChangesAsync() //SAVE CHANGES
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        public async Task<Module[]> GetAllModulesAsync() //RETRIEVE ALL MODULES
        {
            IQueryable<Module> query = _context.Modules;
            query = query.AsNoTracking().OrderBy(c => c.ModuleId);
            return await query.ToArrayAsync();
        }
        public async Task<Module> GetModuleAsyncById(int ModuleId) //RETRIEVE MODULES BY ID
        {
            IQueryable<Module> query = _context.Modules;
            query = query.AsNoTracking().OrderBy(c => c.ModuleId).Where(c => c.ModuleId == ModuleId);
            return await query.FirstOrDefaultAsync();
        }
    }
}

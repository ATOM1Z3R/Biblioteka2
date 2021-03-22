using DataAccessLayer.EFCore.Data;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLayer.EFCore.Repos
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
        }
        public virtual async Task<T> CreateAsync(T entity)
        {
            if(entity is null)
            {
                return null;
            }
            await _context.Set<T>().AddAsync(entity);
            return entity;
            
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            var list = _context.Set<T>().ToList();
            return list;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;
        }

        public virtual void Update(T entity)
        {
             _context.Update(entity);
        }
    }
}

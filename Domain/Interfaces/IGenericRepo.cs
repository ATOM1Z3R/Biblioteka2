using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<T> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

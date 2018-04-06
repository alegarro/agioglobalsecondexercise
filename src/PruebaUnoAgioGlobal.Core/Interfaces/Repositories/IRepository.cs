using PruebaUnoAgioGlobal.Core.Entities.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaUnoAgioGlobal.Core.Interfaces.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetById(int id);
        Task<List<T>> List();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
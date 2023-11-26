using Demo.BLL.Specifications;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<int> Add(T item);
        Task<int> Update(T item);
        Task<int> Delete(T item);
        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> spec);
        Task<T> GetByIdWithSpecAsync(ISpecification<T> spec);
    }
}

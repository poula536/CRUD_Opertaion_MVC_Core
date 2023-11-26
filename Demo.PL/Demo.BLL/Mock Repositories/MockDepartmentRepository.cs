using Demo.BLL.Interfaces;
using Demo.BLL.Specifications;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Mock_Repositories
{
    public class MockDepartmentRepository : IDepartmentRepository
    {
        public int Add(Department department)
        {
            throw new NotImplementedException();
        }

        public int Delete(Department department)
        {
            throw new NotImplementedException();
        }

        public Department Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Department> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Department>> GetAllWithSpecAsync(ISpecification<Department> spec)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetByIdWithSpecAsync(ISpecification<Department> spec)
        {
            throw new NotImplementedException();
        }

        public int Update(Department department)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<Department>.Add(Department item)
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<Department>.Delete(Department item)
        {
            throw new NotImplementedException();
        }

        Task<Department> IGenericRepository<Department>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Department>> IGenericRepository<Department>.GetAll()
        {
            throw new NotImplementedException();
        }

        Task<int> IGenericRepository<Department>.Update(Department item)
        {
            throw new NotImplementedException();
        }
    }
}

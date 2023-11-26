using Demo.BLL.Interfaces;
using Demo.BLL.Specifications;
using Demo.DAL.Context;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppContext _dbContext;
        public EmployeeRepository(MVCAppContext dbContext):base(dbContext) 
        { 
            _dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Employee> GetEmployeesByName(string name)
            => _dbContext.Employees.Where(E=> E.Name.Contains(name));

        public  IQueryable<Employee> GetEmployeesByNameWithSpec(ISpecification<Employee> spec)
        {
            return  ApplySpecification(spec);
        }
        private IQueryable<Employee> ApplySpecification(ISpecification<Employee> spec)
        {
            return SpecificationEvaluator<Employee>.GetQuery(_dbContext.Set<Employee>(), spec);
        }
    }
}

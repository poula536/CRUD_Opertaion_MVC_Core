using Demo.BLL.Interfaces;
using Demo.DAL.Context;
using Demo.DAL.Entities;
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
    }
}

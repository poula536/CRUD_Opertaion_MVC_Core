using Demo.BLL.Specifications;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetEmployeesByDepartmentName(string departmentName);
        IQueryable<Employee> GetEmployeesByName(string name);
        IQueryable<Employee> GetEmployeesByNameWithSpec(ISpecification<Employee> spec);

    }
}

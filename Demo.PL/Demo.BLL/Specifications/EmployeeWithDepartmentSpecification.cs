using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Specifications
{
    public class EmployeeWithDepartmentSpecification : BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentSpecification()
        {
            Includes.Add(E => E.Department);
        }
        public EmployeeWithDepartmentSpecification(int id) : base(E => E.Id == id)
        {
            Includes.Add(E => E.Department);
        }
        public EmployeeWithDepartmentSpecification(string name):base(E => E.Name == name)
        {
            Includes.Add(E => E.Department);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentReopsitory DepartmentReopsitory { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        int Complete();
    }
}

using Company.Data.Contexts;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public UnitOfWork(CompanyDbContext context)
        {
            _context = context;
            DepartmentReopsitory = new DepartmentRepository(context);
            EmployeeRepository = new EmployeeRepository(context);
        }
        public IDepartmentReopsitory DepartmentReopsitory { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }

        public int Complete()
        => _context.SaveChanges();
    }
}

using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repository
{
    public class DepartmentRepository : GenericRepository<Department> ,IDepartmentReopsitory
    { 
        private readonly CompanyDbContext _context;

        public DepartmentRepository(CompanyDbContext context) : base(context) 
        {
            _context = context;
        }

    }
}

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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext _context;

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetEmployeeByName(string name)
        => _context.Employees.Where(x=> x.Name.Trim().ToLower().Contains(name.Trim().ToLower()) ||
        x.Email.Trim().ToLower().Contains(name.Trim().ToLower()) || 
        x.PhoneNumber.Trim().ToLower().Contains(name.Trim().ToLower())

        );

        public IEnumerable<Employee> GetEmployeesByAddress(string Address)
        {
            throw new NotImplementedException();
        }
    }
}

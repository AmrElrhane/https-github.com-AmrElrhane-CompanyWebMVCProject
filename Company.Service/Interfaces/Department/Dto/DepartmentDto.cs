using Company.Data.Entities;
using Company.Service.Interfaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces.Department.Dto
{
    public class DepartmentDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Code { get; set; }

        public ICollection<EmployeeDto>? Employees { get; set; }

        public DateTime CreatAt { get; set; } = DateTime.Now;

    }
}

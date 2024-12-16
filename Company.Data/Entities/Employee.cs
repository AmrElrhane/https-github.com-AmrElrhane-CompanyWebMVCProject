using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Entities
{
    public class Employee : BaseEntity
    {
       
        public string Name { get; set; }
       
        public int Age { get; set; }
        
        public string Address { get; set; }
        
        public decimal Salary { get; set; }
        
        public string Email { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public DateTime HiringDate { get; set; }
        
        public string ImageUrl { get; set; }

        public Department Department { get; set; }

        public int? DepartmentId { get; set; }

    }
}

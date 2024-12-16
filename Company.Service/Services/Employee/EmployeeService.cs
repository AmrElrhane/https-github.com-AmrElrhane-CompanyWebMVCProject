using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Helper;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(EmployeeDto employeeDto)
        {
            //Employee employee = new Employee()
            //{
            //    Address = employeeDto.Address,
            //    Email = employeeDto.Email,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageUrl = employeeDto.ImageUrl,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary,
            //    Name = employeeDto.Name,

            //};
            employeeDto.ImageUrl = DocumentSetting.UploadFile(employeeDto.Image , "Images");



            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(EmployeeDto employeeDto)
        {


            //Employee employee = new Employee()
            //{
            //    Address = employeeDto.Address,
            //    Email = employeeDto.Email,
            //    Age = employeeDto.Age,
            //    DepartmentId = employeeDto.DepartmentId,
            //    HiringDate = employeeDto.HiringDate,
            //    ImageUrl = employeeDto.ImageUrl,
            //    PhoneNumber = employeeDto.PhoneNumber,
            //    Salary = employeeDto.Salary,
            //    Name = employeeDto.Name,

            //};


            Employee employee = _mapper.Map<Employee>(employeeDto);
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            //var mappedemployees = employees.Select(x => new EmployeeDto
            //{
            //    Address = x.Address,
            //    Email = x.Email,
            //    Age = x.Age,
            //    DepartmentId = x.DepartmentId,
            //    HiringDate = x.HiringDate,
            //    ImageUrl = x.ImageUrl,
            //    PhoneNumber = x.PhoneNumber,
            //    Salary = x.Salary,
            //    Name = x.Name,
            //    CreatAt = x.CreatAt,


            //});

            IEnumerable<EmployeeDto> mappedemployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return mappedemployees;
        }

        public EmployeeDto GetById(int? id)
        {


            if (id is null)
                return null;
            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee is null)
                return null;

            //EmployeeDto employeeDto = new EmployeeDto()
            //{
            //    Address = employee.Address,
            //    Email = employee.Email,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    HiringDate = employee.HiringDate,
            //    ImageUrl = employee.ImageUrl,
            //    PhoneNumber = employee.PhoneNumber,
            //    Salary = employee.Salary,
            //    Name = employee.Name,
            //};

            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(employee);


            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployeeByName(string name)
        {
            var employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(name);
            //var mappedemployees = employees.Select(x => new EmployeeDto
            //{
            //    Address = x.Address,
            //    Email = x.Email,
            //    Age = x.Age,
            //    DepartmentId = x.DepartmentId,
            //    HiringDate = x.HiringDate,
            //    ImageUrl = x.ImageUrl,
            //    PhoneNumber = x.PhoneNumber,
            //    Salary = x.Salary,
            //    Name = x.Name,
            //    CreatAt = x.CreatAt,


            //});

            IEnumerable<EmployeeDto> mappedemployees = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            return mappedemployees;

        }

        public void Update(EmployeeDto employee)
        {
            //_unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}

using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Repository.Repository;
using Company.Service.Interfaces;
using Company.Service.Interfaces.Department.Dto;
using Company.Service.Interfaces.Employee.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Add(DepartmentDto departmentDto)
        {
            //var mappeddepartmnent = new DepartmentDto
            //{
            //    Code = departmentDto.Code,
            //    Name = departmentDto.Name,
            //    CreatAt = DateTime.Now,
            //};

            Department department = _mapper.Map<Department>(departmentDto);

            _unitOfWork.DepartmentReopsitory.Add(department);
            _unitOfWork.Complete();
        }

        public void Delete(DepartmentDto departmentDto)
        {
            Department department = _mapper.Map<Department>(departmentDto);

            _unitOfWork.DepartmentReopsitory.Delete(department);
            _unitOfWork.Complete();

        }

        public IEnumerable<DepartmentDto> GetAll()
        {

            var departments = _unitOfWork.DepartmentReopsitory.GetAll();
            IEnumerable<DepartmentDto> mappeddepartments = 
                _mapper.Map<IEnumerable<DepartmentDto>>(departments);

            return mappeddepartments;
        }

        public DepartmentDto GetById(int? id)
        {
            if (id is null)
                return null;
            var department = _unitOfWork.DepartmentReopsitory.GetById(id.Value);
            if (department is null)
                return null;

            DepartmentDto departmentDto = _mapper.Map<DepartmentDto>(department);


            return departmentDto;
        }

        public void Update(DepartmentDto departmentDto)
        {
            //_unitOfWork.DepartmentReopsitory.Update(departmentDto);
            //_unitOfWork.Complete();
        }

    }
}

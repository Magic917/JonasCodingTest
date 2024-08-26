using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Model.Models;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Services;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System.ComponentModel.Design;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task DeleteEmployee(string ecode)
        {
            await _employeeRepository.DeleteEmployee(ecode); 
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {

            var res = await _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
        }

        public async Task<EmployeeInfo> GetEmployeeById(string id)
        {
            var result= await _employeeRepository.GetByCode(id);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task SaveEmployee(EmployeeInfo employee)
        {
            var result=_mapper.Map<Employee>(employee);
            await _employeeRepository.SaveEmployee(result);
        }

        public async Task UpdateEmployee(string ecode, EmployeeInfo employee)
        {
            var emp = await _employeeRepository.GetByCode(ecode);
            if (emp != null)
            {
                var targetEmployee=_mapper.Map<Employee>(emp);
                await _employeeRepository.SaveEmployee(targetEmployee);
            }
            else
            {
                throw new Exception($"Employee code {ecode} doesn't exist");
            }
        }
    }
}

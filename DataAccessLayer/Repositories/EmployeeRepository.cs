using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;
        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper)
        {

            _employeeDbWrapper = employeeDbWrapper;

        }
        public async Task<bool> DeleteEmployee(string employeeCode)
        {
            return await _employeeDbWrapper.DeleteAsync(c=> c.EmployeeCode == employeeCode);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCode(string EmployeeCode)
        {
            var employee= await _employeeDbWrapper.FindAsync(e=>e.EmployeeCode.Equals(EmployeeCode));
            return employee.FirstOrDefault();
            
        }

        public async Task<bool> SaveEmployee(Employee employee)
        {
            var targetEmployee=_employeeDbWrapper.Find(e=>e.EmployeeCode.Equals( employee.EmployeeCode) && e.SiteId.Equals(employee.SiteId)).FirstOrDefault();
            if (targetEmployee != null)
            {
                targetEmployee.EmployeeCode=employee.EmployeeCode;
                targetEmployee.EmployeeName=employee.EmployeeName;
                targetEmployee.Occupation=employee.Occupation;
                targetEmployee.EmployeeStatus=employee.EmployeeStatus;
                targetEmployee.EmailAddress=employee.EmailAddress;
                targetEmployee.Phone=employee.Phone;
                targetEmployee.LastModified=DateTime.Now;
                await _employeeDbWrapper.UpdateAsync(targetEmployee);
            }
            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public Task<bool> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}

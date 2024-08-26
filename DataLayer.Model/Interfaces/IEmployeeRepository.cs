using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface IEmployeeRepository
    {
        //change to async; add SaveCompany & DeleteCompany & UpdateCompany to match API
        Task<IEnumerable<Employee>> GetAll();
        Task<Employee> GetByCode(string EmployeeCode);
        Task<bool> SaveEmployee(Employee employee);

        Task<bool> UpdateEmployee(Employee employee);
        Task<bool> DeleteEmployee(string employeeCode);
    }
}

using BusinessLayer.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployees();
        Task<EmployeeInfo> GetEmployeeById(string id);
        Task SaveEmployee(EmployeeInfo employee);
        Task UpdateEmployee(string ecode, EmployeeInfo employee);
        Task DeleteEmployee(string ecode);
    }
}

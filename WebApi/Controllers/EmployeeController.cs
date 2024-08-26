using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger<CompanyController> logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
    }
        // GET api/<controller>
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            try
            {
                var items = await _employeeService.GetAllEmployees();
                return _mapper.Map<IEnumerable<EmployeeDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            
        }

        // GET api/<controller>/5
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            try
            {
                var item = await _employeeService.GetEmployeeById(employeeCode);
                return _mapper.Map<EmployeeDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;

            }
        }
            // POST api/<controller>
            [HttpPost]
            public async Task<IHttpActionResult>  Post([FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                await _employeeService.SaveEmployee(employee);
                _logger.LogInformation($"An Employee {employee.EmployeeCode} is Saved");
                return Ok();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        // PUT api/<controller>/5

        public async Task<IHttpActionResult> Put(string id, [FromBody] EmployeeDto employeeDto)
        {
            try
            {
                var employee = _mapper.Map<EmployeeInfo>(employeeDto);
                await _employeeService.UpdateEmployee(id, employee);
                _logger.LogInformation($"Employee {employee.EmployeeCode} has been updated");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                await _employeeService.DeleteEmployee(id);
                _logger.LogInformation($"Employee {id} has been deleted");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
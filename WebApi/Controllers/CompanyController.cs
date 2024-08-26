using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using WebApi.Models;
using System.Threading.Tasks;
using BusinessLayer.Model.Models;
using System.Runtime.Remoting.Messaging;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using DataAccessLayer.Model.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            try
            {
                var items = await _companyService.GetAllCompanies();
                return _mapper.Map<IEnumerable<CompanyDto>>(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
                
            };
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task< CompanyDto> Get(string companyCode)
        {
            try
            {
                var item = await _companyService.GetCompanyByCode(companyCode);
                return _mapper.Map<CompanyDto>(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.SaveCompany(company);
                _logger.LogInformation($"Company {company.CompanyName} is Saved");
                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put(string id, [FromBody]CompanyDto companyDto)
        {
            try
            {
                var company = _mapper.Map<CompanyInfo>(companyDto);
                await _companyService.UpdateCompany(id, company);
                _logger.LogInformation($"Company {company.CompanyName} has updated");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                await _companyService.DeleteCompany(id);
                _logger.LogInformation($"Company {id} has deleted");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
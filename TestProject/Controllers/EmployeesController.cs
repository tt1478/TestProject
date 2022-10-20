using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using LoggingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.ActionFilters;


namespace TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployees([FromQuery]string searchTerm = "")
        {
            var employees = await _repository.Employee.GetEmployees(false, searchTerm);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeesDto);
        }
        [HttpGet("{id}", Name = "EmployeeById")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _repository.Employee.GetEmployee(id, false);
            if (employee == null)
            {
                _logger.LogInfo($"Employee with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateEmployee([FromBody]EmployeeForCreationDto employee)
        {
            employee.JobId = employee.JobId == 0 ? null : employee.JobId;
            var employeeEntity = _mapper.Map<Employee>(employee);
            _repository.Employee.CreateEmployee(employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return CreatedAtRoute("EmployeeById", new { id = employeeToReturn.Id }, employeeToReturn);
        }
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEmployeeExistsAttribute))]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody]EmployeeForUpdateDto employee)
        {
            employee.JobId = employee.JobId == 0 ? null : employee.JobId;
            var employeeEntity = HttpContext.Items["employee"] as Employee;
            _mapper.Map(employee, employeeEntity);
            await _repository.SaveAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEmployeeExistsAttribute))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = HttpContext.Items["employee"] as Employee;
            _repository.Employee.DeleteEmployee(employee);
            await _repository.SaveAsync();
            return NoContent();
        }
    }
}

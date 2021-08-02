using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestApiCRUDDemo.EmployeeData;
using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.Controllers
{

    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ILogger<EmployeesController> _logger;
        private IEmployeeData _employeeData;
        public EmployeesController(IEmployeeData employeeData,ILogger<EmployeesController> logger)
        {
            _logger = logger;
            _employeeData = employeeData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            try
            {
                _logger.LogInformation("Fetching all the employees from the storage");
                return Ok(_employeeData.GetEmployees());
            }
            catch (Exception ex)
            {

                _logger.LogError("something went wrong:");
                return StatusCode(500, "internal server error");
            }
            
        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee != null)
            {
                return Ok(_employeeData.GetEmployee(id));
            }
            return NotFound($"Employee with Id:{id} was not found");
        }
        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult PostEmployee(Employee employee)
        {
            try
            {
                _logger.LogInformation("Post is success");
                _employeeData.AddEmployee(employee);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);

            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong");
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            try
            {
                var employee = _employeeData.GetEmployee(id);
                if (employee != null)
                {
                    _logger.LogInformation("Employee deleted.");
                    _employeeData.DeleteEmployee(employee);
                    return Ok();
                }
                return NotFound($"Employee with Id:{id} was not found");
            }
            catch (Exception)
            {
                _logger.LogError("something went wrong");
                return StatusCode(500);
            }
            
        }
        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditEmployee(Guid id, Employee employee)
        {
            try
            {
                var existingEmployee = _employeeData.GetEmployee(id);
                if (existingEmployee != null)
                {
                    _logger.LogInformation("");
                    employee.Id = existingEmployee.Id;
                    _employeeData.EditEmployee(employee);
                }
                return Ok(employee);
            }
            catch (Exception)
            {
                _logger.LogError("something went wrong");
                return StatusCode(500);
            }
            
        }
    }
}

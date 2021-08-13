using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
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
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                _logger.LogInformation("Fetching all the employees from the storage");
                return Ok(await _employeeData.GetEmployees());
            }
            catch (Exception ex)
            {

                _logger.LogError("something went wrong:"+ex);
                return StatusCode(500, "internal server error");
            }
        }
        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetEmployee(Guid id)
        {
            var employee =await _employeeData.GetEmployee(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with Id:{id} was not found");
        }
        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> PostEmployee(Employee employee)
        {
            try
            {  
                await _employeeData.AddEmployee(employee);
                _logger.LogInformation("Post is success");
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong"+ex);
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                var employee =await _employeeData.GetEmployee(id);
                if (employee != null)
                {
                    _logger.LogInformation("Employee deleted.");
                    await _employeeData.DeleteEmployee(employee);
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
    }
}

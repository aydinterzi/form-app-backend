using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.EmployeeData
{
    public interface IEmployeeData
    {
        Task<List<LoginModel>> GetLogin();
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployee(Guid id);
        Task<Employee> AddEmployee(Employee employee);
        Task DeleteEmployee(Employee employee);
        Task<Employee> EditEmployee(Employee employee);
    }
}

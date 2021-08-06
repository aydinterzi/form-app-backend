using RestApiCRUDDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace RestApiCRUDDemo.EmployeeData
{
    public class SqlEmployeeData : IEmployeeData
    {
        private EmployeeContext _employeeContext;
        public SqlEmployeeData(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }
        public async Task<Employee> AddEmployee(Employee employee)
        {
            foreach (var item in _employeeContext.Employees.ToList())
            {
                if (employee.Name == item.Name)
                {
                    item.Name = employee.Name;
                    item.Surname = employee.Surname;
                    item.No = employee.No;
                    item.GirisTarihi = employee.GirisTarihi;
                    item.TelefonNo = employee.TelefonNo;
                    _employeeContext.Employees.Update(item);
                   await _employeeContext.SaveChangesAsync();
                    return employee;
                }
            }
            employee.Id = Guid.NewGuid();
            await _employeeContext.Employees.AddAsync(employee);
            await _employeeContext.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployee(Employee employee)
        {
            _employeeContext.Employees.Remove(employee);
            await _employeeContext.SaveChangesAsync();
        }

        public async Task<Employee> EditEmployee(Employee employee)
        {
            var existingEmployee = _employeeContext.Employees.Find(employee.Id);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                _employeeContext.Employees.Update(existingEmployee);
               await _employeeContext.SaveChangesAsync();
            }
            return employee;
        }

        public async Task<Employee> GetEmployee(Guid id)
        {
            var employee =await _employeeContext.Employees.FindAsync(id);
            return employee;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            return await _employeeContext.Employees.ToListAsync();
        }
        
        public async Task<List<LoginModel>> GetLogin()
        {
            return await _employeeContext.Logins.ToListAsync();
        }
    }
}

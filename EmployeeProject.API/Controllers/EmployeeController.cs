using Microsoft.AspNetCore.Mvc;
using EmployeeProject.Buisiness.Models.Employees;
using EmployeeProject.Logic.EmployeeService;

namespace EmployeeProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        // Constructor to initialize the EmployeeController with an IEmployeeService instance.
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        // Retrieve all employees
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            // Invoke the service to get all employees
            var employees = await _employeeService.GetAllEmployees();
            return employees;
        }

        // Retrieve an employee by ID
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(Guid id)
        {
            // Invoke the service to get an employee by ID
            var employee = await _employeeService.GetEmployeeById(id);

            // Return 404 if employee is not found
            if (employee == null)
                return NotFound();

            return employee;
        }

        // Insert a new employee
        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] InsertEmployeeRequest insertEmployee)
        {
            // Return 400 if the request body is null
            if (insertEmployee == null)
                return BadRequest();

            // Invoke the service to insert a new employee
            try
            {
                await _employeeService.InsertEmployee(insertEmployee);
                return Ok();
            }
            catch (Exception ex)
            { 
                // In case something was wrong - throwing 400 respond with error message
                return BadRequest(ex.Message);
            }
        }

        // Retrieve employees by name and birthday interval
        [HttpGet("{name}/{firstDate:datetime}/{secondDate:datetime}")]
        public async Task<IEnumerable<Employee>> GetEmployeesByNameAndBirthday(string name, DateTime firstDate, DateTime secondDate)
        {
            // Invoke the service to get employees by name and birthday interval
            var employees = await _employeeService.GetEmployeesByNameAndBirthday(name, firstDate, secondDate);
            return employees;
        }

        // Retrieve the average salary for a particular role
        [HttpGet("{roleId:int}")]
        public async Task<ActionResult<double>> GetAverageSalaryByRole(int roleId)
        {
            // Invoke the service to get the average salary for a role
            return await _employeeService.GetAverageSalaryByRole(roleId);
        }

        // Update an employee
        [HttpPut]
        public async Task<ActionResult> UpdateEmployee([FromBody] UpdateEmployeeRequest updateEmployee)
        {
            // Return 400 if the request body is null
            if (updateEmployee == null)
                return BadRequest();

            // Invoke the service to update an employee
            try
            {
                await _employeeService.UpdateEmployee(updateEmployee);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Update an employee's salary by ID
        [HttpPut("{id:guid}/{salary:int}")]
        public async Task<ActionResult> UpdateEmployeeSalaryById(Guid id, int salary)
        {
            // Invoke the service to update an employee's salary by ID
            try
            {
                await _employeeService.UpdateEmployeeSalaryById(id, salary);
                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        // Delete an employee by ID
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteEmployeeById(Guid id)
        {
            // Invoke the service to delete an employee by ID
            try
            {
                await _employeeService.DeleteEmployeeById(id);
                return Ok();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
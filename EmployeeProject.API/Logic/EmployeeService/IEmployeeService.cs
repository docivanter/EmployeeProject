using EmployeeProject.Buisiness.Models.Employees;

namespace EmployeeProject.Logic.EmployeeService
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployees(); // Getting all employees from DB
        Task<Employee> GetEmployeeById(Guid id); // Get Specific employee from DB by Id
        Task InsertEmployee(InsertEmployeeRequest insertEmployee); // Inserting employee to DB
        Task<IEnumerable<Employee>> GetEmployeesByNameAndBirthday(string name, DateTime firstDate, DateTime secondDate); // Get all employees with specific name, born in specific interval
        Task<double> GetAverageSalaryByRole(int roleId); // Get average salary by some role 
        Task UpdateEmployee(UpdateEmployeeRequest updateEmployee); // Updating employee information 
        Task UpdateEmployeeSalaryById(Guid id, int newSalary); // Updating only salary by employee id
        Task DeleteEmployeeById(Guid id); // Deleting employee with specific Id
    }
}

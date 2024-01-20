using EmployeeProject.Buisiness.Models.Employees;
using EmployeeProject.Buisiness.Exceptions;
using EmployeeProject.Models.Employees;
using EmployeeProject.DataAccess.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FluentValidation;
using System.Data;

namespace EmployeeProject.Logic.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private IGenericRepository<Employee> employeeRepository;
        private readonly EmployeeValidator employeeValidator = new EmployeeValidator();
        private readonly IMapper _mapper;

        // Constructor to initialize the EmployeeService with an IGenericRepository<Employee> and IMapper.
        public EmployeeService(IGenericRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this._mapper = mapper;
        }

        // Retrieve all employees
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            // Get all employees from the repository
            var employees = await employeeRepository.Get(e => true);

            // Throw an exception if there are no employees in the database
            if (employees.Count() == 0)
                throw new EmployeeException("There are no employees in DB");

            return employees;
        }

        // Retrieve an employee by ID
        public async Task<Employee> GetEmployeeById(Guid id)
        {
            // Get an employee by ID from the repository
            var employee = await employeeRepository.GetById(e => e.Id == id);

            // Throw an exception if the employee with the specified ID is not found
            if (employee == null)
                throw new EmployeeException("There is no employee with id: " + id.ToString());

            return employee;
        }

        // Insert a new employee
        public async Task InsertEmployee(InsertEmployeeRequest insertEmployee)
        {
            // Map the InsertEmployeeRequest to the Employee model
            var employee = _mapper.Map<EmployeeProject.Buisiness.Models.Employees.Employee>(insertEmployee);

            // Check if the role is CEO and if a CEO already exists
            if (insertEmployee.RoleId == 4)
            {
                var ceo = await employeeRepository.GetById(e => e.RoleId == 4);
                if (ceo != null)
                    throw new EmployeeException("CEO role already exists");
            }

            // Validate the employee using FluentValidation
            var result = employeeValidator.Validate(employee);
            if (!result.IsValid)
                throw new EmployeeException(string.Join("Error: ", result.Errors.Select(s => s.ErrorMessage)));

            // Insert the new employee into the repository
            await employeeRepository.Insert(employee);
        }

        // Retrieve employees by name and birthday interval
        public async Task<IEnumerable<Employee>> GetEmployeesByNameAndBirthday(string name, DateTime firstDate, DateTime secondDate)
        {
            // Get employees by name and birthday interval from the repository
            var employees = await employeeRepository.GetQueryable<Employee>()
                .Where(s => s.FirstName == name && s.BirthDate >= firstDate && s.BirthDate <= secondDate)
                .ToListAsync();

            // Throw an exception if no employees with the specified parameters were found
            if (employees.Count() == 0)
                throw new EmployeeException("No employees with such parameters were found");

            return employees;
        }

        // Retrieve the average salary for a particular role
        public async Task<double> GetAverageSalaryByRole(int roleId)
        {
            // Get the average salary for a role from the repository
            return await employeeRepository.GetQueryable<Employee>()
                .Where(e => e.RoleId == roleId)
                .AverageAsync(e => e.Salary);
        }

        // Update an employee
        public async Task UpdateEmployee(UpdateEmployeeRequest updateEmployee)
        {
            // Map the UpdateEmployeeRequest to the Employee model
            var employee = _mapper.Map<EmployeeProject.Buisiness.Models.Employees.Employee>(updateEmployee);

            // Check if the role is CEO and if a CEO already exists
            if (updateEmployee.RoleId == 4)
            {
                var ceo = await employeeRepository.GetById(e => e.RoleId == 4);
                if (ceo != null)
                    throw new EmployeeException("CEO role already exists");
            }

            // Validate the employee using FluentValidation
            var result = employeeValidator.Validate(employee);
            if (!result.IsValid)
                throw new EmployeeException(string.Join("Error: ", result.Errors.Select(s => s.ErrorMessage)));

            // Update the employee in the repository
            await employeeRepository.Update(employee);
        }

        // Update an employee's salary by ID
        public async Task UpdateEmployeeSalaryById(Guid id, int newSalary)
        {
            // Get an employee by ID from the repository
            var employee = await employeeRepository.GetById(e => e.Id == id);

            // Throw an exception if the employee with the specified ID is not found
            if (employee == null)
                throw new EmployeeException("No employee with id: " + id.ToString());

            // Update the employee's salary
            employee.Salary = newSalary;

            // Update the employee in the repository
            await employeeRepository.Update(employee);
        }

        // Delete an employee by ID
        public async Task DeleteEmployeeById(Guid id)
        {
            // Get an employee by ID from the repository
            var employee = await employeeRepository.GetById(e => e.Id == id);

            // Throw an exception if the employee with the specified ID is not found
            if (employee == null)
                throw new EmployeeException("Employee with id " + id.ToString() + " does not exist");

            // Delete the employee from the repository
            await employeeRepository.Delete(employee);
        }
    }
}
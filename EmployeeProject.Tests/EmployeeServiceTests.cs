using Xunit;
using FakeItEasy;
using FluentAssertions;
using EmployeeProject.Logic.EmployeeService;
using EmployeeProject.DataAccess.Repositories.GenericRepository;
using EmployeeProject.Buisiness.Models.Employees;
using EmployeeProject.Models.Employees;
using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Employee testEmployee = new Employee
        {
            Id = new Guid("99441498-9031-4149-987a-da0c5afa6888"),
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(1990, 5, 15),
            EmploymentDate = new DateTime(2020, 1, 1),
            BossId = null,
            Boss = null,
            Adress = "123 Main St, City",
            Salary = 60000,
            RoleId = 2,
            Role = null
        };
        [Fact]
        public async Task GetAllEmployees_ReturnsEmployees()
        {
            // Arrange
            var mockRepository = A.Fake<IGenericRepository<Employee>>();
            var mockMapper = A.Fake<IMapper>();

            var employeeService = new EmployeeService(mockRepository, mockMapper);

            var employeesList = new List<Employee>
                                {
                                    testEmployee,
                                    new Employee
                                    {
                                        Id = Guid.NewGuid(),
                                        FirstName = "Jane",
                                        LastName = "Smith",
                                        BirthDate = new DateTime(1985, 8, 20),
                                        EmploymentDate = new DateTime(2018, 3, 15),
                                        BossId = null,
                                        Boss = null,
                                        Adress = "456 Oak St, Town",
                                        Salary = 75000,
                                        RoleId = 3,
                                        Role = null
                                    }
                                };

            A.CallTo(() => mockRepository.Get(A<Expression<Func<Employee, bool>>>.Ignored)).Returns(employeesList);

            // Act
            var result = await employeeService.GetAllEmployees();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(employeesList);
        }

        [Fact]
        public async Task InsertEmployee_InsertsEmployee()
        {
            // Arrange
            var mockRepository = A.Fake<IGenericRepository<Employee>>();
            var mockMapper = A.Fake<IMapper>();

            var employeeService = new EmployeeService(mockRepository, mockMapper);

            var insertEmployeeRequest = new InsertEmployeeRequest
            {
                FirstName = "John",
                LastName = "Doe",
                BirthDate = new DateTime(1990, 5, 15),
                EmploymentDate = new DateTime(2020, 1, 1),
                BossId = null,
                Adress = "123 Main St, City",
                Salary = 60000,
                RoleId = 2
            };

            var employee = testEmployee;

            A.CallTo(() => mockMapper.Map<Employee>(insertEmployeeRequest))
                .Returns(employee);

            A.CallTo(() => mockRepository.GetById(e=>e.RoleId == 4))
                .Returns((Employee)null); // Assume no CEO with RoleId 4 exists

            A.CallTo(() => mockRepository.Insert(employee))
                .DoesNothing(); 

            // Act
            await employeeService.InsertEmployee(insertEmployeeRequest);

            // Assert
            A.CallTo(() => mockRepository.Insert(A<Employee>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetEmployeeById_ReturnsEmployee()
        {
            // Arrange
            var mockRepository = A.Fake<IGenericRepository<Employee>>();
            var mockMapper = A.Fake<IMapper>();

            var employeeService = new EmployeeService(mockRepository, mockMapper);

            var employee = testEmployee;

            A.CallTo(() => mockRepository.GetById(A<Expression<Func<Employee, bool>>>.Ignored))
                .Returns(employee);

            // Act
            var result = await employeeService.GetEmployeeById(employee.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(employee.Id);
        }

        [Fact]
        public async Task UpdateEmployee_UpdatesEmployee()
        {
            // Arrange
            var validationResult = new FluentValidation.Results.ValidationResult();
            var mockRepository = A.Fake<IGenericRepository<Employee>>();
            var mockMapper = A.Fake<IMapper>();

            var employeeService = new EmployeeService(mockRepository, mockMapper);

            var updateEmployeeRequest = new UpdateEmployeeRequest
            {
                Id = new Guid("99441498-9031-4149-987a-da0c5afa6888"),
                FirstName = "John",
                LastName = "Doe",
                Birthdate = new DateTime(1990, 5, 15),
                EmploymentDate = new DateTime(2020, 1, 1),
                BossId = null,
                Adress = "123 Main St, City",
                Salary = 60000,
                RoleId = 2
            };

            var mappedEmployee = testEmployee;

            A.CallTo(() => mockMapper.Map<Employee>(A<UpdateEmployeeRequest>.Ignored))
                .Returns(mappedEmployee);

            A.CallTo(() => mockRepository.GetById(A<Expression<Func<Employee, bool>>>.Ignored))
                .Returns((Employee)null);

            // Act
            await employeeService.UpdateEmployee(updateEmployeeRequest);

            // Assert
            A.CallTo(() => mockRepository.Update(A<Employee>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateEmployeeSalaryById_WithValidEmployee_UpdatesSalary()
        {
            // Arrange
            var mockRepository = A.Fake<IGenericRepository<Employee>>();
            var employeeService = new EmployeeService(mockRepository, A.Dummy<IMapper>());

            var existingEmployee = testEmployee;
            var newSalary = 50000;

            A.CallTo(() => mockRepository.GetById(A<Expression<Func<Employee, bool>>>.Ignored))
                .Returns(Task.FromResult(existingEmployee));

            // Act
            await employeeService.UpdateEmployeeSalaryById(testEmployee.Id, newSalary);

            // Assert
            existingEmployee.Salary.Should().Be(newSalary);
            A.CallTo(() => mockRepository.Update(existingEmployee)).MustHaveHappenedOnceExactly();
        }

    }
}
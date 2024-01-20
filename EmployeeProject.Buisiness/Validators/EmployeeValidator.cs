using FluentValidation;
using System.Data;
using EmployeeProject.Buisiness.Models.Employees;

namespace EmployeeProject.Models.Employees
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last name is required.");

            RuleFor(e => e.FirstName).NotEqual(e => e.LastName)
                .WithMessage("First name and last name cannot be the same.");

            RuleFor(e => e.EmploymentDate).GreaterThanOrEqualTo(new DateTime(2000, 1, 1))
                .WithMessage("Employment date cannot be earlier than 2000-01-01.");

            RuleFor(e => e.EmploymentDate).LessThanOrEqualTo(DateTime.Today)
                .WithMessage("Employment date cannot be a future date.");

            RuleFor(e => e.Salary).GreaterThanOrEqualTo(0)
                .WithMessage("Current salary must be non-negative.");

            RuleFor(e => e).Custom((e, context) =>
            {
                if (e.RoleId == 4 && e.BossId.HasValue)
                {
                    context.AddFailure("The CEO cannot have a boss.");
                }
                if (e.RoleId != 4 && !e.BossId.HasValue)
                {
                    context.AddFailure("BossId is empty. Only CEO has no boss.");
                }
                int age = DateTime.Today.Year - e.BirthDate.Year;
                if (age < 18 || age > 70)
                {
                    context.AddFailure("Employee must be at least 18 years old and not older than 70 years.");
                }
            });
        }
    }
}

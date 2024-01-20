using EmployeeProject.Buisiness.Models.Roles;

namespace EmployeeProject.Buisiness.Models.Employees
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public Guid? BossId { get; set; }
        public Employee? Boss { get; set; }
        public string Adress { get; set; }
        public int Salary { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}

using EmployeeProject.Buisiness.Models.Employees;

namespace EmployeeProject.Buisiness.Models.Roles
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<Employee> Employees { get; set; }
    }
}

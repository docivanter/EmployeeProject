using EmployeeProject.Buisiness.Models.Roles;
using EmployeeProject.Buisiness.Models.Employees;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProject.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>(r => r.HasData(
                new Role[]
                {
                    new Role { Id = 1, RoleName = "Worker" },
                    new Role { Id = 2, RoleName = "Regular Employee" },
                    new Role { Id = 3, RoleName = "Boss" },
                    new Role { Id = 4, RoleName = "CEO" }
                }));

            modelBuilder.Entity<Employee>(e => e.HasData(
                new Employee[]
                {
                    new Employee 
                    { 
                        Id = new Guid("18fcf979-4804-4ca1-aeb2-3dbd355242d7"),
                        FirstName = "Pilypas",
                        LastName = "Petrokas",
                        BirthDate = new DateTime(1999, 6, 5),
                        EmploymentDate = new DateTime(2019, 8, 6),
                        Adress = "Vilnius Mira st. 15",
                        Salary = 15000,
                        RoleId = 4
                    },
                    new Employee
                    {
                        Id = new Guid("26f8ebe2-0e5c-4aa3-8cde-bf58cf2afc68"),
                        FirstName = "Vytis",
                        LastName = "Kreivaitis",
                        BirthDate = new DateTime(1986,2,1),
                        EmploymentDate = new DateTime(2012,2,3),
                        Adress = "Vilnius Skila st. 25",
                        Salary = 5000,
                        RoleId = 3
                    },
                    new Employee
                    {
                        Id = new Guid("d47cf8b6-1b6b-4e46-bbd7-59ca7fdaba14"),
                        FirstName = "Salemonas",
                        LastName = "Bauzys",
                        BirthDate = new DateTime(1989, 6, 6),
                        EmploymentDate = new DateTime(2013,1,7),
                        Adress = "Vilnius Vernik st. 51",
                        Salary = 5500,
                        RoleId = 3
                    }
                }));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeDbContext).Assembly);
        }
    }
}

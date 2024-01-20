using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeProject.Buisiness.Models.Employees;

namespace EmployeeProject.DataAccess.Entities.Employees
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.Adress).HasMaxLength(256);

            /*builder.HasOne(e => e.Boss)
                .WithMany()
                .HasForeignKey(e => e.BossId);*/
        }
    }
}

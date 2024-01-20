using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmployeeProject.Buisiness.Models.Roles;

namespace EmployeeProject.DataAccess.Entities.Roles
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.RoleName).HasMaxLength(32);

            /*builder.HasMany(r => r.Employees)
                .WithOne(r => r.Role)
                .HasForeignKey(r => r.RoleId);*/
        }
    }
}

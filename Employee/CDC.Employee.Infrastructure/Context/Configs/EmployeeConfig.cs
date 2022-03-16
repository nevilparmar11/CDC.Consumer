using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CDC.Employee.Infrastructure.Context.Configs
{
    public class EmployeeConfig : IEntityTypeConfiguration<Model.Employee>
    {
        public void Configure(EntityTypeBuilder<Model.Employee> builder)
        {
            builder.ToTable("Employees", "dbo");
            builder.HasKey(x => x.EmployeeId);
            builder.Property(x => x.HiredDate).HasColumnName("Date");
        }
    }
}

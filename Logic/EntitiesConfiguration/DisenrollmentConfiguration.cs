using Logic.Students;
using Logic.Students.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntitiesConfiguration
{
    public class DisenrollmentConfiguration : IEntityTypeConfiguration<Disenrollment>
    {
        public void Configure(EntityTypeBuilder<Disenrollment> builder)
        {
            builder.HasKey(b => b.Id);


            builder.HasOne(b => b.Course);
        }
    }
}

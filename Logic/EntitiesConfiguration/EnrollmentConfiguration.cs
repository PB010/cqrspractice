using Logic.Students;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntitiesConfiguration
{
    class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(b => b.Course);

            builder.HasData(
                new Enrollment(1, 2, 2, Grade.A),
                new Enrollment(2, 2, 3, Grade.C),
                new Enrollment(3, 1, 1, Grade.A),
                new Enrollment(4, 1, 1, Grade.A)
            );
        }
    }
}

using Logic.Students;
using Logic.Students.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntitiesConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasMany(b => b.Disenrollments)
                .WithOne(b => b.Student)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.Enrollments)
                .WithOne(b => b.Student)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Student(1, "Alice", "alice@gmail.com"),
                new Student(2, "Bob", "bob@outlook.com")
                );
        }
    }
}

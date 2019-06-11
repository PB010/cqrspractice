using Logic.Students;
using Logic.Students.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Logic.EntitiesConfiguration
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.HasData(
                new Course(1, "Calculus", 3),
                new Course(2, "Chemistry", 3),
                new Course(3, "Composition", 3),
                new Course(4, "Literature", 4),
                new Course(5, "Trigonometry", 4),
                new Course(6, "Microeconomics", 3),
                new Course(7, "Macroeconomics", 3)
            );
        }
    }
}

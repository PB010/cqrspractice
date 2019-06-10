using Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace API.Utils
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<StudentsDbContext>
    {
        public StudentsDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<StudentsDbContext>();

            var connectionString = configuration["ConnectionString"];

            builder.UseSqlServer(connectionString); 

            return new StudentsDbContext(builder.Options);
        }
    }
}
